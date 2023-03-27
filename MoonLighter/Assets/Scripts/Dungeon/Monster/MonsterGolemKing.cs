using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemKing : Monster
{
    public int mPunchCount = 0;
    public const int PUNCH_COUNT = 5;

    public override void Update()
    {
        base.Update();

        // ��� ����
        if (mCurrState == State.None)
        {

            // ���� �ȿ� ���Դ��� üũ�ϰ�, �������̸�, wake up �ִϸ��̼� ��� �� idle �� ���� ����
            if (IsInTraceScope())
            {
                mAnimator.SetTrigger("WakeUp"); 
                this.SetState(State.Idle);
                return;
            }

        }

        else if (mCurrState == State.Idle)
        {
            mAnimator.SetTrigger("WakeUp");
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
                    // �ߵ� ��ų ���� 
                    if (Random.Range(0, 1000) < 500)
                    {
                        if (Random.Range(0, 1000) < 500)
                        {
                            PunchAttack();
                        }
                        else
                        {
                            WaveAttack();
                        }

                    }
                    else
                    {
                        if (Random.Range(0, 1000) < 500)
                        {
                            RockSpawnAttack();
                        }
                        else
                        {
                            StickyAttack();
                        }

                    }
                }
                else
                {
                    this.SetState(State.Idle);
                    return;
                }
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
            
            // �ִϸ��̼� ���� 
            mAnimator.SetTrigger("Dead");
            // ���Ͱ� ��ġ�� ���������� ���� ���� ����
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }
            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);


        }
    }


    public void PunchAttack()
    {
        // ��ġ ��ų ���� �ִϸ��̼� ���� Ʈ���� 
        mAnimator.SetTrigger("LunchArm");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish �� Attack ���·� �ٽ� ����
    }

    public void WaveAttack()
    {
        // wave �����ǿ� wave ������Ʈ ���??
        this.SetState(State.Wait);
        // -> OnAnimation:Finish �� Attack ���·� �ٽ� ����
    }

    public void StickyAttack()
    {
        // ��ƼŰ ��ų ���� �ִϸ��̼� ���� Ʈ���� 
        mAnimator.SetTrigger("StickyArm");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish �� Attack ���·� �ٽ� ����
    }

    public void RockSpawnAttack()
    {
        // ������ ��ų ���� �ִϸ��̼� ���� Ʈ���� 
        mAnimator.SetTrigger("RockSpawn");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish �� Attack ���·� �ٽ� ����
    }

    public override void OnAnimationEvent(string name)
    {
        // ��ġ ���� ���� �ִϸ��̼� - punch arm �ϴÿ� ���� ������ ȣ��. 
        if ("OnGolemKingPunchStart".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {
                if (mTarget)
                {

                    // Ÿ����ġ ���.
                    Vector3 targetPosition = (mTarget.transform.position).normalized;
                   
                    if (mProjectilePreset)
                    {
                        GameObject instance = GameObject.Instantiate<GameObject>(mProjectilePreset);
                        instance.transform.position = targetPosition;
                        instance.transform.parent = this.mStage.transform;
                        if (instance)
                        {
                            // PunchAttackSkill punch = instance.GetComponent<PunchAttackSkill>();
                            // punch.SetData(this, DungeonUtils.Convert2CardinalDirections(targetPosition));

                        }

                        this.SetState(State.AttackCooltime);
                    }
                    else
                    {
                        Debug.LogErrorFormat("mProjectilePreset is Null");
                    }

                }
                else
                {
                    this.SetState(State.Idle);
                    return;
                }

                return;
            }
            else
            {
               Debug.LogErrorFormat("Unknown Event Name:{0}", name);
            }
        }

        // �� ���� ���� �ִϸ��̼� - �ٴ� ��� ������ ȣ�� 
        if ("OnGolemKingRockSpwan".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

        }
    }


    protected IEnumerator ShotDelayCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        mPunchCount = 0;
        this.SetState(State.Attack);

    }

}
