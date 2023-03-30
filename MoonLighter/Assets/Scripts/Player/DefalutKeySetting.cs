using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefalutKeySetting : UIController
{
    public override void Runing()
    {
        GameKeyManger.Instance.DefaultKeySetting();
    }
}
