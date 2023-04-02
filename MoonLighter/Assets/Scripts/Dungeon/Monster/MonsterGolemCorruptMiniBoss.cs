using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class MonsterGolemCorruptMiniBoss : Monster
{
    public DungeonUtils.Direction mCurrDirection = DungeonUtils.Direction.Down;

    public void OnEnable()
    {
        UiManager.Instance.BossMaxHp(mHp);
    }

    public override void Update()
    {
        base.Update();

        // 플레이어 입장전 대기
        if (mCurrState == State.Ready)
        {
            // 몬스터가 있는 스테이지에 플레이어가 들어온경우.
            if (mStage == DungeonManager.Instance.GetPlayerCurrStage())
            {
                // 상태 idle 로 변경.
                SetState(State.Idle);
            }
        }

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
            this.Movement(mWanderPosition, mSpeed, true);
            this.UpdateAnimationDirection(this.transform.position, mTarget.transform.position);
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
                    // 20% 확률로 배회를 한다.
                    if (Random.Range(0, 1000) < 500)
                    {
                        SmashAttack();
                    }
                    else
                    {
                        if(Random.Range(0, 1000) < 500)
                        {
                            //Teleporation();
                            TeleportStart();
                        }
                        else
                        {
                            SwardAttack();
                        }
                        
                    }
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
                // 이동 한다. (이동 불가시 배회)
                this.Movement(mTarget.transform.position, mSpeed, true);
                this.UpdateAnimationDirection(this.transform.position, mTarget.transform.position);
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
            // 몬스터가 위치한 스테이지에 다이 정보 갱신
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }
         
            // 컬라이더 off
            this.GetComponent<Collider2D>().enabled = false;
       
            
            // 처치 몬스터 리스트에 추가
            DungeonManager.Instance.KillMonsterAdd(mMonsterId);

            // 사망 로직 처리 후에 반드시 State.None 으로 보내서 더이상 업데이트문을 타지 않도록 상태 변경.
            this.SetState(State.None);

            // 사망 연출없음. destroy.
            GameObject.Destroy(this.gameObject);
        }
    }

    public void UpdateAnimationDirection(Vector3 origin, Vector3 destination)
    {
        Vector3 distance = (destination - origin).normalized;

        DungeonUtils.Direction direction = DungeonUtils.Convert2CardinalDirectionsEnum(distance);
        switch (direction)
        {
            case DungeonUtils.Direction.Up:
                {
                    mAnimator.SetFloat("X", 0);
                    mAnimator.SetFloat("Y", 1);
                    mCurrDirection = DungeonUtils.Direction.Up;
                    break;
                }
            case DungeonUtils.Direction.Down:
                {
                    mAnimator.SetFloat("X", 0);
                    mAnimator.SetFloat("Y", -1);
                    mCurrDirection = DungeonUtils.Direction.Down;
                    break;
                }
            case DungeonUtils.Direction.Left:
                {
                    mAnimator.SetFloat("X", -1);
                    mAnimator.SetFloat("Y", 0);
                    mCurrDirection = DungeonUtils.Direction.Left;
                    break;
                }
            case DungeonUtils.Direction.Right:
                {
                    mAnimator.SetFloat("X", 1);
                    mAnimator.SetFloat("Y", 0);
                    mCurrDirection = DungeonUtils.Direction.Right;
                    break;
                }
        }
    }
    public void TeleportStart()
    {
        mAnimator.SetTrigger("TeleportStart");
        // 컬라이더 off
        this.GetComponent<Collider2D>().enabled = false;
        this.SetState(State.Wait);
    }

    public void Teleporation()
    {
        //this.gameObject.SetActive(false);
        mSpriteRenderer.color = new Color(1, 1, 1, 0);

        this.mStage.StartCoroutine(TeleporationCoroutine(this, 3.0f));
    }

    public IEnumerator TeleporationCoroutine(MonsterGolemCorruptMiniBoss owner, float delay)
    {
        yield return new WaitForSeconds(delay);
        //Vector3 position = owner.GenerateRandomRectPosition(owner.mMovableArea);
        mSpriteRenderer.color = new Color(1, 1, 1, 1);
        mAnimator.SetTrigger("TeleportEnd");
        //owner.mAnimator.SetTrigger("TeleportEnd");
        //owner.gameObject.SetActive(true);
        //owner.transform.position = position;
        
    }




    public void SmashAttack()
    {
        mAnimator.SetTrigger("SmashAttack");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish 로 Attack 상태로 다시 변경
    }

    public void SwardAttack()
    {
        mAnimator.SetTrigger("SwordAttack");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish 로 Attack 상태로 다시 변경
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
        bool isSmashAttackDamage = "SmashAttack@Damage".Equals(name, System.StringComparison.OrdinalIgnoreCase);
        bool isSwordAttackDamage = "SwordAttack@Damage".Equals(name, System.StringComparison.OrdinalIgnoreCase);
        bool isFinish = "Finish".Equals(name, System.StringComparison.OrdinalIgnoreCase);

        if (isSwordAttackDamage)
        {
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    mTarget.OnDamage(mMonsterId, this.GetDamage());
                    return;
                }
            }
        }
        else if (isSmashAttackDamage)
        {
            GameObject preset = this.FindSkillPreset("SmashAttackSkill");
            if (preset)
            {
                GameObject clone = GameObject.Instantiate<GameObject>(preset);
                SmashAttackSkill skill = clone.GetComponent<SmashAttackSkill>();
                skill.SetData(this, new Vector3(5, 5, 0), new Vector3(60, 60, 0), 5, 15);
                skill.transform.parent = this.mStage.mBoard.transform;

                switch (mCurrDirection)
                {
                    case DungeonUtils.Direction.Up: { skill.transform.position += new Vector3(0, 2, 0); break; }
                    case DungeonUtils.Direction.Down: { skill.transform.position += new Vector3(0, -2, 0); break; }
                    case DungeonUtils.Direction.Left: { skill.transform.position += new Vector3(-2, 0, 0); break; }
                    case DungeonUtils.Direction.Right: { skill.transform.position += new Vector3(2, 0, 0); break; }
                }

            }
        }
        else if (isFinish)
        {
            this.SetState(State.AttackCooltime);
        }
        else if ("TeleportStartFinished".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            Teleporation();
        }
        else if ("TeleportEndFinished".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            Vector3 position = GenerateRandomRectPosition(mMovableArea);
            
            // 컬라이더 on
            this.GetComponent<Collider2D>().enabled = true;
            this.transform.position = position;
            this.SetState(State.Idle);
        }
        else
        {
            //Debug.LogErrorFormat("Unknown Event Name:{0}", name);
        }
    }
}
