using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GSingleton<GameKeyManger>
{
    public bool mIsShop = default;
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
    }



}
