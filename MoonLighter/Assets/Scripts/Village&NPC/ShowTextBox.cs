using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextBox : MonoBehaviour
{
    private GameObject talk = default;
    private GameObject player = default;
    private bool IsPlayerNearby = false;
    // Start is called before the first frame update
    void Start()
    {
        talk = transform.Find("Talk").gameObject;
        talk.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerNearby)
        {
            talk.SetActive(true);

        }
        else
        {
            talk.SetActive(false);
        }
        //if(talk.activeSelf)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Talk();
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D mCollision)
    {
        if (mCollision.tag == "Player")
        {
            IsPlayerNearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D mCollision)
    {
        if (mCollision.tag == "Player")
        {
            IsPlayerNearby = false;
        }
    }
}
