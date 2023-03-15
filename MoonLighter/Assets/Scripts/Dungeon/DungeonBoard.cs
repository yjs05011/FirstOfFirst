using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBoard : MonoBehaviour
{
    public const int DIRECTION_NONE = 0;
    public const int DIRECTION_TOP = 1;
    public const int DIRECTION_BOTTOM = 2;
    public const int DIRECTION_LEFT = 4;
    public const int DIRECTION_RIGHT = 8;

    // 해당 보드의 문 활성화 가능 여부
    public bool mIsMovableTop = false;
    public bool mIsMovableBottom = false;
    public bool mIsMovableLeft = false;
    public bool mIsMovableRight = false;


    public List<GameObject> mObstacles= new List<GameObject>();

    public List<GameObject> mObjects = new List<GameObject>();

    
    // 입력: 위 아래 
    // 실제로 이 맵은: 위 아래 오른쪽  

    // 문이 활성화 되어 있는가?
    public bool IsMovableDirection(int directions)
    {
        if ((directions & DIRECTION_TOP) == DIRECTION_TOP)
        {
            if(!mIsMovableTop) // true
            {
                return false;
            }
        }

        if ((directions & DIRECTION_BOTTOM) == DIRECTION_BOTTOM)
        {
            if (!mIsMovableBottom) // true
            {
                return false;
            }
        }

        if ((directions & DIRECTION_LEFT) == DIRECTION_LEFT)
        {
            if (!mIsMovableLeft)    // false
            {
                return false;
            }
        }

        if ((directions & DIRECTION_RIGHT) == DIRECTION_RIGHT)
        {
            if (!mIsMovableRight) // true
            {
                return false;
            }
        }

        return true;

    }

}
