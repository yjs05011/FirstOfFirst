using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGolemTurret : Monster
{
    public enum Direction { Up, Right, Down, Left };
    public Direction mDirection = Direction.Down;

    public override void Update()
    {
        base.Update();

        // ��� ����
        if (mCurrState == State.Idle)
        {

            // ���� �������� üũ�ϰ� ���������ϸ� ���� ���·� �ٲ۴�.
            if (IsInTraceScope())
            {
                this.SetState(State.Attack);
                return;
            }

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

                }
                else
                {
                    this.SetState(State.Idle);
                    return;
                }
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
            // die ���� ���µ� �����ϰ� �Ǹ鼭 ������°� ����.
            // �ִϸ��̼� ���� 
            mAnimator.SetTrigger("Dead");
            // ���Ͱ� ��ġ�� ���������� ���� ���� ����
            mStage.AddDieMonsterCount();
            // ��� ���� ó�� �Ŀ� �ݵ�� State.None ���� ������ ���̻� ������Ʈ���� Ÿ�� �ʵ��� ���� ����.
            this.SetState(State.None);


        }
    }

    public override void OnAnimationEvent(string name)
    {
        if ("OnGolemTurretShotDown".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotUp".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotLeft".Equals(name, System.StringComparison.OrdinalIgnoreCase)
            || "OnGolemTurretShotRight".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {

            // ���� ������ ��� ��� ��� ���·� �ٲ۴�.
            if (!IsInTraceScope())
            {
                mAnimator.SetBool("IsShot", false);
                this.SetState(State.Idle);
                return;
            }

            // ���� ���� �ȿ� �ִ� ��� ���� �Ѵ�.
            if (IsInAttackRange())
            {
                if (mTarget)
                {
                    // Ÿ����ġ�� ���ؼ� ���� ã��
                    Vector3 direction = (mTarget.transform.position - this.transform.position).normalized;
                    // �ͷ� ���� ���� ����
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
                            // �ͷ� �߻�ü ���� ����
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
                Debug.LogErrorFormat("Unknown Event Name:{0}", name);
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

    


}
