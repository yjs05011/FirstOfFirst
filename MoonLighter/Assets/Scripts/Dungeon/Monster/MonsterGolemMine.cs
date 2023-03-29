using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemMine : Monster
{
    public override void Update()
    {
        base.Update();

        // ��� ����
        if (mCurrState == State.Ready)
        {
            // ���� ������ ������ ������, wake ��, idle ���·� �ٲ۴�.
            if (IsInTraceScope())
            {
                mAnimator.SetTrigger("Wake");
                this.SetState(State.Idle);
                return;
            }

        }
        else if(mCurrState == State.Idle)
        {
            // ���� ������ �ϸ� ���� ���·� ����.
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
        // ��ȸ ���� ( wake �� ���� ������ ���°� �ƴѰ�� )
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
        }
        // ���� ����
        else if (mCurrState == State.Attack)
        {

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    // ���� ��� ����
                    mAnimator.SetTrigger("Jump");
                    // ��� ���·� ��ȯ
                    this.SetDashState(mTarget.transform.position);
                }

            }
            // ���� ������ �ƴ� ��� ���� �Ѵ�.
            else
            {
                mAnimator.SetTrigger("Move");
                // �̵� �Ѵ�. (�̵� �Ұ��� ��ȸ)
                this.Movement(mTarget.transform.position, mSpeed, true);
            }
        }
        // ��� �ߵ�
        else if (mCurrState == State.Dash)
        {
            //��� �ӵ��� �̵�.
            this.Movement(mDashDestination, mDashSpeed, false);
        }
        // ��� ���� 
        else if (mCurrState == State.Die)
        {
            mAnimator.SetTrigger("Dead");
            //hp bar hide
            mHpBar.SetActive(false);
            // �ö��̴� off
            this.GetComponent<Collider2D>().enabled = false;
            if (mStage)
            {
                // ���Ͱ� ��ġ�� ���������� ���� ���� ����
                mStage.AddDieMonsterCount();
            }

            // óġ ���� ����Ʈ�� �߰�
            DungeonManager.Instance.KillMonsterAdd(mMonsterId);
            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
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
            // ���� ��� ���� �ϸ� �ǰ� ��� ó�� 
            mIsAttackBlock = true;
        }
        if ("Explosion".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            //���� ���� �����̴� ��� ����
            mIsAttackBlock = false;
            // Ÿ�� ���� ������ ������
            //mTarget.OnDamage(mMonsterId, this.mDamage);
            if (mStage)
            {
                // �÷��̾� ���� �ȿ� �ִ��� üũ 
                if (this.IsInSplashDamageRange(mTarget.transform.position))
                {
                    // �÷��̾�� ������ ������.
                    mTarget.OnDamage(this.mDamage);
                }

                // ���� ���� �ȿ� �ִ��� üũ
                List<Monster> monsters = this.mStage.mBoard.GetMonsters();
                for (int idx = 0; idx < monsters.Count; ++idx)
                {
                    Monster monster = monsters[idx];
                    if (monsters[idx].mCurrState != State.None && monsters[idx].mCurrState != State.Die)
                    {
                        if (this.IsInSplashDamageRange(monster.transform.position))
                        {
                            // �ֺ� ���� ������ ������.
                            monster.OnDamage(this.mDamage);
                        }
                    }
                }
            }
            
            // �ڱ� �ڽſ��Ե� ������ ������.
            this.OnDamage(mMaxHP);

        }
        if ("FinishExplosion".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            // ���� --> ���� ���� ������ die �� ���� ��ȯ

            this.SetState(State.Die);
            
        }
    }
}
