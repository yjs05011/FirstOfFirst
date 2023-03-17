using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPositionArray : MonoBehaviour
{
    public static Transform[] mSlotArrays;
    private void Awake()
    {
        mSlotArrays = new Transform[transform.childCount];
        for(int index = 0; index < mSlotArrays.Length ; index ++ )
        {
            mSlotArrays[index] = transform.GetChild(index);
        }
    }
   
}
