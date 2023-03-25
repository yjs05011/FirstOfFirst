using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonBoard : MonoBehaviour
{
    //���� Ÿ�� enum
    public enum BoardType { Random, Pool, Camp, Boss , Start, DungeonBoss}
    // ������ Ÿ��
    public BoardType mType = BoardType.Random;

    // �ش� ������ ���� �������� �̵� ���� ����
    public bool mIsMovableTop = false;
    public bool mIsMovableBottom = false;
    public bool mIsMovableLeft = false;
    public bool mIsMovableRight = false;

    public DungeonHole mHole = null;
    // ��ֹ� ����Ʈ (�ӽ�)
    public List<GameObject> mObstacles= new List<GameObject>();
    // ������Ʈ ����Ʈ (�ӽ�)
    public List<GameObject> mObjects = new List<GameObject>();

    public Transform mMonstersObject = null;
    private List<Monster> mMonsters = new List<Monster>();



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

    // �̵� ������ �� ���� üũ 
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

    public void SetMonster()
    {
        if (mMonstersObject != null)
        {
            mMonsters.Clear();
            for (int i = 0; i < mMonstersObject.childCount; ++i)
            {
                mMonsters.Add(mMonstersObject.GetChild(i).gameObject.GetComponent<Monster>());
            }
        }

        Vector3 position = this.transform.position;
        for (int i = 0; i < mMonsters.Count; ++i)
        {
            float x = position.x - 10.0f;
            float y = position.y - 5.5f;
            mMonsters[i].mMovableArea.x = x;
            mMonsters[i].mMovableArea.y = y;
            mMonsters[i].mMovableArea.width = 20;
            mMonsters[i].mMovableArea.height = 11;
            mMonsters[i].gameObject.SetActive(true);


        }
    }
}
