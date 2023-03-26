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

    // 터렛 (리지드바디 스테틱)

    public enum State
    {
        None, // 비활성화
        Idle, // 대기
        Wander, // 배회
        Attack, // 추적+공격
        Wait, //무한 대기(코드로 제어)
        AttackCooltime, //공격 이후 쿨타임
        Die, // 사망 -> 비활성화
    }


    // 보스
    // 골렘 마인 (자폭 몬스터)
    // 다른 몬스터들도 데미지를 입힘

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

    [Header("Monster Info")]
    public Rect mMovableArea; // 이동 가능한 영역
    [Range(0.1f, 20.0f)]
    public float mAttackDistance = 1.0f; // 자신의 위치를 기준으로 플레이어를 공격 가능한 거리
    [Range(1.2f, 20.0f)]
    public float mTraceScope = 3.0f; // 자신의 위치를 기준으로 플레이어를 추적(이동) 가능한 거리
    [Range(0.0f, 20.0f)]
    public float mSpeed = 1.0f; // 몬스터의 이동 속도 (추적)
    [Range(0.0f, 2.0f)]
    public float mWanderDistance = 1.0f; // 몬스터가 배회할때 랜덤하게 선택될 위치의 최대 거리

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

    // 값이 바뀔 수 있는 정보
    public PlayerAct mTarget = null; // 타겟
    public DungeonStage mStage = null; // 스테이지

    // UI > hp바 
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
        // 내 위치에서부터 랜덤한 방향 벡터 생성
        Vector2 direction = Random.insideUnitCircle.normalized;

        // 랜덤한 방향 벡터의 크기를 거리 N 이하로 조정
        float length = Random.Range(0f, distance);
        Vector3 destination = transform.position + (Vector3)direction * length;

        return destination;
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

    // 추적 가능한 거리 안에 들어왔는지 체크한다.
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

    // 타겟을 공격 할 수 있는 거리 안에 들어왔는지 체크한다.
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

    // 타겟과의 거리를 측정한다
    // 타겟이 이 함수를 사용할 수 없는 경우 False 를 반환한다.
    public bool GetTargetDistance(ref float output)
    {
        if(mTarget)
        {
            Vector3 from = this.transform.position; //몬스터
            Vector3 to = mTarget.transform.position; //플레이어

            Vector3 distance = from - to; //거리
            
            output = distance.magnitude; //길이
            return true; // 이 함수의 값을 사용 할 수 있음
        }

        output = 0.0f;
        return false; // 이 함수의 값을 사용 할 수 없음
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
