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


    public float mTimer;

    // Start is called before the first frame update
    void Start()
    {
        mTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (UiManager.Instance.mIsPlayerUseAnimation == true)
        {
            // 보스 hp바 끄기
            UiManager.Instance.SetBossHpVisible(false);

            mExitDungeon.SetActive(true);
            mReplayKeyboard.SetActive(false);
            PlayerManager.Instance.mIsUiActive = true;
        }
        // if (PlayerManager.Instance.mPlayerStat.isDie == true)    // UiManager.Instance.mIsPlayerDie
        // {
        //     mExitDungeon.SetActive(true);


        // }

    }



    // 키보드 누르면 인벤토리 
    // 미니(퀵슬롯) 인벤토리 On, Off 유무 함수

}
