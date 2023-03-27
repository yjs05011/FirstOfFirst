using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonControl : MonoBehaviour
{
    public GameObject mNewGameCursorImage;

    // 게임을 하고 다시 들어올때 켜지게 하기위해 선언
    public GameObject mPlayingGameCursorImage;
    public GameObject mOptionCursorImage;
    public GameObject mExitCursorImage;

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
            if (mTextCheck == 2)
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
                    break;
                case 2:
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
                mNewGameCursorImage.SetActive(true);
                mOptionCursorImage.SetActive(false);
                mExitCursorImage.SetActive(false);
                break;

            case 1:
                mOptionCursorImage.SetActive(true);
                mNewGameCursorImage.SetActive(false);
                mExitCursorImage.SetActive(false);
                break;
            case 2:
                mExitCursorImage.SetActive(true);
                mNewGameCursorImage.SetActive(false);
                mOptionCursorImage.SetActive(false);
                break;
        }
    }
}
