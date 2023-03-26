using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : GSingleton<ItemManager>
{
    
    public GameObject[] mItemPrefab = new GameObject[6];
    public int mDropCount = 3;
    
    void Start()
    {
        
    }

    //ItemManager.DropItem(아이템 떨굴 오브젝트, 생성할 아이템 넘버, 위치)
    public void DropItem()
    {
        int itemRandom = Random.Range(0, mItemPrefab.Length);
        GameObject dropItem = mItemPrefab[itemRandom];
        GameObject item = Instantiate(dropItem);
        
        
        //item.transform.position = itemPosition;
       
    }

    public struct DropItemPositon
    {
        public int x;
        public int y;

        public DropItemPositon(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 DropPosition()
        {
            return new Vector2(x, y);
        }
    }
}
