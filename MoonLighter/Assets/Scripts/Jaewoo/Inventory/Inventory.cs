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


    public Slot[] mSlot;
    public Slot[] mSlotEquip;

    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(true);

    }
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        mSlot = mChangeSlotGrideSetting.GetComponentsInChildren<Slot>();
        mSlotEquip = mChnageEquipmentSlotGrideSetting.GetComponentsInChildren<Slot>();
    }


    void Update()
    {
        TryOpenInventory();
    }

    //인벤토리 켰을때
    private void TryOpenInventory()
    {
        //KeyManager를 통해 키보드 입력(매니저에서 받는)
        //if(Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))


        //화요일 머지시 지워야함(i눌러서 인벤토리 나오는 부분)
        if (Input.GetKeyDown(KeyCode.I))
        {

            mIsInventoryActiveCheck = !mIsInventoryActiveCheck;
            if (mIsInventoryActiveCheck)
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

    public void AcpuireItem(Item item, int itemCount)
    {
        //Debug.Log(Item.ItemE)
        //얻은 아이템이 장비 타입과 다를때
        if (Item.ItemEnumType.Equiment != item.mItemType )
        {
            for (int index = 0; index < mSlot.Length; index++)
            {
                if (mSlot[index].mItem != null)
                {
                    //슬롯[index]의 이름과 아이템의 이름이 같으면
                    if (mSlot[index].mItem.mItemName == item.mItemName)
                    {
                        //슬롯 개수 올림
                        //mSlot[index].SetSlotCount(itemCount += itemCount);
                        mSlot[index].SetSlotCount(itemCount++);
                        return;
                    }
                }
            }
        }

        for (int index = 0; index < mSlot.Length; index++)
        {
            if (mSlot[index].mItem == null)
            {
                mSlot[index].AddItem(item, itemCount);
                return;
            }
        }
    }


}
