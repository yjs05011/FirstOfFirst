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
    
    public enum EquimentEnumType
    {
        None = 0,
        Weapon,
        Helmet,
        Armor,
        Boots
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
    //아이템 아이디
    public int mItemId;

    //아이템 체력
    public int mItemHp;
    //아이템 공격력
    public int mItemDamage;
    //아이템 방어력
    public int mItemDefense;
    //아이템 이속
    public int mItemMoveSpeed;

    //아이템 가격
    public int mItemPrice;
    //아이템 이미지
    public Sprite mItemSprite;
    

    //저주 아이템 구현시 활성화 시킬 함수
    //아이템 저주 체크
    //public bool mIsCureCheck = false;
    

}
