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
    State_Attack_Skill,
    State_Die,
    State_Fall
}
public class PlayerAct : MonoBehaviour
{
    #region  변수 Public
    public PlayerScriptObjs mPlayerDefaultStat;
    // 플레이어 리지드바디 2d 선언 (물리력 생성을 위해 선언)
    public Rigidbody2D mPlayerRigid;
    // 플레이어 애니메이션을 나타낸다.
    public RectTransform mPlayerWeaponePosition;
    // 플레이어 공격시 박스의 크기를 결정해줌;
    public BoxCollider2D mWeaponeHitBox;
    // 플레이어 공격시 박스 위치를 결정해줌
    public RectTransform mWeaponeHitBoxPosition;
    // 플레이어의 상태가 변화를 위한 시간 변수
    public Animator mPlayerAnimator;
    //플레이어 BoxCollider2D 선언 (플레이어 히트박스로 사용 예정)
    public ActState mState;
    // 플레이어 상태 머신 변수
    public BoxCollider2D mPlayerHitBox;
    // 플레이어에 사용된 애니메이션 클립을 가져오기위한 변수 선언
    public List<AnimationClip> mPlayerAnimation = new List<AnimationClip>();
    // 플레이어가 벽에 부딛치고 있을때(떨어지는 상태) 위치 확인을 위한 변수
    public Vector2 mPlayerPosCheck;
    // 플레이어가 공격키를 여러번 누르는지 확인하는 변수
    public int mAttackRoll = 0;
    // 플레이어가 방향을 나타내는 변수 (0:아래, 1:위 , 2:왼쪽,3:오른쪽)
    public int mPlayerDirection = 0;
    // 플레이어 상태 머신 변수(무기 타입)
    public int mPlayerNowWeapone;
    // 플레이어 애니매이션을 위한 무기 위치 렉트 트랜스폼 변수
    public float mTime = 0;
    // 플레이어가 키를 몇초간 눌렀는지 알기 위한 시간 변수
    public float mHoldingTime = 0;
    // 플레이어 스텟: 스피드를 나타낸다 (임시)
    public float mPlayerSpeed = 0;
    // 플레이어 스텟: 힘을 나타낸다.
    public float mPlayerStr = 0;
    // 플레이어 스텟: 방어력을 나타낸다.
    public float mPlayerDef = 0;
    // 플레이어 스텟: 체력을 나타낸다.
    public float mPlayerHp = 0;
    // 플레이어 스텟: 플레이어의 최대 체력을 나타낸다.
    public float mPlayerMaxHp = 0;
    // 플레이어가 움직이는지 체크하기 위한 bool 변수
    public bool mIsMove = false;
    // 플레이어가 회피 중인지 체크하기 위한 bool 변수
    public bool mIsEvasion = false;
    // 플레이어가 맞는 딜레이를 체크하기 위한 bool 변수
    public bool mIsDelay = false;
    // 플레이어가 스킬을 사용하는지 체크하기 위한 bool 변수
    public bool mIsSkillUse;
    // 키를 꾹 누르고 있는지 확인
    public bool mIsHolding = false;
    // 플레이어 콤보 공격 확인용 bool 변수
    public bool mIsCombo = false;
    public bool mIsFall = false;

