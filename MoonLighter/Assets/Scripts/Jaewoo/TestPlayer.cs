using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private bool isPickUp = false;
    private Inventory mInvetoryTest;

    private void ItemPickUp()
    {
        if(!isPickUp)
        {
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isPickUp = false;
    }
    
   

    public void OntriggerEnter2D(Collider2D other)
    {
        if(gameObject.CompareTag("Item"))
        {
            mInvetoryTest.AcpuireItem(other.gameObject.transform.GetComponent<ItemPickUp>().mItem);            
        }
    }
}
