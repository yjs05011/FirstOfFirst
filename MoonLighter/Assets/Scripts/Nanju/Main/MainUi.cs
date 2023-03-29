using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUi : MonoBehaviour
{
    public GameObject mBossHp;
    public GameObject mBagPendont;
    public GameObject mExitDungeon;
    public GameObject mReplayKeyboard;


    public float mTimer;

    public Text mKillMonsterCount;

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
        ExitDungeon();

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
        // SetKillMonsterCount();

        if (UiManager.Instance.mIsPlayerUseAnimation == true)
        {
            mExitDungeon.SetActive(true);
            mReplayKeyboard.SetActive(false);
        }
        if (PlayerManager.Instance.mPlayerStat.isDie == true)    // UiManager.Instance.mIsPlayerDie
        {
            mExitDungeon.SetActive(true);
            PlayerManager.Instance.mIsUiActive = true;

            if (Input.GetKeyDown(KeyCode.L))
            {
                PlayerManager.Instance.mPlayerStat.isDie = false;

                mExitDungeon.SetActive(false);

                LoadingManager.LoadScene("VillageScene");

            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                PlayerManager.Instance.mPlayerStat.isDie = false;

                mExitDungeon.SetActive(false);

                // 죽었을 때  다시플레이 버튼 누르면 처음 던전 씬으로 안가고 ExitDungeon ui 켜진채로 감
                // -> 로딩씬 켰다가 던전씬  다시 불러오게하기 언니와 상의해보기 
                // 임시로 탈출한거 만들어 논거 확인하기
                LoadingManager.LoadScene("Dungeon");

            }
        }
        else
        {
            mExitDungeon.SetActive(false);
        }
    }


    // [던전] 몇개 몬스터 잡았는지 확인
    // public void SetKillMonsterCount()
    // {
    //     int KillMonster = DungeonManager.Instance.mKillMonsterList.Count();
    //     mKillMonsterCount.text = KillMonster.ToString();


    // }













    // 키보드 누르면 인벤토리 
    // 미니(퀵슬롯) 인벤토리 On, Off 유무 함수

}
