using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DungeonStage;

public class DungeonDoor : MonoBehaviour
{
    [SerializeField]
    private int mDirection = DungeonGenerator.DIRECTION_NONE;

    public DungeonStage mCurrStage = null;
    public DungeonStage mNextStage = null;
    // 일반 문 
    public GameObject mBasicDoor = null;
    // 막힌 문(벽) 
    public GameObject mBlockDoor = null;
    // 다음층 연결 방 문 (미니 보스 방에 추가 생성되는 문) 
    public GameObject mFloorDoor = null;
    // 입장 문(던전 입장 문 :마을에서 던전 들어올때, 추가생성되는 문)  
    public GameObject mEntryDoor = null;
    // 보스 방 문 (3층에서 보스방 연결 문)
    public GameObject mBossRoomDoor = null;

    public Animator mBasicDoorAnim = null;
    public Animator mFloorDoorAnim = null;
    public Animator mEntryDoorAnim = null;
    public Animator mBossRoomDoorAnim = null;

    public enum DoorType { BASIC, FLOOR, ENTRY, BLOCK ,BOSS};
    private DoorType mDoorType = DoorType.BLOCK; // 문없는 타입 

    public enum DoorStatus { CLOSE, OPEN };
    public DoorStatus mDoorStatus = DoorStatus.CLOSE;

    public void Awake()
    {
        mBlockDoor.SetActive(true);
        mBasicDoorAnim = mBasicDoor.GetComponent<Animator>();   
        mEntryDoorAnim= mEntryDoor.GetComponent<Animator>();
        mFloorDoorAnim = mFloorDoor.GetComponent<Animator>();
        mBossRoomDoorAnim = mBossRoomDoor.GetComponent<Animator>();
    }

    public void DoorOpen()
    {
        SetDoorStatus(DoorStatus.OPEN);
        mBasicDoorAnim.SetTrigger("DoorOpen");
        mFloorDoorAnim.SetTrigger("DoorOpen");
    }
 
    public void DoorClose()
    {
        SetDoorStatus(DoorStatus.CLOSE);
        mBasicDoorAnim.SetTrigger("DoorClose");
        mFloorDoorAnim.SetTrigger("DoorClose");
    }

    public void BossRoomDoorOpen()
    {
        mBossRoomDoorAnim.SetTrigger("DoorOpen");
    }

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어에게 문 지날때 해당 스테이지 정보 넣어주기 (플레이어가 자신이있는 스테이지를 가지고있어야함)
            //PlayerAct playerAct = other.gameObject.GetComponent<PlayerAct>();
            //playerAct.OnChangeDungeonStage(this.mNextStage);

            
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
                        // 캐릭터 다음 스테이지로 이동
                        Debug.LogFormat("{0} stage로 이동", mNextStage.name);

                        if ((mDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
                        {
                            Debug.LogFormat("player x:{0} y:{1}로 이동", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).x, mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).y);
                            other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM);
                            mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_BOTTOM);
                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
                        {
                            Debug.LogFormat("player x:{0} y:{1}로 이동", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_TOP).x, mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_TOP).y);
                            other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_TOP);
                            mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_TOP);
                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
                        {
                            Debug.LogFormat("player x:{0} y:{1}로 이동", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).x, mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).y);

                            other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT);
                            mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_RIGHT);
                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
                        {
                            Debug.LogFormat("player x:{0} y:{1}로 이동", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).x, mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).y);

                            other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_LEFT);
                            mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_LEFT);
                        }
                        break;

                    case DoorType.FLOOR:
                        //다음층 스타트 스테이지 x,y
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
                        if (currFloor <= 3)
                        {
                            DungeonStage nextFloor = DungeonGenerator.Instance.InitDungeonBorad(0, 0, currFloor + 1, nextFloorBackwardDirection);

                            if ((mDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
                            {
                                Debug.LogFormat("player [{0}F | x:{1} y:{2}]로 이동", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).y);
                                other.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM);
                                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_BOTTOM);
                            }
                            if ((mDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
                            {
                                Debug.LogFormat("player [{0}F | x:{1} y:{2}]로 이동", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP).y);
                                other.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP);
                                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_TOP);
                            }

                            if ((mDirection & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
                            {
                                Debug.LogFormat("player [{0}F | x:{1} y:{2}]로 이동", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).y);

                                other.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT);
                                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_RIGHT);
                            }
                            if ((mDirection & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
                            {
                                Debug.LogFormat("player [{0}F | x:{1} y:{2}]로 이동", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).y);

                                other.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT);
                                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_LEFT);
                            }
                        }
                        break;
                }
            }
           
        }
    }
}
