using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttackSkill : MonoBehaviour
{
    [Header("Owner")]
    public MonsterGolemKing mOwner = null;
    public AnimationEvent mAnimationEvent = null;
    public Animator mAnimator = null;

    [Header("Skill Table")]
    public Transform mStartPosition = null;

    public float mDamage = 10.0f;

  

    public void SetData(MonsterGolemKing owner, Transform position,  float damage)
    {
        mOwner = owner;
        mDamage = damage;
        mStartPosition = position;

        this.transform.position = mStartPosition.position;
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

    public void OnAnimationEvent(string name)
    {
        bool isWaveFinished = "WaveFinished".Equals(name, System.StringComparison.OrdinalIgnoreCase);
       

        if (isWaveFinished)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            //Debug.LogErrorFormat("Unknown Event Name:{0}", name);
        }
    }
}
