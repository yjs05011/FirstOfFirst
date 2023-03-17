using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBoard : MonoBehaviour
{
    public enum BoardType { None, HealingPool, Camp}

    public BoardType mType = BoardType.None;

    // 해당 보드의 문의 방향으로 이동 가능 여부
    public bool mIsMovableTop = false;
    public bool mIsMovableBottom = false;
    public bool mIsMovableLeft = false;
    public bool mIsMovableRight = false;


    public List<GameObject> mObstacles= new List<GameObject>();

    public List<GameObject> mObjects = new List<GameObject>();

    public BoardType GetBoardType() 
    { 
        return mType; 
    }

    // 문이 활성화 되어 있는가?
    public bool IsMovableDirection(int directions)
    {
        if ((directions & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            if(!mIsMovableTop) // true
            {
                return false;
            }
        }

        if ((directions & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            if (!mIsMovableBottom) // true
            {
                return false;
            }
        }

        if ((directions & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {
            if (!mIsMovableLeft)    // false
            {
                return false;
            }
        }

        if ((directions & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {
            if (!mIsMovableRight) // true
            {
                return false;
            }
        }

        return true;

    }

}
