using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class MonsterGolemKing : Monster
{
    private int mPunchCount = 0;
    public const int PUNCH_COUNT = 5;
    public Transform mWaveStartPosition = null;
    public float mWaveCoolTime = 20.0f;
    private float mTimer = 0.0f;

    public override void Update()
    {
        base.Update();

        // ��� ����
        if (mCurrState == State.Ready)
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
            this.SetState(State.Attack);

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
                    mTimer += Time.deltaTime;

                    if(mTimer >= mWaveCoolTime )
                    {
                        mTimer = 0.0f;
                        WaveAttack();
                    }
                    //PunchAttack();
                  

                    //// �ߵ� ��ų ���� 
                    //if (Random.Range(0, 1000) < 500)
                    //{
                    //    if (Random.Range(0, 1000) < 500)
                    //    {
                    //        PunchAttack();
                    //    }
                    //    else
                    //    {
                    //        RockSpawnAttack();
                    //    }
                    //
                    //}
                    //else
                    //{
                    //    if (Random.Range(0, 1000) < 500)
                    //    {
                    //        WaveAttack();
                    //    }
                    //    else
                    //    {
                    //        StickyAttack();
                    //    }
                    //
                    //}
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
        // wave �����ǿ� wave ������Ʈ ���
        WaveSkill();
        // wave �� ���� �ϰ� ��ŷ�� �ٽ� idle ���·� ����
        this.SetState(State.Idle);

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
        // -> OnAnimation:OnGolemKingRockSpwan �� Rock ���� 
    }

    public override void OnAnimationEvent(string name)
    {
        // ��ġ ���� ���� �ִϸ��̼� - punch arm �ϴÿ� ���� ������ ȣ��. 
        if ("OnGolemKingPunchStart".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            RepeatPunchAttack();
        }
        // �� ���� ���� �ִϸ��̼� - �ٴ� ��� ������ ȣ�� 
        else if ("OnGolemKingRockSpwan".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            for (int i = 0; i < 40; i++)
            {
                GameObject instance = GameObject.Instantiate<GameObject>(FindSkillPreset("RockSpwanPreset"));
                instance.transform.parent = this.mStage.transform;

                if (instance)
                {
                    Rock rock = instance.GetComponent<Rock>();
                    rock.SetData(this, this.IsRandomPositionInsidePolygonCollider((PolygonCollider2D)this.FindCollider2D("RockSpawnArea")));

                }
            }
        }
        else
        {
            Debug.LogErrorFormat("Unknown Event Name:{0}", name);
        }
    }

    public void WaveSkill()
    {
        GameObject preset = this.FindSkillPreset("WavePreset");
        if (preset)
        {
            GameObject clone = GameObject.Instantiate<GameObject>(preset);
            WaveAttackSkill skill = clone.GetComponent<WaveAttackSkill>();
            skill.SetData(this, mWaveStartPosition, 20.0f);
            skill.transform.parent = this.mStage.mBoard.transform;
        }
    }



    public void RepeatPunchAttack()
    {
        if(mPunchCount >= PUNCH_COUNT)
        {
            mPunchCount = 0;

            mAnimator.SetTrigger("RecoverArm");
            SetState(State.Idle);
            return;
        }
        ++mPunchCount;


        // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
        if (IsInAttackRange())
        {
            if (mTarget)
            {

                // Ÿ����ġ ���.
                GameObject instance = GameObject.Instantiate<GameObject>(this.FindSkillPreset("PunchSpawnPreset"));
                instance.transform.position = mTarget.transform.position;
                instance.transform.parent = this.mStage.transform;
                PunchAttackSkill skill = instance.GetComponent<PunchAttackSkill>();
                skill.SetData(this, 100.0f);

            }
            else
            {
                this.SetState(State.Idle);
                return;
            }

            return;
        }
    }


    protected IEnumerator ShotDelayCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        mPunchCount = 0;
        this.SetState(State.Attack);
    }

}
