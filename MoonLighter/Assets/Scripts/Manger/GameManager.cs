using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GSingleton<GameManager>
{
    [SerializeField]
    public bool mIsShop = default;
    [SerializeField]
    public float mPlayerHp = default;
    [SerializeField]
    public float mPlayerSpeed = default;
    [SerializeField]
    public float mPlayerStr = default;
    [SerializeField]
    public float mPlayerDef = default;
    [SerializeField]
    public float mPlayerMoney = default;
    [SerializeField]
    public float mPlayerMaxHp = default;
    // Start is called before the first frame update
    public bool mIsNight = default;
    protected override void Init()
    {
        base.Init();
    }


}

