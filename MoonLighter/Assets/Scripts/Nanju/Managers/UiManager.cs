using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    public static UiManager Instance;
    public bool mIsInventoryLock = false;
    // 보스 hp 표시
    public bool mIsBossHpVisible = false;

    // [던전]
    // 보스 Hp
    public float mBossMaxHp;
    public float mBossCurrentHp;


    public bool mIsHpChange = false;
    // 던전 확인
    public bool mIsDungeonCheck = false;

    // [플레이어]
    public bool mIsPlayerDie = false;
    public bool mIsPlayerUseAnimation = false;
    public bool mIsPlayerFinishAnimation = false;
    public bool mIsResultUi = false;
    //키변경 하기 위한 변수 선언
    public bool mIsKeyChanged = false;
    // 무기 변경
    public bool mIsWeaponChange = false;


    // [Ui]
    public bool mExitDungeonUiOnOffCheck = false;
    public bool mOptionExit = false;
    public bool mIsInventoryInteraction = false;

    // [마을]
    // 테이블 아이템을 열었을 시 값이 변하는 변수 선언
    public bool mItemTableOpen = false;
    // 마을 게시판을 열었을 시 값이 변하는 변수 선언
    public bool mVillageNoticeBoardOpen = false;
    // 대장간과 말했을때 값이 변하는 변수 선언
    public bool mBlacksmithTalk = false;
    // 마녀랑 말했을때 값이 변하는 변수 선언
    public bool mWitchTalk = false;

  


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // [던전] 인벤토리 Lock 나타내기
    public void SetInventoryLock(bool value)
    {
        mIsInventoryLock = value;
    }

    // [던전] 보스  hp on off
    public void SetBossHpVisible(bool value)
    {
        mIsBossHpVisible = value;
    }

    // [던전] 보스 현재 hp 설정 함수
    public void BossCurrentHp(float value)
    {
        mBossCurrentHp = value;
        mIsHpChange = true;

    }
    // [던전] 보스 maxHp 설정 함수
    public void BossMaxHp(float value)
    {
        mBossMaxHp = value;
        mBossCurrentHp = mBossMaxHp;
    }


    // [던전] 던전인지 확인 요청 함수
    public void DungeonCheck(bool value)
    {
        mIsDungeonCheck = value;
    }

    // [플레이어] 플레이어가 죽었는지 확인 요청
    public void PlayerDie(bool value)
    {
        mIsPlayerDie = value;
    }

    // [플레이어] 팬던트를 사용하였을때 애니메이션 실행 요청 함수
    public void PlayerUsePendant(bool value)
    {
        mIsPlayerUseAnimation = value;
    }

    // [Ui] 플레이어 특정(탈출)애니메이션이 끝났을 지 확인 요청
    public void PlayerFinishAnimation(bool value)
    {
        mIsPlayerFinishAnimation = value;
    }

 
    // [마을]

    // 테이블 아이템을 열었을 시 값이 확인 선언
    public void GetItemTableOpen(bool value)
    {
        mItemTableOpen = value;
    }

    // 마을 게시판를 열었을 시 값 확인 함수
    public void GetVillageNoticeBoardOpen(bool value)
    {
        mVillageNoticeBoardOpen = value;
    }

    // 대장장이와 이야기 했는지 확인하는 함수
    public void GetBlacksmithTalk(bool value)
    {
        mBlacksmithTalk = value;
    }

    // 마녀와 이야기를 했는지 확인하는 함수
    public void GetWitchTalk(bool value)
    {
        mWitchTalk = value;
    }










}
