using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttackSkill : MonoBehaviour
{
    [Header("Owner")]
    public Monster mOwner = null;

    [Header("Skill Table")]
    public Vector3 mMinScale = Vector3.zero;
    public Vector3 mMaxScale = Vector3.one;
    [Range(0.0f, 10.0f)]
    public float mDuration = 10.0f;
    public float mDamage = 10.0f;

    [Header("Var")]
    public float mTime = 0.0f;

    public void SetData(Monster owner, Vector3 minScale, Vector3 maxScale, float duration, float damage)
    {
        mOwner = owner;
        mMinScale = minScale;
        mMaxScale = maxScale;
        mDuration = duration;
        mDamage = damage;

        mTime = 0.0f;
        this.transform.localScale = mMinScale;
        this.transform.position = mOwner.transform.position;
    }

    public void Update()
    {
        mTime += Time.deltaTime;

        if(mTime > mDuration)
        {
            this.transform.localScale = mMaxScale;
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            float rate = mTime / mDuration;
            this.transform.localScale = Vector3.Lerp(mMinScale, mMaxScale, rate);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerAct player = collision.GetComponent<PlayerAct>();
            if(player)
            {
                player.OnDamage(mOwner.GetMonsterId(), mDamage);
            }
        }
    }
}
