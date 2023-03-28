using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : GSingleton<InventoryManager>
{   
    //2차원 배열판
    public GameObject[] mInventoryArray;
    
    //인벤토리아이템 List에 저장
    public List<GameObject> mSlots = new List<GameObject>();
    //장비창아이템 List에 저장
    public List<GameObject> mEquipmentSlots = new List<GameObject>();  
 

   
   
   //서왑 temp 말고 C#의 기능이용
   //("선택한 아이템", "바꿔야할 아이템") = ("바꿔야할 아이템", "선택한 아이템");
   public void Swap()
   {

   }

}
