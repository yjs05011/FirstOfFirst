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
        public List<GameObject> presetList;
    }

    [System.Serializable]
    public class Collider2DLink
    {
        public string key;
        public Collider2D collider;
    }

    public enum Type
    {
        NORMAL,
        BOSS
    }

    public enum State
    {
        None, // ��Ȱ��ȭ
        Idle, // ���
        Wander, // ��ȸ
        Attack, // ����+����
        Dash, // 
        Wait, //���� ���(�ڵ�� ����)
        AttackCooltime, //���� ���� ��Ÿ��
        Die, // ��� -> ��Ȱ��ȭ
        Ready, // �غ� (Wake ���� ����)
    }

    // ���� id 
    public enum MonsterID
    {
        None = 0,
        BabySlime = 1,
        GolemTurret = 2,
        FlyingGolem = 3,
        GolemMine = 4,
        GolemMiniBoss = 5,
        GolemCorruptMiniBoss = 6,
        GolemKing = 10
    }


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


    [Header("Collider")]
    public List<Collider2DLink> mColliders = new List<Collider2DLink>();

    [Header("Monster Info")]
    public MonsterID mMonsterId = MonsterID.None; // ���� id
    public Type mType; // ���� Ÿ��
    public Rect mMovableArea; // �̵� ������ ����
    [Range(0.1f, 20.0f)]
    public float mAttackDistance = 1.0f; // �ڽ��� ��ġ�� �������� �÷��̾ ���� ������ �Ÿ�
    [Range(1.2f, 20.0f)]
    public float mTraceScope = 3.0f; // �ڽ��� ��ġ�� �������� �÷��̾ ����(�̵�) ������ �Ÿ�
    [Range(0.0f, 20.0f)]
    public float mSpeed = 1.0f; // ������ �̵� �ӵ� (����)
    [Range(0.0f, 2.0f)]
    public float mWanderDistance = 1.0f; // ���Ͱ� ��ȸ�Ҷ� �����ϰ� ���õ� ��ġ�� �ִ� �Ÿ�
    [Range(0.0f, 3.0f)]
    public float mSplashAttackDistance = 0.0f; // ���÷��� ���� ����.

    [Header("Monster Hp")]
    public float mHp = 100.0f;
    public float mMaxHP = 100.0f;
    [Header("Monster Damage")]
    public float mDamage = 10.0f;
    public float mAttackInterval = 1.0f; // ���� ��Ÿ��
    public float mAttackTime = 0.0f;    // ���� ��Ÿ�� (�ٲ�°�)
    public bool mIsAttackBlock = false;

    [Header("Monster Dash")]
    public bool mIsDash = false;
    [Range(0.0f, 20.0f)]
    public float mDashSpeed = 5.0f;
    [Range(1.2f, 20.0f)]
    public float mDashDistance = 1.0f;
    public Vector3 mDashDestination = Vector3.zero; // ��ô� ������ ��ġ�θ� �޸��� ( ���� �Ұ��� )

    [Header("Monster State")]
    public State mPrevState = State.Idle;
    public State mCurrState = State.Idle;
    protected Vector3 mWanderPosition = Vector3.zero;

    // ���� �ٲ� �� �ִ� ����
    public PlayerAct mTarget = null; // Ÿ��
    public DungeonStage mStage = null; // ��������

    // UI : hp bar ������Ʈ
    public GameObject mHpBar = null;
    // UI : HP fill image (normal monster hp)
    public Image mImgHp = null;


    public void Start()
    {
        if (mAnimationEvent)
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
        if (mTarget == null)
        {
            this.mTarget = GameObject.FindObjectOfType<PlayerAct>();
        }

        if (mRigidBody.bodyType != RigidbodyType2D.Static)
        {
            mRigidBody.velocity = Vector2.zero;
        }

        // ���߿�
        if (mStage == null)
        {
            mStage = DungeonGenerator.Instance.mStages[0];
            mStage.mBoard.GetMonsters().Add(this);
        }
    }



    public MonsterID GetMonsterId()
    {
        return mMonsterId;
    }


    public void SetStage(DungeonStage stage)
    {
        mStage = stage;
    }

    public void SetState(State state)
    {
        mPrevState = mCurrState;
        mCurrState = state;
    }

    public void SetDashState(Vector3 destination)
    {
        this.SetState(State.Dash);
        mDashDestination = destination;
    }

    public void Movement(Vector3 destination, float speed, bool changeWanderWhenBlocked)
    {
        // �Ϲ� �ӵ� �̵�
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (changeWanderWhenBlocked && !IsMovablePosition(nextPosition))
        {
            mWanderPosition = GenerateRandomAroundPosition(this.mWanderDistance);
            this.SetState(State.Wander);
            return;
        }

        transform.position = nextPosition;
    }

    public virtual void OnDamage(float damage)
    {
        if (mHp <= 0)
        {
            return;
        }
        if (mIsAttackBlock)
        {
            Debug.Log("attack block");
            return;
        }

        mHp -= damage;
        //�ǰ� ����
        this.Flash();
        //hp ����
        if (mType == Type.NORMAL)
        {
            if (mImgHp != null)
            {
                mImgHp.fillAmount = mHp / mMaxHP;
            }
            if (mHp <= 0.0f)
            {
                this.SetState(State.Die);
                // �κ� UI����Ǹ� �ּ� Ǯ�� ���� �ַ���
                //ItemManager.Instance.DropItem(this.transform.position);
            }
        }
        if (mType == Type.BOSS)
        {

            UiManager.Instance.BossCurrentHp(mHp);

            if (mHp <= 0.0f)
            {
                this.SetState(State.Die);
                if (mMonsterId != MonsterID.GolemKing)
                {
                    // �κ� UI����Ǹ� �ּ� Ǯ�� ���� �ַ���
                    //ItemManager.Instance.DropItem(this.transform.position);
                }
                UiManager.Instance.SetBossHpVisible(false);
            }
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


    public Vector3 GenerateRandomAroundPosition(float distance = 1.0f)
    {
        // �� ��ġ�������� ������ ���� ���� ����
        Vector2 direction = Random.insideUnitCircle.normalized;

        // ������ ���� ������ ũ�⸦ �Ÿ� N ���Ϸ� ����
        float length = Random.Range(0f, distance);
        Vector3 destination = transform.position + (Vector3)direction * length;

        return destination;
    }

    public Vector3 GenerateRandomRectPosition(Rect rect)
    {
        return new Vector3(
            Random.Range(rect.xMin, rect.xMax),
            Random.Range(rect.yMin, rect.yMax),
            0
        );
    }

    public GameObject FindSkillPreset(string key)
    {
        int count = mSkillPresets.Count;
        for (int idx = 0; idx < count; ++idx)
        {
            if (mSkillPresets[idx].key.Equals(key, System.StringComparison.OrdinalIgnoreCase))
            {
                return mSkillPresets[idx].preset;
            }
        }
        return null;
    }

    public List<GameObject> FindSkillPresetList(string key)
    {
        int count = mSkillPresets.Count;
        for (int idx = 0; idx < count; ++idx)
        {
            if (mSkillPresets[idx].key.Equals(key, System.StringComparison.OrdinalIgnoreCase))
            {
                return mSkillPresets[idx].presetList;
            }
        }
        return null;
    }

    public Collider2D FindCollider2D(string key)
    {
        int count = mColliders.Count;
        for (int idx = 0; idx < count; ++idx)
        {
            if (mColliders[idx].key.Equals(key, System.StringComparison.OrdinalIgnoreCase))
            {
                return mColliders[idx].collider;
            }
        }
        return null;
    }

    // ���� ��ǥ�� �̵� ������ �������� üũ
    public bool IsMovablePosition(Vector3 position)
    {
        if (mMovableArea.Contains(position))
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

    public Vector3 IsRandomPositionInsidePolygonCollider(PolygonCollider2D collider)
    {
        float worldX = collider.transform.position.x;
        float worldY = collider.transform.position.y;

        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        Vector2[] points = collider.points;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].x < minX)
            {
                minX = points[i].x;
            }
            if (points[i].x > maxX)
            {
                maxX = points[i].x;
            }
            if (points[i].y < minY)
            {
                minY = points[i].y;
            }
            if (points[i].y > maxY)
            {
                maxY = points[i].y;
            }
        }

        collider.gameObject.SetActive(true);

        int count = 40;

        Vector2 position = new Vector2();
        while (count > 0)
        {
            Debug.Log("IsRandomPositionInsidePolygonCollider");
            position.x = Random.Range(minX + worldX, maxX + worldX);
            position.y = Random.Range(minY + worldY, maxY + worldY);

            if (collider.OverlapPoint(position))
            {
                break;
            }
            --count;
        }
        return position;
    }

    // ���� ������ �Ÿ� �ȿ� ���Դ��� üũ�Ѵ�.
    public bool IsInTraceScope()
    {
        float distance = 0.0f;
        if (GetTargetDistance(mTarget.transform.position, ref distance))
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
        if (mIsDash && GetTargetDistance(mTarget.transform.position, ref distance))
        {
            if (distance <= mDashDistance)
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
        if (GetTargetDistance(mTarget.transform.position, ref distance))
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
    public bool GetTargetDistance(Vector3 to, ref float output)
    {
        if (mTarget)
        {
            Vector3 from = this.transform.position; //����

            Vector3 distance = from - to; //�Ÿ�

            output = distance.magnitude; //����
            return true; // �� �Լ��� ���� ��� �� �� ����
        }

        output = 0.0f;
        return false; // �� �Լ��� ���� ��� �� �� ����
    }

    // ���÷��� �����ȿ� ������ ���Կ��� üũ �Լ�
    public bool IsInSplashDamageRange(Vector3 position)
    {
        float distance = 0.0f;
        if (GetTargetDistance(position, ref distance))
        {
            if (distance <= mSplashAttackDistance)
            {
                return true;
            }
        }
        return false;
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

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, mSplashAttackDistance);

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
