using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMine : DungeonMonster
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
                    // �ֺ� �������� �������ε�... �ٸ� ���ʹ�..?
                    mTarget.OnDamage(this.mDamage);

                    this.SetState(State.Die);
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
            // die ���� ���µ� �����ϰ� �Ǹ鼭 ������°� ����.

            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);

            // ���� ���� ���� ���ֿ��ٰ� add �ʿ�.
        }
    }
}
