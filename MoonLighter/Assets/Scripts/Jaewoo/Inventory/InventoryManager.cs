using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   //5x5 정수배열을 만듬
   //정수 배열 위치(0,0)~(4,4마)다 slot프리팹의 위치를 순서대로 넣어줌.
   //정수 배열 위치마다 [slot의 데이터 값]을 받게 해줌

//아이템으로 태그된걸 먹었어요.
//질문이
//콜라이더 태그로 아이템이란걸 먹었어요
//이 아이템은 무슨아이템인지 판단을
   //인벤토리 매니저로 드랍된 아이템 콜라이더 충돌로 확인. 
   //tag= = "아이템"

   
   //확인된 아이템의 이름을 판단해서 인벤토리에서 인스턴스?
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

   
   void Start()
   {
      //배열 초기화 및 선언
      for(int indexArrayY = 0; indexArrayY < mInventoryArray.Length; indexArrayY ++)
      {
         for(int indexArrayX = 0; indexArrayX < mInventoryArray.Length; indexArrayX ++)
         {
            //mInventoryArray[indexArrayY,indexArrayX] = 
         }

      }
      
   }
   public void InventorySlotArray()
   {      
      for(int indexY = 0; indexY < mSlotArray.Length ; indexY ++)
      {
         for(int indexX = 0; indexX < mSlotArray.Length; indexX ++)
         {
           // mSlotArray[indexY,indexX]
         }
      }
   }

   

 


}
