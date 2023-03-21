using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonHole : MonoBehaviour
{
    public float mDelayTime = 1.0f;
    public float mFallingDamegeRate = 0.45f;

    private DungeonStage mStage = null;

    public void SetStage(DungeonStage stage)
    {
        mStage = stage;
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Falling(other));
            Debug.Log("Player Fall");

            other.transform.position = mStage.GetEntryPosition();

        }
    }

    IEnumerator Falling(Collider2D other)
    {
      
        yield return new WaitForSeconds(mDelayTime);

        PlayerAct player = other.GetComponent<PlayerAct>();
        float falldamage = player.GetPlayerMaxHp() * mFallingDamegeRate;

        player.OnFalling(falldamage);

    }

  
}
