using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpControl : MonoBehaviour
{
    public Image mBossHpBar;


    // public float mBossHpChange;

    // Start is called before the first frame update
    private void OnEnable()
    {
        mBossHpBar.fillAmount = UiManager.Instance.mBossCurrentHp / UiManager.Instance.mBossMaxHp;

    }

    // Update is called once per frame
    void Update()
    {
        // 보스 체력 데미지 (보스 hp가 바뀌면 fillAmount 변경하기)
        if (UiManager.Instance.mIsHpChange == true)
        {
            mBossHpBar.fillAmount = UiManager.Instance.mBossCurrentHp / UiManager.Instance.mBossMaxHp;
        }

    }




}
