using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBabySlime : Monster
{
    public Animator mAnimator;

    public override void Update()
    {
        base.Update();

        // 대기 상태
        if (mCurrState == State.Idle)
        {
            mAnimator.SetBool("IsMove", false);

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
            mAnimator.SetBool("IsMove", true);

            // 배회 목적지에 도착했는지 체크한다.
            if (Vector3.Distance(transform.position, mWanderPosition) < Mathf.Epsilon)
            {
                this.SetState(State.Idle);
                return;
            }

            // 배회 한다.
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, mWanderPosition, mSpeed * Time.deltaTime);
            // 만약 자신이 움직일 수 있는 영역을 넘어가려는 경우 중단한다.
            if (!IsMovablePosition(nextPosition))
            {
                mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                this.SetState(State.Wander);
                return;
            }
            transform.position = nextPosition;
        }
        // 공격 상태
        else if (mCurrState == State.Attack)
        {
            mAnimator.SetBool("IsMove", true);
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
                    mTarget.OnDamage(this.mDamage);
                }
                else
                {
                    this.SetState(State.Idle);
                    return;
                }
            }
            // 공격 역역이 아닌 경우 추적 한다.
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
        // 사망 상태 

        else if (mCurrState == State.Die)
        {
            // 몬스터가 위치한 스테이지에 다이 정보 갱신
            mStage.AddDieMonsterCount();

            // die 연출 없는데 투명하게 되면서 사라지는거 넣자.

            // 사망 로직 처리 후에 반드시 State.None 으로 보내서 더이상 업데이트문을 타지 않도록 상태 변경.
            this.SetState(State.None);

           
        }
    }
}
