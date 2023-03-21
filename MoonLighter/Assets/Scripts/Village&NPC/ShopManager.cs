using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopManager : GSingleton<ShopManager>
{
    public List<GameObject> mItemTables;
    private List<int> mTableNumber;
    private Sprite[] mItems;
    
    public static List<GameObject> mShopNPC = new List<GameObject>();
    public bool mIsShopStart = false;

    public override void Update()
    {
        if(mIsShopStart)
        {
            if(mShopNPC.Count > 0)
            {
                if(mTableNumber.Count > 0)
                {

                }
            }
        }
        
    }

    protected override void Init()
    {
        base.Init();
        
        mItemTables = new List<GameObject>(GameObject.FindGameObjectsWithTag("ItemTable"));
        mItems = new Sprite[mItemTables.Count];
    }

    public void SetOnItem(int tableNumber,Sprite item)
    {
        mItemTables[tableNumber].GetComponentInChildren<SpriteRenderer>().sprite = item;
        mItems[tableNumber] = item;
        mTableNumber.Add(tableNumber);
    }
    public void SetOutItem(int tableNumber)
    {
        mItemTables[tableNumber].GetComponentInChildren<SpriteRenderer>().sprite = null;
        mItems[tableNumber] = null;
        mTableNumber.Remove(tableNumber);
    }
    
}
