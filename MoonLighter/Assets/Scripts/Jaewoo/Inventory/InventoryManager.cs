using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : GSingleton<InventoryManager>
{   
    public Inventory mInventory;
    public int mBagCount = 0;
    public bool mIsManagerAddCheck = false;    
    public bool mIsMoveController = false;
    public Dictionary<int,Slot> mDataInventory = new Dictionary<int, Slot>();
    public Slot[,] mInventorySlots = new Slot[4,5];
    public Slot[,] mEquipmentSlots = new Slot[4,2];  
    
    public Slot[,] mChestInventorySlots = new Slot[4,5];
       
    public Slot[,] mChestSlots = new Slot[4,7];
    public Sprite[,] mInventoryFind = new Sprite[4,5];
    public int[,] mInventoryCount = new int[4,5];

    
    public void InventoryFind()
    {
        for(int y=0; y < 4; y ++)
        {
            for(int x =0; x < 5; x++)
            {
                mInventoryFind[y,x] = mInventorySlots[y,x].mItemSprite;
                mInventoryCount[y,x] = mInventorySlots[y,x].mItemCount;
            }
        }      
    }

    public void BagCount()
    {
        mBagCount = 0;
        for(int y = 0; y < 4; y ++)
        {
            for(int x = 0; x <5; x ++)
            {               
                if(mInventorySlots[y,x].mItem !=null)
                {                    
                    mBagCount++;
                }
                
            }
        }
    }
    
}
