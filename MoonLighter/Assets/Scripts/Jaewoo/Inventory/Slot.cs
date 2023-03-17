using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    
    public Item mItem;
    public int mItemCount;
    public Sprite mItemSprite;

    [SerializeField]
    private Text mTextCount;
    [SerializeField]
    private GameObject mChangeImage;
    
    //알파값 조절
    private void SetColor(float alpha)
    {    
        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, alpha);
    }

    
    public void AddItem(Item item, int itemCount =1)
    {
        mItem = item;
        mItemCount = itemCount;
        mItemSprite = mItem.mItemSprite;

        //아이템 enum 타입이 소비일때
        if(mItem.mItemType != Item.ItemEnumType.Equiment)
        {
            mChangeImage.SetActive(true);
            mTextCount.text = mItemCount.ToString();
        }
        else
        {
            mTextCount.text = " ";
            mChangeImage.SetActive(false);
        }
        SetColor(1);
    }

    //슬롯에 대한 아이템 갯수 업데이트
    public void SetSlotCount(int slotItemCount)
    {
        mItemCount += slotItemCount;
        mTextCount.text = mItemCount.ToString();

        if(mItemCount <= 0)
        {
            ClearSlot();
        }
    }

    //해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        mItem = null;
        mItemCount = 0;
        mItemSprite = null;
        SetColor(0);

        mTextCount.text = "";
        mChangeImage.SetActive(false);
    }
}
