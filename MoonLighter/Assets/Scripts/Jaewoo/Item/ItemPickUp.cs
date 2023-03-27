using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Slot[] mSlot;
    [SerializeField]
    private Inventory mInventory;
    [SerializeField]
    private Item mItem;
    [SerializeField]
    private GameObject mPlayerBagDistance = default;
    [SerializeField]
    private Rigidbody2D mItemRigidBody = default;

    private void Awake()
    {
        mInventory = GameObject.FindObjectOfType<Inventory>();
    }
    void start()
    {
        mItemRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int mItemCount = 1;
            
            if(this.gameObject.transform.GetComponent<ItemPickUp>().mItem != null)
            {
                mInventory.AcpuireItem(this.gameObject.transform.GetComponent<ItemPickUp>().mItem, mItemCount);
                GetItemMove();
                return;
            }                       
        }
    }

    public void GetItemMove()
    {
        
        Destroy(this.gameObject);
    } 

}
