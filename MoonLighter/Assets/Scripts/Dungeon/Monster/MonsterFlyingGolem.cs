using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlyingGolem : Monster
{
    public Animator mAnimator;

    public override void Update()
    {
        base.Update();

        // ��� ����
        if (mCurrState == State.Idle)
        {

            // ���� �������� üũ�ϰ� ���������ϸ� ���� ���·� �ٲ۴�.
            if (IsInTraceScope())
            {
                this.SetState(State.Attack);
                return;
            }

            // 20% Ȯ���� ��ȸ�� �Ѵ�.
            if (Random.Range(0, 1000) < 200)
            {
                mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                this.SetState(State.Wander);
            }
        }
        // ��ȸ ���� ( �Ұ� ���� ��� )
        else if (mCurrState == State.Wander)
        {
           
            // ��ȸ �������� �����ߴ��� üũ�Ѵ�.
            if (Vector3.Distance(transform.position, mWanderPosition) < Mathf.Epsilon)
            {
                this.SetState(State.Idle);
                return;
            }

            // ��ȸ �Ѵ�.
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, mWanderPosition, mSpeed * Time.deltaTime);
            // ���� �ڽ��� ������ �� �ִ� ������ �Ѿ���� ��� �ߴ��Ѵ�.
            if (!IsMovablePosition(nextPosition))
            {
                mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                this.SetState(State.Wander);
                return;
            }
            transform.position = nextPosition;
        }
        // ���� ����
        else if (mCurrState == State.Attack)
        {
            
            // ���� ������ ��� ��� ��� ���·� �ٲ۴�.
            if (!IsInTraceScope())
            {
                mAnimator.SetBool("IsAttack", false);
                this.SetState(State.Idle);
                return;
            }

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {

                mAnimator.SetBool("IsAttack", true);
                this.SetState(State.Wait);

            }
            // ���� ������ �ƴ� ��� ���� �Ѵ�.
            else
            {
                mAnimator.SetBool("IsAttack", false);
                Vector3 nextPosition = Vector3.MoveTowards(transform.position, mTarget.transform.position, mSpeed * Time.deltaTime);
                if (!IsMovablePosition(nextPosition))
                {
                    mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                    this.SetState(State.Wander);
                    return;
                }

                transform.position = nextPosition;
            }
        }
        // ��� ���� 

        else if (mCurrState == State.Die)
        {
            // die ���� ���µ� �����ϰ� �Ǹ鼭 ������°� ����.

            // �ִϸ��̼� ���� 
            mAnimator.SetTrigger("Dead");
            // ���Ͱ� ��ġ�� ���������� ���� ���� ����
            mStage.AddDieMonsterCount();
            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);

          
        }
    }

    public override void OnAnimationEvent(string name)
    {
        if ("Attack".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    mAnimator.SetBool("IsAttack", true);
                    mTarget.OnDamage(this.mDamage);
                }

            }
        }
        else if ("AttackBlockOff".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            mIsAttackBlock = false;
        }
        else if ("AttackBlockOn".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            mIsAttackBlock = true;

        }
        else if ("FinishAttack".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            mIsAttackBlock = true;
            this.SetState(State.Idle);
            mAnimator.SetBool("IsAttack", false);
        }
        else
        {
            Debug.LogErrorFormat("Unknown Event Name:{0}", name);
        }

    }
}
