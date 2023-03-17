using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   private Slot[] mSlot;
   [SerializeField]
   //슬롯 5x5 배열 
   private GameObject[,] mSlotArray = new GameObject[5,5];

   //[SerializeField]
   ////오브젝트 local position 배열값저장
  // private GameObject[,] m

   [SerializeField]
   //슬롯 아이템 값 5x5배열
   private GameObject[,] mSlotItemArray = new GameObject[5,5];

   [SerializeField]
   //자리 바꿀시 값 저장 배열
   private GameObject[] mSlotValueArray = new GameObject[2];

   [SerializeField]
   //2차원배열로 구분되는 위치
   private int[,] mInventoryArray = new int[5,5];

   public void InventorySlotArray()
   {      
      for(int indexY = 0; indexY < mSlotArray.Length ; indexY ++)
      {
         for(int indexX = 0; indexX < mSlotArray.Length; indexX ++)
         {
            mSlotArray[indexY,indexX]= default;
         }
      }
   }

   

 


}
