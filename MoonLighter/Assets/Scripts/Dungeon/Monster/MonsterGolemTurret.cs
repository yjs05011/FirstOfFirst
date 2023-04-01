using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemTurret : Monster
{
    
    public int mShotCount = 3;

    public override void Update()
    {
        base.Update();
      
        // 대기 상태
        if (mCurrState == State.Idle)
        {
           
            // 추적 가능한지 체크하고 추적가능하면 공격 상태로 바꾼다.
            if (IsInTraceScope())
            {
                StartCoroutine(ShotDelayCoroutine());
                
                return;
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
                    if (mShotCount > 0)
                    {
                        mAnimator.SetBool("IsShot", true);
                        --mShotCount;
                    }
                    else
                    {
                        mAnimator.SetBool("IsShot", false);
                        this.SetState(State.Idle);
                    }
                }
                else
                {
                    mAnimator.SetBool("IsShot", false);
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
            // die 연출 없는데 투명하게 되면서 사라지는거 넣자.
            // 애니메이션 다이 
            mAnimator.SetTrigger("Dead");
            // 몬스터가 위치한 스테이지에 다이 정보 갱신
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }

            // 컬라이더 off
            this.GetComponent<Collider2D>().enabled = false;
            //hp bar hide
            mHpBar.SetActive(false);
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
        if ("OnGolemTurretShotDown".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotUp".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotLeft".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotRight".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

        
            // 공격 영역 안에 있는 경우 공격 한다.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    
                        // 타겟위치와 비교해서 방향 찾고
                        Vector3 direction = (mTarget.transform.position - this.transform.position).normalized;
                        // 터렛 몸통 방향 변경
                        this.SetRotation(DungeonUtils.Convert2CardinalDirectionsEnum(direction));
                        if (mProjectilePreset)
                        {
                            GameObject instance = GameObject.Instantiate<GameObject>(mProjectilePreset);
                            instance.transform.position = this.transform.position;
                            instance.transform.parent = this.mStage.transform;
                            if (instance)
                            {
                                Projectile projectile = instance.GetComponent<Projectile>();
                                projectile.SetData(this, DungeonUtils.Convert2CardinalDirections(direction));
                                // 터렛 발사체 방향 변경
                                projectile.SetRotation(DungeonUtils.Convert2CardinalDirectionsEnum(direction));
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
                //Debug.LogErrorFormat("Unknown Event Name:{0}", name);
            }
        }
    }
    public void SetRotation(DungeonUtils.Direction direction)
    {
        if (mCurrState == State.Die|| mCurrState == State.None)
        {
            return;
        }

        switch (direction)
        {
            case DungeonUtils.Direction.Down:
                mAnimator.SetFloat("Y", -1.0f);
                mAnimator.SetFloat("X", 0.0f);
                break;
            case DungeonUtils.Direction.Up:
                mAnimator.SetFloat("Y", 1.0f);
                mAnimator.SetFloat("X", 0.0f);
                break;
            case DungeonUtils.Direction.Left:
                mAnimator.SetFloat("X", -1.0f);
                mAnimator.SetFloat("Y", 0.0f);
                break;
            case DungeonUtils.Direction.Right:
                mAnimator.SetFloat("X", 1.0f);
                mAnimator.SetFloat("Y", 0.0f);
                break;
        }
        
    }

    protected IEnumerator ShotDelayCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        mShotCount = 3;
        this.SetState(State.Attack);

    }


}
