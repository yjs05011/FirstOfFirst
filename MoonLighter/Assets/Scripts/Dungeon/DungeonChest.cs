using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonChest : MonoBehaviour
{
    public GameObject mChest = null;
    private Animator mChestAnim = null;

    public enum ChestID { None = 0, WoodChest = 1, BossChest =2 }
    [SerializeField]
    private ChestID mChestID = ChestID.None;
    public enum ChestState { Lock , Unlock , WaitInput , Open, Close }
    public ChestState mState = ChestState.Lock;
    public GameObject mInteractionMenu = null;
    public TMP_Text mInteractionKey = null;
    public Collider2D mTrriger = null;

    void Awake()
    {
        mChestAnim = mChest.GetComponent<Animator>();
        SetChestState(ChestState.Lock);
        mTrriger = mChest.GetComponent<Collider2D>();
    }

    private void Start()
    {
        mTrriger.enabled = false;
    }

    public void SetChestState(ChestState state)
    {
        mState = state;
    }

    public ChestState GetChestState()
    {
        return mState;
    }
    public ChestID GetChestID()
    {
        return mChestID;
    }

    public void SetInteractionKeyValue(string value)
    {
        mInteractionKey.text = value;
    }

    public void Update()
    {
        // 잠긴 상태이거나, 열린 상태이면 리턴.
        if(mState == ChestState.Lock )//|| mState == ChestState.Open)
        {
            return;
        }
        if (mState == ChestState.Open)
        {
            //if (mChestID == ChestID.BossChest)
            //{
                // 임시> 상자 열면 탈출하게 처리.
                   mState = ChestState.Lock;
            // DungeonManager.Instance.TestDungeonExitInit();
            //DungeonManager.Instance.TestDungeonEnterInit();
            LoadingManager.LoadScene("Dungeon");
            return;
            //}
           // return;
        }
        if(mState == ChestState.Unlock)
        {
            // 언락애니메이션 출력
            mChestAnim.SetTrigger("ChestUnlock");
            // 캐릭터  근처왓는지 체크를 위한 트리거용 컬라이더 활성화 처리
            mTrriger.enabled = true;
        }
        if (mState == ChestState.WaitInput)
        {
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))
            {
                // 상자 오픈 애니메이션 출력
                mChestAnim.SetTrigger("ChestOpen");
                // 상자 열림 상태로 변경
                SetChestState(ChestState.Open);

                // 상자 UI 호출 
                Debug.Log("UI : Chest open");

            }
        }
        // 상자 UI에서 상자의 상태를 close 로 변경 해주고, close anim 출력을위해 만들어둔 상태 
        if (mState == ChestState.Close)
        {
            // 상자 닫는 애니메이션 출력
            mChestAnim.SetTrigger("ChestClose");
            SetChestState(ChestState.Unlock);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 키입력 대기 상태로 변경
            SetChestState(ChestState.WaitInput);
            
            // 옵션에 설정된 키로 키 변경
            // 재욱이 코드 올라가면 주석 풀기.
            // string keyValue = GameKeyManger.keyString[GameKeyManger.KeyAction.INTERRUPT];
            //SetInteractionKeyValue("keyValue");

            // 오픈 키 UI 띄우기
            Debug.Log("UI : Chest open guide UI Active true");
            mInteractionMenu.SetActive(true);

        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 키입력 대기 상태 해제. 이전 상태로
            SetChestState(ChestState.Unlock);

            // 오픈 키 UI 띄운거 끄기
            Debug.Log("UI : Chest open guide UI Active false");
            mInteractionMenu.SetActive(false);

        }
    }

    // 상자 UI 끌때 상자 상태도 Close 로 바꿔줘야함 (재우)


}
