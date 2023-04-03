using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour
{
    // 플레이어가 몬스터에게 입히는 데미지
    public float mDamage;
    public PlayerAct player;
    private void Awake()
    {
        player = transform.parent.GetComponent<PlayerAct>();
    }
    public void OnEnable()
    {
        mDamage = player.mPlayerStr;
        if (player.mState == ActState.State_Attack_Combo_Three)
        {
            mDamage = player.mPlayerStr * 2;

        }
        else if (player.mPlayerNowWeapone == 1 && player.mState == ActState.State_Attack_Skill)
        {
            mDamage = player.mPlayerStr * 3;
        }
        else if (player.mPlayerNowWeapone == 2 && player.mState == ActState.State_Attack_Skill)
        {
            mDamage = player.mPlayerStr * 2;
        }
        else
        {
            mDamage = player.mPlayerStr;
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {

            other.GetComponent<Monster>().OnDamage(mDamage);
        }
        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<DungeonBreakable>().OnBreak();
        }
    }
}
