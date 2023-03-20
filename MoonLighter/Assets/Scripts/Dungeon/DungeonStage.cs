using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DungeonStage;

public class DungeonStage : MonoBehaviour
{
    // 바닥 형태 리스트
    public List<DungeonBoard> mBoards = new List<DungeonBoard>();
    // 문 4방향 오브젝트
    public DungeonDoor mDoorTop = null;
    public DungeonDoor mDoorRight = null;
    public DungeonDoor mDoorBottom = null;
    public DungeonDoor mDoorLeft = null;
  
    public int mFloor = 0;
    public int mDoorDirections = 0;
    public int mBoardX = 0;
    public int mBoardY = 0;

    [SerializeField]
    private DungeonStage mConnectedStageTop = null;
    [SerializeField]
    private DungeonStage mConnectedStageRight = null;
    [SerializeField]
    private DungeonStage mConnectedStageBottom = null;
    [SerializeField]
    private DungeonStage mConnectedStageLeft= null;

    public DungeonBoard.BoardType mBoradStyle = DungeonBoard.BoardType.Start;

    public GameObject mStartPointTop = null;
    public GameObject mStartPointRight = null;
    public GameObject mStartPointBottom = null;
    public GameObject mStartPointLeft = null;

    public Vector3 mEntryPosition = Vector3.zero;


    public void Awake()
    {
        mDoorTop.SetDoorDirection(DungeonGenerator.DIRECTION_TOP);
        mDoorRight.SetDoorDirection(DungeonGenerator.DIRECTION_RIGHT);
        mDoorBottom.SetDoorDirection(DungeonGenerator.DIRECTION_BOTTOM);
        mDoorLeft.SetDoorDirection(DungeonGenerator.DIRECTION_LEFT);
    }
    public void SetFloor(int floor)
    {
        mFloor = floor;
    }
    public int GetFloor()
    {
        return mFloor;
    }

