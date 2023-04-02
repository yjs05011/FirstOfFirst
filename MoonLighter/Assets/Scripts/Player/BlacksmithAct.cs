using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithAct : VillageUpgrade
{
    public override void Buy(float money)
    {
        if (VillageManager.Instance.IsBlackSmithBuild)
        {

        }
        else
        {
            PlayerManager.Instance.mPlayerStat.Money -= money;
            VillageManager.Instance.IsBlackSmithBuild = true;
            LoadingManager.LoadScene("VillageScene");
        }

    }
}
