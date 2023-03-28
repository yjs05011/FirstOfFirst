using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : GSingleton<InventoryManager>
{   
    public Inventory mInventory;
    public int mBagCount = 0;
    //2차원 배열판
    public GameObject[] mInventoryArray;
    
    //인벤토리아이템 GameObject에 저장
    public GameObject[,] mSlots = new GameObject[4,5];
    //장비창아이템 GameObject에 저장
    public GameObject[,] mEquipmentSlots = new GameObject[4,2];
 
    public void BagCount()
    {
        
    }

   
   
   //서왑 temp 말고 C#의 기능이용
   //("선택한 아이템", "바꿔야할 아이템") = ("바꿔야할 아이템", "선택한 아이템");
   

}
