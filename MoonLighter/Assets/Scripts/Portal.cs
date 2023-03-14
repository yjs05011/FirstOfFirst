using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Animator portalAni;
    private bool Isplayernearby = false;
    private bool Isplayerinfrontofportal = false;
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
            Vector3 playerPos = player.transform.localPosition;
            float dist = Vector3.Distance(this.transform.localPosition, playerPos);
            if(dist < 0.5f) 
            {
                talk.SetActive(true);
            }
            else
            {
                talk.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            Isplayernearby=true;
            player = collision.gameObject;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Isplayernearby=false;   
        }
    }

}

