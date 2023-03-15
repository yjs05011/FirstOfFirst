using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
   public Item mItem;
   public GameObject mPlayerDistance = default;
   private Rigidbody2D mItemRigidBody = default;

   void start()
   {
        mItemRigidBody = gameObject.GetComponent<Rigidbody2D>();
   }

  

   

   
}
