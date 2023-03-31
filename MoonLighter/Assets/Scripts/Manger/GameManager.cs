using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GSingleton<GameManager>
{
    [SerializeField]
    public bool mIsShop = default;
    [SerializeField]
    public float mPlayerHp = default;
    [SerializeField]
    public float mPlayerSpeed = default;
    [SerializeField]
    public float mPlayerStr = default;
    [SerializeField]
    public float mPlayerDef = default;
    [SerializeField]
    public float mPlayerMoney = default;
    [SerializeField]
    public float mPlayerMaxHp = default;
    [SerializeField]
    public bool mIsNight = default;
    [SerializeField]
    public List<int> mTableNumber = default;
    [SerializeField]
    public int[] mItemPrice = default;
    [SerializeField]
    public int[] mItemsNumber = default;
    [SerializeField]
    public Vector3 mBedPosition = default;
    [SerializeField]
    public bool mIsBlackSmithBuild = default;
    [SerializeField]
    public bool mIsWitchHouseBuild = default;
    //[SerializeField]
    //public Slot[,] mInventorySlots = default;
    //[SerializeField]
    //public Slot[,] mEquipmentSlots = default;
    //// 인벤토리, 플레이어가 끼고 있는 아이템
    //// 상점의 아이템(그냥 상점매니저에 저장하고 있는거 (NPC 빼고) 모두 다)
    //// 대장간, 마녀
    // 아이템 정보
    // 창고
    protected override void Init()
    {
        base.Init();
    }


}

