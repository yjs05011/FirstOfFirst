using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GSingleton<PlayerManager>
{
    //플레이어 스텟 스크립트 변수
    public PlayerStat PlayerStat;
    protected override void Init()
    {
        base.Init();
    }
}
