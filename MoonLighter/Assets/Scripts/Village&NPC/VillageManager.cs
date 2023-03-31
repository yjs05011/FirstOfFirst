using UnityEngine;

public class VillageManager : GSingleton<VillageManager>
{
    
    public bool IsBlackSmithBuild;      // 저장
    public bool IsWitchHouseBuild;      // 저장
    public bool IsWillHouseUpgrade;     // 저장
    public string WillHouse;
    protected override void Init()
    {
        base.Init();
        IsBlackSmithBuild = false;
        IsWitchHouseBuild = false;
        IsWillHouseUpgrade = false;
        WillHouse = "ShopLv1";

        //mBlackSmith = transform.Find("BlackSmithHouse_old").gameObject;
        //mWitchHouse = transform.Find("WitchHouse_old").gameObject;
    }

}