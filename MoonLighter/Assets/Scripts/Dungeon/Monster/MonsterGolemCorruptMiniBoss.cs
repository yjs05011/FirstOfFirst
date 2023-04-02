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

        // �÷��̾� ������ ���
        if (mCurrState == State.Ready)
        {
            // ���Ͱ� �ִ� ���������� �÷��̾ ���°��.
            if (mStage == DungeonManager.Instance.GetPlayerCurrStage())
            {
                // ���� idle �� ����.
                SetState(State.Idle);
            }
        }

        // ��� ����
        if (mCurrState == State.Idle)
        {
            // ���� �������� üũ�ϰ� ���������ϸ� ���� ���·� �ٲ۴�.
            if (IsInTraceScope())
            {
                this.SetState(State.Attack);
                return;
            }

            // 20% Ȯ���� ��ȸ�� �Ѵ�.
            if (Random.Range(0, 1000) < 200)
            {
                mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
                this.SetState(State.Wander);
            }
        }
        // ��ȸ ���� ( �Ұ� ���� ��� )
        else if (mCurrState == State.Wander)
        {
            // ��ȸ �������� �����ߴ��� üũ�Ѵ�.
            if (Vector3.Distance(transform.position, mWanderPosition) < Mathf.Epsilon)
            {
                this.SetState(State.Idle);
                return;
            }

            // ��ȸ �Ѵ�.
            this.Movement(mWanderPosition, mSpeed, true);
            this.UpdateAnimationDirection(this.transform.position, mTarget.transform.position);
        }
        // ���� ����
        else if (mCurrState == State.Attack)
        {
            // ���� ������ ��� ��� ��� ���·� �ٲ۴�.
            if (!IsInTraceScope())
            {
                this.SetState(State.Idle);
                return;
            }

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    // 20% Ȯ���� ��ȸ�� �Ѵ�.
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
            // ���� ������ �ƴ� ��� ���� �Ѵ�.
            else
            {
                // �̵� �Ѵ�. (�̵� �Ұ��� ��ȸ)
                this.Movement(mTarget.transform.position, mSpeed, true);
                this.UpdateAnimationDirection(this.transform.position, mTarget.transform.position);
            }
        }
        // ���� ��Ÿ�� (���� ��)
        else if (mCurrState == State.AttackCooltime)
        {
            mAttackTime += Time.deltaTime;
            if (mAttackInterval <= mAttackTime)
            {
                mAttackTime = 0.0F;
                this.SetState(State.Attack);
            }
        }
        // ��� ���� 
        else if (mCurrState == State.Die)
        {
            // ���Ͱ� ��ġ�� ���������� ���� ���� ����
            if (mStage)
            {
                mStage.AddDieMonsterCount();
            }
         
            // �ö��̴� off
            this.GetComponent<Collider2D>().enabled = false;
       
            
            // óġ ���� ����Ʈ�� �߰�
            DungeonManager.Instance.KillMonsterAdd(mMonsterId);

            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);

            // ��� �������. destroy.
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
        // �ö��̴� off
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
        // -> OnAnimation:Finish �� Attack ���·� �ٽ� ����
    }

    public void SwardAttack()
    {
        mAnimator.SetTrigger("SwordAttack");
        this.SetState(State.Wait);
        // -> OnAnimation:Finish �� Attack ���·� �ٽ� ����
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
            
            // �ö��̴� on
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
