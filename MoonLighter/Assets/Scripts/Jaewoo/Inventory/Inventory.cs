using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //인벤토리 활성화 확인. true면 인벤토리가 열림.이동 막음
    public static bool mIsInventoryActiveCheck = false;
    //마을인지 확인. false면 돋보기 칸 비활성화, true면 활성화
    public static bool mIsVillageCheck = false;

    [SerializeField]
    //인벤토리 base 이미지
    private GameObject mChangeInventoryBase;
    [SerializeField]
    private GameObject mChangeSlotGrideSetting;
    [SerializeField]
    private GameObject mChnageEquipmentSlotGrideSetting;

    private Slot[] mSlot;
    private Slot[] mSlotEquip;
   
    void Start()
    {
        mSlot = mChangeSlotGrideSetting.GetComponentsInChildren<Slot>();
        mSlotEquip = mChnageEquipmentSlotGrideSetting.GetComponentsInChildren<Slot>();
    }

    
    void Update()
    {
        TryOpenInventory();   
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            
            mIsInventoryActiveCheck = !mIsInventoryActiveCheck;
            if(mIsInventoryActiveCheck)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    //GameObject 인벤토리 베이스 활성화 
    private void OpenInventory()
    {
        mChangeInventoryBase.SetActive(true);
    }
    //GameObject 인벤토리 베이스 비활성화 
    private void CloseInventory()
    {
        mChangeInventoryBase.SetActive(false);
    }

    public void AcpuireItem(Item item, int itemCount = 1)
    {
        //얻은 아이템이 장비 타입과 다를때
        if(Item.ItemEnumType.Equiment != item.mItemType)
        {
            for(int index = 0; index < mSlot.Length; index ++)
            {
                //슬롯이 null이면 런타임 오류
                if(mSlot[index].mItem != null)
                {
                    //슬롯[index]의 이름과 아이템의 이름이 같으면
                    if(mSlot[index].mItem.mItemName == item.mItemName)
                    {   
                        //슬롯 개수 올림
                        mSlot[index].SetSlotCount(itemCount);
                        return;
                    }
                }
            }
        }

        for(int index = 0; index < mSlot.Length; index ++)
        {
            if(mSlot[index].mItem ==null)
            {
                mSlot[index].AddItem(item, itemCount);
                return;
            }
        }
    }

    
}
