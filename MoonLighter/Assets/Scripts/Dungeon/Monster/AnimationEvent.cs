using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public delegate void AnimationEventDelegate(string name);

    private AnimationEventDelegate mAnimationEvent = null;

    public void SetDelegate(AnimationEventDelegate eventDelegate)
    {
        mAnimationEvent = eventDelegate;
    }

    public void OnAnimationEvent(string name)
    {
        if(mAnimationEvent != null)
        {
            mAnimationEvent(name);
        }
    }
}
