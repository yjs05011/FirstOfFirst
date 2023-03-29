using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.GridLayoutGroup;

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

        // 대기 상태
        if (mCurrState == State.Ready)
        {

            // 범위 안에 들어왔는지 체크하고, 범위안이면, wake up 애니메이션 출력 후 idle 로 상태 변경
            if (IsInTraceScope())
            {
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
        // wait중에도 타이머는 동작하게 
        else if(mCurrState == State.Wait)
        {
            mTimer += Time.deltaTime;

            if (mTimer >= mWaveCoolTime)
            {
                mTimer = 0.0f;
                WaveAttack();
            }
        }
        // 공격 상태
        else if (mCurrState == State.Attack)
        {
            // 추적 영역을 벗어난 경우 대기 상태로 바꾼다.
            if (!IsInTraceScope())
            {
                this.SetState(State.Idle);
                return;
            }

            // 공격 영역 안에 있는 경우 공격 한다.
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
                                      
                   if (Random.Range(0, 1000) < 500)
                   {
                       PunchAttack();
                   }
                   else
                   {
                       if (Random.Range(0, 1000) < 500)
                       {
                          // RockSpawnAttack();
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
        // 공격 쿨타임 (공격 후)
        else if (mCurrState == State.AttackCooltime)
        {
            mAttackTime += Time.deltaTime;
            if (mAttackInterval <= mAttackTime)
            {
                mAttackTime = 0.0F;
                this.SetState(State.Attack);
            }
        }
        // 사망 상태 

        else if (mCurrState == State.Die)
        {

            // 애니메이션 다이 
            mAnimator.SetTrigger("Dead");

            // 컬라이더 off
            //this.GetComponent<Collider2D>().enabled = false;

            //hp bar 가 없음.
            //mHpBar.SetActive(false);

            // 몬스터가 위치한 스테이지에 다이 정보 갱신
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }

            // 처치 몬스터 리스트에 추가
            DungeonManager.Instance.KillMonsterAdd(mMonsterId);

            // 사망 로직 처리 후에 반드시 State.None 으로 보내서 더이상 업데이트문을 타지 않도록 상태 변경.
            this.SetState(State.None);

            // 던전 퇴장 UI 연결 전 테스트용 코드
            TestDungeonExit();
        }
    }

    // 던전 나가기 UI 연결 전 던전 탈출 테스트 함수 
    public void TestDungeonExit()
    {
        DungeonGenerator.Instance.Init();
        DungeonGenerator.Instance.OnDestroyMySelf();
        GFunc.LoadScene("VillageScene");
    }


    public void PunchAttack()
    {
        // 펀치 스킬 시전 애니메이션 시작 트리거 
        mAnimator.SetTrigger("LunchArm");
        this.SetState(State.Wait);
        // -> OnAnimation:OnGolemKingPunchEnd 시점에서 idle 상태로 다시 변경
        Debug.Log(" PunchAttack()");
    }

    public void WaveAttack()
    {
        // wave 포지션에 wave 오브젝트 출력
        WaveSkill();
        // wave 는 시전 하고 골렘킹은 
        //this.SetState(State.Attack);
        Debug.Log(" WaveAttack()");
    }

    public void StickyAttack()
    {
        // 스티키 스킬 시전 애니메이션 시작 트리거 
        mAnimator.SetTrigger("StickyArm");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish 로 Attack 상태로 다시 변경
        Debug.Log(" StickyAttack()");
    }

    public void RockSpawnAttack()
    {
        // 락스폰 스킬 시전 애니메이션 시작 트리거 
        mAnimator.SetTrigger("RockSpawn");
        this.SetState(State.Wait);
        Debug.Log(" RockSpawnAttack()");
        // -> OnAnimation:OnGolemKingRockSpwan 로 Rock 생성 , 스폰 모션 끝나는 시점(OnGolemKingRockSpwanFinish)에  Attack 상태로 다시 변경
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
        // 펀치 공격 시전 애니메이션 - punch arm 하늘에 날린 시점에 호출. 
        if ("OnGolemKingPunchStart".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            RepeatPunchAttack();
        }
        // 펀치 공격 종료 후 팔 다시 원위치 recover arm 이 끝난 시점에 호출
        else if ("OnGolemKingPunchEnd".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("OnGolemKingPunchEnd");
            this.SetState(State.Attack);
        }
        // 락 스폰 시전 애니메이션 - 바닥 찍는 시점에 호출 
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
        // 락 스폰 애니메이션 끝난 시점.
        else if ("OnGolemKingRockSpwanFinish".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            this.SetState(State.Attack);
        }
        // 스티키 암 스킬 종료.
        else if("StickyArmAttackFinished".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            this.SetState(State.Attack);
        }
        // 다이 애니메이션 끝나면 호출
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
        if(mPunchCount >= PUNCH_COUNT)
        {
            mPunchCount = 0;

            mAnimator.SetTrigger("RecoverArm");
            Debug.Log("recoverarm start");
            return;
        }
        ++mPunchCount;


        // 공격 영역 안에 있는 경우 공격 한다.
        if (IsInAttackRange())
        {
            if (mTarget)
            {

                // 타겟위치 잡고.
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


    protected IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        mPunchCount = 0;
        this.SetState(State.Attack);
    }

}
