using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : MonoBehaviour
{
   
    public GameObject NightSky;

    private void Start()
    {
        if(SetPosition.Instance.mIsNight)
        {
            NightSky.SetActive(true);
        }
        else
        {
            NightSky.SetActive(false);
        }
    }

}
