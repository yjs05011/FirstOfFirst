using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPositionArray : MonoBehaviour
{
    public static Transform[] mSlotArrays;    

    //InvetroySlot 의 하위 들을 배열에 차례대로 담음.
    public void SlotPosition()
    {
        mSlotArrays = new Transform[transform.childCount];
        for (int index = 0; index < mSlotArrays.Length; index++)
        {
            mSlotArrays[index] = transform.GetChild(index);
            //mSlotArrays[index].transform.position = transform.GetChild(index).position;
        }
    }

}
