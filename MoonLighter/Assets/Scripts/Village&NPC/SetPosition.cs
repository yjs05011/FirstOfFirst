using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : GSingleton<SetPosition>
{
    //public static SetPosition mInstance;
    public Vector3 mSettingPosition = default;
    public bool mIsNight = false;
}
