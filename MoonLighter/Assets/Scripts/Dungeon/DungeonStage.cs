using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStage : MonoBehaviour
{
    public List<DungeonBoard> mBoards = new List<DungeonBoard>();


    public GameObject mDoorRight = null;
    public GameObject mDoorLeft = null;
    public GameObject mDoorUp = null;
    public GameObject mDoorDown = null;

    public int mDoorDirections = 0;



    // 문 방향에 맞는 보드 설정
    void SetStageBoard()
    {

        List<DungeonBoard> boards = new List<DungeonBoard>();
        GetFilteredBoards(mDoorDirections, ref boards);

        // 필터링된 보드 중에 하나를 선택하기 위한 랜덤 수 뽑기
        int random = Random.Range(0, mBoards.Count);  

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
    public void GetFilteredBoards(int directions, ref List<DungeonBoard> output)
    {
        int count = mBoards.Count;
        for (int idx = 0; idx < count; ++idx)
        {
            DungeonBoard board = mBoards[idx];

            if (board.IsMovableDirection(directions))
            {
                if (!output.Contains(board))
                {
                    output.Add(board);
                }
            }
        }
    }


    //문 방향 결정
    public void SetDoorDirections(int doorDirections)
    {

        mDoorDirections = doorDirections;
        mDoorRight.SetActive((doorDirections & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT);
        mDoorLeft.SetActive((doorDirections & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT);
        mDoorUp.SetActive((doorDirections & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP);
        mDoorDown.SetActive((doorDirections & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM);

        SetStageBoard();

    }

    public int GetDoorDirections()
    {
        return mDoorDirections;
    }




}
