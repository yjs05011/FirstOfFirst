using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonGenerator : MonoBehaviour
{
    public GameObject mStagePrefab = null; 
    public const int WIDTH = 10;
    public const int HEIGHT = 10;

    public const int START_X = 0;
    public const int START_Y = 0;

    public const int DIRECTION_NONE = 0;
    public const int DIRECTION_TOP = 1;
    public const int DIRECTION_BOTTOM = 2;
    public const int DIRECTION_LEFT = 4;
    public const int DIRECTION_RIGHT = 8;

    public static int TOTAL_COUNT = 10;
    public static int CREATE_COUNT = 0;

    public static int[] mDungeonBoard = new int[WIDTH * HEIGHT];

    public List<DungeonStage> mStages = new List<DungeonStage>();
  
    public bool IsCreatedCampRoom = false;
   
    public DungeonBoard.BoardType mBoardType = DungeonBoard.BoardType.Start;

    public int mDepth = 0;
    public DungeonStage mLastRoom = null;
    public void Start()
    {
       
        // 방 생성 종 개수 방어로직
        int safetyTotalCount = (Mathf.Max(WIDTH, HEIGHT) / 2) * Mathf.Min(WIDTH, HEIGHT);
        if(TOTAL_COUNT > safetyTotalCount)
        {
            TOTAL_COUNT = safetyTotalCount;
        }
        // 보드 전체 빈 방으로 설정.
        for (int idx = 0; idx < mDungeonBoard.Length; ++idx)
        {
            mDungeonBoard[idx] = 0;
        }
        IsCreatedCampRoom = false;
        OnGenerate();
    }

 

    public void OnGenerate()
    {
        Debug.LogFormat("w: {0}, h : {1}", WIDTH, HEIGHT);

        GameObject roomObject = Instantiate(mStagePrefab);
        roomObject.name = string.Format("Stage {0},{1}", START_X, START_Y);
        roomObject.transform.position = new Vector3(START_X * 26.0f, START_Y * 15.0f, 0);

        DungeonStage startStage = roomObject.GetComponent<DungeonStage>();
        startStage.SetBoardXY(START_X, START_Y);
        mStages.Add(startStage);

        GenerateStage(startStage, startStage, GenerateDirections(DIRECTION_NONE, START_X, START_Y));
        Debug.LogFormat("TotalCount:{0} CreateCount:{1}", TOTAL_COUNT, CREATE_COUNT);
       
        CheckLastRoom(startStage, startStage, mDepth);
        SetRoomStyle();
        SetDoorOpen();
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
    

    public void GenerateStage(DungeonStage stage, DungeonStage prevStage, int directions)
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
                GameObject roomObject = Instantiate(mStagePrefab);
                roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                nextStage.SetBoardXY(nextX, nextY);
                stage.SetConnectedStage(DIRECTION_TOP,  nextStage);
                nextStage.SetConnectedStage(DIRECTION_BOTTOM,  stage);
                mStages.Add(nextStage);
                GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_BOTTOM, nextX, nextY));
                linkDoors |= DIRECTION_TOP;
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
                GameObject roomObject = Instantiate(mStagePrefab);
                roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                nextStage.SetBoardXY(nextX, nextY);
                stage.SetConnectedStage(DIRECTION_BOTTOM,  nextStage);
                nextStage.SetConnectedStage(DIRECTION_TOP,  stage);
                mStages.Add(nextStage);
                GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_TOP, nextX, nextY));
                linkDoors |= DIRECTION_BOTTOM;
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
                GameObject roomObject = Instantiate(mStagePrefab);
                roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                nextStage.SetBoardXY(nextX, nextY);
                stage.SetConnectedStage(DIRECTION_LEFT,  nextStage);
                nextStage.SetConnectedStage(DIRECTION_RIGHT,  stage);
                mStages.Add(nextStage);
                GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_RIGHT, nextX, nextY));
                linkDoors |= DIRECTION_LEFT;
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
                GameObject roomObject = Instantiate(mStagePrefab);
                roomObject.name = string.Format("Stage {0},{1}", nextX, nextY);
                roomObject.transform.position = new Vector3(nextX * 26.0f, nextY * 15.0f, 0);

                DungeonStage nextStage = roomObject.GetComponent<DungeonStage>();
                nextStage.SetBoardXY(nextX, nextY);
                stage.SetConnectedStage(DIRECTION_RIGHT, nextStage);
                nextStage.SetConnectedStage(DIRECTION_LEFT,  stage);
                mStages.Add(nextStage);

                GenerateStage(nextStage, stage, GenerateDirections(DIRECTION_LEFT, nextX, nextY));
                linkDoors |= DIRECTION_RIGHT;
            }
            else
            {
               
                stage.SetConnectedStage(DIRECTION_RIGHT,  prevStage);
                linkDoors |= DIRECTION_RIGHT;
            }
        }


        stage.SetDoors(linkDoors);
    }

    public void SetRoomStyle()
    {
       
        for (int i = 0; i < mStages.Count; ++i)
        {

            if (mStages[i].GetBoardX() == START_X && mStages[i].GetBoardY() == START_Y)
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.Start);
            }
            else if (mStages[i] == mLastRoom)
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.BOSS);
            }
            else if (mStages[i] == mLastRoom.GetPrevStage())
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.POOL);
            }
            else if(i >= TOTAL_COUNT / 2)
            {
                if (IsCreatedCampRoom == false)
                {
                    if (mStages[i].GetBoardType() != DungeonBoard.BoardType.BOSS && mStages[i].GetBoardType() != DungeonBoard.BoardType.POOL)
                    {
                        IsCreatedCampRoom = true;
                        mStages[i].SetBoadStyle(DungeonBoard.BoardType.CAMP);
                    }

                }
                else
                {
                    mStages[i].SetBoadStyle(DungeonBoard.BoardType.RANDOM);
                }
            }
            else
            {
                mStages[i].SetBoadStyle(DungeonBoard.BoardType.RANDOM);
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
                        if (nextValue < HEIGHT && IsEmpty(x, nextValue))
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
                    // 스타트방은 아래 입장 문 만들어야해서 예외처리
                    if (x != START_X && y != START_Y)
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
                        if(nextValue < WIDTH && IsEmpty(nextValue, y))
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
