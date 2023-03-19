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


    public void Awake()
    {
        mBlockDoor.SetActive(true);
        mBasicDoorAnim = mBasicDoor.GetComponent<Animator>();   
        mEntryDoorAnim= mEntryDoor.GetComponent<Animator>();
        mFloorDoorAnim = mFloorDoor.GetComponent<Animator>();
    }

    public void DoorOpen()
    {
        mBasicDoorAnim.SetTrigger("DoorOpen");
        mFloorDoorAnim.SetTrigger("DoorOpen");
    }
 
    public void DoorClose()
    {
        mBasicDoorAnim.SetTrigger("DoorClose");
        mFloorDoorAnim.SetTrigger("DoorClose");
    }
    public void SetCurrStage(DungeonStage stage)
    {
        mCurrStage = stage;
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
       mBasicDoor.SetActive(true);
       mBlockDoor.SetActive(false);
    }

    public void SetFloorDoor()
    {
        mBlockDoor.SetActive(false);
        mFloorDoor.SetActive(true);
    }

    public void SetEntryDoor()
    {
        mBlockDoor.SetActive(false);
        mEntryDoor.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 캐릭터 다음 스테이지로 이동
            Debug.LogFormat("{0} stage로 이동", mNextStage.name);

            if ((mDirection & DungeonGenerator.DIRECTION_TOP) == DungeonGenerator.DIRECTION_TOP)
            {
                Debug.LogFormat("player x:{0} y:{1}로 이동", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).x , mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM).y);
                other.transform.position = mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_BOTTOM);
                mNextStage.SetEntryPoint(DungeonGenerator.DIRECTION_BOTTOM);
            }
            if ((mDirection & DungeonGenerator.DIRECTION_BOTTOM) == DungeonGenerator.DIRECTION_BOTTOM)
            {
                Debug.LogFormat("player x:{0} y:{1}로 이동", mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_TOP).x , mNextStage.GetStartPoint(DungeonGenerator.DIRECTION_TOP).y);
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

        }
    }
}
