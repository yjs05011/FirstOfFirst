using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static DungeonStage;

public class DungeonStage : MonoBehaviour
{
    // �������� �ٴ� ����
    public DungeonBoard mBoard = null;

    // �ٴ� ���� ����Ʈ
    public List<DungeonBoard> mBoards = new List<DungeonBoard>();
    // �� 4���� ������Ʈ
    public DungeonDoor mDoorTop = null;
    public DungeonDoor mDoorRight = null;
    public DungeonDoor mDoorBottom = null;
    public DungeonDoor mDoorLeft = null;
  
    // ���������� ��
    public int mFloor;
    // ���������� ���� ���� ���� ����
    public int mDoorDirections = 0;
    // ���� ����� ���������� x,y ��
    public int mBoardX = 0;
    public int mBoardY = 0;

    // ���������� ����� ��/��/��/�� ������ �������� ���� ���� 
    [SerializeField]
    private DungeonStage mConnectedStageTop = null;
    [SerializeField]
    private DungeonStage mConnectedStageRight = null;
    [SerializeField]
    private DungeonStage mConnectedStageBottom = null;
    [SerializeField]
    private DungeonStage mConnectedStageLeft= null;

    // �������� �ٴ�(����)�� Ÿ��
    public DungeonBoard.BoardType mBoradStyle = DungeonBoard.BoardType.Start;

    // �������� 4���� ��ŸƮ ����Ʈ 
    public GameObject mStartPointTop = null;
    public GameObject mStartPointRight = null;
    public GameObject mStartPointBottom = null;
    public GameObject mStartPointLeft = null;

    // ���� ������
    public Vector3 mEntryPosition = Vector3.zero;
    // 
    public int mBackwardDirection = DungeonGenerator.DIRECTION_NONE;

    // �÷��̾ ���������� ���� �ִ��� üũ �� ����
    public bool mIsPlayerEntered = false;



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


    public void SetIsEnterd(bool value)
    {
        mIsPlayerEntered = value;
    }

