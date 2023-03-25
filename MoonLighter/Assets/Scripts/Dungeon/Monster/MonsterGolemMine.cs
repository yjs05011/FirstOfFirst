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
            mAnimator.SetBool("IsMove", false);

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
            mAnimator.SetBool("IsMove", true);

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
            mAnimator.SetBool("IsMove", true);
            // ���� ������ ��� ��� ��� ���·� �ٲ۴�.
            if (!IsInTraceScope())
            {
                this.SetState(State.Idle);
                return;
            }

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    mTarget.OnDamage(this.mDamage);
                }
                else
                {
                    this.SetState(State.Idle);
                    return;
                }
            }
            // ���� ������ �ƴ� ��� ���� �Ѵ�.
            else
            {
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

            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);

            // ���� ���� ���� ���ֿ��ٰ� add �ʿ�.
        }
    }
}
