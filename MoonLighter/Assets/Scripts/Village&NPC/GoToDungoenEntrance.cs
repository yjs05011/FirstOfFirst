using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDungoenEntrance : MonoBehaviour
{
    public Vector3 mPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SetPosition.Instance.mSettingPosition= mPosition;
            LoadingManager.LoadScene("DungeonEntrance");
            //GFunc.LoadScene("DungeonEntrance");
            
        }
    }
}
