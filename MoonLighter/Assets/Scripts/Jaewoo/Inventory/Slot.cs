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
        
        // FOR DEBUG:
        // InventoryManager.Instance.spRendererInitializedTime = System.DateTime.Now;
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
                mItemSprite = mInventoryItemVaule.mItemSprite;
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
        mItem = item;
        mItemCount = itemCount;
        mItemSprite = mItem.mItemSprite;
        slotRender.sprite = mItemSprite;
        // Debug.Log(mItem.mItemType);
        // Debug.Log(mItem.mItemType != Item.ItemEnumType.Equiment);
        //아이템 타입 구분
        //아이템 타입이 장비가 아닐때
        if (mItem.mItemType != Item.ItemEnumType.Equiment )
        {
            //mChangeImage.FindChildObj("ItemImage").transform.GetComponent<SpriteRenderer>();
            // mChangeImage.SetActive(true);
            mTextCount.text = mItemCount.ToString();
            if(mItemCount == 0 )
            {

            }
            SetColor(1);
        }
        //아이템 타입이 장비일때
        else 
        {
            mTextCount.text = " ";
            //mTextCount.gameObject.SetActive(true);
            SetColor(1);
            // mChangeImage.SetActive(false);
        }
        //SetColor(1);
        
    }

    //슬롯에 대한 아이템 갯수 업데이트
    public void SetSlotCount(int slotItemCount)
    {        
        Debug.Log(mItem);
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
        Debug.Log($"Clear slot name: {gameObject.name}, Parent name: {transform.parent.parent.parent.parent.name}");
        }
        mItem = null;
        mItemCount = 0;
        mItemSprite = null;
        SetColor(0);

        mTextCount.text = "";
        //mChangeImage.SetActive(false);
    }
}
