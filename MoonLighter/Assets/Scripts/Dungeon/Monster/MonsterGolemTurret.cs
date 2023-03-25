using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemTurret : Monster
{
    public Animator mAnimator;

    public override void Update()
    {
        base.Update();

        // 대기 상태
        if (mCurrState == State.Idle)
        {
            mAnimator.SetBool("IsShot", false);

            // 추적 가능한지 체크하고 추적가능하면 공격 상태로 바꾼다.
            if (IsInTraceScope())
            {
                this.SetState(State.Attack);
                return;
            }
       
        }
       
        // 공격 상태
        else if (mCurrState == State.Attack)
        {
            
            // 추적 영역을 벗어난 경우 대기 상태로 바꾼다.
            if (!IsInTraceScope())
            {
                mAnimator.SetBool("IsShot", false);
                this.SetState(State.Idle);
                return;
            }

            // 공격 영역 안에 있는 경우 공격 한다.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    mAnimator.SetBool("IsShot", true);
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
                mAnimator.SetBool("IsShot", false);

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
        // 공격 쿨타임 (공격 후)
        else if(mCurrState == State.AttackCooltime)
        {
            mAttackTime += Time.deltaTime;
            if(mAttackInterval <= mAttackTime)
            {
                mAttackTime = 0.0F;
                this.SetState(State.Attack);
            }
        }
        // 사망 상태 

        else if (mCurrState == State.Die)
        {
            // die 연출 없는데 투명하게 되면서 사라지는거 넣자.

            // 사망 로직 처리 후에 반드시 State.None 으로 보내서 더이상 업데이트문을 타지 않도록 상태 변경.
            this.SetState(State.None);

            // 잡은 몬스터 관리 주최에다가 add 필요.
        }
    }

    public override void OnAnimationEvent(string name)
    {
        if ("OnGolemTurretShotDown".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotUp".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotLeft".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotRight".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

            // 추적 영역을 벗어난 경우 대기 상태로 바꾼다.
            if (!IsInTraceScope())
            {
                mAnimator.SetBool("IsShot", false);
                this.SetState(State.Idle);
                return;
            }

            // 공격 영역 안에 있는 경우 공격 한다.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    mAnimator.SetBool("IsShot", true);

                    if (mProjectilePreset)
                    {
                        GameObject instance = GameObject.Instantiate<GameObject>(mProjectilePreset);
                        instance.transform.position = this.transform.position;
                        if (instance)
                        {
                            Vector3 direction = (mTarget.transform.position - this.transform.position).normalized;

                            Projectile projectile = instance.GetComponent<Projectile>();
                            projectile.SetData(this, DungeonUtils.Convert2CardinalDirections(direction));
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
    }
}
