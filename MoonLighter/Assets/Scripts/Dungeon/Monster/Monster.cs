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
        None, // 비활성화
        Idle, // 대기
        Wander, // 배회
        Attack, // 추적+공격
        Dash, // 
        Wait, //무한 대기(코드로 제어)
        AttackCooltime, //공격 이후 쿨타임
        Die, // 사망 -> 비활성화
        Ready, // 준비 (Wake 이전 상태)
    }

    // 몬스터 id 
    public enum MonsterID
    {
        None = 0,
        BabySlime = 1,
        GolemTurret = 2,
        FlyingGolem = 3,
        GolemMine  =4,
        GolemMiniBoss = 5,
        GolemCorruptMiniBoss = 6,
        GolemKing = 10
    }


    // 컴포넌트
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
    public MonsterID mMonsterId = MonsterID.None; // 몬스터 id
    public Type mType; // 몬스터 타입
    public Rect mMovableArea; // 이동 가능한 영역
    [Range(0.1f, 20.0f)]
    public float mAttackDistance = 1.0f; // 자신의 위치를 기준으로 플레이어를 공격 가능한 거리
    [Range(1.2f, 20.0f)]
    public float mTraceScope = 3.0f; // 자신의 위치를 기준으로 플레이어를 추적(이동) 가능한 거리
    [Range(0.0f, 20.0f)]
    public float mSpeed = 1.0f; // 몬스터의 이동 속도 (추적)
    [Range(0.0f, 2.0f)]
    public float mWanderDistance = 1.0f; // 몬스터가 배회할때 랜덤하게 선택될 위치의 최대 거리
    [Range(0.0f, 3.0f)]
    public float mSplashAttackDistance = 0.0f; // 스플래시 공격 범위.

    [Header("Monster Hp")]
    public float mHp = 100.0f;
    public float mMaxHP = 100.0f;
    [Header("Monster Damage")]
    public float mDamage = 10.0f;
    public float mAttackInterval = 1.0f; // 어택 쿨타임
    public float mAttackTime = 0.0f;    // 어택 쿨타임 (바뀌는값)
    public bool mIsAttackBlock = false;

    [Header("Monster Dash")]
    public bool mIsDash = false;
    [Range(0.0f, 20.0f)]
    public float mDashSpeed = 5.0f;
    [Range(1.2f, 20.0f)]
    public float mDashDistance = 1.0f;
    public Vector3 mDashDestination = Vector3.zero; // 대시는 정해진 위치로만 달린다 ( 추적 불가능 )

    [Header("Monster State")]
    public State mPrevState = State.Idle;
    public State mCurrState = State.Idle;
    protected Vector3 mWanderPosition = Vector3.zero;

    // 값이 바뀔 수 있는 정보
    public PlayerAct mTarget = null; // 타겟
    public DungeonStage mStage = null; // 스테이지

    // UI : hp bar 오브젝트
    public GameObject mHpBar = null;
    // UI : HP fill image 
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

    // 몬스터를 초기화 할때 사용
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

        // 개발용
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
        // 일반 속도 이동
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
        if(mIsAttackBlock)
        {
            Debug.Log("attack block");
            return;
        }

        mHp -= damage;
        //피격 연출
        this.Flash();
        //hp 갱신
        if (mType == Type.NORMAL)
        {
            if (mImgHp != null)
            {
                mImgHp.fillAmount = mHp / mMaxHP;
            }
            if (mHp <= 0.0f)
            {
                this.SetState(State.Die);
              //  ItemManager.Instance.DropItem(this.transform.position);
            }
        }
        if(mType == Type.BOSS)
        {
            
            UiManager.Instance.BossCurrentHp(mHp);
            
            if (mHp <= 0.0f)
            {
                this.SetState(State.Die);
                if (mMonsterId != MonsterID.GolemKing)
                {
                  //  ItemManager.Instance.DropItem(this.transform.position);
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


    public Vector3 GenerateRandomAroundPosition(float distance=1.0f)
    {
        // 내 위치에서부터 랜덤한 방향 벡터 생성
        Vector2 direction = Random.insideUnitCircle.normalized;

        // 랜덤한 방향 벡터의 크기를 거리 N 이하로 조정
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
        for(int idx=0; idx<count; ++idx)
        {
            if(mSkillPresets[idx].key.Equals(key, System.StringComparison.OrdinalIgnoreCase))
            {
                return mSkillPresets[idx].preset;
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

    // 현재 좌표가 이동 가능한 영역인지 체크
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
            position.x = Random.Range(minX+worldX, maxX+worldX);
            position.y = Random.Range(minY+worldY, maxY+worldY);

            if (collider.OverlapPoint(position))
            {
                break;
            }
            --count;
        }
        return position;
    }

    // 추적 가능한 거리 안에 들어왔는지 체크한다.
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
        if(mIsDash && GetTargetDistance(mTarget.transform.position, ref distance))
        {
            if(distance <= mDashDistance)
            {
                return true;
            }
        }
        return false;
    }

    // 타겟을 공격 할 수 있는 거리 안에 들어왔는지 체크한다.
    public bool IsInAttackRange()
    {
        float distance = 0.0f;
        if(GetTargetDistance(mTarget.transform.position, ref distance))
        {
            if (distance <= mAttackDistance)
            {
                return true;
            }
        }
        return false;
    }

    // 타겟과의 거리를 측정한다
    // 타겟이 이 함수를 사용할 수 없는 경우 False 를 반환한다.
    public bool GetTargetDistance(Vector3 to, ref float output)
    {
        if(mTarget)
        {
            Vector3 from = this.transform.position; //몬스터

            Vector3 distance = from - to; //거리
            
            output = distance.magnitude; //길이
            return true; // 이 함수의 값을 사용 할 수 있음
        }

        output = 0.0f;
        return false; // 이 함수의 값을 사용 할 수 없음
    }

    // 스플래시 범위안에 포지션 포함여부 체크 함수
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


    // 실제 게임에는 표기되지 않고 에디터상에서 거리 디버깅을 위한 기즈모 표기
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

        // 플레이어가 타겟인 경우
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
