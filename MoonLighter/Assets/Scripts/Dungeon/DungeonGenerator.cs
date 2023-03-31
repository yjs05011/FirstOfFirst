using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static DungeonDoor;
using static UnityEditor.VersionControl.Asset;


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

    // 스테이지 프리팹
    public GameObject mStagePrefab = null;
    public GameObject mHiddenStagePrefab = null;
    public GameObject mBossStagePrefab = null;

    // 던전 맵(스테이지가 그려진 전체 board)의 가로 , 세로
    public const int WIDTH = 10;
    public const int HEIGHT = 10;

    // 방향 const (비트 연산을 위한 값)
    public const int DIRECTION_NONE = 0;
    public const int DIRECTION_TOP = 1;
    public const int DIRECTION_BOTTOM = 2;
    public const int DIRECTION_LEFT = 4;
    public const int DIRECTION_RIGHT = 8;

    // 힐링 풀 max 힐량 const
    public const int POOL_MAX_HEAL_POINT = 100;

    // 생설될 스테이지 최대 개수 (시작 스테이지 제외한 개수)
    public static int TOTAL_COUNT = 10;
    // 생성된 스테이지 카운트용 변수
    public static int CREATE_COUNT = 0;

    // 던전 맵 (스테이지가 그려질 전체 보드) 
    public static int[] mDungeonBoard = new int[WIDTH * HEIGHT*3];

    // 생성된 스테이지들의 리스트 
    public List<DungeonStage> mStages = new List<DungeonStage>();

    // 캠프 타입 스테이지 생성여부 체크용 bool type 변수
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
        // 방 생성 종 개수 방어로직
        int safetyTotalCount = (Mathf.Max(WIDTH, HEIGHT) / 2) * Mathf.Min(WIDTH, HEIGHT);
        if (TOTAL_COUNT > safetyTotalCount)
        {
            TOTAL_COUNT = safetyTotalCount;
        }

        // 보드 전체 빈 방으로 설정.
        for (int idx = 0; idx < mDungeonBoard.Length; ++idx)
        {
            mDungeonBoard[idx] = 0;
        }
        IsCreatedCampRoom = false;
        
        // 스테이지 생성 개수 초기화
        CREATE_COUNT = 0;
        
        // 기존에 생성했던 스테이지를 모두 삭제(게임오브젝트)
        StagesDelete();


        Debug.LogFormat("w: {0}, h : {1}", WIDTH, HEIGHT);

        GameObject roomObject = Instantiate(mStagePrefab);
        roomObject.name = string.Format("Stage {0},{1}", startX, startY);
        roomObject.transform.position = new Vector3(startX * 26.0f, startY * 15.0f, 0);

        DungeonStage startStage = roomObject.GetComponent<DungeonStage>();
        startStage.SetFloor(floor);
        startStage.SetBoardXY(startX, startY);
        startStage.mBackwardDirection = backwardDirection;
        
        mStages.Add(startStage);

        // prev stage 정보를 기준으로 문을 만들어야하기 때문에 1층인지 구분하기 위한 코드
        if (floor == 1)
        {
            GenerateStage(startStage, startStage, GenerateDirections(backwardDirection, startX, startY), floor);
            CheckLastRoom(startStage, startStage, mDepth);
            // 플레이어가 위치한 스테이지 정보 갱신
            DungeonManager.Instance.SetPlayerCurrStage(startStage);
            // 스테이지의 플레이어 입장 여부 갱신
            startStage.SetIsEnterd(true);
        }
        else
        {
            GenerateStage(startStage, mLastRoom, GenerateDirections(backwardDirection, startX, startY), floor);
            CheckLastRoom(startStage, mLastRoom, mDepth);

            // 3층인 경우, 라스트룸 위에 보스 룸 추가 생성
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

        Debug.LogFormat("TotalCount:{0} CreateCount:{1}", TOTAL_COUNT, CREATE_COUNT);

         // [Notify] Enter Stage (First)
        startStage.OnStageEnter(DungeonDoor.TansferInfo.FirstRoom);

        return startStage;
    }

  

    public void StagesDelete()
    {
        GameObject deletStage = null;
        for (int i = 0; i < mStages.Count; ++i )
        {
            string stageName = mStages[i].name;
            deletStage = GameObject.Find(stageName);
            GameObject.Destroy(deletStage);
            mStages.RemoveAt(i);
            --i;   
        }
    }


    // 좌표에 방이 비었는지 체크
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
        for(int i = 0; i< mStages.Count; ++i)
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
                if(stage.mBackwardDirection != DIRECTION_TOP)
                {
                    GameObject roomObject = Instantiate(mStagePrefab);
                    roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                    roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                    DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                    nextStage.SetBoardXY(nextX, nextY);
                    stage.SetConnectedStage(DIRECTION_TOP,  nextStage);
                    nextStage.SetConnectedStage(DIRECTION_BOTTOM,  stage);
                    nextStage.SetFloor(floor);
                    mStages.Add(nextStage);
                    GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_BOTTOM, nextX, nextY), floor);
                    linkDoors |= DIRECTION_TOP;
                }
            }
            else
            {
                stage.SetConnectedStage(DIRECTION_TOP,  prevStage);
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
                    stage.SetConnectedStage(DIRECTION_BOTTOM,  nextStage);
                    nextStage.SetConnectedStage(DIRECTION_TOP,  stage);
                    nextStage.SetFloor(floor);
                    mStages.Add(nextStage);
                    GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_TOP, nextX, nextY), floor);
                    linkDoors |= DIRECTION_BOTTOM;
                }
            }
            else
            {
                stage.SetConnectedStage(DIRECTION_BOTTOM,  prevStage);
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
                    stage.SetConnectedStage(DIRECTION_LEFT,  nextStage);
                    nextStage.SetConnectedStage(DIRECTION_RIGHT,  stage);
                    nextStage.SetFloor(floor);
                    mStages.Add(nextStage);
                    GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_RIGHT, nextX, nextY), floor);
                    linkDoors |= DIRECTION_LEFT;
                }
            }
            else
            {
                stage.SetConnectedStage(DIRECTION_LEFT,  prevStage);
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
                    nextStage.SetConnectedStage(DIRECTION_LEFT,  stage);
                    nextStage.SetFloor(floor);
                    mStages.Add(nextStage);

                    GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_LEFT, nextX, nextY), floor);
                    linkDoors |= DIRECTION_RIGHT;
                }
            }
            else
            {
               
                stage.SetConnectedStage(DIRECTION_RIGHT,  prevStage);
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
            else if(i >= TOTAL_COUNT / 2)
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

    public void CheckLastRoom(DungeonStage stage,DungeonStage prevStage, int depth)
    {

        if (stage.GetConnectedStage(DIRECTION_TOP) != null)
        {
            if (prevStage != stage.GetConnectedStage(DIRECTION_TOP))
            {
                CheckLastRoom(GetStageByXY(stage.GetBoardX(), stage.GetBoardY() + 1),stage, depth+1);
            }
        }
        if (stage.GetConnectedStage(DIRECTION_BOTTOM) != null)
        {
            if (prevStage != stage.GetConnectedStage(DIRECTION_BOTTOM))
            {
                CheckLastRoom(GetStageByXY(stage.GetBoardX(), stage.GetBoardY() - 1),stage, depth+1);
            }
        }
        if (stage.GetConnectedStage(DIRECTION_RIGHT) != null)
        {
            if (prevStage != stage.GetConnectedStage(DIRECTION_RIGHT))
            {
                CheckLastRoom(GetStageByXY(stage.GetBoardX() + 1, stage.GetBoardY()),stage, depth+1);
            }
        }
        if (stage.GetConnectedStage(DIRECTION_LEFT) != null)
        {
            if (prevStage != stage.GetConnectedStage(DIRECTION_LEFT)) 
            { 
                CheckLastRoom(GetStageByXY(stage.GetBoardX() - 1, stage.GetBoardY()),stage, depth+1);
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

    // 문 방향 랜덤 설정
    public int GenerateDirections(int backward, int x, int y)
    {
        int output = DIRECTION_NONE;
        int randomPercent = 90;
        // 이전방 문과 연결될 방향 넣기.(이전 방에서 위의 방향으로 온경우, 아래 방향 문이 열려있어야 하니까, backward로 넣어준다)
        output |= backward;

        // 추가 가능한 방 개수가 0인경우, 연결될 문 방향만 넣고 리턴
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
                    if(random < randomPercent)
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
                    // 1층 스타트방은 아래 입장 문 만들어야해서 랜덤 생성 제외하기위한 예외처리
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
                        if(nextValue < (WIDTH) && IsEmpty(nextValue, y))
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
