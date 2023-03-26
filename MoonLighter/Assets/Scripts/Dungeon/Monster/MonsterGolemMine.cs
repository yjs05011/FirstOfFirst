using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemMine : Monster
{
    public override void Update()
    {
        base.Update();

        // 대기 상태
        if (mCurrState == State.Idle)
        {
            // 추적 가능한지 체크하고 추적가능하면 wake 후, 공격 상태로 바꾼다.
            if (IsInTraceScope())
            {
                mAnimator.SetTrigger("Wake");
                this.SetState(State.Attack);
                return;
            }

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
            if (mStage)
            {
                // 몬스터가 위치한 스테이지에 다이 정보 갱신
                mStage.AddDieMonsterCount();

            }
            // 사망 로직 처리 후에 반드시 State.None 으로 보내서 더이상 업데이트문을 타지 않도록 상태 변경.
            this.SetState(State.None);
        }
    }

    public override void OnAnimationEvent(string name)
    {
        if ("Explosion".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            // 타겟 한테 데미지 입히고
            mTarget.OnDamage(this.mDamage);
            // 주변 몬스터 데미지 주기...

        }
        if ("FinishExplosion".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            // 점프 --> 자폭 끝난 시점에 die 로 상태 전환됨)
            this.SetState(State.Die);
            mAnimator.SetTrigger("Dead");
        }
    }
}