    public Vector3 GetStartPoint(int direction) 
    {
        if ((direction & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            return mStartPointTop.transform.position;
        }
        if ((direction & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            return mStartPointBottom.transform.position;
        }
        if ((direction & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {
            return mStartPointLeft.transform.position;
        }
        if ((direction & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {
            return mStartPointRight.transform.position;
        }
        return Vector3.zero; 
   
    }

    public Vector3 GetEntryPosition()
    {
        return mEntryPosition;
    }

    public void SetEntryPoint(int direction)
    {
        if ((direction & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            mEntryPosition = mStartPointTop.transform.position;
        }
        if ((direction & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            mEntryPosition = mStartPointBottom.transform.position;
        }
        if ((direction & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {
            mEntryPosition = mStartPointLeft.transform.position;
        }
        if ((direction & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {
            mEntryPosition = mStartPointRight.transform.position;
        }
       
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
    public DungeonStage GetConnectedStage(int direction)
    {
        if ((direction & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            return mConnectedStageTop;
        }
        if ((direction & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            return mConnectedStageBottom;
        }
        if ((direction & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {
            return mConnectedStageLeft;
        }
        if ((direction & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {
            return mConnectedStageRight;
        }
        return null;
    }

    public void SetConnectedStage(int direction,  DungeonStage stage)
    {

        if ((direction & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP) 
        {
            mConnectedStageTop = stage;
            mDoorTop.SetCurrStage(this);
            mDoorTop.SetNextStage(stage);
        }
        if ((direction & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {
            mConnectedStageRight = stage;
            mDoorRight.SetCurrStage(this);
            mDoorRight.SetNextStage(stage);
        }
        if ((direction & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            mConnectedStageBottom = stage;
            mDoorBottom.SetCurrStage(this);
            mDoorBottom.SetNextStage(stage);
        }
        if ((direction & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)  
        {
            mConnectedStageLeft = stage;
            mDoorLeft.SetCurrStage(this);
            mDoorLeft.SetNextStage(stage);
            
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
                if(mBoradStyle == DungeonBoard.BoardType.POOL)
                {
                    DungeonHealingPool healingPool = boards[index].transform.Find("Pool").gameObject.GetComponent<DungeonHealingPool>();
                    healingPool.InitPoolHeal();
                }
                break;    
            }
        }
    }

    public void SetBoadStyle(DungeonBoard.BoardType type)
    {
        mBoradStyle = type;
        if (type == DungeonBoard.BoardType.BOSS || type == DungeonBoard.BoardType.Start)
        {
            SetAddDoor(type);
        }
      
        SetStageBoard();

    }
    public DungeonBoard.BoardType GetBoardType()
    {
        return mBoradStyle;
    }


    // 문 방향에 맞는 보드 리스트 필터링
    public void GetFilteredBoards(int directions, DungeonBoard.BoardType type, ref List<DungeonBoard> output)
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


    //문 방향 설정
    public void SetDoors(int doorDirections)
    {
        mDoorDirections = doorDirections;

            if ((doorDirections & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
            {
                    mDoorTop.SetDoors();
            }
            if ((doorDirections & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
            {
                    mDoorBottom.SetDoors();
            }
            if ((doorDirections & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
            {
                    mDoorLeft.SetDoors();
            }
            if ((doorDirections & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
            {
                    mDoorRight.SetDoors();
            }
        
    }

    public DungeonDoor GetDoorByDirection(int doorDirections)
    {
        if ((doorDirections & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            return mDoorTop;
        }
        if ((doorDirections & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            return mDoorBottom;
        }
        if ((doorDirections & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {
            return mDoorLeft;
        }
        if ((doorDirections & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {
            return mDoorRight;
        }
        else
        {
            return null;    
        }
    }

    public void SetDoorsOpen()
    {
        mDoorTop.DoorOpen();
        mDoorBottom.DoorOpen();
        mDoorLeft.DoorOpen();
        mDoorRight.DoorOpen();
    }


    public void SetDoorsClose()
    {
        mDoorTop.DoorClose();
        mDoorBottom.DoorClose();
        mDoorLeft.DoorClose();
        mDoorRight.DoorClose();
    }

    //문 추가 (보스 stage : 다음 층 연결 문 추가 | 시작 stage : 던전 입장 or 해당 층 입장 문 추가)
    public void SetAddDoor(DungeonBoard.BoardType type)
    {
        if ((mDoorDirections & DungeonGenerator.DIRECTION_TOP) != DungeonGenerator.DIRECTION_TOP)
        {
            mDoorTop.SetCurrStage(this);
            if (type == DungeonBoard.BoardType.BOSS)
            {
                mDoorTop.SetFloorDoor();
            }
            if (type == DungeonBoard.BoardType.Start)
            {
                mDoorTop.SetEntryDoor();
            }
            return;
        }
        if ((mDoorDirections & DungeonGenerator.DIRECTION_LEFT) != DungeonGenerator.DIRECTION_LEFT)
        {
            mDoorLeft.SetCurrStage(this);
            if (type == DungeonBoard.BoardType.BOSS)
            {
                mDoorLeft.SetFloorDoor();
            }
            if (type == DungeonBoard.BoardType.Start)
            {
                mDoorLeft.SetEntryDoor();
            }
            return;
        }
        if ((mDoorDirections & DungeonGenerator.DIRECTION_RIGHT) != DungeonGenerator.DIRECTION_RIGHT)
        {
            mDoorRight.SetCurrStage(this);
            if (type == DungeonBoard.BoardType.BOSS)
            {
                mDoorRight.SetFloorDoor();
            }
            if (type == DungeonBoard.BoardType.Start)
            {
                mDoorRight.SetEntryDoor();
            }
            return;
        }
        if ((mDoorDirections & DungeonGenerator.DIRECTION_BOTTOM) != DungeonGenerator.DIRECTION_BOTTOM)
        {
            mDoorBottom.SetCurrStage(this);
            if (type == DungeonBoard.BoardType.BOSS)
            {
                mDoorBottom.SetFloorDoor();
            }
            if (type == DungeonBoard.BoardType.Start)
            {
                mDoorBottom.SetEntryDoor();
            }
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

            return GetConnectedStage(DungeonGenerator.DIRECTION_TOP);
        }
        else if ((mDoorDirections & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {

            return GetConnectedStage(DungeonGenerator.DIRECTION_LEFT);
        }
        else if ((mDoorDirections & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {

            return GetConnectedStage(DungeonGenerator.DIRECTION_RIGHT);

        }
        else if ((mDoorDirections & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {

            return GetConnectedStage(DungeonGenerator.DIRECTION_BOTTOM);
        }
        else
        {
            return null;
        }
    }


}
