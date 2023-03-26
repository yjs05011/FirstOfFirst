using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DungeonStage;

public class DungeonDoor : MonoBehaviour
{
    public enum TansferInfo
    {
        Normal,
        FirstRoom,
        LastRoom,
        Boss
    }

    [SerializeField]
    private int mDirection = DungeonGenerator.DIRECTION_NONE;
    // ���� ��������
    public DungeonStage mCurrStage = null;
    // ���� ����� ��������
    public DungeonStage mNextStage = null;
    // �Ϲ� �� 
    public GameObject mBasicDoor = null;
    // ���� ��(��) 
    public GameObject mBlockDoor = null;
    // ������ ���� �� �� (�̴� ���� �濡 �߰� �����Ǵ� ��) 
    public GameObject mFloorDoor = null;
    // ���� ��(���� ���� �� :�������� ���� ���ö�, �߰������Ǵ� ��)  
    public GameObject mEntryDoor = null;
    // ���� �� �� (3������ ������ ���� ��)
    public GameObject mBossRoomDoor = null;

    public Animator mBasicDoorAnim = null;
    public Animator mFloorDoorAnim = null;
    public Animator mEntryDoorAnim = null;
    public Animator mBossRoomDoorAnim = null;

    public enum DoorType { BASIC, FLOOR, ENTRY, BLOCK ,BOSS};
    [SerializeField]
    private DoorType mDoorType = DoorType.BLOCK; // ������ Ÿ�� 

    public enum DoorStatus { CLOSE, OPEN };
    public DoorStatus mDoorStatus = DoorStatus.CLOSE;

    public Collider2D mPlayerCollider = null;

    public void Awake()
    {
        mBlockDoor.SetActive(true);
       

    }
    public void Init()
    {
        if (mBasicDoor != null)
        {
            mBasicDoorAnim = mBasicDoor.GetComponent<Animator>();
        }
        if (mEntryDoor != null)
        {
            mEntryDoorAnim = mEntryDoor.GetComponent<Animator>();
        }
        if (mFloorDoor != null)
        {
            mFloorDoorAnim = mFloorDoor.GetComponent<Animator>();
        }

        if (mBossRoomDoor != null)
        {
            mBossRoomDoorAnim = mBossRoomDoor.GetComponent<Animator>();
        }
        
    }

    public void DoorOpen()
    {
        Init();
        SetDoorStatus(DoorStatus.OPEN);
        if (mBasicDoor != null && mBasicDoor.activeSelf)
        {
            mBasicDoorAnim.SetTrigger("DoorOpen");
        }
        if (mFloorDoor != null && mFloorDoor.activeSelf)
        {
            mFloorDoorAnim.SetTrigger("DoorOpen");
        }
        if (mBossRoomDoor != null && mBossRoomDoor.activeSelf)
        {
            SetDoorStatus(DoorStatus.OPEN);
            mBossRoomDoorAnim.SetTrigger("DoorOpen");
        }
    }
 
    public void DoorClose()
    {
        Init();
        SetDoorStatus(DoorStatus.CLOSE);
        if (mBasicDoor != null)
        {
            mBasicDoorAnim.SetTrigger("DoorClose");
        }
        if (mFloorDoor != null)
        {
            mFloorDoorAnim.SetTrigger("DoorClose");
        }
        if (mBossRoomDoor != null)
        {
            SetDoorStatus(DoorStatus.CLOSE);
            mBossRoomDoorAnim.SetTrigger("DoorClose");
        }
    }

    //public void BossRoomDoorOpen()
    //{
    //    Init();
    //    if (mBossRoomDoor != null)
    //    {
    //        SetDoorStatus(DoorStatus.OPEN);
    //        mBossRoomDoorAnim.SetTrigger("DoorOpen");
    //    }
    //}
    
    //public void BossRoomDoorClose()
    //{
    //    Init();
    //    if (mBossRoomDoor != null)
    //    {
    //        SetDoorStatus(DoorStatus.CLOSE);
    //        mBossRoomDoorAnim.SetTrigger("DoorClose");
    //    }
    //}


    public void SetCurrStage(DungeonStage stage)
    {
        mCurrStage = stage;
    }

    public DungeonStage GetCurrStage()
    {
        return mCurrStage;
    }
    public void SetNextStage(DungeonStage stage)
    {
        mNextStage = stage;
    }

    public DungeonStage GetNextStage()
    {
        return mNextStage;
    }

