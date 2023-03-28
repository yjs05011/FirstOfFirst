using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpControl : MonoBehaviour
{
    public Image mBossHpBar;

    // public float mBossHpChange;

    // Start is called before the first frame update
    void Start()
    {
        mBossHpBar.fillAmount = UiManager.Instance.mBossCurrentHp / UiManager.Instance.mBossMaxHp;

    }

    // Update is called once per frame
    void Update()
    {
        BossHpDamage();
    }


    // 보스 체력 데미지
    public void BossHpDamage()
    {
        mBossHpBar.fillAmount = UiManager.Instance.mBossCurrentHp / UiManager.Instance.mBossMaxHp;


    }
}
