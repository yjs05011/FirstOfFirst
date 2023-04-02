using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DungeonGenerator : MonoBehaviour
{
    public static DungeonGenerator Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // �������� ������
    public GameObject mStagePrefab = null;
    public GameObject mHiddenStagePrefab = null;
    public GameObject mBossStagePrefab = null;

    // ���� ��(���������� �׷��� ��ü board)�� ���� , ����
    public const int WIDTH = 10;
    public const int HEIGHT = 10;

    // ���� const (��Ʈ ������ ���� ��)
    public const int DIRECTION_NONE = 0;
    public const int DIRECTION_TOP = 1;
    public const int DIRECTION_BOTTOM = 2;
    public const int DIRECTION_LEFT = 4;
    public const int DIRECTION_RIGHT = 8;

    // ���� Ǯ max ���� const
    public const int POOL_MAX_HEAL_POINT = 100;

    // ������ �������� �ִ� ���� (���� �������� ������ ����)
    public static int TOTAL_COUNT = 10;
    // ������ �������� ī��Ʈ�� ����
    public static int CREATE_COUNT = 0;

    // ���� �� (���������� �׷��� ��ü ����) 
    public static int[] mDungeonBoard = new int[WIDTH * HEIGHT * 3];

    // ������ ������������ ����Ʈ 
    public List<DungeonStage> mStages = new List<DungeonStage>();

    // ķ�� Ÿ�� �������� �������� üũ�� bool type ����
    public bool IsCreatedCampRoom = false;


    //public DungeonBoard.BoardType mBoardType = DungeonBoard.BoardType.Start;

    public int mDepth = 0;
    public DungeonStage mLastRoom = null;

    public int mStartX = 0;
    public int mStartY = 0;
    public int mStartFloor = 1;


    public void DungeonGenerate()
    {
        mStages.Clear();
        InitDungeonBorad(mStartX, mStartY, mStartFloor, DIRECTION_NONE);
    }

    public DungeonStage InitDungeonBorad(int startX, int startY, int floor, int backwardDirection)
    {
        // �� ���� �� ���� ������
        int safetyTotalCount = (Mathf.Max(WIDTH, HEIGHT) / 2) * Mathf.Min(WIDTH, HEIGHT);
        if (TOTAL_COUNT > safetyTotalCount)
        {
            TOTAL_COUNT = safetyTotalCount;
        }

        // ���� ��ü �� ������ ����.
        for (int idx = 0; idx < mDungeonBoard.Length; ++idx)
        {
            mDungeonBoard[idx] = 0;
        }
        IsCreatedCampRoom = false;

        // �������� ���� ���� �ʱ�ȭ
        CREATE_COUNT = 0;

        // ������ �����ߴ� ���������� ��� ����(���ӿ�����Ʈ)
        StagesDelete();

        //Debug.LogFormat("w: {0}, h : {1}", WIDTH, HEIGHT);

        GameObject roomObject = Instantiate(mStagePrefab);
        roomObject.name = string.Format("Stage {0},{1}", startX, startY);
        roomObject.transform.position = new Vector3(startX * 26.0f, startY * 15.0f, 0);

        DungeonStage startStage = roomObject.GetComponent<DungeonStage>();
        startStage.SetFloor(floor);
        startStage.SetBoardXY(startX, startY);
        startStage.mBackwardDirection = backwardDirection;

        mStages.Add(startStage);

        // prev stage ������ �������� ���� �������ϱ� ������ 1������ �����ϱ� ���� �ڵ�
        if (floor == 1)
        {
            GenerateStage(startStage, startStage, GenerateDirections(backwardDirection, startX, startY), floor);
            CheckLastRoom(startStage, startStage, mDepth);
            // �÷��̾ ��ġ�� �������� ���� ����
            DungeonManager.Instance.SetPlayerCurrStage(startStage);
            // ���������� �÷��̾� ���� ���� ����
            startStage.SetIsEnterd(true);
        }
        else
        {
            GenerateStage(startStage, mLastRoom, GenerateDirections(backwardDirection, startX, startY), floor);
            CheckLastRoom(startStage, mLastRoom, mDepth);

            // 3���� ���, ��Ʈ�� ���� ���� �� �߰� ����
            if (floor == 3)
            {
                int bossRoomX = mLastRoom.GetBoardX();
                int bossRoomY = mLastRoom.GetBoardY() + 1;
                GameObject bossRoomObject = Instantiate(mBossStagePrefab);

                bossRoomObject.name = string.Format("Stage {0},{1}", bossRoomX, bossRoomY);
                bossRoomObject.transform.position = new Vector3(mLastRoom.transform.position.x, mLastRoom.transform.position.y + 23.8f, 0);

                DungeonStage BossStage = bossRoomObject.GetComponent<DungeonStage>();
                BossStage.SetFloor(floor);
                BossStage.SetBoardXY(bossRoomX, bossRoomY);
                BossStage.mBackwardDirection = DIRECTION_TOP;

                mLastRoom.SetConnectedStage(DIRECTION_TOP, BossStage);
                BossStage.SetConnectedStage(DIRECTION_BOTTOM, mLastRoom);

                mStages.Add(BossStage);
                DungeonManager.Instance.SetDungeonBossRoom(BossStage);
            }
        }
        SetRoomStyle(startX, startY);
        SetDoorOpen();

        //Debug.LogFormat("TotalCount:{0} CreateCount:{1}", TOTAL_COUNT, CREATE_COUNT);

        // [Notify] Enter Stage (First)
        startStage.OnStageEnter(DungeonDoor.TansferInfo.FirstRoom);

        return startStage;
    }



    public void StagesDelete()
    {
        GameObject deletStage = null;
        for (int i = 0; i < mStages.Count; ++i)
        {
            string stageName = mStages[i].name;
            deletStage = GameObject.Find(stageName);
            GameObject.Destroy(deletStage);
            mStages.RemoveAt(i);
            --i;
        }
    }


    // ��ǥ�� ���� ������� üũ
    public bool IsEmpty(int x, int y)
    {
        if (mDungeonBoard[y * WIDTH + x] != 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetDoorOpen()
    {
        for (int i = 0; i < mStages.Count; ++i)
        {
            mStages[i].SetDoorsOpen();
        }
    }

    public void SetRoom(int x, int y)
    {
        mDungeonBoard[y * WIDTH + x] = 1;
    }


    public void GenerateStage(DungeonStage stage, DungeonStage prevStage, int directions, int floor)
    {
        int linkDoors = DIRECTION_NONE;
        int x = stage.GetBoardX();
        int y = stage.GetBoardY();

        if ((directions & DIRECTION_TOP) == DIRECTION_TOP)
        {
            int nextX = x;
            int nextY = y + 1;
            if (nextX != prevStage.GetBoardX() || nextY != prevStage.GetBoardY())
            {
                if (stage.mBackwardDirection != DIRECTION_TOP)
                {
                    GameObject roomObject = Instantiate(mStagePrefab);
                    roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                    roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                    DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                    nextStage.SetBoardXY(nextX, nextY);
                    stage.SetConnectedStage(DIRECTION_TOP, nextStage);
                    nextStage.SetConnectedStage(DIRECTION_BOTTOM, stage);
                    nextStage.SetFloor(floor);
                    mStages.Add(nextStage);
                    GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_BOTTOM, nextX, nextY), floor);
                    linkDoors |= DIRECTION_TOP;
                }
            }
            else
            {
                stage.SetConnectedStage(DIRECTION_TOP, prevStage);
                linkDoors |= DIRECTION_TOP;
            }
        }

        if ((directions & DIRECTION_BOTTOM) == DIRECTION_BOTTOM)
        {
            int nextX = x;
            int nextY = y - 1;
            if (nextX != prevStage.GetBoardX() || nextY != prevStage.GetBoardY())
            {
                if (stage.mBackwardDirection != DIRECTION_BOTTOM)
                {
                    GameObject roomObject = Instantiate(mStagePrefab);
                    roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                    roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                    DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                    nextStage.SetBoardXY(nextX, nextY);
                    stage.SetConnectedStage(DIRECTION_BOTTOM, nextStage);
                    nextStage.SetConnectedStage(DIRECTION_TOP, stage);
                    nextStage.SetFloor(floor);
                    mStages.Add(nextStage);
                    GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_TOP, nextX, nextY), floor);
                    linkDoors |= DIRECTION_BOTTOM;
                }
            }
            else
            {
                stage.SetConnectedStage(DIRECTION_BOTTOM, prevStage);
                linkDoors |= DIRECTION_BOTTOM;
            }
        }

        if ((directions & DIRECTION_LEFT) == DIRECTION_LEFT)
        {
            int nextX = x - 1;
            int nextY = y;
            if (nextX != prevStage.GetBoardX() || nextY != prevStage.GetBoardY())
            {
                if (stage.mBackwardDirection != DIRECTION_LEFT)
                {
                    GameObject roomObject = Instantiate(mStagePrefab);
                    roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                    roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                    DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                    nextStage.SetBoardXY(nextX, nextY);
                    stage.SetConnectedStage(DIRECTION_LEFT, nextStage);
                    nextStage.SetConnectedStage(DIRECTION_RIGHT, stage);
                    nextStage.SetFloor(floor);
                    mStages.Add(nextStage);
                    GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_RIGHT, nextX, nextY), floor);
                    linkDoors |= DIRECTION_LEFT;
                }
            }
            else
            {
                stage.SetConnectedStage(DIRECTION_LEFT, prevStage);
                linkDoors |= DIRECTION_LEFT;
            }
        }

        if ((directions & DIRECTION_RIGHT) == DIRECTION_RIGHT)
        {
            int nextX = x + 1;
            int nextY = y;
            if (nextX != prevStage.GetBoardX() || nextY != prevStage.GetBoardY())
            {
                if (stage.mBackwardDirection != DIRECTION_RIGHT)
                {
                    GameObject roomObject = Instantiate(mStagePrefab);
                    roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                    roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                    DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                    nextStage.SetBoardXY(nextX, nextY);
                    stage.SetConnectedStage(DIRECTION_RIGHT, nextStage);
                    nextStage.SetConnectedStage(DIRECTION_LEFT, stage);
                    nextStage.SetFloor(floor);
                    mStages.Add(nextStage);

                    GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_LEFT, nextX, nextY), floor);
                    linkDoors |= DIRECTION_RIGHT;
                }
            }
            else
            {

                stage.SetConnectedStage(DIRECTION_RIGHT, prevStage);
                linkDoors |= DIRECTION_RIGHT;
            }
        }


        stage.SetDoors(linkDoors);
    }

    public void SetRoomStyle(int startX, int startY)
    {

        for (int i = 0; i < mStages.Count; ++i)
        {

            if (mStages[i].GetBoardX() == startX && mStages[i].GetBoardY() == startY)
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.Start);
            }
            else if (mStages[i] == mLastRoom)
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.Boss);
            }
            else if (mStages[i] == mLastRoom.GetPrevStage())
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.Pool);
            }
            else if (mStages[i] == DungeonManager.Instance.GetDungeonBossRoom())
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.DungeonBoss);
            }
            else if (i >= TOTAL_COUNT / 2)
            {
                if (IsCreatedCampRoom == false)
                {
                    if (mStages[i].GetBoardType() != DungeonBoard.BoardType.Boss && mStages[i].GetBoardType() != DungeonBoard.BoardType.Pool && mStages[i].GetBoardType() != DungeonBoard.BoardType.DungeonBoss)
                    {
                        IsCreatedCampRoom = true;
                        mStages[i].SetBoadStyle(DungeonBoard.BoardType.Camp);
                    }

                }
                else
                {
                    mStages[i].SetBoadStyle(DungeonBoard.BoardType.Random);
                }
            }
            else
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.Random);
            }

        }

    }

    public void CheckLastRoom(DungeonStage stage, DungeonStage prevStage, int depth)
    {

        if (stage.GetConnectedStage(DIRECTION_TOP) != null)
        {
            if (prevStage != stage.GetConnectedStage(DIRECTION_TOP))
            {
                CheckLastRoom(GetStageByXY(stage.GetBoardX(), stage.GetBoardY() + 1), stage, depth + 1);
            }
        }
        if (stage.GetConnectedStage(DIRECTION_BOTTOM) != null)
        {
            if (prevStage != stage.GetConnectedStage(DIRECTION_BOTTOM))
            {
                CheckLastRoom(GetStageByXY(stage.GetBoardX(), stage.GetBoardY() - 1), stage, depth + 1);
            }
        }
        if (stage.GetConnectedStage(DIRECTION_RIGHT) != null)
        {
            if (prevStage != stage.GetConnectedStage(DIRECTION_RIGHT))
            {
                CheckLastRoom(GetStageByXY(stage.GetBoardX() + 1, stage.GetBoardY()), stage, depth + 1);
            }
        }
        if (stage.GetConnectedStage(DIRECTION_LEFT) != null)
        {
            if (prevStage != stage.GetConnectedStage(DIRECTION_LEFT))
            {
                CheckLastRoom(GetStageByXY(stage.GetBoardX() - 1, stage.GetBoardY()), stage, depth + 1);
            }

        }

        if (mDepth < depth)
        {
            mDepth = depth;
            mLastRoom = stage;
        }

    }
    public DungeonStage GetLastRoom()
    {
        return mLastRoom;
    }

    public DungeonStage GetStageByXY(int x, int y)
    {
        for (int i = 0; i < mStages.Count; ++i)
        {
            if (mStages[i].GetBoardX() == x && mStages[i].GetBoardY() == y)
            {
                return mStages[i];
            }
        }
        return null;
    }

    // �� ���� ���� ����
    public int GenerateDirections(int backward, int x, int y)
    {
        int output = DIRECTION_NONE;
        int randomPercent = 90;
        // ������ ���� ����� ���� �ֱ�.(���� �濡�� ���� �������� �°��, �Ʒ� ���� ���� �����־�� �ϴϱ�, backward�� �־��ش�)
        output |= backward;

        // �߰� ������ �� ������ 0�ΰ��, ����� �� ���⸸ �ְ� ����
        if (TOTAL_COUNT - CREATE_COUNT == 0)
        {
            return output;
        }
        else
        {
            int prevCreateCount = CREATE_COUNT;

            while (prevCreateCount == CREATE_COUNT)
            {
                if (TOTAL_COUNT - CREATE_COUNT >= 1 && backward != DIRECTION_TOP)
                {
                    int random = UnityEngine.Random.Range(0, 100 + 1);
                    if (random < randomPercent)
                    {
                        int nextValue = y + 1;
                        if (nextValue < (HEIGHT) && IsEmpty(x, nextValue))
                        {
                            SetRoom(x, nextValue);

                            output |= DIRECTION_TOP;
                            ++CREATE_COUNT;
                        }
                    }
                }

                if (TOTAL_COUNT - CREATE_COUNT >= 1 && backward != DIRECTION_LEFT)
                {
                    int random = UnityEngine.Random.Range(0, 100 + 1);

                    if (random < randomPercent)
                    {
                        int nextValue = x - 1;
                        if (nextValue >= 0 && IsEmpty(nextValue, y))
                        {
                            SetRoom(nextValue, y);

                            output |= DIRECTION_LEFT;
                            ++CREATE_COUNT;
                        }
                    }
                }

                if (TOTAL_COUNT - CREATE_COUNT >= 1 && backward != DIRECTION_BOTTOM)
                {
                    // 1�� ��ŸƮ���� �Ʒ� ���� �� �������ؼ� ���� ���� �����ϱ����� ����ó��
                    if (backward == DIRECTION_NONE)
                    {
                        int random = UnityEngine.Random.Range(0, 100 + 1);

                        if (random < randomPercent)
                        {
                            int nextValue = y - 1;
                            if (nextValue >= 0 && IsEmpty(x, nextValue))
                            {
                                SetRoom(x, nextValue);

                                output |= DIRECTION_BOTTOM;
                                ++CREATE_COUNT;
                            }
                        }
                    }
                }

                if (TOTAL_COUNT - CREATE_COUNT >= 1 && backward != DIRECTION_RIGHT)
                {
                    int random = UnityEngine.Random.Range(0, 100 + 1);

                    if (random < randomPercent)
                    {
                        int nextValue = x + 1;
                        if (nextValue < (WIDTH) && IsEmpty(nextValue, y))
                        {
                            SetRoom(nextValue, y);

                            output |= DIRECTION_RIGHT;
                            ++CREATE_COUNT;
                        }
                    }
                }
            }
        }
        return output;
    }


}
