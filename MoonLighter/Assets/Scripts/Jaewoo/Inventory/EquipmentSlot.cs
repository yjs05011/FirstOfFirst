using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlot : MonoBehaviour
{    
    public int mEquipmentItemCount;
    public Item mEquipmentItem;
    public Sprite mEquipmentItemSprite;
    public SpriteRenderer mEquipmentSlotRender = default;
    
    

    [SerializeField]
    private TMP_Text mTextCount = default;

void Awake()
{
   
}

   private void Start()
    {
        
        mTextCount = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
        mEquipmentSlotRender = gameObject.FindChildObj("ItemImage").GetComponent<SpriteRenderer>();          
    }    

    
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
       
        if (mEquipmentItem.mItemType != Item.ItemEnumType.Equiment )
        {    
            mTextCount.text = mEquipmentItemCount.ToString();
            
            if(mEquipmentItemCount == 0 )
            {
                ClearSlot();
            }
            SetColor(1);
        }
        
        {            
            mTextCount.text = " ";
            SetColor(1);
           
        }
        
    }

    
    public void SetSlotCount(int slotItemCount)
    {
        mEquipmentItemCount += slotItemCount;
        mTextCount.text = mEquipmentItemCount.ToString();

        if (mEquipmentItemCount <= 0)
        {
            ClearSlot();
        }
    }

    
    private void ClearSlot()
    {
        mEquipmentItem = null;
        mEquipmentItemCount = 0;
        mEquipmentItemSprite = null;
        SetColor(0);

        mTextCount.text = "";
      
    }

    
   
}
