using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAct : VillageUpgrade
{
    // Start is called before the first frame update
    public override void Buy(float money)
    {
        base.Buy(money);
        if (VillageManager.Instance.IsWitchHouseBuild)
        {

        }
        else
        {
            PlayerManager.Instance.mPlayerStat.Money -= money;
            VillageManager.Instance.IsWitchHouseBuild = false;
            Debug.Log(VillageManager.Instance.IsWitchHouseBuild);
        }
    }
}
