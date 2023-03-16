using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Animator portalAni;
    private bool Isplayernearby = false;
    private GameObject talk = default;
    private GameObject player = default;
    // Start is called before the first frame update
    void Start()
    {
        portalAni= GetComponent<Animator>();
        talk = transform.Find("talk").gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        portalAni.SetBool("IsPlayerNearby",Isplayernearby);
        if(Isplayernearby)
        {
            Vector3 mPlayerPos = player.transform.localPosition;
            float mDistance = Vector3.Distance(this.transform.localPosition, mPlayerPos);
            if(mDistance < 0.5f) 
            {
                talk.SetActive(true);
            }
            else
            {
                talk.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D mCollision)
    {
        
        if (mCollision.tag == "Player")
        {
            Isplayernearby=true;
            player = mCollision.gameObject;
        }
        
    }
    private void OnTriggerExit2D(Collider2D mCollision)
    {
        if(mCollision.tag == "Player")
        {
            Isplayernearby=false;   
        }
    }

}

