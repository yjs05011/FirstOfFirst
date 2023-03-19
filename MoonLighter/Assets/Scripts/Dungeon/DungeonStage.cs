using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static DungeonStage;

public class DungeonStage : MonoBehaviour
{
    // 바닥 형태 리스트
    public List<DungeonBoard> mBoards = new List<DungeonBoard>();
    // 일반 문 리스트
    public List<GameObject> mBasicDoors = new List<GameObject>();
    // 막힌 문(벽) 리스트
    public List<GameObject> mBlockDoors = new List<GameObject>();
    // 보스 방 문 리스트
    public List<GameObject> mBossRoomDoors = new List<GameObject>();
    // 문 방향 enum
    public enum DoorDirection { UP, RIGHT, DOWN, LEFT };

    public int mDoorDirections = 0;
    public int mBoardX = 0;
    public int mBoardY = 0;

    [SerializeField]
    private DungeonStage[] mConnectedStage = new DungeonStage[4];
    public DungeonBoard.BoardType mBoradStyle = DungeonBoard.BoardType.Start;

    public void Awake()
    {
        mConnectedStage = new DungeonStage[4];
    }

    public int GetBoardX()
    {
        return mBoardX;
    }
    public int GetBoardY()
    {
        return mBoardY;
    }
    public void SetBoardXY(int x, int y)
    {
        mBoardX = x;
        mBoardY = y;
    }
    public DungeonStage GetConnectedStage(DoorDirection direction)
    {
        switch (direction)
        {
            case DoorDirection.UP:
                return mConnectedStage[(int)DoorDirection.UP];
                
            case DoorDirection.RIGHT:
                return mConnectedStage[(int)DoorDirection.RIGHT];
                
            case DoorDirection.DOWN:
                return mConnectedStage[(int)DoorDirection.DOWN];
               
            case DoorDirection.LEFT:
                return mConnectedStage[(int)DoorDirection.LEFT];

            default:
                return null;
        }
    }

    public void SetConnectedStage(DoorDirection direction,  DungeonStage stage)
    {
        switch (direction)
        {
            case DoorDirection.UP:
                mConnectedStage[0] = stage;
                break;
            case DoorDirection.RIGHT:
                mConnectedStage[1] = stage;
                break;
            case DoorDirection.DOWN:
                mConnectedStage[(int)DoorDirection.DOWN] = stage;
                break;
            case DoorDirection.LEFT:
                mConnectedStage[(int)DoorDirection.LEFT] = stage;
                break;
        }
    }

    // 문 방향에 맞는 보드 설정
    public void SetStageBoard()
    {

        List<DungeonBoard> boards = new List<DungeonBoard>();
        GetFilteredBoards(mDoorDirections, mBoradStyle, ref boards);

        // 필터링된 보드 중에 하나를 선택하기 위한 랜덤 수 뽑기
        int random = UnityEngine.Random.Range(0, boards.Count);  

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

    public void SetBoadStyle(DungeonBoard.BoardType type)
    {
        mBoradStyle = type;
        if (type == DungeonBoard.BoardType.BOSS)
        {
            SetFloorDoors();
        }
        SetStageBoard();

    }
    public DungeonBoard.BoardType GetBoardType()
    {
        return mBoradStyle;
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
    public void SetDoors(int doorDirections)
    {
        
        mDoorDirections = doorDirections;
   
        mBasicDoors[(int)DoorDirection.RIGHT].SetActive((doorDirections & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT);
        mBasicDoors[(int)DoorDirection.LEFT].SetActive((doorDirections & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT);
        mBasicDoors[(int)DoorDirection.UP].SetActive((doorDirections & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP);
        mBasicDoors[(int)DoorDirection.DOWN].SetActive((doorDirections & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM);


        mBlockDoors[(int)DoorDirection.RIGHT].SetActive((doorDirections & DungeonGenerator.DIRECTION_RIGHT) != DungeonGenerator.DIRECTION_RIGHT);
        mBlockDoors[(int)DoorDirection.LEFT].SetActive((doorDirections & DungeonGenerator.DIRECTION_LEFT) != DungeonGenerator.DIRECTION_LEFT);
        mBlockDoors[(int)DoorDirection.UP].SetActive((doorDirections & DungeonGenerator.DIRECTION_TOP) != DungeonGenerator.DIRECTION_TOP);
        mBlockDoors[(int)DoorDirection.DOWN].SetActive((doorDirections & DungeonGenerator.DIRECTION_BOTTOM) != DungeonGenerator.DIRECTION_BOTTOM);

    }
    //다음 층 문 추가
    public void SetFloorDoors()
    {
       
        if((mDoorDirections & DungeonGenerator.DIRECTION_TOP) != DungeonGenerator.DIRECTION_TOP)
        {
            mBlockDoors[(int)DoorDirection.UP].SetActive(false);
            mBossRoomDoors[(int)DoorDirection.UP].SetActive(true);
            return;
        }
        if ((mDoorDirections & DungeonGenerator.DIRECTION_LEFT) != DungeonGenerator.DIRECTION_LEFT)
        {
            mBlockDoors[(int)DoorDirection.LEFT].SetActive(false);
            mBossRoomDoors[(int)DoorDirection.LEFT].SetActive(true);
            return;
        }
        if((mDoorDirections & DungeonGenerator.DIRECTION_RIGHT) != DungeonGenerator.DIRECTION_RIGHT)
        {
             mBlockDoors[(int)DoorDirection.RIGHT].SetActive(false);
             mBossRoomDoors[(int)DoorDirection.RIGHT].SetActive(true);
             return;
        }
        if ((mDoorDirections & DungeonGenerator.DIRECTION_BOTTOM) != DungeonGenerator.DIRECTION_BOTTOM)
        {
            mBlockDoors[(int)DoorDirection.DOWN].SetActive(false);
            mBossRoomDoors[(int)DoorDirection.DOWN].SetActive(true);
            return;
        }

    }

    public int GetDoorDirections()
    {
        return mDoorDirections;
    }

    public DungeonStage GetPrevStage()
    {
        if ((mDoorDirections & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {

            return GetConnectedStage(DoorDirection.UP);
        }
        else if ((mDoorDirections & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {

            return GetConnectedStage(DoorDirection.LEFT);
        }
        else if ((mDoorDirections & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {

            return GetConnectedStage(DoorDirection.RIGHT);

        }
        else if ((mDoorDirections & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {

            return GetConnectedStage(DoorDirection.DOWN);
        }
        else
        {
            return null;
        }
    }


}
