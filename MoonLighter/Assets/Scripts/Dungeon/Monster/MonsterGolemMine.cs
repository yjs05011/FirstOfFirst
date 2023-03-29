using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemMine : Monster
{
    public override void Update()
    {
        base.Update();

        // 대기 상태
        if (mCurrState == State.Ready)
        {
            // 추적 가능한 영역에 들어오면, wake 후, idle 상태로 바꾼다.
            if (IsInTraceScope())
            {
                mAnimator.SetTrigger("Wake");
                this.SetState(State.Idle);
                return;
            }

        }
        else if(mCurrState == State.Idle)
        {
            // 추적 가능한 하면 공격 상태로 변경.
            if (IsInTraceScope()) 
            {
                this.SetState(State.Attack);
                return;
            }
            else
            {
                mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                this.SetState(State.Wander);
            }
        
        }
        // 배회 상태 ( wake 후 추적 가능한 상태가 아닌경우 )
        else if (mCurrState == State.Wander)
        {
            // 배회 목적지에 도착했는지 체크한다.
            if (Vector3.Distance(transform.position, mWanderPosition) < Mathf.Epsilon)
            {
                this.SetState(State.Idle);
                return;
            }

            // 배회 한다.
            this.Movement(mWanderPosition, mSpeed, true);
        }
        // 공격 상태
        else if (mCurrState == State.Attack)
        {

            // 공격 영역 안에 있는 경우 공격 한다.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    // 점프 모션 시작
                    mAnimator.SetTrigger("Jump");
                    // 대시 상태로 전환
                    this.SetDashState(mTarget.transform.position);
                }

            }
            // 공격 역역이 아닌 경우 추적 한다.
            else
            {
                mAnimator.SetTrigger("Move");
                // 이동 한다. (이동 불가시 배회)
                this.Movement(mTarget.transform.position, mSpeed, true);
            }
        }
        // 대시 발동
        else if (mCurrState == State.Dash)
        {
            //대시 속도로 이동.
            this.Movement(mDashDestination, mDashSpeed, false);
        }
        // 사망 상태 
        else if (mCurrState == State.Die)
        {
            mAnimator.SetTrigger("Dead");
            //hp bar hide
            mHpBar.SetActive(false);
            // 컬라이더 off
            this.GetComponent<Collider2D>().enabled = false;
            if (mStage)
            {
                // 몬스터가 위치한 스테이지에 다이 정보 갱신
                mStage.AddDieMonsterCount();
            }

            // 처치 몬스터 리스트에 추가
            DungeonManager.Instance.KillMonsterAdd(mMonsterId);
            // 사망 로직 처리 후에 반드시 State.None 으로 보내서 더이상 업데이트문을 타지 않도록 상태 변경.
            this.SetState(State.None);
        }
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
        if ("JumpStart".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            // 점프 모션 시작 하면 피격 블락 처리 
            mIsAttackBlock = true;
        }
        if ("Explosion".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            //폭발 시작 시점이니 블락 해제
            mIsAttackBlock = false;
            // 타겟 한테 데미지 입히고
            //mTarget.OnDamage(mMonsterId, this.mDamage);
            if (mStage)
            {
                // 플레이어 범위 안에 있는지 체크 
                if (this.IsInSplashDamageRange(mTarget.transform.position))
                {
                    // 플레이어에게 데미지 입힌다.
                    mTarget.OnDamage(this.mDamage);
                }

                // 몬스터 범위 안에 있는지 체크
                List<Monster> monsters = this.mStage.mBoard.GetMonsters();
                for (int idx = 0; idx < monsters.Count; ++idx)
                {
                    Monster monster = monsters[idx];
                    if (monsters[idx].mCurrState != State.None && monsters[idx].mCurrState != State.Die)
                    {
                        if (this.IsInSplashDamageRange(monster.transform.position))
                        {
                            // 주변 몬스터 데미지 입힌다.
                            monster.OnDamage(this.mDamage);
                        }
                    }
                }
            }
            
            // 자기 자신에게도 데미지 입힌다.
            this.OnDamage(mMaxHP);

        }
        if ("FinishExplosion".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            // 점프 --> 자폭 끝난 시점에 die 로 상태 전환

            this.SetState(State.Die);
            
        }
    }
}
