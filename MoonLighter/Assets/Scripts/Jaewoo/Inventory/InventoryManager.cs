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

   

   //1차원 배열로 아이템 자리값
   private Transform[] mItemValue = new Transform[20];
   //2차원배열
   private GameObject[,] mSlotArray = new GameObject[5,5];

   





   // private Slot[] mSlot;
   // [SerializeField]
   // //슬롯 5x5 배열 
   // private GameObject[] mSlotArray = new GameObject[20];   
   // [SerializeField]
   // //슬롯 아이템 값 4x5배열
   // private GameObject[,] mSlotItemArray = new GameObject[4,5];
   // [SerializeField]
   // //자리 바꿀시 값 저장 배열
   // private GameObject[] mSlotValueArray = new GameObject[2];
   // [SerializeField]
   // //배열 자리
   // private int[,] mInventoryArray = new int[4,5];
   // private Transform[] mArray = new Transform[20];
   
   // void Start()
   // {  
   //    for(int index = 0; index < mSlotArray.Length; index ++)
   //    {
   //       mArray[index] = mSlotArray[index];
   //    }
   //    //배열 초기화 및 선언
   //    for(int indexArrayY = 0; indexArrayY < 4; indexArrayY ++)
   //    {
   //       for(int indexArrayX = 0; indexArrayX < 5; indexArrayX ++)
   //       {
   //           mSlotArray[indexArrayY, indexArrayX] =
   //       }
   //    }      
   // }

   // public void InventorySlotArray()
   // {      
   //    for(int indexY = 0; indexY < mSlotArray.Length ; indexY ++)
   //    {
   //       for(int indexX = 0; indexX < mSlotArray.Length; indexX ++)
   //       {
   //         // mSlotArray[indexY,indexX]
   //       }
   //    }
   // }

   

 


}
