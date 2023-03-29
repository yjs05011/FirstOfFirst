using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class VillageStatus : MonoBehaviour
{

    public GameObject mBlackSmith;
    public GameObject mBlackSmithNew;
    public GameObject mWitchHouse;
    public GameObject mWitchHouseNew;

    private void Update()
    {
        if(VillageManager.Instance.IsBlackSmithBuild)
        {
            mBlackSmithNew.SetActive(true);
        }
        else
        {
            mBlackSmith.SetActive(true);
        }
        if (VillageManager.Instance.IsWitchHouseBuild)
        {
            mWitchHouseNew.SetActive(true);
        }
        else
        {
            mWitchHouse.SetActive(true);
        }
        if(VillageManager.Instance.IsWillHouseUpgrade)
        {
            VillageManager.Instance.WillHouse = "ShopLv2";
        }
    }

    //public void BuildBlackSmith()
    //{
    //    mBlackSmith.SetActive(false);
    //    VillageManager.Instance.IsBlackSmithBuild= true;
    //    mBlackSmithNew.SetActive(true);
    //}
    //public void BuildWitchHouse()
    //{
    //    mWitchHouse.SetActive(false);
    //    VillageManager.Instance.IsWitchHouseBuild= true;
    //    mWitchHouseNew.SetActive(true);
    //}
    //public void UpgradeWillHouse()
    //{
    //    VillageManager.Instance.WillHouse = "ShopLv2";
    //}
}
