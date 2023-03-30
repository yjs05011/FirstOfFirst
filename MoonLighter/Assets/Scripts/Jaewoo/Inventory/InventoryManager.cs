using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : GSingleton<InventoryManager>
{   
    public Inventory mInventory;
    public int mBagCount = 0;
    //인벤토리아이템 GameObject에 저장
    public Slot[,] mInventorySlots = new Slot[4,5];
    //장비창아이템 GameObject에 저장
    
    public Slot[,] mEquipmentSlots = new Slot[4,2];  
   

}
