using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActState
{
    None,
    State_Move,
    State_Attack_Combo_One,
    State_Attack_Combo_Two,
    State_Attack_Combo_Three,
    State_Evasion,
    State_Enter_Pool,
    State_Die
}
public class PlayerAct : MonoBehaviour
{
    public PlayerScriptObjs mPlayerDefaultStat;
    // 플레이어 리지드바디 2d 선언 (물리력 생성을 위해 선언)
    public Rigidbody2D mPlayerRigid;
    //플레이어 BoxCollider2D 선언 (플레이어 히트박스로 사용 예정)
    public BoxCollider2D mPlayerHitBox;
    // 플레이어 스텟: 스피드를 나타낸다 (임시)
    public float mPlayerSpeed = 0;
    // 플레이어 스텟: 힘을 나타낸다.
    public float mPlayerStr = 0;
    // 플레이어 스텟: 방어력을 나타낸다.
    public float mPlayerDef = 0;
    // 플레이어 스텟: 체력을 나타낸다.
    public float mPlayerHp = 0;
    // 플레이어 애니메이션을 나타낸다.
    public Animator mPlayerAnimator;
    // 플레이어가 움직이는지 체크하기 위한 bool 변수
    public bool mIsMove = false;
    // 플레이어가 회피 중인지 체크하기 위한 bool 변수
    public bool mIsEvasion = false;
    // 플레이어가 맞는 딜레이를 체크하기 위한 bool 변수
    public bool mIsDelay = false;
    // 플레이어가 공격키를 여러번 누르는지 확인하는 변수
    public int mAttackRoll = 0;
    // 플레이어 콤보 공격 확인용 bool 변수
    public bool mIsCombo = false;
    // 플레이어가 방향을 나타내는 변수 (0:아래, 1:위 , 2:왼쪽,3:오른쪽)
    public int mPlayerDirection = 0;
    // 플레이어 상태 머신 변수
    public ActState mState;
    // 플레이어 상태 머신 변수
<<<<<<< HEAD
    private PlayerState mNowState;
    
    // 플레이어 현재 무기 상태(1:대검,2:단검,3:활,4:글러브,5:창)
=======
>>>>>>> 6e967babf0c5ac66e81a3261566b929fc56e9508
    public int mPlayerNowWeapone;
    // 플레이어 애니매이션을 위한 무기 위치 렉트 트랜스폼 변수
    public RectTransform mPlayerWeaponePosition;
    // 플레이어 공격시 박스의 크기를 결정해줌;
    public BoxCollider2D mWeaponeHitBox;
    // 플레이어 공격시 박스 위치를 결정해줌
    public RectTransform mWeaponeHitBoxPosition;
    // 플레이어의 상태가 변화를 위한 시간 변수
    public float mTime = 0;
    // 플레이어가 키를 몇초간 눌렀는지 알기 위한 시간 변수
    public float mHoldingTime = 0;
    // 플레이어에 사용된 애니메이션 클립을 가져오기위한 변수 선언
    public List<AnimationClip> mPlayerAnimation = new List<AnimationClip>();
    // 플레이어 상태 머신 타입 변경
    private PlayerState mNowState;


    void Awake()
    {

        mPlayerAnimator = GetComponent<Animator>();
        mPlayerRigid = GetComponent<Rigidbody2D>();
        mPlayerHitBox = GetComponent<BoxCollider2D>();
        mWeaponeHitBoxPosition = transform.GetChild(0).GetComponent<RectTransform>();
        mWeaponeHitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        SetActionType(ActState.State_Move);
        mPlayerNowWeapone = 1;
        PlayerManager.Instance.mPlayerStat.Hp = mPlayerDefaultStat.hp;
        PlayerManager.Instance.mPlayerStat.Speed = mPlayerDefaultStat.Speed;
        PlayerManager.Instance.mPlayerStat.Str = mPlayerDefaultStat.str;
        PlayerManager.Instance.mPlayerStat.Def = mPlayerDefaultStat.def;
        PlayerManager.Instance.mPlayerStat.Money = mPlayerDefaultStat.Money;
        mPlayerDef = PlayerManager.Instance.mPlayerStat.Def;
        mPlayerSpeed = PlayerManager.Instance.mPlayerStat.Speed;
        mPlayerHp = PlayerManager.Instance.mPlayerStat.Hp;
        mPlayerStr = PlayerManager.Instance.mPlayerStat.Str;
        if (PlayerManager.Instance.mPlayerBeforPos != null)
        {
            transform.position = PlayerManager.Instance.mPlayerBeforPos;
        }


    }