    public bool GetIsEnterd()
    {
        return mIsPlayerEntered;
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

    public void SetEntryPoint(int backwardDirection)
    {
        if ((backwardDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            mEntryPosition = mStartPointTop.transform.position;
        }
        if ((backwardDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            mEntryPosition = mStartPointBottom.transform.position;
        }
        if ((backwardDirection & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {
            mEntryPosition = mStartPointLeft.transform.position;
        }
        if ((backwardDirection & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
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

    // �� ���⿡ �´� ���� ����
    public void SetStageBoard()
    {

        List<DungeonBoard> boards = new List<DungeonBoard>();
        GetFilteredBoards(mDoorDirections, mBoradStyle, ref boards);

        // ���͸��� ���� �߿� �ϳ��� �����ϱ� ���� ���� �� �̱�
        int random = UnityEngine.Random.Range(0, boards.Count);  

        for(int index=0; index < boards.Count; ++index)
        {
            Debug.Log(boards[index].gameObject.name);
            
            if(index == random)
            {
                mBoard = boards[index];

                mBoard.SetHoleToStage(this);
                mBoard.SetMonster();
                mBoard.gameObject.SetActive(true);
                if(mBoradStyle == DungeonBoard.BoardType.Pool)
                {
                    DungeonHealingPool healingPool = mBoard.transform.Find("Pool").gameObject.GetComponent<DungeonHealingPool>();
                    healingPool.InitPoolHeal();
                }
                break;    
            }
        }
    }



    public void SetBoadStyle(DungeonBoard.BoardType type)
    {
        // 3�� ������ �� ����ó�� (3���� ������ ���� ���� Ÿ�� ���尡 ���;��ؼ�) 
        if (GetFloor() == 3 && type == DungeonBoard.BoardType.Boss)
        {
            mBoradStyle = DungeonBoard.BoardType.Random;
        }
        else
        {
            mBoradStyle = type;
        }
        if (type == DungeonBoard.BoardType.Boss || type == DungeonBoard.BoardType.Start)
        {
            SetAddDoor(type);
        }
      
        SetStageBoard();

    }
    public DungeonBoard.BoardType GetBoardType()
    {
        return mBoradStyle;
    }


    // �� ���⿡ �´� ���� ����Ʈ ���͸�
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


    //�� ���� ����
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

    //�� �߰� (�� ���� stage : ���� �� ���� �� �߰� | ���� stage : ���� ���� or �ش� �� ���� �� �߰�)
    public void SetAddDoor(DungeonBoard.BoardType type)
    {
        // ���۹��� ���� �����ϴ� �κ�
        if(type == DungeonBoard.BoardType.Start)
        {
            // ���� ������ ������ ��� (���̵� �� ����)
            if(mBackwardDirection != DungeonGenerator.DIRECTION_NONE)
            {
                if(mBackwardDirection == DungeonGenerator.DIRECTION_TOP)
                {
                    mDoorTop.SetCurrStage(this);
                    mDoorTop.SetFloorDoor();
                }
                if (mBackwardDirection == DungeonGenerator.DIRECTION_BOTTOM)
                {
                    mDoorBottom.SetCurrStage(this);
                    mDoorBottom.SetFloorDoor();
                }
                if (mBackwardDirection == DungeonGenerator.DIRECTION_LEFT)
                {
                    mDoorLeft.SetCurrStage(this);
                    mDoorLeft.SetFloorDoor();
                }
                if (mBackwardDirection == DungeonGenerator.DIRECTION_RIGHT)
                {
                    mDoorRight.SetCurrStage(this);
                    mDoorRight.SetFloorDoor();
                }

            }
            // ó�� 1���� ������ ��� (Ȳ�����)
            else
            {
                mDoorBottom.SetCurrStage(this);
                mDoorBottom.SetEntryDoor();
            }
        }


        if(type == DungeonBoard.BoardType.Boss)
        {
            if (mFloor == 3)
            {
                mDoorTop.SetCurrStage(this);
                mDoorTop.SetBossRoomDoor();
                return;
            }
            else
            {
                if ((mDoorDirections & DungeonGenerator.DIRECTION_TOP) != DungeonGenerator.DIRECTION_TOP)
                {
                    int nextStageX = GetBoardX();
                    int nextStageY = GetBoardY() + 1;

                    if (DungeonGenerator.Instance.GetStageByXY(nextStageX, nextStageY) == null)
                    {
                        mDoorTop.SetCurrStage(this);
                        mDoorTop.SetFloorDoor();
                        return;
                    }
                }
                if ((mDoorDirections & DungeonGenerator.DIRECTION_LEFT) != DungeonGenerator.DIRECTION_LEFT)
                {
                    int nextStageX = GetBoardX() - 1;
                    int nextStageY = GetBoardY();

                    if (DungeonGenerator.Instance.GetStageByXY(nextStageX, nextStageY) == null)
                    {
                        mDoorLeft.SetCurrStage(this);
                        mDoorLeft.SetFloorDoor();
                        return;
                    }
                }
                if ((mDoorDirections & DungeonGenerator.DIRECTION_RIGHT) != DungeonGenerator.DIRECTION_RIGHT)
                {
                    int nextStageX = GetBoardX() + 1;
                    int nextStageY = GetBoardY();

                    if (DungeonGenerator.Instance.GetStageByXY(nextStageX, nextStageY) == null)
                    {
                        mDoorRight.SetCurrStage(this);
                        mDoorRight.SetFloorDoor();
                        return;
                    }
                }
                if ((mDoorDirections & DungeonGenerator.DIRECTION_BOTTOM) != DungeonGenerator.DIRECTION_BOTTOM)
                {
                    int nextStageX = GetBoardX();
                    int nextStageY = GetBoardY() - 1;

                    if (DungeonGenerator.Instance.GetStageByXY(nextStageX, nextStageY) == null)
                    {
                        mDoorBottom.SetCurrStage(this);
                        mDoorBottom.SetFloorDoor();
                        return;
                    }
                }
            }
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

    public bool IsHole(Vector3 worldPosition)
    {
        return mBoard.IsHole(worldPosition);
    }
}
