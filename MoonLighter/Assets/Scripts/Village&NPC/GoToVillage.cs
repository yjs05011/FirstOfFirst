using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToVillage : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LoadingManager.LoadScene("VillageScene");
            //GFunc.LoadScene("VillageScene");
        }
    }
}
