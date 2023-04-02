using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Slot[] mSlot;
    
    public Inventory mInventory;
    public ChestInventory mChestInventory;
   
    public Item mItem;
    
    public GameObject mPlayerBagDistance = default;
    
    public Rigidbody2D mItemRigidBody = default;

    public void Awake()
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
                InventoryManager.Instance.mIsManagerAddCheck = true;   
                
                mInventory.AcpuireItem(this.gameObject.transform.GetComponent<ItemPickUp>().mItem, mItemCount);
                DungeonManager.Instance.DungeonDropItemDelete(mItem);
                
                
                GetItemMove();
                return;
            }                       
        }        
        if(other.CompareTag("Hole"))
        {
            GetItemMove();
        }
    }
   

    public void GetItemMove()
    {
        
        Destroy(this.gameObject);
    } 

}
