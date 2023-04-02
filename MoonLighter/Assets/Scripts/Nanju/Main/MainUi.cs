using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUi : MonoBehaviour
{
    public GameObject mBossHp;
    public GameObject mBagPendont;
    public GameObject mExitDungeon;
    public GameObject mReplayKeyboard;

    // EscUI 켜고, 끄기 
    public GameObject mEscUI;
    public int mEscControl = 0;


    private float mTimer;

    // Start is called before the first frame update
    void Start()
    {
        mTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EscUiControl();
        BossHp();
        Pendont();
        PendantUseCheck(mTimer);
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            ExitDungeon();
        }

    }

    // 보스 체력바 ON OFF 하는 함수
    public void BossHp()
    {
        if (UiManager.Instance.mIsBossHpVisible == true)
        {
            mBossHp.SetActive(true);
        }
        else
        {
            mBossHp.SetActive(false);
        }
    }



    // 팬던트 On, Off 하는 함수
    public void Pendont()
    {
        if (UiManager.Instance.mIsDungeonCheck == true)
        {
            mBagPendont.SetActive(true);
        }
        else
        {
            mBagPendont.SetActive(false);
        }
    }

    // 팬던트 사용 유무 함수
    public void PendantUseCheck(float time)
    {
        if (200 <= PlayerManager.Instance.mPlayerStat.Money)
        {
            if (Input.GetKey(KeyCode.L))
            {
                Debug.Log(mTimer);
                time += Time.deltaTime;
                mTimer = time;
                if (mTimer >= 1.5)
                {
                    mTimer = 0;
                    UiManager.Instance.PlayerUsePendant(true);
                }
            }
        }
        else
        {
            /*Do noting*/
        }
    }


    // ExitDungeon UI 활성화 시키기
    public void ExitDungeon()
    {

        if (UiManager.Instance.mIsPlayerFinishAnimation == true)
        {
            // 보스 hp바 끄기
            UiManager.Instance.SetBossHpVisible(false);
            // 팬던트 off 하기
            UiManager.Instance.mIsDungeonCheck = false;

            // 다시하기 버튼 키 끄기
            mReplayKeyboard.SetActive(false);
            // 퇴장 ui 키기
            mExitDungeon.SetActive(true);
            PlayerManager.Instance.mIsUiActive = true;
        }
        if (PlayerManager.Instance.mPlayerStat.isDie == true)
        {
            // 보스 hp바 끄기
            UiManager.Instance.SetBossHpVisible(false);
            // 팬던트 off 하기
            UiManager.Instance.mIsDungeonCheck = false;

            // 다시하기 버튼 키 키기
            mReplayKeyboard.SetActive(true);
            // 퇴장 ui 키기
            mExitDungeon.SetActive(true);
            PlayerManager.Instance.mIsUiActive = true;
        }


    }

    // esc 키 받았을 때 esc ui 컨트롤 하기
    public void EscUiControl()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mEscControl++;
            if (mEscControl == 1)
            {
                PlayerManager.Instance.mIsUiActive = true;
                mEscUI.SetActive(true);
            }
            else if (mEscControl == 2)
            {
                PlayerManager.Instance.mIsUiActive = false;
                mEscUI.SetActive(false);
                mEscControl = 0;
            }

        }
    }


}
