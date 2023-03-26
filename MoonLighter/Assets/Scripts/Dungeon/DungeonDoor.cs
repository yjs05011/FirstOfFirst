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
    // 현재 스테이지
    public DungeonStage mCurrStage = null;
    // 문에 연결된 스테이지
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
    [SerializeField]
    private DoorType mDoorType = DoorType.BLOCK; // 문없는 타입 

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
            // 플레이어에게 문 지날때 해당 스테이지 정보 넣어주기 (플레이어가 자신이있는 스테이지를 가지고있어야함)
            //PlayerAct playerAct = other.gameObject.GetComponent<PlayerAct>();
            //playerAct.OnChangeDungeonStage(this.mNextStage);

            // 문닫히는 연출이 끝나는시점에 층이동 함수 호출을 하기위해, other를 다른 함수에서도 사용할수있게 맴버 변수에 넣어둠.
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
                        // 플레이어가 이동한 스테이지 좌표로 카메라 이동 
                        DungeonManager.Instance.CameraMoveByPos(mNextStage.transform.position);
                        // 플레이어가 위치한 스테이지 정보 갱신
                        DungeonManager.Instance.SetPlayerCurrStage(mNextStage);

                        //Enter Stage
                        mNextStage.OnStageEnter(TansferInfo.Normal);
                        break;

                    case DoorType.FLOOR:
                        {
                            // 스테이지 나감을 알림
                            mCurrStage.OnStageExit(TansferInfo.LastRoom);

                            mFloorDoorAnim.SetTrigger("DoorClose");
                            // door close 애니메이션 종료 시점에서 StartFloorChange()를 호출. 

                        }
                        break;
                    case DoorType.BOSS:
                        {

                            if (this.GetCurrStage().GetFloor() == 3)
                            {
                                // 문 닫히는 애니메이션 출력하고.
                                mBossRoomDoorAnim.SetTrigger("DoorClose");
                                // 문 닫히는 연출 끝날때 에니메이션 이벤트로 EnterBossRoom() 호출

                            }
                            else
                            {
                                Debug.LogError("잘못생성된 문입니다.");
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

        Debug.Log("player 보스방으로 이동");
        DungeonStage bossStage = DungeonManager.Instance.GetDungeonBossRoom();
        mPlayerCollider.transform.position = bossStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM);

        // 보스방 이전 스테이지 (3층 라스트 룸)의 포지션에 y 축만 증가한 좌표
        Vector3 bossRoomCameraPos = new Vector3(this.GetCurrStage().transform.position.x, this.GetCurrStage().transform.position.y + 15.0f, 0);
        // 카메라 보스방여부 true 설정
        DungeonManager.Instance.GetDungeonCamera().SetIsBossRoom(true);
        // 카메라 이동 타입 변경 (보스 스테이지 입장시 즉시 카메라 이동을위해)
        DungeonManager.Instance.GetDungeonCamera().SetCameraType(DungeonCameraController.CameraMoveType.Immediately);
        // 카메라 이동할 위치 
        DungeonManager.Instance.CameraMoveByPos(bossRoomCameraPos);
        // 플레이어가 위치한 스테이지 정보 갱신 (보스 스테이지)
        DungeonManager.Instance.SetPlayerCurrStage(bossStage);
        // 스테이지의 플레이어 입장 여부 갱신
        bossStage.SetIsEnterd(true);

        // [Notify] Enter Stage
        mNextStage.OnStageEnter(TansferInfo.Boss);

    }


    public IEnumerator FloorChange()
    {
        
        // 층이동 로딩 씬 fade in
        DungeonUIFadeInOutTransition transition = DungeonManager.Instance.GetTransitionUI();

        yield return transition.TransitionFadeOut();

        //다음층 시작 스테이지의 백워드 방향 설정 
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
        // 3층 이하
        if (currFloor < 3)
        {
            // 다음 층 던전 생성
            DungeonStage nextFloor = DungeonGenerator.Instance.InitDungeonBorad(0, 0, currFloor + 1, nextFloorBackwardDirection);
            // 플레이어가 위치한 스테이지 정보 갱신 (다음 층 시작 스테이지)
            DungeonManager.Instance.SetPlayerCurrStage(nextFloor);
           

            if ((mDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
            {
                Debug.LogFormat("player [{0}F | x:{1} y:{2}]로 이동", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).y);
                mPlayerCollider.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM);
                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_BOTTOM);
            }
            if ((mDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
            {
                Debug.LogFormat("player [{0}F | x:{1} y:{2}]로 이동", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP).y);
                mPlayerCollider.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_TOP);
                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_TOP);
            }

            if ((mDirection & DungeonGenerator.DIRECTION_LEFT) == DungeonGenerator.DIRECTION_LEFT)
            {
                Debug.LogFormat("player [{0}F | x:{1} y:{2}]로 이동", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT).y);

                mPlayerCollider.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_RIGHT);
                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_RIGHT);
            }
            if ((mDirection & DungeonGenerator.DIRECTION_RIGHT) == DungeonGenerator.DIRECTION_RIGHT)
            {
                Debug.LogFormat("player [{0}F | x:{1} y:{2}]로 이동", currFloor + 1, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).x, nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT).y);

                mPlayerCollider.transform.position = nextFloor.GetStartPoint(DungeonGenerator.DIRECTION_LEFT);
                nextFloor.SetEntryPoint(DungeonGenerator.DIRECTION_LEFT);
            }
            // 카메라 이동 타입 변경 (다음 층 첫 스테이지 입장시 즉시 카메라 이동을위해)
            DungeonManager.Instance.GetDungeonCamera().SetCameraType(DungeonCameraController.CameraMoveType.Immediately);
            // 플레이어가 이동한 스테이지 좌표로 카메라 이동 
            DungeonManager.Instance.CameraMoveByPos(nextFloor.transform.position);

            SetPlayerCollider(null);
            // 층이동 로딩 씬 fade out
            yield return transition.TransitionFadeIn();

            // [Notify] Enter Stage
            mNextStage.OnStageEnter(TansferInfo.FirstRoom);
        }
    }

}
