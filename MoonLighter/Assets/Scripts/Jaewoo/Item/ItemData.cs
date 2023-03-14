using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public int mId => id;
    public int mPrice => price;
    public string mName => name_;
    public string mTooltip => toolTip;
    public Sprite mIcon => icon;

    //아이템 아이디
    [SerializeField] private int id;
    //아이템 가격
    [SerializeField] private int price;
    //아이템 이름
    [SerializeField] private string name_;
    //아이템 설명
    [SerializeField] private string toolTip;
    //아이템 아이콘    
    [SerializeField] private Sprite icon;
    //드랍 아이템 프리팹으로    
    [SerializeField] private GameObject dropItemPrefab;

    public abstract Item CreateItem();
}
