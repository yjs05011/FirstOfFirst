using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{

    // 하트 표시 하기
    public GameObject mHpEmptyHeart;
    public GameObject mHpFullHeart;
    public PlayerAct mPlayer;
    public Image mHpBar;
    public float mMaxHp = 170f;
    public float mMinHp = 0f;
    public float mCurrentHp = 0f;
    public int mHeal = 5;
    public int mDamage = 5;
    public Text mHpText;


    // Start is called before the first frame update
    void Start()
    {
        // Hp 0으로 초기화
        // mCurrentHp = mMaxHp;

        mHpText.text = mCurrentHp.ToString();

        // StartCoroutine(WaiteForIt());
    }

    // Update is called once per frame
    void Update()
    {
        // mMaxHp = PlayerManager.Instance.mPlayerStat.Hp;
        // if (PlayerManager.Instance.mIsPlayerHpChange)
        // {
        //     HpChange();
        // }
    }


    // 체력 회복하기
    public void HpChange()
    {
        mCurrentHp = mPlayer.mPlayerHp;
        mHpBar.fillAmount = mCurrentHp / mMaxHp;
        mHpText.text = mCurrentHp.ToString();


        if (mCurrentHp == mMaxHp / 2)
        {
            mHpEmptyHeart.SetActive(false);
            mHpFullHeart.SetActive(true);
        }
        else
        {
            mHpFullHeart.SetActive(false);
            mHpEmptyHeart.SetActive(true);
        }

    }


    //체력이 소모되게 하기
    // public void DamageZone()
    // {
    //     if (mMaxHp > 0)
    //     {
    //         mCurrentHp -= mDamage;
    //         mHpBar.fillAmount = mCurrentHp / mMaxHp;
    //         mHpText.text = mCurrentHp.ToString();
    //         // Debug.Log(mCurrentHp / mMaxHp);

    //         // Hp 가 35가 되면 빈 하트로 표시 하기
    //         if (mCurrentHp == 35)
    //         {
    //             mHpFullHeart.SetActive(false);
    //             mHpEmptyHeart.SetActive(true);
    //         }
    //     }
    // }

    // 체력 소모가 되는지 시간 줘서 확인하기
    // IEnumerator WaiteForIt()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(1f);
    //         // DamageZone();
    //         HealZone();
    //     }

    // }

}
