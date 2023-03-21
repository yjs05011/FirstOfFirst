using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBoard : MonoBehaviour
{
    public enum BoardType { RANDOM, POOL, CAMP, BOSS , Start}

    public BoardType mType = BoardType.RANDOM;

    // 해당 보드의 문의 방향으로 이동 가능 여부
    public bool mIsMovableTop = false;
    public bool mIsMovableBottom = false;
    public bool mIsMovableLeft = false;
    public bool mIsMovableRight = false;


    public List<GameObject> mObstacles= new List<GameObject>();

    public List<GameObject> mObjects = new List<GameObject>();

    public void SetBoardType(BoardType type)
    {
        mType = type;
    }

    public BoardType GetBoardType() 
    { 
        return mType; 
    }

    // 이동 가능한 문 방향 체크 
    public bool IsMovableDirection(int directions)
    {
        if ((directions & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            if(!mIsMovableTop) 
            {
                return false;
            }
        }

        if ((directions & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            if (!mIsMovableBottom) 
            {
                return false;
            }
        }

        if ((directions & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {
            if (!mIsMovableLeft) 
            {
                return false;
            }
        }

        if ((directions & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {
            if (!mIsMovableRight) 
            {
                return false;
            }
        }

        return true;

    }

}
