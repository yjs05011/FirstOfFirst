using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemMiniBoss : Monster
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
            this.Movement(mWanderPosition, mSpeed, true);
            this.UpdateAnimationDirection(this.transform.position, mTarget.transform.position);
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

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    // 20% Ȯ���� ��ȸ�� �Ѵ�.
                    if (Random.Range(0, 1000) < 500)
                    {
                        SmashAttack();
                    }
                    else
                    {
                        SwardAttack();
                    }
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
                // �̵� �Ѵ�. (�̵� �Ұ��� ��ȸ)
                this.Movement(mTarget.transform.position, mSpeed, true);
                this.UpdateAnimationDirection(this.transform.position, mTarget.transform.position);
            }
        }
        // ���� ��Ÿ�� (���� ��)
        else if (mCurrState == State.AttackCooltime)
        {
            mAttackTime += Time.deltaTime;
            if (mAttackInterval <= mAttackTime)
            {
                mAttackTime = 0.0F;
                this.SetState(State.Attack);
            }
        }
        // ��� ���� 
        else if (mCurrState == State.Die)
        {           
            // ���Ͱ� ��ġ�� ���������� ���� ���� ����
            if(mStage)
            {
                mStage.AddDieMonsterCount();
            }

            // die ���� ���µ� �����ϰ� �Ǹ鼭 ������°� ����.

            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);
        }
    }

    public void UpdateAnimationDirection(Vector3 origin, Vector3 destination)
    {
        Vector3 distance = (destination - origin).normalized;

        DungeonUtils.Direction direction = DungeonUtils.Convert2CardinalDirectionsEnum(distance);
        switch(direction)
        {
            case DungeonUtils.Direction.Up: 
                { 
                    mAnimator.SetFloat("X", 0);
                    mAnimator.SetFloat("Y", 1);
                    break; 
                }
            case DungeonUtils.Direction.Down:
                {
                    mAnimator.SetFloat("X", 0);
                    mAnimator.SetFloat("Y", -1);
                    break;
                }
            case DungeonUtils.Direction.Left:
                {
                    mAnimator.SetFloat("X", -1);
                    mAnimator.SetFloat("Y", 0);
                    break;
                }
            case DungeonUtils.Direction.Right:
                {
                    mAnimator.SetFloat("X", 1);
                    mAnimator.SetFloat("Y", 0);
                    break;
                }
        }
    }

    public void SmashAttack()
    {
        mAnimator.SetTrigger("SmashAttack");
        this.SetState(State.Wait);
    }

    public void SwardAttack()
    {
        mAnimator.SetTrigger("SwordAttack");
        this.SetState(State.Wait);
    }


    public override void OnAnimationEvent(string name)
    {
        bool isDamage = "Damage".Equals(name, System.StringComparison.OrdinalIgnoreCase);
        bool isFinish = "Finish".Equals(name, System.StringComparison.OrdinalIgnoreCase);

        if (isDamage)
        {
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    mTarget.OnDamage(this.GetDamage());
                    return;
                }
            }
        }
        else if(isFinish)
        {
            this.SetState(State.AttackCooltime);
        }
        else
        {
            Debug.LogErrorFormat("Unknown Event Name:{0}", name);
        }
    }
}
