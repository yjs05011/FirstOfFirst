using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GSingleton<PlayerManager>
{
    //플레이어 스텟 스크립트 변수
    [SerializeField]
    public PlayerStat mPlayerStat = default;
    //현재 플레이어가 Ui를 켰는지 확인하는 함수
    public bool mIsUiActive = default;
    public bool mIsMoneyChange = default;
    public Vector2 mPlayerBeforPos = default;
    public bool mIsPlayerHpChange = default;
    // 현재 플레이어의 위치가 상점인지 체크하는 변수
    public bool mIsShop = default;
    protected override void Init()
    {
        base.Init();
        mPlayerStat = new PlayerStat();
    }
}
