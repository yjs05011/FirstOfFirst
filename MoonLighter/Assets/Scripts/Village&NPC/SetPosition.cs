using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : GSingleton<SetPosition>
{
    //public static SetPosition mInstance;
    public Vector3 mSettingPosition;
    public GameObject mPlayer;

    public void LoadOnPosition(string goTo)
    {
        if(goTo == "Village")
        {
            GFunc.LoadScene("VillageScene");
            mPlayer = GameObject.Find("Player");
            mPlayer.transform.position = mSettingPosition;
        }
        if(goTo == "DungeonEntrance")
        {
            GFunc.LoadScene("DungeonEntrance");
            mPlayer = GameObject.Find("Player");
            mPlayer.transform.position = new Vector3(0.6f,-16,0);
        }
    }

}
