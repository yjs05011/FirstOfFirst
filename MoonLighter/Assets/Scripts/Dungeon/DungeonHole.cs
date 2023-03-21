using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonHole : MonoBehaviour
{
    public float mDelayTime = 0.2f;
    public float mTimer = 0.0f;
    public float mFallingDamegeRate = 0.45f;

    public bool mIsFall = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mIsFall = true; 

            Falling(other);
            Debug.Log("Player Fall");

        }
    }

    IEnumerator Falling(Collider2D other)
    {
        
        float timer = 0.1f;
        PlayerAct player = other.GetComponent<PlayerAct>();
        //float falldamage = player.GetPlayerMaxHp() * mFallingDamegeRate;
        //
        //player.OnFalling(falldamage);

        yield return null;
    }

  


}
