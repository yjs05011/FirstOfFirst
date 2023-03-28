using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForbiddenControl : MonoBehaviour
{
    public Text mChangeText;
    public int mCurrentMiniInventoryText;
    public GameObject mForbidden;

    public bool mIsInventoryLock = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MiniInventoryTextChange();
        if (mIsInventoryLock == true)
        {
            mForbidden.SetActive(true);
        }
        else
        {
            mForbidden.SetActive(false);
        }
    }

    void MiniInventoryTextChange()
    {
        mChangeText.text = mCurrentMiniInventoryText.ToString();

        if (0 <= mCurrentMiniInventoryText && mCurrentMiniInventoryText < 20)
        {
            mForbidden.SetActive(false);
        }
        else if (mCurrentMiniInventoryText == 20)
        {

            mForbidden.SetActive(true);
        }

    }

    // 인벤토리 
    public void SetInventoryLock(bool value)
    {
        mIsInventoryLock = value;
    }
}

