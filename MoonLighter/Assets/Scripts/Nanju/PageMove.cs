using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageMove : MonoBehaviour
{
    public GameObject mTownbardStaff;
    public GameObject mShopUpgrade;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // TownbardStaff 에서 키보드를 누르면 ShopUpgrade으로 넘어가기
        if (Input.GetKeyDown(KeyCode.E))
        {
            mTownbardStaff.SetActive(false);
            mShopUpgrade.SetActive(true);
        }
        // ShopUpgrade 에서 키보드를 누르면 TownbardStaff 으로 넘어가기
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mTownbardStaff.SetActive(true);
            mShopUpgrade.SetActive(false);
        }

    }
}
