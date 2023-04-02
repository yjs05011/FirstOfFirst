using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStat : MonoBehaviour
{
    public int id;
    public int type;
    public int count;
    public int MaxCount;

    public virtual void Awake()
    {


    }

    public virtual void SetStat(int _id, int _type, int _count, int _MaxCount)
    {
        id = _id;
        type = _type;
        count = _count;
        MaxCount = _MaxCount;
    }
}
