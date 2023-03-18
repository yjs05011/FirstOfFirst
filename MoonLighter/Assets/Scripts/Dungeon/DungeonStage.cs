using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStage : MonoBehaviour
{
    // 바닥 형태 리스트
    public List<DungeonBoard> mBoards = new List<DungeonBoard>();
    // 일반 문 리스트
    public List<GameObject> mBasicDoors = new List<GameObject>();
    // 막힌 문(벽) 리스트
    public List<GameObject> mBlockDoors = new List<GameObject>();
    // 문 방향 enum
    public enum DoorDirection { UP, RIGHT, DOWN, LEFT};

    public int mDoorDirections = 0;



    // 문 방향에 맞는 보드 설정
    void SetStageBoard(DungeonBoard.BoardType type)
    {

        List<DungeonBoard> boards = new List<DungeonBoard>();
        GetFilteredBoards(mDoorDirections, type, ref boards);

        // 필터링된 보드 중에 하나를 선택하기 위한 랜덤 수 뽑기
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

    

    // 문 방향에 맞는 보드 리스트 필터링
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


    //문 방향 결정
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
