using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MonsterGolemKing : Monster
{
    private int mPunchCount = 0;
    public const int PUNCH_COUNT = 5;
    public Transform mWaveStartPosition = null;
    private float mWaveCoolTime = 10.0f;
    public float mTimer = 0.0f;


    public override void Update()
    {
        base.Update();

        // ��� ����
        if (mCurrState == State.Ready)
        {

            // ���� �ȿ� ���Դ��� üũ�ϰ�, �������̸�, wake up �ִϸ��̼� ��� �� idle �� ���� ����
            if (IsInTraceScope())
            {
                UiManager.Instance.mIsBossHpVisible = true;
                UiManager.Instance.BossMaxHp(this.mMaxHP);
                mAnimator.SetTrigger("WakeUp");
                this.SetState(State.Idle);
                return;
            }

        }
        else if (mCurrState == State.Idle)
        {

            if (IsInTraceScope())
            {
                this.SetState(State.Attack);
                return;
            }

        }
        // wait�߿��� Ÿ�̸Ӵ� �����ϰ� 
        else if (mCurrState == State.Wait)
        {
            mTimer += Time.deltaTime;

            if (mTimer >= mWaveCoolTime)
            {
                mTimer = 0.0f;
                WaveAttack();
            }
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

                    if (mTimer >= mWaveCoolTime)
                    {
                        mTimer = 0.0f;
                        WaveAttack();
                    }

                    if (Random.Range(0, 1000) < 350)
                    {
                        PunchAttack();
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

            // �ö��̴� off
            //this.GetComponent<Collider2D>().enabled = false;


            // ���Ͱ� ��ġ�� ���������� ���� ���� ����
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }

            // óġ ���� ����Ʈ�� �߰�
            DungeonManager.Instance.KillMonsterAdd(mMonsterId);

            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);


        }
    }




    public void PunchAttack()
    {
        // ��ġ ��ų ���� �ִϸ��̼� ���� Ʈ���� 
        mAnimator.SetTrigger("LunchArm");
        this.SetState(State.Wait);
        // -> OnAnimation:OnGolemKingPunchEnd �������� idle ���·� �ٽ� ����
        Debug.Log(" PunchAttack()");
    }

    public void WaveAttack()
    {
        // wave �����ǿ� wave ������Ʈ ���
        WaveSkill();
        // wave �� ���� �ϰ� ��ŷ�� 
        //this.SetState(State.Attack);
        Debug.Log(" WaveAttack()");
    }

    public void StickyAttack()
    {
        // ��ƼŰ ��ų ���� �ִϸ��̼� ���� Ʈ���� 
        mAnimator.SetTrigger("StickyArm");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish �� Attack ���·� �ٽ� ����
        Debug.Log(" StickyAttack()");
    }

    public void RockSpawnAttack()
    {
        // ������ ��ų ���� �ִϸ��̼� ���� Ʈ���� 
        mAnimator.SetTrigger("RockSpawn");
        this.SetState(State.Wait);
        Debug.Log(" RockSpawnAttack()");
        // -> OnAnimation:OnGolemKingRockSpwan �� Rock ���� , ���� ��� ������ ����(OnGolemKingRockSpwanFinish)��  Attack ���·� �ٽ� ����
    }

    public override void OnAnimationEvent(string name)
    {
        if (mCurrState == State.Die || mCurrState == State.None)
        {
            if (mAnimator)
            {
                mAnimator.StopPlayback();
            }
            return;
        }
        // ��ġ ���� ���� �ִϸ��̼� - punch arm �ϴÿ� ���� ������ ȣ��. 
        if ("OnGolemKingPunchStart".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            RepeatPunchAttack();
        }
        // ��ġ ���� ���� �� �� �ٽ� ����ġ recover arm �� ���� ������ ȣ��
        else if ("OnGolemKingPunchEnd".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("OnGolemKingPunchEnd");
            this.SetState(State.Attack);
        }
        // �� ���� ���� �ִϸ��̼� - �ٴ� ��� ������ ȣ�� 
        else if ("OnGolemKingRockSpwan".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            for (int i = 0; i < 40; i++)
            {
                //GameObject instance = GameObject.Instantiate<GameObject>(FindSkillPreset("RockSpwanPreset"));
                //instance.transform.parent = this.mStage.transform;
                List<GameObject> RockPreserList = FindSkillPresetList("RockSpwanPresetList");
                int randomIndex = Random.Range(0, RockPreserList.Count);
                GameObject instance = GameObject.Instantiate(RockPreserList[randomIndex]);
                instance.transform.parent = this.mStage.transform;

                if (instance)
                {
                    Rock rock = instance.GetComponent<Rock>();
                    rock.SetData(this, this.IsRandomPositionInsidePolygonCollider((PolygonCollider2D)this.FindCollider2D("RockSpawnArea")), 30.0f);
                    // 
                    //rock.mAnimator.SetFloat("RockType", Random.Range(0.0f, 6.0f));

                }

            }

        }
        // �� ���� �ִϸ��̼� ���� ����.
        else if ("OnGolemKingRockSpwanFinish".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            this.SetState(State.Attack);
        }
        // ��ƼŰ �� ��ų ����.
        else if ("StickyArmAttackFinished".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            this.SetState(State.Attack);
        }
        // ���� �ִϸ��̼� ������ ȣ��
        else if ("DeadFinish".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
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
        if (mPunchCount >= PUNCH_COUNT)
        {
            mPunchCount = 0;

            mAnimator.SetTrigger("RecoverArm");
            Debug.Log("recoverarm start");
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
        else
        {
            mPunchCount = 0;
            mAnimator.SetTrigger("RecoverArm");
        }
    }


    protected IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        mPunchCount = 0;
        this.SetState(State.Attack);
    }

}
