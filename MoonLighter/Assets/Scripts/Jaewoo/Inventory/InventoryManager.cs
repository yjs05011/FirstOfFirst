using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   //5x5 정수배열을 만듬
   //정수 배열 위치마다 slot프리팹의 위치를 순서대로 받게 해줌.
   //정수 배열 위치마다 slot값을 받게 해줌.
   //
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
   //배열 자리
   private int[,] mInventoryArray = new int[5,5];

   // void Start()
   // {
   //    for(int indexArrayY = 0; indexArrayY < mInventoryArray.Length; )
   //    mInventoryArray
   // }
   public void InventorySlotArray()
   {      
      for(int indexY = 0; indexY < mSlotArray.Length ; indexY ++)
      {
         for(int indexX = 0; indexX < mSlotArray.Length; indexX ++)
         {
            //mSlotArray[indexY,indexX]
         }
      }
   }

   

 


}
