using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemMine : Monster
{
    public Animator mAnimator;

    public override void Update()
    {
        base.Update();

        // 대기 상태
        if (mCurrState == State.Idle)
        {
            // 추적 가능한지 체크하고 추적가능하면 공격 상태로 바꾼다.
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
                    mAnimator.SetTrigger("Jump");

                }

            }
            // 공격 역역이 아닌 경우 추적 한다.
            else
            {
                mAnimator.SetTrigger("Move");
                Vector3 nextPosition = Vector3.MoveTowards(transform.position, mTarget.transform.position, mSpeed * Time.deltaTime);
                if (!IsMovablePosition(nextPosition))
                {
                    mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                    this.SetState(State.Attack);
                    return;
                }

                transform.position = nextPosition;
            }
        }
        // 사망 상태 

        else if (mCurrState == State.Die)
        {
            // 애니메이션 다이 
            mAnimator.SetTrigger("Dead");

            // 몬스터가 위치한 스테이지에 다이 정보 갱신
            mStage.AddDieMonsterCount();

            // die 연출 끝남 (die 애니메이션이 따로 없고, 자폭 하면서 사라지는 애니메이션출력 후 die 로 상태 전환됨)
            //GameObject.Destroy(this.gameObject);

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
            this.SetState(State.Die);

        }
    }
}
