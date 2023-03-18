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


    public void Start()
    {
        int safetyTotalCount = (Mathf.Max(WIDTH, HEIGHT) / 2) * Mathf.Min(WIDTH, HEIGHT);
        if(TOTAL_COUNT > safetyTotalCount)
        {
            TOTAL_COUNT = safetyTotalCount;
        }


        for (int idx = 0; idx < mDungeonBoard.Length; ++idx)
        {
            mDungeonBoard[idx] = 0;
        }

        OnGenerate();
    }


    public void OnGenerate()
    {
        Debug.LogFormat("w: {0}, h : {1}", WIDTH, HEIGHT);
            //int creatCount = 0;
        //GenerateStage(START_X, START_Y, START_X, START_Y, GenerateDirections(DIRECTION_NONE, START_X, START_Y, creatCount), creatCount);
        //++CREATE_COUNT;
        GenerateStage(START_X, START_Y, START_X, START_Y, GenerateDirections(DIRECTION_NONE, START_X, START_Y));
        Debug.LogFormat("TotalCount:{0} CreateCount:{1}", TOTAL_COUNT, CREATE_COUNT);

      
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

    public void SetRoom(int x, int y)
    {
        mDungeonBoard[y * WIDTH + x] = 1;
    }

    public void GenerateStage(int x, int y, int px, int py, int directions)
    {

        DebugDirections(x, y, directions);

        int linkDoors = DIRECTION_NONE;
        GameObject roomObject = Instantiate(mStagePrefab);
        roomObject.name = string.Format("Stage {0},{1}" , x, y);
        roomObject.transform.position = new Vector3(x * 26.0f, y * 15.0f, 0);
 
        DungeonStage room = roomObject.GetComponent<DungeonStage>();

     

        if ((directions & DIRECTION_TOP) == DIRECTION_TOP)
        {
            int nextX = x;
            int nextY = y + 1;
            if (nextX != px || nextY != py)
            {
                GenerateStage(nextX, nextY, x, y, GenerateDirections(DIRECTION_BOTTOM, nextX, nextY));
                linkDoors |= DIRECTION_TOP;
            }
            else
            {
                linkDoors |= DIRECTION_TOP;
            }
        }

        if ((directions & DIRECTION_BOTTOM) == DIRECTION_BOTTOM)
        {
            int nextX = x;
            int nextY = y - 1;
            if (nextX != px || nextY != py)
            {
                GenerateStage(nextX, nextY, x, y, GenerateDirections(DIRECTION_TOP, nextX, nextY));
                linkDoors |= DIRECTION_BOTTOM;
            }
            else
            {
                linkDoors |= DIRECTION_BOTTOM;
            }
        }

        if ((directions & DIRECTION_LEFT) == DIRECTION_LEFT)
        {
            int nextX = x - 1;
            int nextY = y;
            if (nextX != px || nextY != py)
            {
                GenerateStage(nextX, nextY, x, y, GenerateDirections(DIRECTION_RIGHT, nextX, nextY));
                linkDoors |= DIRECTION_LEFT;
            }
            else
            {
                linkDoors |= DIRECTION_LEFT;
            }
        }

        if ((directions & DIRECTION_RIGHT) == DIRECTION_RIGHT)
        {
            int nextX = x + 1;
            int nextY = y;
            if (nextX != px || nextY != py)
            {
                GenerateStage(nextX, nextY, x, y, GenerateDirections(DIRECTION_LEFT, nextX, nextY));
                linkDoors |= DIRECTION_RIGHT;
            }
            else
            {
                linkDoors |= DIRECTION_RIGHT;
            }
        }

        room.SetStyle(linkDoors,DungeonBoard.BoardType.RANDOM);
    }

    // 문 방향 랜덤 설정
    public int GenerateDirections(int backward, int x, int y)
    {
        //int validCreatRoomCount = TOTAL_COUNT - CREATE_COUNT;
        int output = DIRECTION_NONE;
        int randomPercent = 90;

        // 이전방 문과 연결될 방향 넣기.(이전 방에서 위의 방향으로 온경우, 아래 방향 문이 열려있어야 하니까, backward로 넣어준다)
        output |= backward;

        // 추가 가능한 방 개수가 1개만 경우, 연결될 문 방향만 넣고 리턴,
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
                            //randomPercent -= random;
                            ++CREATE_COUNT;
                            
                            //--validCreatRoomCount;
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
                            //randomPercent -= random;
                            ++CREATE_COUNT;
                            //--validCreatRoomCount;
                        }
                    }
                }
                if (TOTAL_COUNT - CREATE_COUNT >= 1 && backward != DIRECTION_BOTTOM)
                {
                    int random = UnityEngine.Random.Range(0, 100 + 1);

                    if (random < randomPercent)
                    {
                        int nextValue = y - 1;
                        if(nextValue >= 0 && IsEmpty(x, nextValue)) 
                        {
                            SetRoom(x, nextValue);

                            output |= DIRECTION_BOTTOM;
                            //randomPercent -= random;
                            ++CREATE_COUNT;
                            //--validCreatRoomCount;
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
                            //--validCreatRoomCount;
                        }
                    }
                }
            }

        
        }
        return output;
    }

    

    public void DebugDirections(int x, int y, int directions)
    {
        string append = "";


        if ((directions & DIRECTION_TOP) == DIRECTION_TOP)
        {
            append += "[O]";

        }
        else
        {
            append += "[X]";

        }

        if ((directions & DIRECTION_BOTTOM) == DIRECTION_BOTTOM)
        {
            append += "[O]";

        }
        else
        {
            append += "[X]";

        }

        if ((directions & DIRECTION_LEFT) == DIRECTION_LEFT)
        {
            append += "[O]";

        }
        else
        {
            append += "[X]";

        }

        if ((directions & DIRECTION_RIGHT) == DIRECTION_RIGHT)
        {
            append += "[O]";

        }
        else
        {
            append += "[X]";
        }

        Debug.LogFormat("{0},{1}:{2}", x, y, append);
    }
}