    void Update()
    {
        if (PlayerManager.Instance.mIsUiActive)
        {

        }
        else
        {
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]))
            {
                if (!mIsCombo)
                {
                    mAttackRoll++;
                }


            }
            switch (mState)
            {
                case ActState.State_Move:
                    mNowState.Action(ActState.State_Move);
                    if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.EVASION]))
                    {
                        mTime = 0;
                        SetActionType(ActState.State_Evasion);
                    }
                    else if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]) && !PlayerManager.Instance.mIsShop)
                    {
                        mTime = 0;
                        mIsCombo = true;
                        SetActionType(ActState.State_Attack_Combo_One);

                    }
                    // PlayerMove();
                    break;
                case ActState.State_Attack_Combo_One:
                    mPlayerRigid.velocity = Vector2.zero;
                    mTime += Time.deltaTime;
                    if (mTime > 1.3f)
                    {
                        mTime = 0;
                        SetActionType(ActState.State_Move);
                        mPlayerAnimator.SetBool("IsAttack", false);
                    }
                    if (mIsCombo)
                    {
                        mTime = 0;
                        SetActionType(ActState.State_Attack_Combo_Two);
                    }
                    break;
                case ActState.State_Attack_Combo_Two:
                    mTime += Time.deltaTime;
                    if (mTime > 1.3f)
                    {
                        mTime = 0;
                        SetActionType(ActState.State_Move);
                        mPlayerAnimator.SetBool("IsAttack", false);
                    }
                    if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]))
                    {
                        if (mIsCombo)
                        {

                            SetActionType(ActState.State_Attack_Combo_Three);
                        }
                    }

                    break;
                case ActState.State_Evasion:
                    break;

                case ActState.State_Enter_Pool:
                    mNowState.Action(ActState.State_Enter_Pool);
                    if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.EVASION]))
                    {
                        SetActionType(ActState.State_Evasion);
                    }
                    break;
                case ActState.State_Die:
                    break;
            }
        }



    }
    //플레이어 이동 구현
    public void OnDamage(float MonsterDamage)
    {
        if (mIsDelay)
        {
        }
        else
        {
            mIsDelay = true;
            StartCoroutine(HitDelay(0.5f));
            float calculateDamage = MonsterDamage - mPlayerDef;
            if (calculateDamage < 0)
            {
            }
            else
            {
                mPlayerHp -= calculateDamage;
            }
            if (mPlayerHp < 0)
            {
                SetActionType(ActState.State_Die);
                PlayerManager.Instance.mPlayerStat.isDie = true;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pool"))
        {
            mPlayerAnimator.SetBool("IsPool", true);
            mIsEvasion = false;
            mPlayerAnimator.SetBool("IsEvasion", false);
            SetActionType(ActState.State_Enter_Pool);
        }
        if (other.CompareTag("Hole"))
        {
            if (mIsEvasion)
            {

            }
            else
            {
                mPlayerAnimator.SetTrigger("IsFalling");
                mPlayerRigid.velocity *= 0.1f;
                mIsEvasion = false;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pool"))
        {
            mPlayerAnimator.SetBool("IsPool", false);
            mIsEvasion = false;
            mPlayerAnimator.SetBool("IsEvasion", false);
            SetActionType(ActState.State_Move);

        }
    }
    void HoldingKey()
    {
        mHoldingTime = 0;
        mHoldingTime += Time.deltaTime;
    }
    // 플레이어 현재 상태를 저장하는 함수
    public void SetActionType(ActState state)
    {
        this.mState = state;

        Component State = gameObject.GetComponent<PlayerState>() as Component;
        if (State != null)
        {
            Destroy(State);
        }
        switch (state)
        {
            case global::ActState.State_Attack_Combo_One:
                mNowState = gameObject.AddComponent<PlayerAttackComboOne>();
                mNowState.Action(state);
                break;
            case global::ActState.State_Attack_Combo_Two:
                mNowState = gameObject.AddComponent<PlayerAttackComboTwo>();
                mNowState.Action(state);
                break;
            case global::ActState.State_Attack_Combo_Three:
                mNowState = gameObject.AddComponent<PlayerAttackComboThree>();
                mNowState.Action(state);
                break;
            case global::ActState.State_Evasion:
                mNowState = gameObject.AddComponent<PlayerEvasion>();
                mNowState.Action(state);
                break;
            case global::ActState.State_Move:
                mNowState = gameObject.AddComponent<PlayerMove>();
                break;
            case global::ActState.State_Enter_Pool:
                mNowState = gameObject.AddComponent<PlayerMove>();
                break;
            default:
                break;
        }
    }
    IEnumerator HitDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        mIsDelay = false;
    }

}
