using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCount : ItemData
{
   public int mMaxTenAmount => maxTenAmount;
   public int mMaxFiveAmount => maxFiveAmount;
   //최대수량 10인 아이템의 데이터
   [SerializeField] private int maxTenAmount = 10;
   //최대수량 5인 아이템의 데이터
   [SerializeField] private int maxFiveAmount = 5;

}
