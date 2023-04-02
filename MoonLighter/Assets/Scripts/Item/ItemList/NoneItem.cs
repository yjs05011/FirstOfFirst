using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneItem : ItemStat
{
    int itemId = 0;
    int itemType = 0;
    int itemCount = 0;
    int itemMaxCount = 1;
    public override void Awake()
    {
        base.Awake();
        base.SetStat(itemId, itemType, itemType, itemMaxCount);
    }


}
