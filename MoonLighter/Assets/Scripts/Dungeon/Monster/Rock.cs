using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public enum State 
    { 
        None,  // none
        Spwan, // ��������
        Fall,  // �������� ����
        Wait,  // ��� ���� (5������ �ִٰ� �����鼭 �����)
        Explosion // ����? ������� 
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
    public float mDamage = 10.0f;

    public Monster mOwner = null;
    public Vector3 mDropPosition = Vector3.zero;
   
    public Transform mRock = null;

    private float mWaitTime = 3.0f;

    public void SetData(Monster owner, Vector3 spwanPos, float damage)
    {
        mOwner = owner;
        mDropPosition = spwanPos;
        mDamage = damage;
        this.transform.position = mDropPosition;
        mAnimationEvent.SetDelegate(OnAnimationEvent);
        mState = State.Spwan;
       
    }

 

    public void Update()
    {
        if (mState == State.Spwan)
        {
            mAnimator.SetTrigger("RockFall");
            
            //if (!mOwner.IsMovablePosition(this.transform.position))
            //{
                SetState(State.Fall); // fall -> wait �� �������� �ִϸ��̼� ���� ������ FallFinished �̺�Ʈ�� ����. 
            //}
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
                mWaitTime = 3.0f;
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
        if ("FallFinished".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
             SetState(State.Wait);
        }
    }
}