    #endregion
    // 플레이어 상태 머신 타입 변경
    private PlayerState mNowState;
    // 플레이어가 풀 안에서 힐을 하고 있는지 확인하는 변수
    private bool mIsHealing;
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
        mPlayerDef = PlayerManager.Instance.mPlayerStat.Def;
        mPlayerSpeed = PlayerManager.Instance.mPlayerStat.Speed;
        mPlayerHp = PlayerManager.Instance.mPlayerStat.Hp;
        mPlayerStr = PlayerManager.Instance.mPlayerStat.Str;
        mPlayerMaxHp = PlayerManager.Instance.mPlayerStat.MaxHp;
        if (PlayerManager.Instance.mPlayerBeforPos != default)
        {
            transform.position = PlayerManager.Instance.mPlayerBeforPos;
        }
        if (SetPosition.Instance.mSettingPosition != default)
        {
            transform.position = SetPosition.Instance.mSettingPosition;
            SetPosition.Instance.mSettingPosition = Vector3.zero;
        }



    }
    private void OnGUI()
    {
        Event keyEvent = Event.current;

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
            if (UiManager.Instance.mIsPlayerUseAnimation)
            {
                mPlayerAnimator.SetTrigger("IsPortal");
            }
            switch (mState)
            {
                case ActState.State_Move:
                    if (mIsFall)
                    {
                        SetActionType(ActState.State_Fall);
                    }
                    mPlayerAnimator.SetBool("IsAttack", false);
                    mPlayerAnimator.SetBool("IsPool", false);
                    transform.GetChild(0).gameObject.SetActive(false);
                    // mPlayerHitBox.isTrigger = false;
                    mNowState.Action(ActState.State_Move);
                    mPlayerAnimator.SetBool("IsSkill", false);
                    mPlayerAnimator.SetBool("IsSKillHoling", false);
                    mPlayerAnimator.SetBool("IsSkillUse", false);
                    if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.EVASION]))
                    {
                        mTime = 0;
                        SetActionType(ActState.State_Evasion);
                    }
                    else if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]) && !PlayerManager.Instance.mIsShop)
                    {
                        mTime = 0;
                        mIsCombo = true;
                        mPlayerAnimator.SetFloat("WeaponType", mPlayerNowWeapone);


                        SetActionType(ActState.State_Attack_Combo_One);

                    }
                    if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.SKILL]))
                    {
                        mPlayerRigid.velocity = Vector2.zero;
                        mTime = 0;
                        mPlayerAnimator.SetFloat("WeaponType", mPlayerNowWeapone);
                        mPlayerAnimator.SetBool("IsSkill", true);
                        mPlayerAnimator.SetTrigger("IsSkillTrigger");
                        SetActionType(ActState.State_Attack_Skill);
                    }
                    if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.WEAPONCHANGE]))
                    {
                        if (mPlayerNowWeapone == 1)
                        {
                            mPlayerNowWeapone = 2;

                        }
                        else
                        {
                            mPlayerNowWeapone = 1;
                        }
                        UiManager.Instance.mIsWeaponChange = true;
                    }
                    break;
                case ActState.State_Attack_Combo_One:
                    mPlayerHitBox.isTrigger = false;
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
                    mPlayerHitBox.isTrigger = false;
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
                case ActState.State_Attack_Skill:
                    mTime += Time.deltaTime;

                    if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.SKILL]))
                    {
                        if (mTime > 0.5f)
                        {
                            mTime = 0;
                            mNowState.Action(ActState.State_Attack_Skill);
                            mPlayerAnimator.SetBool("IsSkillUse", true);
                        }
                        else
                        {
                            SetActionType(ActState.State_Move);
                            mPlayerAnimator.SetBool("IsSkill", false);
                            mPlayerAnimator.SetBool("IsSKillHoling", false);
                            mPlayerAnimator.SetBool("IsSkillUse", false);
                        }

                    }
                    break;
                case ActState.State_Evasion:
                    break;
                case ActState.State_Enter_Pool:
                    mPlayerHitBox.isTrigger = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                    mNowState.Action(ActState.State_Enter_Pool);
                    mPlayerAnimator.SetBool("IsSkill", false);
                    mPlayerAnimator.SetBool("IsSKillHoling", false);
                    mPlayerAnimator.SetBool("IsSkillUse", false);
                    if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.EVASION]))
                    {
                        SetActionType(ActState.State_Evasion);
                    }
                    if (mIsHealing)
                    {
                        mIsHealing = false;
                    }
                    break;
                case ActState.State_Die:
                    mPlayerRigid.velocity = Vector2.zero;
                    break;
                case ActState.State_Fall:

                    break;
            }
        }



    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pool"))
        {
            mPlayerAnimator.SetBool("IsPool", true);
            mIsHealing = true;
            mIsEvasion = false;
            mPlayerAnimator.SetBool("IsEvasion", false);
            SetActionType(ActState.State_Enter_Pool);
        }
        if (other.CompareTag("Hole"))
        {
            mIsFall = true;


        }


    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            mPlayerHitBox.isTrigger = false;
            mPlayerRigid.position = mPlayerPosCheck;
        }
        if (other.CompareTag("Hole"))
        {

            Debug.Log("!!!");
            mIsFall = true;

        }
        if (other.CompareTag("Trap"))
        {
            if (mPlayerSpeed >= PlayerManager.Instance.mPlayerStat.Speed * 0.7f)
            {
                mPlayerSpeed *= 0.9f;
            }


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pool"))
        {
            mPlayerAnimator.SetBool("IsPool", false);
            mIsEvasion = false;
            mIsHealing = false;
            mPlayerAnimator.SetBool("IsEvasion", false);
            SetActionType(ActState.State_Move);

        }
        if (other.CompareTag("Trap"))
        {
            mPlayerSpeed = PlayerManager.Instance.mPlayerStat.Speed;
        }
    }
    public void HoldingKey()
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
            case global::ActState.State_Attack_Skill:
                mNowState = gameObject.AddComponent<PlayerAttackSkill>();
                break;
            case global::ActState.State_Fall:
                mNowState = gameObject.AddComponent<PlayerFall>();
                break;
            default:
                break;
        }
    }
    //키입력에 딜레이를 주기 위한 변수

    public float GetPlayerMaxHp()
    {
        return mPlayerMaxHp;
    }
    public float GetPlayerHp()
    {
        return mPlayerHp;
    }
    public void OnHealing(float number)
    {
        if (mPlayerMaxHp > mPlayerHp)
        {
            mPlayerHp += number;
            PlayerManager.Instance.mPlayerStat.Hp += number;
            UiManager.Instance.mIsHpChange = true;
            if (mPlayerHp <= mPlayerMaxHp)
            {
                mPlayerHp = mPlayerMaxHp;
            }
        }

    }

    //플레이어 몬스터 히트 구현
    public void OnDamage(Monster.MonsterID id, float MonsterDamage)
    {
        Debug.Log(MonsterDamage);
        if (mIsDelay || mState == ActState.State_Die)
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
                PlayerManager.Instance.mPlayerStat.Hp -= calculateDamage;
                UiManager.Instance.mIsHpChange = true;
            }
            if (mPlayerHp <= 0)
            {
                mPlayerHp = 0;
                PlayerManager.Instance.mPlayerStat.Hp = 0;
                SetActionType(ActState.State_Die);
                mPlayerAnimator.SetTrigger("IsDie");
                PlayerManager.Instance.mPlayerWasKilled = (int)id;
                PlayerManager.Instance.mPlayerStat.isDie = true;
            }
        }


    }
    public void OnDamage(float MonsterDamage)
    {
        Debug.Log(MonsterDamage);
        if (mIsDelay || mState == ActState.State_Die)
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
                PlayerManager.Instance.mPlayerStat.Hp -= calculateDamage;
                UiManager.Instance.mIsHpChange = true;

            }
            if (mPlayerHp < 0)
            {
                SetActionType(ActState.State_Die);
                PlayerManager.Instance.mPlayerStat.isDie = true;
            }
        }


    }

    public IEnumerator HolingTimer(float time)
    {

        yield return new WaitForSeconds(time);
        mIsHolding = true;

    }
    public void SetPosToFallCheck()
    {
        switch (mPlayerDirection)
        {
            //아래
            case 0:
                mPlayerPosCheck = mPlayerRigid.position + new Vector2(0, -0.2f);
                break;
            //위
            case 1:
                mPlayerPosCheck = mPlayerRigid.position + new Vector2(0, +0.2f);
                break;
            //왼쪽
            case 2:
                mPlayerPosCheck = mPlayerRigid.position + new Vector2(-0.2f, 0);
                break;
            //오른쪽
            case 3:
                mPlayerPosCheck = mPlayerRigid.position + new Vector2(0.2f, 0);
                break;
        }
    }
    public void SetSkillAnimation()
    {
        mPlayerAnimator.SetBool("IsSKillHoling", true);
    }
    IEnumerator HitDelay(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        mIsDelay = false;
    }
    public void SetDie()
    {
        UiManager.Instance.PlayerUsePendant(false);
        UiManager.Instance.PlayerFinishAnimation(true);
    }

}
