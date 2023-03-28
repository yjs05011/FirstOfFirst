using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttackSkill : MonoBehaviour
{
    public MonsterGolemKing mOwner = null;
    public AnimationEvent mAnimationEvent = null;
    public Animator mAnimator = null;
    public float mDamage = 10.0f;

    public void SetData(MonsterGolemKing owner, float damage)
    {
        mOwner = owner;
        mDamage = damage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerAct player = collision.GetComponent<PlayerAct>();
            if(player)
            {
                player.OnDamage(mOwner.GetMonsterId(),mDamage);
            }
        }
    }


    public void OnAnimationEvent(string name)
    {
        bool isPunchDownFinished = "GolemKingPunchDownFinished".Equals(name, System.StringComparison.OrdinalIgnoreCase);
        bool isPunchUpFinished = "GolemKingPunchUpFinished".Equals(name, System.StringComparison.OrdinalIgnoreCase);

        if(isPunchDownFinished)
        {
            mAnimator.SetTrigger("PunchUp");
        }
        else if (isPunchUpFinished)
        {
            mOwner.RepeatPunchAttack();
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            Debug.LogErrorFormat("Unknown Event Name:{0}", name);
        }
    }
}
