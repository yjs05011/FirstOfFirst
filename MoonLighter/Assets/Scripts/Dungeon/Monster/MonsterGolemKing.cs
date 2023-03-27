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

        // 대기 상태
        if (mCurrState == State.None)
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
            mAnimator.SetTrigger("WakeUp");
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
                    // 발동 스킬 선택 
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
            // 몬스터가 위치한 스테이지에 다이 정보 갱신
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }
            // 사망 로직 처리 후에 반드시 State.None 으로 보내서 더이상 업데이트문을 타지 않도록 상태 변경.
            this.SetState(State.None);


        }
    }


    public void PunchAttack()
    {
        // 펀치 스킬 시전 애니메이션 시작 트리거 
        mAnimator.SetTrigger("LunchArm");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish 로 Attack 상태로 다시 변경
    }

    public void WaveAttack()
    {
        // wave 포지션에 wave 오브젝트 출력??
        this.SetState(State.Wait);
        // -> OnAnimation:Finish 로 Attack 상태로 다시 변경
    }

    public void StickyAttack()
    {
        // 스티키 스킬 시전 애니메이션 시작 트리거 
        mAnimator.SetTrigger("StickyArm");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish 로 Attack 상태로 다시 변경
    }

    public void RockSpawnAttack()
    {
        // 락스폰 스킬 시전 애니메이션 시작 트리거 
        mAnimator.SetTrigger("RockSpawn");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish 로 Attack 상태로 다시 변경
    }

    public override void OnAnimationEvent(string name)
    {
        // 펀치 공격 시전 애니메이션 - punch arm 하늘에 날린 시점에 호출. 
        if ("OnGolemKingPunchStart".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

            // 공격 영역 안에 있는 경우 공격 한다.
            if (IsInAttackRange())
            {
                if (mTarget)
                {

                    // 타겟위치 잡고.
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

        // 락 스폰 시전 애니메이션 - 바닥 찍는 시점에 호출 
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
