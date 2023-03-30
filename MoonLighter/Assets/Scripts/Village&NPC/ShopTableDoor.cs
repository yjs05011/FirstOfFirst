using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTableDoor : MonoBehaviour
{
    private Animator mTableDoor;
    
    // Start is called before the first frame update
    void Start()
    {
        mTableDoor= GetComponent<Animator>();
    }

    // Update is called once per frame
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            mTableDoor.SetBool("DoorOpen",true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            mTableDoor.SetBool("DoorOpen", false);
        }
    }
}
