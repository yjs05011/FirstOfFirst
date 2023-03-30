using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrade : VillageUpgrade
{
    public override void Buy(float money)
    {
        base.Buy(money);
        if (VillageManager.Instance.IsWillHouseUpgrade)
        {

        }
        else
        {
            PlayerManager.Instance.mPlayerStat.Money -= money;
            VillageManager.Instance.IsWillHouseUpgrade = true;
            Debug.Log(VillageManager.Instance.IsWillHouseUpgrade);
        }
    }
}
