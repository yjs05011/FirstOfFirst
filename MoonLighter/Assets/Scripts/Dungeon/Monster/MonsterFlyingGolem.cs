using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlyingGolem : Monster
{
   
    public override void Update()
    {
        base.Update();

        // 대기 상태
        if (mCurrState == State.Idle)
        {
            // 추적 가능한지 체크하고 추적가능하면 공격 상태로 바꾼다.
            if (IsInTraceScope())
            {
                this.SetState(State.Attack);
                return;
            }

            // 20% 확률로 배회를 한다.
            if (Random.Range(0, 1000) < 200)
            {
                mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                this.SetState(State.Wander);
            }
        }
        // 배회 상태 ( 할게 없는 경우 )
        else if (mCurrState == State.Wander)
        {
            // 배회 목적지에 도착했는지 체크한다.
            if (Vector3.Distance(transform.position, mWanderPosition) < Mathf.Epsilon)
            {
                this.SetState(State.Idle);
                return;
            }

            // 배회 한다.
            this.Movement(mWanderPosition, mSpeed, false);
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

            if(IsInAttackRange())
            {
                mAnimator.SetTrigger("Attack");
                return;
            }

            // 대쉬 영역 안에 타겟이있는 경우 공격모션을 시작한다.
            if (IsInDashRange())
            {
                mAnimator.SetTrigger("Attack");
                this.SetDashState(mTarget.transform.position);
                return;
            }

            // 이동 한다. (이동 불가시 배회)
            this.Movement(mTarget.transform.position, mSpeed, false);
        }
        // 대시 발동
        else if(mCurrState == State.Dash)
        {
            this.Movement(mDashDestination, mDashSpeed, false);
        }
        // 사망 상태 

        else if (mCurrState == State.Die)
        {
            // die 연출 없는데 투명하게 되면서 사라지는거 넣자.
            // 컬라이더 off
            this.GetComponent<Collider2D>().enabled = false;
            //hp bar hide
            mHpBar.SetActive(false);
            // 애니메이션 다이 
            mAnimator.SetTrigger("Dead");
            // 몬스터가 위치한 스테이지에 다이 정보 갱신
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }

            // 처치 몬스터 리스트에 추가
            DungeonManager.Instance.KillMonsterAdd(this);
            // 사망 로직 처리 후에 반드시 State.None 으로 보내서 더이상 업데이트문을 타지 않도록 상태 변경.
            this.SetState(State.None);

          
        }
    }

    public override void OnAnimationEvent(string name)
    {
        Debug.LogFormat("MonsterFlyingGolem : {0}", name);

        if ("Attack".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    mTarget.OnDamage(mMonsterId,this.mDamage);
                }

            }
        }
        else if ("AttackBlockOff".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            mIsAttackBlock = false;
        }
        else if ("AttackBlockOn".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            mIsAttackBlock = true;

        }
        else if ("FinishAttack".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            mIsAttackBlock = true;
            this.SetState(State.Idle);
        }
        else
        {
            Debug.LogErrorFormat("Unknown Event Name:{0}", name);
        }

    }
}
