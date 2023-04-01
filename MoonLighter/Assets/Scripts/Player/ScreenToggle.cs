using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenToggle : UIController
{
    // Start is called before the first frame update

    // Update is called once per frame
    public override void Update()
    {

    }
    public override void Runing(bool flag)
    {
        base.Runing(flag);
        SoundManager.Instance.mIsFullScreen = !SoundManager.Instance.mIsFullScreen;
        SoundManager.Instance.SetFullScreen(SoundManager.Instance.mIsFullScreen);
        if (SoundManager.Instance.mIsFullScreen)
        {
            transform.GetChild(0).GetComponent<Text>().text = "켜기";
        }
        else
        {
            transform.GetChild(0).GetComponent<Text>().text = "끄기";
        }
    }

}
