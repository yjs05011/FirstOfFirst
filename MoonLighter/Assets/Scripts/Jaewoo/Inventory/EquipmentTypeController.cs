using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentTypeController : MonoBehaviour
{

    public Item mItem;
    public GameObject[] mEquipmentSlot;
    void Awake()
    {
        mEquipmentSlot = new GameObject[transform.childCount];
        for(int index = 0 ; index < mEquipmentSlot.Length; index ++)
        {
            mEquipmentSlot[index] = transform.GetChild(index).gameObject;
        }
    }
    

        

}
