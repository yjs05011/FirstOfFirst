using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscControl : MonoBehaviour
{
    public GameObject mGameBackCursorImage;
    public GameObject mOptionCursorImage;
    public GameObject mMainMenuCursorImage;
    public GameObject mExitCursorImage;

    public GameObject mOptionUi;

    // public KeyCode mUpKey= KeyCode.W;

    public int mTextCheck;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (mTextCheck == 3)
            {

            }
            else
            {
                mTextCheck++;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (mTextCheck == 0)
            {

            }
            else
            {
                mTextCheck--;
            }
        }
        // 선택하였을때 실행
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (mTextCheck)
            {
                case 0:
                    gameObject.SetActive(false);
                    break;
                case 1:
                    gameObject.SetActive(false);
                    break;
                case 2:
                    mOptionUi.SetActive(true);
                    break;
                case 3:
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                    break;
            }
        }
        // 무슨 이름인지 체크하여 활성화,비활성화 하기
        switch (mTextCheck)
        {
            case 0:
                mGameBackCursorImage.SetActive(true);
                mOptionCursorImage.SetActive(false);
                break;

            case 1:
                mOptionCursorImage.SetActive(true);
                mGameBackCursorImage.SetActive(false);
                mMainMenuCursorImage.SetActive(false);
                break;
            case 2:
                mMainMenuCursorImage.SetActive(true);
                mOptionCursorImage.SetActive(false);
                mExitCursorImage.SetActive(false);
                break;
            case 3:
                mExitCursorImage.SetActive(true);
                mMainMenuCursorImage.SetActive(false);
                break;
        }

    }

}
