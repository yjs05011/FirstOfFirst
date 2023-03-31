using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopManager : GSingleton<ShopManager>
{
    public int mTablesNumber;
    public List<int> mTableNumber; // 저장
    public List<GameObject> mItemTables;
    public List<GameObject> mShopNPC;
    public List<GameObject> mWaitShopNPC;
    public int[] mItemPrice;    // 저장
    public int[] mItemID;
    public Sprite[] mItems;     //저장(할수 있을것 같나)
    public int[] mItemsNumber;  // 저장
    public  Vector3 mBedPosition;       // 저장
    protected override void Init()
    {
        base.Init();
        mTablesNumber = 0;
        mTableNumber = new List<int>();
        mShopNPC = new List<GameObject>();
        mItems = new Sprite[8];
        mItemPrice= new int[8];
        mItemsNumber = new int[8];
        mBedPosition = new Vector3(3,3,0);
    }
}
