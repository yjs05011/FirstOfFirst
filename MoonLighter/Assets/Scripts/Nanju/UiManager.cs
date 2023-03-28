using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : GSingleton<UiManager>
{
    // MainUi가 켰졌는지 확인하기
    // 던전에서 인벤토리 Ui 켰는지 확인하는 변수
    // QuickInventory 켰는지 확인하기

    //플레이어 스텟 스크립트 변수
    [SerializeField]
    public bool mQuickInventory = default;
    protected override void Init()
    {
        base.Init();
    }

}
