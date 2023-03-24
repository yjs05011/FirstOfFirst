using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonBoard : MonoBehaviour
{
    //보드 타입 enum
    public enum BoardType { Random, Pool, Camp, Boss , Start, DungeonBoss}
    // 보드의 타입
    public BoardType mType = BoardType.Random;

    // 해당 보드의 문의 방향으로 이동 가능 여부
    public bool mIsMovableTop = false;
    public bool mIsMovableBottom = false;
    public bool mIsMovableLeft = false;
    public bool mIsMovableRight = false;

    public DungeonHole mHole = null;
    // 장애물 리스트 (임시)
    public List<GameObject> mObstacles= new List<GameObject>();
    // 오브젝트 리스트 (임시)
    public List<GameObject> mObjects = new List<GameObject>();
 

    public void SetBoardType(BoardType type)
    {
        mType = type;
   
    }

    public BoardType GetBoardType() 
    { 
        return mType; 
    }

    public void SetHoleToStage(DungeonStage stage)
    {
        if (mHole != null)
        {
            mHole.SetStage(stage);
        }
    }

    // 이동 가능한 문 방향 체크 
    public bool IsMovableDirection(int directions)
    {
        if ((directions & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            if (!mIsMovableTop)
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

    public bool IsHole(Vector3 worldPosition)
    {
        if(mHole != null)
        {
            return mHole.IsHole(worldPosition);
        }
        return false;
    }
}
