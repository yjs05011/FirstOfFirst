using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAni : MonoBehaviour
{
    public  static bool IsAnimationFinished = false;

    public void AnimationFinish()
    {
        IsAnimationFinished = true;
    }
}
