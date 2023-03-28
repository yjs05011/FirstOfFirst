using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForbiddenControl : MonoBehaviour
{
    public Text mChangeText;
    public int mCurrentMiniInventoryText;
    public GameObject mForbidden;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MiniInventoryTextChange();
        if (UiManager.Instance.mIsInventoryLock == true)
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


}

