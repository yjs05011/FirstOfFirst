using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject mStagePrefab = null; 
    public const int WIDTH = 7;
    public const int HEIGHT = 7;

    public const int START_X = 0;
    public const int START_Y = 0;

    public const int DIRECTION_NONE = 0;
    public const int DIRECTION_TOP = 1;
    public const int DIRECTION_BOTTOM = 2;
    public const int DIRECTION_LEFT = 4;
    public const int DIRECTION_RIGHT = 8;

    public static int TOTAL_COUNT = 20;


    public static int[] mDungeonBoard = new int[WIDTH * HEIGHT];

    public List<DungeonStage> mStages = new List<DungeonStage>();


    public void Start()
    {
        for (int idx = 0; idx < mDungeonBoard.Length; ++idx)
        {
            mDungeonBoard[idx] = 0;
        }

        OnGenerate();
    }


    public void OnGenerate()
    {
        int creatCount = 0;
        GenerateStage(START_X, START_Y, START_X, START_Y, GenerateDirections(DIRECTION_NONE, START_X, START_Y, creatCount), creatCount);

        for (int idy = 0; idy < HEIGHT; ++idy)
        {

            for (int idx = 0; idx < WIDTH; ++idx)
            {
                //GenerateStage(START_X, START_Y, START_X, START_Y, GenerateDirections(DIRECTION_NONE, START_X, START_Y, false));
                Debug.Log(mDungeonBoard[idy * WIDTH + idx]);
            }

        }
        // CheckDirections();
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

    public void GenerateStage(int x, int y, int px, int py, int directions, int creatCount)
    {

        // 현재 방이 빈방이 아닌경우
        if (IsEmpty(x, y) == false)
        {
            return;
        }

        mDungeonBoard[y * WIDTH + x] = 1;

        DebugDirections(x, y, directions);

        ++creatCount;

        int linkDoors = DIRECTION_NONE;
        GameObject roomObject = Instantiate(mStagePrefab);
        roomObject.name = "Stage" + creatCount;
        roomObject.transform.position = new Vector3(x * 26.0f, y * 15.0f, 0);

        DungeonStage room = roomObject.GetComponent<DungeonStage>();

        bool isLastRoom = (TOTAL_COUNT >= 20);

        if ((directions & DIRECTION_TOP) == DIRECTION_TOP)
        {
            int nextX = x;
            int nextY = y + 1;
            if (nextX != px || nextY != py)
            {
                if (nextY < HEIGHT)
                {
                    if (IsEmpty(nextX, nextY))
                    {
                        if (creatCount < TOTAL_COUNT)
                        {
                            GenerateStage(nextX, nextY, x, y, GenerateDirections(DIRECTION_BOTTOM, nextX, nextY, creatCount), creatCount);
                            linkDoors |= DIRECTION_TOP;
                        }
                    }
                }
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
                if (nextY < 0)
                {
                    if (IsEmpty(nextX, nextY))
                    {
                        if (creatCount < TOTAL_COUNT)
                        {
                            GenerateStage(nextX, nextY, x, y, GenerateDirections(DIRECTION_TOP, nextX, nextY, creatCount), creatCount);
                            linkDoors |= DIRECTION_BOTTOM;
                        }
                    }
                }
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
                if (nextX < 0)
                {
                    if (IsEmpty(nextX, nextY))
                    {
                        if ((creatCount < TOTAL_COUNT))
                        {
                            GenerateStage(nextX, nextY, x, y, GenerateDirections(DIRECTION_RIGHT, nextX, nextY, creatCount), creatCount);
                            linkDoors |= DIRECTION_LEFT;
                        }
                    }
                }
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
                if (nextX < WIDTH)
                {
                    if (IsEmpty(nextX, nextY))
                    {
                        if ((creatCount < TOTAL_COUNT))
                        {
                            GenerateStage(nextX, nextY, x, y, GenerateDirections(DIRECTION_LEFT, nextX, nextY, creatCount), creatCount);
                            linkDoors |= DIRECTION_RIGHT;
                        }
                    }
                }
            }
            else
            {
                linkDoors |= DIRECTION_RIGHT;
            }
        }

        room.SetDoorDirections(linkDoors);
    }

    public int GenerateDirections(int backward, int x, int y, int creatCount)
    {
        int output = DIRECTION_NONE;
        int randomPercent = 90;

        output |= backward;


        {
            int random = UnityEngine.Random.Range(0, 100 + 1);

            if (random < randomPercent && y < HEIGHT)
            {
                output |= DIRECTION_TOP;


                randomPercent -= random;
            }

        }

        {
            int random = UnityEngine.Random.Range(0, 100 + 1);

            if (random < randomPercent && y >= 1)
            {
                output |= DIRECTION_BOTTOM;

                randomPercent -= random;
            }

        }

        {
            int random = UnityEngine.Random.Range(0, 100 + 1);

            if (random < randomPercent && x >= 1)
            {
                output |= DIRECTION_LEFT;

                randomPercent -= random;
            }

        }
        {
            int random = UnityEngine.Random.Range(0, 100 + 1);

            if (random < randomPercent && x < WIDTH)
            {
                output |= DIRECTION_RIGHT;


            }

        }
        if (output == DIRECTION_NONE)
        {
            return GenerateDirections(backward, x, y, creatCount);
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
