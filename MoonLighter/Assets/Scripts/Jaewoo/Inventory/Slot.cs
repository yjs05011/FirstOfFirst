using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Slot : MonoBehaviour
{
    public int mItemCount;
    public Item mItem;

    [Space(2)]
    [Header("Slot Renderer")]
    public Sprite mItemSprite;
    public SpriteRenderer slotRender = default;
    
    [Space(2)]
    [Header(" ")]
    [SerializeField]
    private TMP_Text mTextCount = default;
    [SerializeField]
    private GameObject mChangeImage;
    

    private void Start()
    {                      
        mTextCount = gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        slotRender = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();     
        
       
    }

    public Item mInventoryItemVaule
    {
        get { return mInventoryItemVaule; }
        set
        {
            mInventoryItemVaule = value;
            if (mInventoryItemVaule != null)
            {
                mItemSprite = mInventoryItemVaule.mItemSprite;
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
        mItem = item;
        mItemCount = itemCount;
        mItemSprite = mItem.mItemSprite;
        slotRender.sprite = mItemSprite;
        
        if (mItem.mItemType != Item.ItemEnumType.Equiment )
        {
           
            mTextCount.text = mItemCount.ToString();
            if(mItemCount == 0 )
            {

            }
            SetColor(1);
        }
        
        else 
        {
            mTextCount.text = " ";
            
            SetColor(1);
           
        }
        
        
    }

    
    public void SetSlotCount(int slotItemCount)
    {        
       
        mItemCount += slotItemCount;
        mTextCount.text = mItemCount.ToString();

        if (mItemCount <= 0)
        {
            ClearSlot();
        }
    }

    //해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        if(transform.parent.parent.parent.parent != default || transform.parent.parent.parent.parent != null)
        {
       
        }
        mItem = null;
        mItemCount = 0;
        mItemSprite = null;
        SetColor(0);

        mTextCount.text = "";
       
    }
}
