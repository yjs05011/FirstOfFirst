using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlyingGolem : Monster
{
   
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
            this.Movement(mWanderPosition, mSpeed, false);
        }
        // ���� ����
        else if (mCurrState == State.Attack)
        {
            
            // ���� ������ ��� ��� ��� ���·� �ٲ۴�.
            if (!IsInTraceScope())
            {
                this.SetState(State.Idle);
                return;
            }

            if(IsInAttackRange())
            {
                mAnimator.SetTrigger("Attack");
                return;
            }

            // �뽬 ���� �ȿ� Ÿ�����ִ� ��� ���ݸ���� �����Ѵ�.
            if (IsInDashRange())
            {
                mAnimator.SetTrigger("Attack");
                this.SetDashState(mTarget.transform.position);
                return;
            }

            // �̵� �Ѵ�. (�̵� �Ұ��� ��ȸ)
            this.Movement(mTarget.transform.position, mSpeed, false);
        }
        // ��� �ߵ�
        else if(mCurrState == State.Dash)
        {
            this.Movement(mDashDestination, mDashSpeed, false);
        }
        // ��� ���� 

        else if (mCurrState == State.Die)
        {
            // die ���� ���µ� �����ϰ� �Ǹ鼭 ������°� ����.
            // �ö��̴� off
            this.GetComponent<Collider2D>().enabled = false;
            //hp bar hide
            mHpBar.SetActive(false);
            // �ִϸ��̼� ���� 
            mAnimator.SetTrigger("Dead");
            // ���Ͱ� ��ġ�� ���������� ���� ���� ����
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }

            // óġ ���� ����Ʈ�� �߰�
            DungeonManager.Instance.KillMonsterAdd(this);
            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);

          
        }
    }

    public override void OnAnimationEvent(string name)
    {
        Debug.LogFormat("MonsterFlyingGolem : {0}", name);

        if ("Attack".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    mTarget.OnDamage(mMonsterId,this.mDamage);
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
        }
        else
        {
            Debug.LogErrorFormat("Unknown Event Name:{0}", name);
        }

    }
}
