using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStage : MonoBehaviour
{
    // �ٴ� ���� ����Ʈ
    public List<DungeonBoard> mBoards = new List<DungeonBoard>();
    // �Ϲ� �� ����Ʈ
    public List<GameObject> mBasicDoors = new List<GameObject>();
    // ���� ��(��) ����Ʈ
    public List<GameObject> mBlockDoors = new List<GameObject>();
    // �� ���� enum
    public enum DoorDirection { UP, RIGHT, DOWN, LEFT};

    public int mDoorDirections = 0;



    // �� ���⿡ �´� ���� ����
    void SetStageBoard(DungeonBoard.BoardType type)
    {

        List<DungeonBoard> boards = new List<DungeonBoard>();
        GetFilteredBoards(mDoorDirections, type, ref boards);

        // ���͸��� ���� �߿� �ϳ��� �����ϱ� ���� ���� �� �̱�
        int random = Random.Range(0, boards.Count);  

        for(int index=0; index < boards.Count; ++index)
        {
            Debug.Log(boards[index].gameObject.name);
            
            if(index == random)
            {
                boards[index].gameObject.SetActive(true);
                break;    
            }
        }
    }

    

    // �� ���⿡ �´� ���� ����Ʈ ���͸�
    public void GetFilteredBoards(int directions,DungeonBoard.BoardType type, ref List<DungeonBoard> output)
    {
        
        int count = mBoards.Count;
        for (int idx = 0; idx < count; ++idx)
        {
            DungeonBoard board = mBoards[idx];

            if (board.GetBoardType() == type)
            {
                if (board.IsMovableDirection(directions))
                {
                    if (!output.Contains(board))
                    {
                        output.Add(board);
                    }
                }
            }
        }
    }


    //�� ���� ����
    public void SetStyle(int doorDirections, DungeonBoard.BoardType type)
    {

        mDoorDirections = doorDirections;
        mBasicDoors[(int)DoorDirection.RIGHT].SetActive((doorDirections & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT);
        mBasicDoors[(int)DoorDirection.LEFT].SetActive((doorDirections & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT);
        mBasicDoors[(int)DoorDirection.UP].SetActive((doorDirections & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP);
        mBasicDoors[(int)DoorDirection.DOWN].SetActive((doorDirections & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM);
        //mDoorLeft.SetActive((doorDirections & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT);
        //mDoorUp.SetActive((doorDirections & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP);
        //mDoorDown.SetActive((doorDirections & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM);

        mBlockDoors[(int)DoorDirection.RIGHT].SetActive((doorDirections & DungeonGenerator.DIRECTION_RIGHT) != DungeonGenerator.DIRECTION_RIGHT);
        mBlockDoors[(int)DoorDirection.LEFT].SetActive((doorDirections & DungeonGenerator.DIRECTION_LEFT) != DungeonGenerator.DIRECTION_LEFT);
        mBlockDoors[(int)DoorDirection.UP].SetActive((doorDirections & DungeonGenerator.DIRECTION_TOP) != DungeonGenerator.DIRECTION_TOP);
        mBlockDoors[(int)DoorDirection.DOWN].SetActive((doorDirections & DungeonGenerator.DIRECTION_BOTTOM) != DungeonGenerator.DIRECTION_BOTTOM);
        SetStageBoard(type);

    }

    public int GetDoorDirections()
    {
        return mDoorDirections;
    }




}
