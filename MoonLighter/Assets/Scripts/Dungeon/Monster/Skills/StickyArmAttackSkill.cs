using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyArmAttackSkill : MonoBehaviour
{
    public MonsterGolemKing mOwner = null;
  
    public float mDamage = 10.0f;

    public void SetData(MonsterGolemKing owner, float damage)
    {
        mOwner = owner;
        mDamage = damage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerAct player = collision.GetComponent<PlayerAct>();
            if (player)
            {
                player.OnDamage(mOwner.GetMonsterId(), mDamage);
            }
        }
    }

}
