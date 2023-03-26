using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    
    [System.Serializable]
    public class SkillPreset
    {
        public string key;
        public GameObject preset;
    }

    public enum Type
    {
        NORMAL,
        BOSS
    }

    // �ͷ� (������ٵ� ����ƽ)

    public enum State
    {
        None, // ��Ȱ��ȭ
        Idle, // ���
        Wander, // ��ȸ
        Attack, // ����+����
        Wait, //���� ���(�ڵ�� ����)
        AttackCooltime, //���� ���� ��Ÿ��
        Die, // ��� -> ��Ȱ��ȭ
    }


    // ����
    // �� ���� (���� ����)
    // �ٸ� ���͵鵵 �������� ����

    // ������Ʈ
    [Header("Componenet")]
    public Rigidbody2D mRigidBody = null;
    public SpriteRenderer mSpriteRenderer = null;
    public Animator mAnimator = null;

    [Header("Animation")]
    public AnimationEvent mAnimationEvent = null;

    [Header("Preset")]
    public GameObject mProjectilePreset = null;
    public List<SkillPreset> mSkillPresets = new List<SkillPreset>();

    [Header("Monster Info")]
    public Rect mMovableArea; // �̵� ������ ����
    [Range(0.1f, 20.0f)]
    public float mAttackDistance = 1.0f; // �ڽ��� ��ġ�� �������� �÷��̾ ���� ������ �Ÿ�
    [Range(1.2f, 20.0f)]
    public float mTraceScope = 3.0f; // �ڽ��� ��ġ�� �������� �÷��̾ ����(�̵�) ������ �Ÿ�
    [Range(0.0f, 20.0f)]
    public float mSpeed = 1.0f; // ������ �̵� �ӵ� (����)
    [Range(0.0f, 2.0f)]
    public float mWanderDistance = 1.0f; // ���Ͱ� ��ȸ�Ҷ� �����ϰ� ���õ� ��ġ�� �ִ� �Ÿ�

    [Header("Monster Hp")]
    public float mHp = 100.0f;
    public float mMaxHP = 100.0f;
    [Header("Monster Damage")]
    public float mDamage = 10.0f;
    public float mAttackInterval = 1.0f;
    public float mAttackTime = 0.0f;
    public bool mIsAttackBlock = false;

    [Header("Monster Dash")]
    public bool mIsDash = false;
    [Range(0.0f, 20.0f)]
    public float mDashSpeed = 5.0f;
    [Range(1.2f, 20.0f)]
    public float mDashDistance = 1.0f;

    [Header("Monster State")]
    public State mPrevState = State.Idle;
    public State mCurrState = State.Idle;
    protected Vector3 mWanderPosition = Vector3.zero;

    // ���� �ٲ� �� �ִ� ����
    public PlayerAct mTarget = null; // Ÿ��
    public DungeonStage mStage = null; // ��������

    // UI > hp�� 
    public Image mImgHp = null;

    public void Start()
    {
        if(mAnimationEvent)
        {
            mAnimationEvent.SetDelegate(OnAnimationEvent);
        }
    }

    public virtual void OnAnimationEvent(string name)
    {
        
    }

    // ���͸� �ʱ�ȭ �Ҷ� ���
    public void Init(float attackDistance, float traceScope, float speed, float wanderDistance, float maxHp, float damage)
    {
        mAttackDistance = attackDistance;
        mTraceScope = traceScope;
        mSpeed = speed;
        mWanderDistance = wanderDistance;
        mMaxHP = maxHp;
        mHp = mMaxHP;
        mDamage = damage;
    }

    public virtual void Update()
    {
        if(mTarget == null)
        {
            this.mTarget = GameObject.FindObjectOfType<PlayerAct>();
        }

        if(mRigidBody.bodyType != RigidbodyType2D.Static)
        {
            mRigidBody.velocity = Vector2.zero;
        }
    }


    public void SetStage(DungeonStage stage)
    {
        mStage= stage;
    }

    public void SetState(State state)
    {
        mPrevState = mCurrState;
        mCurrState = state;
    }

    public virtual void OnDamage(float damage)
    {
        if (mHp <= 0) 
        {
            return;
        }
        if(mIsAttackBlock)
        {
            Debug.Log("attack block");
            return;
        }

        mHp -= damage;
        this.Flash();
        if (mImgHp != null)
        {
            mImgHp.fillAmount = mHp / mMaxHP;
        }
        if (mHp <= 0.0f)
        {
            this.SetState(State.Die);
        }
    }

    public void Flash()
    {
        if (mSpriteRenderer)
        {
            this.StartCoroutine(FlashCoroutine());
        }
        else
        {
            Debug.LogErrorFormat("SpriteRenderer is Null.");
        }
    }

    protected IEnumerator FlashCoroutine()
    {
        mSpriteRenderer.material.SetFloat("_FlashAmount", 1.0f);
        yield return new WaitForSeconds(0.05f);
        mSpriteRenderer.material.SetColor("_FlashColor", Color.white);
        yield return new WaitForSeconds(0.05f);
        mSpriteRenderer.material.SetColor("_FlashColor", Color.red);
        yield return new WaitForSeconds(0.05f);
        mSpriteRenderer.material.SetFloat("_FlashAmount", 0.0f);
    }


    public Vector3 GenerateRandomAroundPosition(float distance=1.0f)
    {
        // �� ��ġ�������� ������ ���� ���� ����
        Vector2 direction = Random.insideUnitCircle.normalized;

        // ������ ���� ������ ũ�⸦ �Ÿ� N ���Ϸ� ����
        float length = Random.Range(0f, distance);
        Vector3 destination = transform.position + (Vector3)direction * length;

        return destination;
    }

    // ���� ��ǥ�� �̵� ������ �������� üũ
    public bool IsMovablePosition(Vector3 position)
    {
        if(mMovableArea.Contains(position))
        {
            int layerMask = LayerMask.GetMask("Default");
            Vector3 direction = (position - this.transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, 1.0f, layerMask);
            if (hit.collider == null)
            {
                return true;
            }
        }
        return false;
    }

    // ���� ������ �Ÿ� �ȿ� ���Դ��� üũ�Ѵ�.
    public bool IsInTraceScope()
    {
        float distance = 0.0f;
        if (GetTargetDistance(ref distance))
        {
            if (distance <= mTraceScope)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsInDashRange()
    {
        float distance = 0.0f;
        if(mIsDash && GetTargetDistance(ref distance))
        {
            if(distance <= mDashDistance)
            {
                return true;
            }
        }
        return false;
    }

    // Ÿ���� ���� �� �� �ִ� �Ÿ� �ȿ� ���Դ��� üũ�Ѵ�.
    public bool IsInAttackRange()
    {
        float distance = 0.0f;
        if(GetTargetDistance(ref distance))
        {
            if (distance <= mAttackDistance)
            {
                return true;
            }
        }
        return false;
    }

    // Ÿ�ٰ��� �Ÿ��� �����Ѵ�
    // Ÿ���� �� �Լ��� ����� �� ���� ��� False �� ��ȯ�Ѵ�.
    public bool GetTargetDistance(ref float output)
    {
        if(mTarget)
        {
            Vector3 from = this.transform.position; //����
            Vector3 to = mTarget.transform.position; //�÷��̾�

            Vector3 distance = from - to; //�Ÿ�
            
            output = distance.magnitude; //����
            return true; // �� �Լ��� ���� ��� �� �� ����
        }

        output = 0.0f;
        return false; // �� �Լ��� ���� ��� �� �� ����
    }

    // ���� ���ӿ��� ǥ����� �ʰ� �����ͻ󿡼� �Ÿ� ������� ���� ����� ǥ��
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(mMovableArea.xMin, mMovableArea.yMin, 0.0f), new Vector3(mMovableArea.xMin, mMovableArea.yMax));
        Gizmos.DrawLine(new Vector3(mMovableArea.xMin, mMovableArea.yMax, 0.0f), new Vector3(mMovableArea.xMax, mMovableArea.yMax));
        Gizmos.DrawLine(new Vector3(mMovableArea.xMax, mMovableArea.yMax, 0.0f), new Vector3(mMovableArea.xMax, mMovableArea.yMin));
        Gizmos.DrawLine(new Vector3(mMovableArea.xMax, mMovableArea.yMin, 0.0f), new Vector3(mMovableArea.xMin, mMovableArea.yMin));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, mTraceScope);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, mDashDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, mAttackDistance);

        // �÷��̾ Ÿ���� ���
        if (mTarget && IsInTraceScope())
        {
            if (IsInAttackRange())
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.cyan;
            }
            Gizmos.DrawLine(this.transform.position, mTarget.transform.position);
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(mAttackDistance, mAttackDistance, 0));
        }
    }

    public float GetDamage()
    {
        return mDamage;
    }
}
