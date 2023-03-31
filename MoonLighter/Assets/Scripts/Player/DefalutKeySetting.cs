using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefalutKeySetting : UIController
{
    public override void Runing(int i)
    {
        GameKeyManger.Instance.DefaultKeySetting();
        StartCoroutine("Delay");
    }
    IEnumerator Delay()
    {
        UiManager.Instance.mIsKeyChanged = true;
        yield return null;
        UiManager.Instance.mIsKeyChanged = false; ;
    }
}
