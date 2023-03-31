using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static DungeonDoor;

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

    // ���� �� ����
    public int mBackwardDirection = DungeonGenerator.DIRECTION_NONE;

    // �÷��̾ ���������� ���� �ִ��� üũ �� ����
    public bool mIsPlayerEntered = false;
    

    // óġ�� ���� ��
    public int mMonsterDieCount = 0;

    // 미니 보스 보드 배열
    public DungeonBoard[] mMiniBossBoard = new DungeonBoard[2];

    public void Awake()
    {
        mDoorTop.SetDoorDirection(DungeonGenerator.DIRECTION_TOP);
        mDoorRight.SetDoorDirection(DungeonGenerator.DIRECTION_RIGHT);
        mDoorBottom.SetDoorDirection(DungeonGenerator.DIRECTION_BOTTOM);
        mDoorLeft.SetDoorDirection(DungeonGenerator.DIRECTION_LEFT);
        DoorInit();
    }

  

    public void DoorInit()
    {
        mDoorTop.Init();
        mDoorRight.Init();
        mDoorBottom.Init();
        mDoorLeft.Init();
    }

    // 스테이지에 몬스터가 모두 처치된 상태인지 체크해서 반환하는 함수.
    public bool IsCleanStage()
    {
        // 보드의 몬스터수와 처치 몬스터수가 같을때 (모두 처치한경우)
        if (mBoard.GetBoardMonsterCount() == mMonsterDieCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddDieMonsterCount(int count = 1)
    {
        mMonsterDieCount += count;

        // 보드의 몬스터수와 처치 몬스터수가 같을때 (모두 처치한경우)
        if(mBoard.GetBoardMonsterCount() == mMonsterDieCount)
        {
            // 문 오픈
            SetDoorsOpen();
            // 상자 있을경우 상자 오픈.
            SetChestUnlock();

        }
    }
    
    public void SetChestUnlock()
    {
        if(mBoard.GetChest() != null)
        {
            mBoard.GetChest().SetChestState(DungeonChest.ChestState.Unlock);
            // 던전 메니저에 언락 상자 갱신.
            DungeonManager.Instance.UnlockChestAdd(mBoard.mChest.GetChestID());
        }
       
        return;
    }

        // �÷��̾ ���������� �������� �˸�
    public void OnStageEnter(DungeonDoor.TansferInfo transferInfo)
    {
        this.StartCoroutine(OnStageEnterCoroutine(transferInfo));
    }

    public IEnumerator OnStageEnterCoroutine(DungeonDoor.TansferInfo transferInfo)
    {
        yield return new WaitForSeconds(0.5f);

        // 미니 보스 스테이지 , 던전 보스 스테이지 진입 시 main ui HP bar 활성화.
        if (mBoard.GetBoardType() == DungeonBoard.BoardType.Boss || mBoard.GetBoardType() == DungeonBoard.BoardType.DungeonBoss)
        {
            UiManager.Instance.SetBossHpVisible(true);
        }

        // 층 이동 후 입장 floor door close 
        if (transferInfo == DungeonDoor.TansferInfo.FirstRoom)
        {
            if (GetEntryFloorDoorDirection() != null)
            {
                GetEntryFloorDoorDirection().DoorClose();
            }
        }
        // 스테이지의 플레이어 입장 여부 갱신
        SetIsEnterd(true);
        // 보드의 몬스터 수와 처치한 몬스터 수가 다를경우 (몬스터를 처치하지않은 방)
        if (mBoard.GetBoardMonsterCount() != mMonsterDieCount)
        {
            SetDoorsClose();
        }


        Debug.LogFormat("The player is enter the stage. ({0} Floor X:{1}, Y:{2}) - {3}", mFloor, mBoardX, mBoardY, transferInfo.ToString());
    }

    // 층 이동으로 입장한 방향의 문 찾아 반환하는 함수. 
    public DungeonDoor GetEntryFloorDoorDirection()
    {
        if(mDoorTop.GetDoorDirection() == mBackwardDirection)
        {
            return mDoorTop;
        }
        if (mDoorRight.GetDoorDirection() == mBackwardDirection)
        {
            return mDoorRight;
        }
        if (mDoorBottom.GetDoorDirection() == mBackwardDirection)
        {
            return mDoorBottom;
        }
        if (mDoorLeft.GetDoorDirection() == mBackwardDirection)
        {
            return mDoorLeft;
        }
        else
        {
            return null;
        }

    }

    // �÷��̾ ���������� ������ �˸�
    public void OnStageExit(DungeonDoor.TansferInfo transferInfo)
    {
        this.StartCoroutine(OnStageExitCoroutine(transferInfo));
    }

    public IEnumerator OnStageExitCoroutine(DungeonDoor.TansferInfo transferInfo)
    {
        Debug.LogFormat("The player is exit the stage. ({0} Floor X:{1}, Y:{2}) - {3}", mFloor, mBoardX, mBoardY, transferInfo.ToString());
        yield return true;
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
                mBoard.SetMonster(this);
                
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
        // 타입이 보스(미니 보스) 인 경우, 층에 해당하는 보드를 넣어서 전달.
        if (type == DungeonBoard.BoardType.Boss)
        {
            int index = mFloor - 1;
            DungeonBoard board = mMiniBossBoard[index];

            output.Add(board);
        }
        else
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
