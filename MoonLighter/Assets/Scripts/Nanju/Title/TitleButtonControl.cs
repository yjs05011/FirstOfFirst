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
    public GameObject mOptionUi;
    public GameObject mStartScreenLogo;
    public GameObject mTitleButtons;

    public PlayerScriptObjs mPlayerDefaultStat;

    public int mTextCheck;
    public int mOptionKey = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UiManager.Instance.mIsOptionActive)
        {
            mTitleButtons.SetActive(false);
        }
        else
        {
            mTitleButtons.SetActive(true);
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
                        PlayerManager.Instance.mPlayerStat.Hp = mPlayerDefaultStat.hp;
                        PlayerManager.Instance.mPlayerStat.Speed = mPlayerDefaultStat.Speed;
                        PlayerManager.Instance.mPlayerStat.Str = mPlayerDefaultStat.str;
                        PlayerManager.Instance.mPlayerStat.Def = mPlayerDefaultStat.def;
                        PlayerManager.Instance.mPlayerStat.Money = mPlayerDefaultStat.Money;
                        PlayerManager.Instance.mPlayerStat.MaxHp = mPlayerDefaultStat.MaxHp;
                        LoadingManager.LoadScene("VillageScene");
                        UiManager.Instance.mIsKeySelection = false;
                        UiManager.Instance.mIsSceneChaged = true;
                        break;
                    case 1:
                        mStartScreenLogo.SetActive(false);

                        mOptionUi.SetActive(true);
                        UiManager.Instance.mIsOptionActive = true; ;
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
            // if (Input.GetKeyDown(KeyCode.Escape))
            // {

            //     mStartScreenLogo.SetActive(true);
            //     mTitleButtons.SetActive(true);

            // }
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
    // public void EnterGame()
    // {
    //     GFunc.LoadScene("VillageScene");
    // }
}
