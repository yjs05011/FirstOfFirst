using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : GSingleton<InventoryManager>
{   
    public Inventory mInventory;
    public int mBagCount = 0;
    public bool mIsManagerAddCheck = false;
    public bool mIsEquipmentCheck = false;

    public Dictionary<int,Slot> mDataInventory = new Dictionary<int, Slot>();
    //인벤토리아이템 Slot 저장
    //public Slot[,] mInventorySlots = new Slot[4,5];
    public Slot[,] mInventorySlots = new Slot[4,5];

    //장비창아이템 Slot 저장    
    public Slot[,] mEquipmentSlots = new Slot[4,2];  

    //체스트 창 인벤토리
    public Slot[,] mChestInventorySlots = new Slot[4,5];


    //상자 Slot 저장
    public Slot[,] mChestSlots = new Slot[4,7];

   // public Slot[,] mInventoryChestSlot = new Slot[4,5];
   

}
