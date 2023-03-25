using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlot : MonoBehaviour
{
    public bool mIsWeapon;
    private int mEquipmentItemCount;
    public Item mEquipmentItem;
    public Sprite mEquipmentItemSprite;
    public SpriteRenderer mEquipmentSlotRender = default;
    public EquipmentTypeController mEquipmentSlotParent;

    [SerializeField]
    private TMP_Text mTextCount = default;

void Awake()
{
    mEquipmentSlotParent = transform.parent.GetComponent<EquipmentTypeController>();
}

   private void Start()
    {
        // for(int index = 0; index < gameObject.transform.parent.transform.childCount; index ++)
        // {
        //     gameObject.transform.parent.transform.GetChild(index);
        //     if( gameObject.transform.parent.transform.GetChild(0)|| gameObject.transform.parent.transform.GetChild(1))
        //     {               
        //         mEquipmentItem.mItemType = Item.ItemEnumType.Equiment;

        //     }
        //     if(gameObject.transform.parent.transform.GetChild(2)||gameObject.transform.parent.transform.GetChild(3)||gameObject.transform.parent.transform.GetChild(4))

        // }{}
       if(mEquipmentSlotParent.transform.GetChild(0) || mEquipmentSlotParent.transform.GetChild(1))
       {}
        
        
        mTextCount = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
        mEquipmentSlotRender = gameObject.FindChildObj("ItemImage").GetComponent<SpriteRenderer>();  
        
    }
    

    //아이템 값 저장
    public Item mInventoryItemVaule
    {
        get { return mInventoryItemVaule; }
        set
        {
            mInventoryItemVaule = value;
            if (mInventoryItemVaule != null)
            {
                mEquipmentItemSprite = mInventoryItemVaule.mItemSprite;
                SetColor(1);
            }
            else
            {
                SetColor(0);
            }
        }
    }

    //인벤토리에 들어올 스프라이트의 알파값 조절
    private void SetColor(float alpha)
    {
        transform.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
    }

     public void AddItem(Item item, int itemCount)
    {
        mEquipmentItem = item;
        mEquipmentItemCount = itemCount;
        mEquipmentItemSprite = mEquipmentItem.mItemSprite;

        mEquipmentSlotRender.sprite = item.mItemSprite;
        // Debug.Log(mItem.mItemType);
        // Debug.Log(mItem.mItemType != Item.ItemEnumType.Equiment);
        //아이템 타입 구분
       // if(gameObject.transform.parent.transform.GetChild(0) == Item.ItemEnumType.Equiment)
        if (mEquipmentItem.mItemType != Item.ItemEnumType.Equiment)
        {    
            mTextCount.text = mEquipmentItemCount.ToString();
            if(mEquipmentItemCount == 0 )
            {

            }
            SetColor(1);
        }
        else
        {            
            mTextCount.text = " ";
            SetColor(1);
            // mChangeImage.SetActive(false);
        }
        //SetColor(1);
    }

    //슬롯에 대한 아이템 갯수 업데이트
    public void SetSlotCount(int slotItemCount)
    {
        mEquipmentItemCount += slotItemCount;
        mTextCount.text = mEquipmentItemCount.ToString();

        if (mEquipmentItemCount <= 0)
        {
            ClearSlot();
        }
    }

    //해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        mEquipmentItem = null;
        mEquipmentItemCount = 0;
        mEquipmentItemSprite = null;
        SetColor(0);

        mTextCount.text = "";
        //mChangeImage.SetActive(false);
    }

    
   
}
