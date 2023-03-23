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

    // �ϴ� ���� �۾� �ȵ������� ���Ƿ� ����.


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

        // �������϶��� ���� ������ ���� ���� return;
        if (isAttackAtion || isHitAction || isDead)
        {
            return;
        }

        if (mTarget != null)
        {
            //Ÿ���� ���ݹ����ȿ� �ִٸ� ���ݻ��·� �ٲ۴�.
            if (Vector3.Distance(mTarget.position, transform.position) < mAttackRadius)
            {

                mState = State.ATTACK_START;
                mAttackTargetPos = mTarget.position;
                mAttackStartPos = this.transform.position;


            }
            // ���� ���� ���� ���� �� ��ġ�� �̵� ���� �����϶�,
            else if (Vector3.Distance(mStartPos, transform.position) < mAreaRadius)
            {
                // Ÿ���� �̵����ɹ����ȿ������� �̵�(�i�ư���) ���·� �ٲ۴�
                if (Vector3.Distance(mTarget.position, transform.position) < mMoveRadius)
                {

                    mState = State.MOVE;
                }
                else
                {   // player �� ���� �濡�� �������, ������������ �ٽ� ��ġ �ʿ�

                    // �÷��̾ �������� �־������ ���̵�.
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
                        // ���� ��������� �ִϸ��̼� ��� �ʿ�
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
                    else // ���� ���� �������� ��
                    {
                        // ���� ��������� �ִϸ��̼� ���� �ʿ�
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
                    // knockback ���� ��� �� ���� ����.
                    if (IsHitAtionEnd(Time.deltaTime))
                    {
                        SetState(State.IDLE);
                    }
                    break;
                }
            case State.DIE:
                {
                    // �״� ���� �߰��ؾ���. ��! �ϰ� �������...
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
            // �÷��̾� �����ؾ��ϴ� �÷��̾� ��Ʈ�� ��ũ��Ʈ ã�Ƽ� on damage �Լ��� ������ ����. 
            mTarget.GetComponent<PlayerAct>().OnDamage(mBaseAttack);
        }
    }




    public override void OnDamage(float damage)
    {
        
        if (mState != State.DIE)
        {
            // �������� ��������� ���� ó�� �ʿ�
        

            // knockback ���� �з��� ��ǥ�� �����صΰ�
            Vector2 opposite = (mTarget.position - this.transform.position);
            opposite = opposite.normalized * mThrust;
            mHitOppositePos = transform.position - (Vector3)opposite;

            // ������ ����
            mHP -= damage;
            mImgHp.fillAmount = mHP / mMaxHP;
         



            if (mHP <= 0)
            {
                SetState(State.DIE);
                Die();
            }
            else
            {
                // ���� hit ��ȯ
                SetState(State.HIT);
            }
        }

    }

    public void Die()
    {

        mImgHp.enabled = false;
        mAnimator.SetTrigger("IsDead");
  
       // ������ ��� ���� ó�� �߰� �ʿ�
       // 
       //
    }

    
    public bool IsAttackAtionEnd(float time)
    {
        mAttackActionTime -= time;

        if (mAttackActionTime < 0)
        {
            Debug.Log("���� ���� ��");
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
