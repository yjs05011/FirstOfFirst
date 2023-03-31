using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : UIController
{
    public bool mIsCheck;
    // Start is called before the first frame update
    public KeyCode ChangeKey;
    public int mKeyNumber;
    public int mNowKeyNumber;
    private Text text;


    string[] Arrow = new string[5] { "▲", "▼", "▶", "◀", "〓" };
    // Update is called once per frame
    public void Start()
    {
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name == transform.name)
            {
                mNowKeyNumber = i;
            }
        }
        Debug.Log(mNowKeyNumber);
        switch (GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)mNowKeyNumber - 1])
        {
            case KeyCode.UpArrow:
                text.text = Arrow[0];
                break;
            case KeyCode.DownArrow:
                text.text = Arrow[1];
                break;
            case KeyCode.RightArrow:
                text.text = Arrow[2];
                break;
            case KeyCode.LeftArrow:
                text.text = Arrow[3];
                break;
            case KeyCode.Space:
                text.text = Arrow[4];
                break;
            default:
                text.text = GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)mNowKeyNumber - 1].ToString();
                break;
        }
    }
    public override void Update()
    {
        if (UiManager.Instance.mIsKeyChanged)
        {
            switch (GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)mNowKeyNumber - 1])
            {
                case KeyCode.UpArrow:
                    text.text = Arrow[0];
                    break;
                case KeyCode.DownArrow:
                    text.text = Arrow[1];
                    break;
                case KeyCode.RightArrow:
                    text.text = Arrow[2];
                    break;
                case KeyCode.LeftArrow:
                    text.text = Arrow[3];
                    break;
                case KeyCode.Space:
                    text.text = Arrow[4];
                    break;
                default:
                    text.text = GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)mNowKeyNumber - 1].ToString();
                    break;
            }
        }
    }

    public override void Runing(int i)
    {
        base.Runing();

        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]))
        {
            StartCoroutine("Delay");
        }

        mKeyNumber = i - 1;
        Debug.Log("!!");
    }
    private void OnGUI()
    {
        if (mIsCheck)
        {
            Event KeycodeEvent = Event.current;
            UiManager.Instance.mIsKeyChanged = true;
            if (KeycodeEvent.isKey)
            {
                GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)mKeyNumber] = KeycodeEvent.keyCode;
                Debug.Log(GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)mKeyNumber]);
                switch (GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)mNowKeyNumber - 1])
                {
                    case KeyCode.UpArrow:
                        text.text = Arrow[0];
                        break;
                    case KeyCode.DownArrow:
                        text.text = Arrow[1];
                        break;
                    case KeyCode.RightArrow:
                        text.text = Arrow[2];
                        break;
                    case KeyCode.LeftArrow:
                        text.text = Arrow[3];
                        break;
                    case KeyCode.Space:
                        text.text = Arrow[4];
                        break;
                    default:
                        text.text = GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)mNowKeyNumber - 1].ToString();
                        break;
                }
                GameKeyManger.Instance.SaveKeyData();
                mIsCheck = false;
                UiManager.Instance.mIsKeyChanged = false;
            }
        }

    }
    IEnumerator Delay()
    {

        yield return new WaitForSeconds(0.1f);
        mIsCheck = true;




    }
}
