using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 오브젝트에 안붙이고 사용하기위해
[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    //아이템 유형
    public enum ItemEnumType
    {
        //None 기준
        None = 0,
        //장비
        Equiment ,
        //포션
        Potion,
        //재료
        Ingredient
    }

    
    #region 저주 아이템 구현시 활성화 시킬 함수
    // public enum ItemCurseEnum
    // {
    //     Broken = 0,

    // }
    #endregion


    //아이템 타입
    public ItemEnumType mItemType;    
    //아이템 이름
    public string mItemName;

    //아이템 체력
    public int mItemHp {get;set;}
    //아이템 공격력
    public int mItemDamage{get;set;}
    //아이템 방어력
    public int mItemDefense{get;set;}
    //아이템 이속
    public int mItemMoveSpeed{get;set;}

    //아이템 가격
    public int mItemPrice{get;set;}
    //아이템 이미지
    public Sprite mItemSprite;
    //아이템 프리팹
    public GameObject mItemPrefab;

    //저주 아이템 구현시 활성화 시킬 함수
    //아이템 저주 체크
    //public bool mIsCureCheck = false;
    

}
