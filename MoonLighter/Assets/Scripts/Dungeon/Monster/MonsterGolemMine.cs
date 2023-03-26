using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemMine : Monster
{
    public override void Update()
    {
        base.Update();

        // ��� ����
        if (mCurrState == State.Idle)
        {
            // ���� �������� üũ�ϰ� ���������ϸ� wake ��, ���� ���·� �ٲ۴�.
            if (IsInTraceScope())
            {
                mAnimator.SetTrigger("Wake");
                this.SetState(State.Attack);
                return;
            }

        }

        // ���� ����
        else if (mCurrState == State.Attack)
        {

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    // ���� ��� ����
                    mAnimator.SetTrigger("Jump");
                    // ��� ���·� ��ȯ
                    this.SetDashState(mTarget.transform.position);
                }

            }
            // ���� ������ �ƴ� ��� ���� �Ѵ�.
            else
            {
                mAnimator.SetTrigger("Move");
                // �̵� �Ѵ�. (�̵� �Ұ��� ��ȸ)
                this.Movement(mTarget.transform.position, mSpeed, true);
            }
        }
        // ��� �ߵ�
        else if (mCurrState == State.Dash)
        {
            //��� �ӵ��� �̵�.
            this.Movement(mDashDestination, mDashSpeed, false);
        }
        // ��� ���� 
        else if (mCurrState == State.Die)
        {
            if (mStage)
            {
                // ���Ͱ� ��ġ�� ���������� ���� ���� ����
                mStage.AddDieMonsterCount();

            }
            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);
        }
    }

    public override void OnAnimationEvent(string name)
    {
        if ("Explosion".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            // Ÿ�� ���� ������ ������
            mTarget.OnDamage(this.mDamage);
            // �ֺ� ���� ������ �ֱ�...

        }
        if ("FinishExplosion".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            // ���� --> ���� ���� ������ die �� ���� ��ȯ��)
            this.SetState(State.Die);
            mAnimator.SetTrigger("Dead");
        }
    }
}
