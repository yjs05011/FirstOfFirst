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
        Wait, // 대기 상태 (5초정도 있다가 터지면서 사라짐)
        Explosion // 폭발? 사라지는 
    }

    [Header("Componenet")]
    public Animator mAnimator = null;
    public AnimationEvent mAnimationEvent = null;

    [Header("Falling Object Table")]
    [Range(0.1f, 100.0f)]
    public float mSpeed = 1.5f;

    [Range(1.0f, 1000.0f)]
    public float mEffectiveRange = 10.0f;

    [Header("Rock Data")]
    public State mState = State.None;

    public Monster mOwner = null;
    public Vector3 mDropPosition = Vector3.zero;
   
    public Transform mRock = null;

    public float mWaitTime = 2.5f;

    public void SetData(Monster owner, Vector3 dropPos)
    {
        mOwner = owner;
        mDropPosition = dropPos;
        mAnimationEvent.SetDelegate(OnAnimationEvent);
        mState = State.Fall;
       
    }

 

    public void Update()
    {
        if (mState == State.Fall)
        {
            mAnimator.SetTrigger("Rock");
            this.transform.Translate(mDropPosition, Space.World);
            if (!mOwner.IsMovablePosition(this.transform.position))
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
                mWaitTime = 5.0f;
            }
        }
        if(mState == State.Explosion)
        {
            this.Explosion();
        }
    }

   

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            PlayerAct player = collision.GetComponent<PlayerAct>();
            if (player)
            {
                player.OnDamage(mOwner.GetMonsterId(),mOwner.GetDamage());
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
