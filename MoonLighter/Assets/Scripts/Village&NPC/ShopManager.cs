using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopManager : GSingleton<ShopManager>
{
    public List<int> mTableNumber;
    public List<GameObject> mItemTables;
    public List<GameObject> mShopNPC;
    public Sprite[] mItems;

    protected override void Init()
    {
        base.Init();
        mTableNumber = new List<int>();
        mShopNPC = new List<GameObject>();
        mItems = new Sprite[8];
    }
}
