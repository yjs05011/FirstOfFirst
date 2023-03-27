using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public enum State 
    { 
        None, // none
        Fall, // 떨어지는 상태
        Wait, // 대기 상태 (2.5초정도 있다가 터지면서 사라짐)
        Explosion // 폭발? 사라지는 
    }

    [Header("Componenet")]
    public Animator mAnimator = null;
    public AnimationEvent mAnimationEvent = null;

    [Header("Projectile Table")]
    [Range(0.1f, 100.0f)]
    public float mSpeed = 1.5f;

    [Range(1.0f, 1000.0f)]
    public float mEffectiveRange = 10.0f;

    [Header("Projectile Data")]
    public State mState = State.None;

    public Monster mOwner = null;
    public Vector3 mDirection = Vector3.zero;

    public Vector3 mStartPosition = Vector3.zero;

    public Transform mProjectile = null;

    public float mWaitTime = 2.5f;

    public void SetData(Monster owner, Vector3 direction)
    {
        mOwner = owner;
        mDirection = direction;

        mStartPosition = this.transform.position;
        mAnimationEvent.SetDelegate(OnAnimationEvent);
        mState = State.Fall;
        mAnimator.SetTrigger("Rock");
    }

 

    public void Update()
    {
        if (mState == State.Fall)
        {
            Vector3 movement = mDirection * mSpeed * Time.deltaTime;
            this.transform.Translate(movement, Space.World);
            if (!IsEffectiveRange() || !mOwner.IsMovablePosition(this.transform.position))
            {
                SetState(State.Wait);
            }
        }
        if(mState == State.Wait)
        {
            if (mWaitTime > 0)
            {
                mWaitTime -= Time.deltaTime;
            }
            else
            {
                SetState(State.Explosion);
                mWaitTime = 2.5f;
            }
        }
        if(mState == State.Explosion)
        {
            this.Explosion();
        }
    }

    public bool IsEffectiveRange()
    {
        Vector3 from = mStartPosition; //시작위치
        Vector3 to = this.transform.position; //현재위치

        Vector3 distance = from - to; //거리

        if (distance.magnitude <= mEffectiveRange)
        {
            return true;
        }
        return false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            PlayerAct player = collision.GetComponent<PlayerAct>();
            if (player)
            {
                player.OnDamage(mOwner.GetDamage());
            }
        }
    }

    public void Explosion()
    {
        //mAnimator.SetTrigger("Explosion");
        Destroy(this.gameObject);
    }

    public void SetState(State state)
    {
        mState = state;
    }


    public void OnAnimationEvent(string name)
    {
        //if ("destroy".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        //{
        //    GameObject.Destroy(this.gameObject);
        //}
    }
}
