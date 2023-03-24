using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabySlime : Monster
{
    public Transform mTarget;

    public enum State { IDLE, ATTACK_START, ATTACK_END, MOVE, HIT, DIE }
    public State mState = State.IDLE;

    [SerializeField]
    private float mAttackRadius;
    [SerializeField]
    private float mMoveRadius;

    [SerializeField]
    private float mAreaRadius;


    private float mThrust = 2.0f;

    public Animator mAnimator;

    public float mHitActionTime;
    public float mAttackActionTime;

    public Vector3 mAttackTargetPos;
    public Vector3 mAttackStartPos;
    public Vector3 mHitOppositePos;

    public Image mImgHp = null;

    public float mMaxHP = 0;

    // 일단 스폰 작업 안되있으니 임의로 설정.


    void Start()
    {
        mAnimator = GetComponent<Animator>();
        if (GameObject.FindWithTag("Player") != null)
        {
            mTarget = GameObject.FindWithTag("Player").transform;
        }

        Init(this.gameObject.transform.position);
    }

    public void Init(Vector3 startPos)
    {
        mHP = 10;
        mName = "BabySlime";
        mMoveSpeed = 1.0f;
        mBaseAttack = 10;
   
        mMaxHP = mHP;
        mImgHp.enabled = true;
        mImgHp.fillAmount = (float)mHP / (float)mMaxHP;
       

        mAreaRadius = 10.0f;
        mMoveRadius = 4.0f;
        mAttackRadius = 0.1f;
        mAttackActionTime = 1.0f;

        mState = State.IDLE;
       
        mHitActionTime = 1.0f;
        mHitOppositePos = Vector3.zero;
        mStartPos = startPos;
        Reposition();
    }

    private void Update()
    {
        UpdateState();
        UpdateAction();

    }

    public void UpdateState()
    {
        bool isDead = (mState == State.DIE);
        bool isAttackAtion = (mState == State.ATTACK_START || mState == State.ATTACK_END);
        bool isHitAction = (mState == State.HIT);

        // 공격중일때는 상태 변경을 막기 위해 return;
        if (isAttackAtion || isHitAction || isDead)
        {
            return;
        }

        if (mTarget != null)
        {
            //타겟이 공격범위안에 있다면 공격상태로 바꾼다.
            if (Vector3.Distance(mTarget.position, transform.position) < mAttackRadius)
            {

                mState = State.ATTACK_START;
                mAttackTargetPos = mTarget.position;
                mAttackStartPos = this.transform.position;


            }
            // 스폰 지점 으로 부터 내 위치가 이동 가능 범위일때,
            else if (Vector3.Distance(mStartPos, transform.position) < mAreaRadius)
            {
                // 타겟이 이동가능범위안에왔을때 이동(쫒아가기) 상태로 바꾼다
                if (Vector3.Distance(mTarget.position, transform.position) < mMoveRadius)
                {

                    mState = State.MOVE;
                }
                else
                {   // player 가 현재 방에서 나간경우, 스폰지점으로 다시 배치 필요

                    // 플레이어가 일정범위 멀어진경우 아이들.
                    mState = State.IDLE;

                }
            }
        }
        else
        {
            mState = State.IDLE;
        }


    }


    public void UpdateAction()
    {
        switch (mState)
        {
            case State.IDLE:
                {
                    mAnimator.SetBool("IsMove", false);
                    break;
                }
            case State.ATTACK_START:
                {
                    if (!IsAttackAtionEnd(Time.deltaTime))
                    {
                        // 공격 모션있으면 애니메이션 출력 필요
                        Debug.LogFormat("{0} Attack !!!!", mName);
                    }
                    else
                    {
                   
                        mState = State.IDLE;
                    }


                    break;
                }
            case State.ATTACK_END:
                {
                    // attackout -> idle
                    if (transform.position == mAttackStartPos)
                    {
                        mState = State.IDLE;
                    }
                    else // 공격 시작 지점으로 빽
                    {
                        // 공격 모션있으면 애니메이션 종료 필요
                        Debug.LogFormat("{0} Attack END!!!!", mName);
                        
                    }
                    break;
                }
            case State.MOVE:
                {
                    
                    mAnimator.SetBool("IsMove", true);
                    transform.position = Vector3.MoveTowards(transform.position, mTarget.position, mMoveSpeed * Time.deltaTime);
                    break;
                }
           
            case State.HIT:
                {
                    transform.position = Vector3.MoveTowards(transform.position, mHitOppositePos, mThrust * Time.deltaTime);
                    // knockback 종료 대기 후 상태 변경.
                    if (IsHitAtionEnd(Time.deltaTime))
                    {
                        SetState(State.IDLE);
                    }
                    break;
                }
            case State.DIE:
                {
                    // 죽는 연출 추가해야함. 펑! 하고 사라지는...
                    Debug.LogFormat("{0} DIE", mName);
                    break;
                }
        }
    }

    public void Attack()
    {
        if (mTarget == null)
        {
            return;
        }
        if (Vector3.Distance(mTarget.position, transform.position) < 1.0f)
        {
            // 플레이어 접근해야하니 플레이어 컨트롤 스크립트 찾아서 on damage 함수에 데미지 전달. 
            mTarget.GetComponent<PlayerAct>().OnDamage(mBaseAttack);
        }
    }




    public override void OnDamage(float damage)
    {
        
        if (mState != State.DIE)
        {
            // 공격중인 모션있으면 종료 처리 필요
        

            // knockback 으로 밀려날 좌표를 저장해두고
            Vector2 opposite = (mTarget.position - this.transform.position);
            opposite = opposite.normalized * mThrust;
            mHitOppositePos = transform.position - (Vector3)opposite;

            // 데미지 차감
            mHP -= damage;
            mImgHp.fillAmount = mHP / mMaxHP;
         



            if (mHP <= 0)
            {
                SetState(State.DIE);
                Die();
            }
            else
            {
                // 상태 hit 전환
                SetState(State.HIT);
            }
        }

    }

    public void Die()
    {

        mImgHp.enabled = false;
        mAnimator.SetTrigger("IsDead");
  
       // 아이템 드랍 관련 처리 추가 필요
       // 
       //
    }

    
    public bool IsAttackAtionEnd(float time)
    {
        mAttackActionTime -= time;

        if (mAttackActionTime < 0)
        {
            Debug.Log("공격 연출 끝");
            mState = State.ATTACK_END;
            mAttackActionTime = 2.0f;
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool IsHitAtionEnd(float time)
    {
        mHitActionTime -= time;

        if (mHitActionTime < 0)
        {
            SetState(State.IDLE);
            mHitOppositePos = Vector3.zero;
            mHitActionTime = 1.0f;
            return true;
        }
        else
        {
            return false;
        }
    }




    public void SetState(State state)
    {
        mState = state;
    }



    public void Reposition()
    {
        transform.position = mStartPos;

    }
}
