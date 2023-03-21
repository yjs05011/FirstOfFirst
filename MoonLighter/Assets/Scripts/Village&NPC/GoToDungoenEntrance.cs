using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDungoenEntrance : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
            GFunc.LoadScene("DungeonEntrance");
            
        }
    }
}
