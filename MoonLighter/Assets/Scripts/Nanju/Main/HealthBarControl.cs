using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{

    // 하트 표시 하기
    public GameObject mHpEmptyHeart;
    public GameObject mHpFullHeart;
    public Image mHpBar;
    public float mMaxHp;
    public float mMinHp = 0f;
    public float mCurrentHp = 0f;
    public int mHeal = 5;
    public int mDamage = 5;
    public Text mHpText;


    // Start is called before the first frame update
    void Start()
    {
        mCurrentHp = PlayerManager.Instance.mPlayerStat.Hp;
        mMaxHp = PlayerManager.Instance.mPlayerStat.MaxHp;
        mHpBar.fillAmount = mCurrentHp / mMaxHp;
        mHpText.text = mCurrentHp.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        HpChange();

    }


    // 체력 회복하기
    public void HpChange()
    {

        if (UiManager.Instance.mIsHpChange)
        {
            mCurrentHp = PlayerManager.Instance.mPlayerStat.Hp;
            mMaxHp = PlayerManager.Instance.mPlayerStat.MaxHp;
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
        PlayerManager.Instance.mIsPlayerHpChange = false;

    }


    // 체력이 소모되게 하기
    public void DamageZone()
    {
        if (Input.anyKeyDown)
        {
            PlayerManager.Instance.mIsPlayerHpChange = true;
            PlayerManager.Instance.mPlayerStat.Hp -= 5;
        }

    }


}
