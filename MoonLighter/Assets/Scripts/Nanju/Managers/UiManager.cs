using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    public static UiManager Instance;
    public bool mIsInventoryLock = false;
    // 보스 hp 표시
    public bool mIsBossHpVisible = false;

    // [던전] 보스 HP
    public float mBossMaxHp;
    public float mBossCurrentHp;

    // [던전] scroll(레벨 지도) 언제 켜질지 확인
    //스테이지 1만 하는 건지 물어보기
    // [던전] 레벨 Text change





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

    // [던전] 인벤토리 Lock 나타내기 유무
    public void SetInventoryLock(bool value)
    {
        mIsInventoryLock = value;
    }













    // MainUi가 켰졌는지 확인하기
    // 던전에서 인벤토리 Ui 켰는지 확인하는 변수
    // QuickInventory 켰는지 확인하기

    //플레이어 스텟 스크립트 변수
    // [SerializeField]
    // public bool mQuickInventory = default;
    // protected override void Init()
    // {
    //     base.Init();
    // }

}
