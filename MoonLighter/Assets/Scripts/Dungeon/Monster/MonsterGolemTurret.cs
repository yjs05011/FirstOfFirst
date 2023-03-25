using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemTurret : Monster
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
       
        }
       
        // ���� ����
        else if (mCurrState == State.Attack)
        {
            mAnimator.SetBool("IsShot", true);
            // ���� ������ ��� ��� ��� ���·� �ٲ۴�.
            if (!IsInTraceScope())
            {
                mAnimator.SetBool("IsShot", false);
                this.SetState(State.Idle);
                return;
            }

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    if (mProjectilePreset)
                    {
                        GameObject instance = GameObject.Instantiate<GameObject>(mProjectilePreset);
                        instance.transform.position = this.transform.position;
                        if (instance)
                        {
                            Vector3 direction = (mTarget.transform.position - this.transform.position).normalized;

                            Projectile projectile = instance.GetComponent<Projectile>();
                            projectile.SetData(this, DungeonUtils.Convert2CardinalDirections(direction));
                        }
                        this.SetState(State.AttackCooltime);
                    }
                    else
                    {
                        mTarget.OnDamage(this.mDamage);
                        this.SetState(State.AttackCooltime);
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
        // ���� ��Ÿ�� (���� ��)
        else if(mCurrState == State.AttackCooltime)
        {
            mAttackTime += Time.deltaTime;
            if(mAttackInterval <= mAttackTime)
            {
                mAttackTime = 0.0F;
                this.SetState(State.Attack);
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
