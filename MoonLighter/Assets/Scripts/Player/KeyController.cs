using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : UIController
{
    public bool mIsCheck;
    // Start is called before the first frame update
    public KeyCode ChangeKey;
    public int mKeyNumber;
    // Update is called once per frame
    public override void Update()
    {
        if (mIsCheck)
        {
            OnGUI();
        }
    }

    public override void Runing(int i)
    {
        base.Runing();
        mIsCheck = true;
        mKeyNumber = i - 1;
    }
    private void OnGUI()
    {
        Event KeycodeEvent = Event.current;
        if (KeycodeEvent.isKey)
        {

        }
    }
}
