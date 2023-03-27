using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonChest : MonoBehaviour
{
    public GameObject mChest = null;
    private Animator mChestAnim = null;

    public enum ChestState { Lock , Unlock , WaitInput , Open, Close}
    public ChestState mState = ChestState.Lock;

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


    public void Update()
    {
        // 잠긴 상태이거나, 열린 상태이면 리턴.
        if(mState == ChestState.Lock || mState == ChestState.Open)
        {
            return;
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
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]))
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

            // 오픈 키 UI 띄우기
            Debug.Log("UI : Chest open guide UI Active true");
            // UI 만들어져있나 확인 필요. 

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
            // UI 만들어져있나 확인 필요. 

        }
    }

    // 상자 UI 끌때 상자 상태도 Close 로 바꿔줘야함 (재우)


}
