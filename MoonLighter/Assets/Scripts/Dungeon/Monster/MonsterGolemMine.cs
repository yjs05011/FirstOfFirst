using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemMine : Monster
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
                    mAnimator.SetTrigger("Jump");

                }

            }
            // ���� ������ �ƴ� ��� ���� �Ѵ�.
            else
            {
                mAnimator.SetTrigger("Move");
                Vector3 nextPosition = Vector3.MoveTowards(transform.position, mTarget.transform.position, mSpeed * Time.deltaTime);
                if (!IsMovablePosition(nextPosition))
                {
                    mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                    this.SetState(State.Attack);
                    return;
                }

                transform.position = nextPosition;
            }
        }
        // ��� ���� 

        else if (mCurrState == State.Die)
        {
            // �ִϸ��̼� ���� 
            mAnimator.SetTrigger("Dead");

            // ���Ͱ� ��ġ�� ���������� ���� ���� ����
            mStage.AddDieMonsterCount();

            // die ���� ���� (die �ִϸ��̼��� ���� ����, ���� �ϸ鼭 ������� �ִϸ��̼���� �� die �� ���� ��ȯ��)
            //GameObject.Destroy(this.gameObject);

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
            this.SetState(State.Die);

        }
    }
}
