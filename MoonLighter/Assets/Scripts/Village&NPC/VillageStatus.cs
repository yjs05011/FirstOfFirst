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

    private void Start()
    {
        if(VillageManager.Instance.IsBlackSmithBuild)
        {
            //mBlackSmith.SetActive(false);
            mBlackSmithNew.SetActive(true);
        }
        else
        {
            mBlackSmith.SetActive(true);
        }
        if (VillageManager.Instance.IsWitchHouseBuild)
        {
            //mWitchHouse.SetActive(false);
            mWitchHouseNew.SetActive(true);
        }
        else
        {
            mWitchHouse.SetActive(true);
        }
        if(VillageManager.Instance.IsWillHouseUpgrade)
        {
            ShopManager.Instance.mBedPosition = new Vector3(3.4f, 4.7f, 0);
            VillageManager.Instance.WillHouse = "ShopLv2";
        }   
    }
}
