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
    // 보스 방 문 
    public GameObject mFloorDoor = null;
    // 입장 문 
    public GameObject mEntryDoor = null;

    public Animator mBasicDoorAnim = null;
    public Animator mFloorDoorAnim = null;
    public Animator mEntryDoorAnim = null;

    public enum DoorType { BASIC, FLOOR, ENTRY, BLOCK };
    private DoorType mDoorType = DoorType.BLOCK; // 문없는 타입 

    public enum DoorStatus { CLOSE, OPEN };
    public DoorStatus mDoorStatus = DoorStatus.CLOSE;

    public void Awake()
    {
        mBlockDoor.SetActive(true);
        mBasicDoorAnim = mBasicDoor.GetComponent<Animator>();   
        mEntryDoorAnim= mEntryDoor.GetComponent<Animator>();
        mFloorDoorAnim = mFloorDoor.GetComponent<Animator>();
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
                        int nextFloorX = 0;
                        int nextFloorY = 0;
                        int nextFloorBackwardDirection = 0;
                        if ((mDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
                        {
                            nextFloorX = mCurrStage.GetBoardX();
                            nextFloorY = mCurrStage.GetBoardY() + 1;
                            nextFloorBackwardDirection = DungeonGenerator.DIRECTION_BOTTOM;
                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
                        {
                            nextFloorX = this.GetCurrStage().GetBoardX();
                            nextFloorY = this.GetCurrStage().GetBoardY() - 1;
                            nextFloorBackwardDirection = DungeonGenerator.DIRECTION_TOP;
                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
                        {
                            nextFloorX = this.GetCurrStage().GetBoardX() + 1;
                            nextFloorY = this.GetCurrStage().GetBoardY();
                            nextFloorBackwardDirection = DungeonGenerator.DIRECTION_LEFT;
                        }
                        if ((mDirection & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
                        {
                            nextFloorX = this.GetCurrStage().GetBoardX() - 1;
                            nextFloorY = this.GetCurrStage().GetBoardY();
                            nextFloorBackwardDirection = DungeonGenerator.DIRECTION_RIGHT;
                        }

                        int currFloor = this.GetCurrStage().GetFloor();
                        if (currFloor <= 3)
                        {
                            DungeonGenerator.Instance.InitDungeonBorad(nextFloorX, nextFloorY, currFloor + 1, nextFloorBackwardDirection);

                            DungeonStage nextFloor = DungeonGenerator.Instance.GetStageByXY(nextFloorX, nextFloorY);

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