    public void SetDoorDirection(int direction)
    {
        mDirection = direction;
    }

    public int GetDoorDirection()
    {
        return mDirection;
    }
    public void SetDoors()
    {
       SetDoorType(DoorType.BASIC);
       mBasicDoor.SetActive(true);
       mBlockDoor.SetActive(false);
    }

    public void SetFloorDoor()
    {
        SetDoorType(DoorType.FLOOR);
        mBlockDoor.SetActive(false);
        mFloorDoor.SetActive(true);
    }
    public void SetBossRoomDoor()
    {
        SetDoorType(DoorType.BOSS);
        mBlockDoor.SetActive(false);
        mBossRoomDoor.SetActive(true);
    }

    public void SetEntryDoor()
    {
        SetDoorType(DoorType.ENTRY);
        mBlockDoor.SetActive(false);
        mEntryDoor.SetActive(true);
    }

    public DoorType GetDoorType()
    {
        return mDoorType;
    }
    public void SetDoorType(DoorType type)
    {
        mDoorType = type;
    }

    public DoorStatus GetDoorStatus()
    {
        return mDoorStatus;
    }

    public void SetDoorStatus(DoorStatus status)
    {
        mDoorStatus = status;
    }
    public void SetPlayerCollider(Collider2D other)
    {
        mPlayerCollider = other;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾�� �� ������ �ش� �������� ���� �־��ֱ� (�÷��̾ �ڽ����ִ� ���������� �������־����)
            //PlayerAct playerAct = other.gameObject.GetComponent<PlayerAct>();
            //playerAct.OnChangeDungeonStage(this.mNextStage);

            // �������� ������ �����½����� ���̵� �Լ� ȣ���� �ϱ�����, other�� �ٸ� �Լ������� ����Ҽ��ְ� �ɹ� ������ �־��.
            SetPlayerCollider(other);

            if (GetDoorStatus() == DoorStatus.CLOSE)
            {
                return;
            }
            else
            {
                switch (mDoorType)
                {
                    case DoorType.BASIC:

                        if (mNextStage == null)
                        {
                            break;
                        }
                         // Exit Stage
                        mCurrStage.OnStageExit(TansferInfo.Normal);

                        // ĳ���� ���� ���������� �̵�
                        Debug.LogFormat("{0} stage�� �̵�", mNextStage.name);


                        if ((mDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
                        {
                            Debug.LogFormat("player x:{0} y:{1}�� �̵�", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).x, mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).y);
                            other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM);

                            mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_BOTTOM);

                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
                        {
                            Debug.LogFormat("player x:{0} y:{1}�� �̵�", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_TOP).x, mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_TOP).y);
                            other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_TOP);
                            mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_TOP);
                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
                        {
                            Debug.LogFormat("player x:{0} y:{1}�� �̵�", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).x, mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).y);

                            other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT);
                            mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_RIGHT);
                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
                        {
                            Debug.LogFormat("player x:{0} y:{1}�� �̵�", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).x, mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).y);

                            other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_LEFT);
                            mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_LEFT);
                        }
                        // �÷��̾ �̵��� �������� ��ǥ�� ī�޶� �̵� 
                        DungeonManager.Instance.CameraMoveByPos(mNextStage.transform.position);
                        // �÷��̾ ��ġ�� �������� ���� ����
                        DungeonManager.Instance.SetPlayerCurrStage(mNextStage);

                        //Enter Stage
                        mNextStage.OnStageEnter(TansferInfo.Normal);
                        break;

                    case DoorType.FLOOR:
                        {
                            // �������� ������ �˸�
                            mCurrStage.OnStageExit(TansferInfo.LastRoom);

                            mFloorDoorAnim.SetTrigger("DoorClose");
                            // door close �ִϸ��̼� ���� �������� StartFloorChange()�� ȣ��. 

                        }
                        break;
                    case DoorType.BOSS:
                        {

                            if (this.GetCurrStage().GetFloor() == 3)
                            {
                                // �� ������ �ִϸ��̼� ����ϰ�.
                                mBossRoomDoorAnim.SetTrigger("DoorClose");
                                // �� ������ ���� ������ ���ϸ��̼� �̺�Ʈ�� EnterBossRoom() ȣ��

                            }
                            else
                            {
                                Debug.LogError("�߸������� ���Դϴ�.");
                            }
                            break;
                        }
                }
            }
           
        }
    }

    public bool IsPlayerEnterDoor()
    {
        if(mPlayerCollider != null)
        {
            return true;
        }
        return false;
    }

    public void EnterBossRoom()
    {
        // Exit Stage
        mCurrStage.OnStageExit(TansferInfo.Normal);

        Debug.Log("player ���������� �̵�");
        DungeonStage bossStage = DungeonManager.Instance.GetDungeonBossRoom();
        mPlayerCollider.transform.position = bossStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM);

        // ������ ���� �������� (3�� ��Ʈ ��)�� �����ǿ� y �ุ ������ ��ǥ
        Vector3 bossRoomCameraPos = new Vector3(this.GetCurrStage().transform.position.x, this.GetCurrStage().transform.position.y + 15.0f, 0);
        // ī�޶� �����濩�� true ����
        DungeonManager.Instance.GetDungeonCamera().SetIsBossRoom(true);
        // ī�޶� �̵� Ÿ�� ���� (���� �������� ����� ��� ī�޶� �̵�������)
        DungeonManager.Instance.GetDungeonCamera().SetCameraType(DungeonCameraController.CameraMoveType.Immediately);
        // ī�޶� �̵��� ��ġ 
        DungeonManager.Instance.CameraMoveByPos(bossRoomCameraPos);
        // �÷��̾ ��ġ�� �������� ���� ���� (���� ��������)
        DungeonManager.Instance.SetPlayerCurrStage(bossStage);
        // ���������� �÷��̾� ���� ���� ����
        bossStage.SetIsEnterd(true);

        // [Notify] Enter Stage
        mNextStage.OnStageEnter(TansferInfo.Boss);

    }


    public IEnumerator FloorChange()
    {
        
        // ���̵� �ε� �� fade in
        DungeonUIFadeInOutTransition transition = DungeonManager.Instance.GetTransitionUI();

        yield return transition.TransitionFadeOut();

        //������ ���� ���������� ����� ���� ���� 
        int nextFloorBackwardDirection = 0;
        if ((mDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
        {
            nextFloorBackwardDirection = DungeonGenerator.DIRECTION_BOTTOM;
        }
        if ((mDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
        {
            nextFloorBackwardDirection = DungeonGenerator.DIRECTION_TOP;
        }
        if ((mDirection & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
        {
            nextFloorBackwardDirection = DungeonGenerator.DIRECTION_LEFT;
        }
        if ((mDirection & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
        {
            nextFloorBackwardDirection = DungeonGenerator.DIRECTION_RIGHT;
        }

        int currFloor = this.GetCurrStage().GetFloor();
        // 3�� ����
        if (currFloor < 3)
        {
            // ���� �� ���� ����
            DungeonStage nextFloor = DungeonGenerator.Instance.InitDungeonBorad(0, 0, currFloor + 1, nextFloorBackwardDirection);
            // �÷��̾ ��ġ�� �������� ���� ���� (���� �� ���� ��������)
            DungeonManager.Instance.SetPlayerCurrStage(nextFloor);
           

            if ((mDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
            {
                Debug.LogFormat("player [{0}F | x:{1} y:{2}]�� �̵�", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).y);
                mPlayerCollider.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM);
                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_BOTTOM);
            }
            if ((mDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
            {
                Debug.LogFormat("player [{0}F | x:{1} y:{2}]�� �̵�", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP).y);
                mPlayerCollider.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP);
                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_TOP);
            }

            if ((mDirection & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
            {
                Debug.LogFormat("player [{0}F | x:{1} y:{2}]�� �̵�", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).y);

                mPlayerCollider.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT);
                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_RIGHT);
            }
            if ((mDirection & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
            {
                Debug.LogFormat("player [{0}F | x:{1} y:{2}]�� �̵�", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).y);

                mPlayerCollider.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT);
                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_LEFT);
            }
            // ī�޶� �̵� Ÿ�� ���� (���� �� ù �������� ����� ��� ī�޶� �̵�������)
            DungeonManager.Instance.GetDungeonCamera().SetCameraType(DungeonCameraController.CameraMoveType.Immediately);
            // �÷��̾ �̵��� �������� ��ǥ�� ī�޶� �̵� 
            DungeonManager.Instance.CameraMoveByPos(nextFloor.transform.position);

            SetPlayerCollider(null);
            // ���̵� �ε� �� fade out
            yield return transition.TransitionFadeIn();

            // [Notify] Enter Stage
            mNextStage.OnStageEnter(TansferInfo.FirstRoom);
        }
    }

}
