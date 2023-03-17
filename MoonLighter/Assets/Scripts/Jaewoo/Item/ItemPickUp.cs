using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item mItem;
    public GameObject mPlayerBagDistance = default;
    private Rigidbody2D mItemRigidBody = default;

    void start()
    {
        mItemRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {

            Destroy(this.gameObject);
        }
    }


}
