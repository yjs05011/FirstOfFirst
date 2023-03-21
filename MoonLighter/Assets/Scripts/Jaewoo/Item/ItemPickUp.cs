using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Inventory mInventoryTest;
    public Item mItem;
    public GameObject mPlayerBagDistance = default;
    private Rigidbody2D mItemRigidBody = default;

    void start()
    {
        mItemRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mInventoryTest.AcpuireItem(gameObject.transform.GetComponent<ItemPickUp>().mItem);
            Destroy(this.gameObject);
        }
        
    }


}
