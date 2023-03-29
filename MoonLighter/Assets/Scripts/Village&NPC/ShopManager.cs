using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopManager : GSingleton<ShopManager>
{
    public List<int> mTableNumber;
    public List<GameObject> mItemTables;
    public List<GameObject> mShopNPC;
    public List<GameObject> mWaitShopNPC;
    public int[] mItemPrice;
    public Sprite[] mItems;
    public int[] mItemsNumber;
    protected override void Init()
    {
        base.Init();
        mTableNumber = new List<int>();
        mShopNPC = new List<GameObject>();
        mItems = new Sprite[8];
        mItemPrice= new int[8];
        mItemsNumber = new int[8];
    }
}
