using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    public static bool mIsShopStart = false;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        mIsShopStart = false;
        ShopManager.Instance.mShopNPC.Clear();
        ShopManager.Instance.mItemTables = new List<GameObject>(GameObject.FindGameObjectsWithTag("ItemTable"));
        for (int index = 0; index < ShopManager.Instance.mItemTables.Count; index++)
        {
            ShopManager.Instance.mItemTables[index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ShopManager.Instance.mItems[index];
        }
    }
    
    // Update is called once per frame
    public virtual void Update()
    {
        if (mIsShopStart)
        {
            Invoke("NPCGetItem", 3);
        }
        Debug.Log(ShopManager.Instance.mItemTables.Count);
    }
    public void SetOnItem(int tableNumber, Sprite item)
    {
        ShopManager.Instance.mItemTables[tableNumber].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item;
        ShopManager.Instance.mItems[tableNumber] = item;
        ShopManager.Instance.mTableNumber.Add(tableNumber);
    }
    public void SetOutItem(int tableNumber)
    {
        ShopManager.Instance.mItemTables[tableNumber].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        ShopManager.Instance.mItems[tableNumber] = null;
        ShopManager.Instance.mTableNumber.Remove(tableNumber);
    }

    public void NPCGetItem()
    {
        CancelInvoke();
        if (ShopManager.Instance.mShopNPC.Count > 0)
        {

            if (ShopManager.Instance.mTableNumber.Count > 0)
            {
                int randomShopNPC = Random.Range(0, ShopManager.Instance.mShopNPC.Count);
                int randomTable = Random.Range(0, ShopManager.Instance.mTableNumber.Count);

                ShopManager.Instance.mShopNPC[randomShopNPC].GetComponent<ShopNPC>().mTablePosition = ShopManager.Instance.mItemTables[ShopManager.Instance.mTableNumber[randomTable]].transform.position;
                ShopManager.Instance.mShopNPC[randomShopNPC].GetComponent<ShopNPC>().mItem = ShopManager.Instance.mItems[ShopManager.Instance.mTableNumber[randomTable]];
                ShopManager.Instance.mShopNPC[randomShopNPC].GetComponent<ShopNPC>().mTableNumber = ShopManager.Instance.mTableNumber[randomTable];

                ShopManager.Instance.mShopNPC.RemoveAt(randomShopNPC);
                ShopManager.Instance.mItems[ShopManager.Instance.mTableNumber[randomTable]] = null;
                ShopManager.Instance.mTableNumber.RemoveAt(randomTable);
            }
        }
    }
}
