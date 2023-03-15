using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ActState
{
    STATE_MOVE,
    STATE_ATTACK_COMBO_ONE,
    STATE_ATTACK_COMBO_TWO,
    STATE_ATTACK_COMBO_THREE,
    STATE_EVASION
}
public class PlayerAct : MonoBehaviour
{
    // 플레이어 리지드바디 2d 선언 (물리력 생성을 위해 선언)
    public Rigidbody2D mPlayerRigid;
    //플레이어 BoxCollider2D 선언 (플레이어 히트박스로 사용 예정)
    public BoxCollider2D mPlayerHitBox;
    // 플레이어 스텟: 스피드를 나타낸다 (임시)
    public float mPlayerSpeed = 0;
    // 플레이어 애니메이션을 나타낸다.
    public Animator mPlayerAnimator;
    // 플레이어가 움직이는지 체크하기 위한 bool 변수
    public bool mIsMove = false;
    // 플레이어가 회피 중인지 체크하기 위한 bool 변수
    public bool mIsEvasion = false;
    // 플레이어가 공격키를 여러번 누르는지 확인하는 변수
    public int mAttackRoll = 0;
    // 플레이어 콤보 공격 확인용 bool 변수
    public bool mIsCombo = false;
    // 플레이어가 방향을 나타내는 변수 (0:아래, 1:위 , 2:왼쪽,3:오른쪽)
    public int mPlayerDirection = 0;
    // 플레이어 상태 머신 변수
    private ActState mState;
    // 플레이어 상태 머신 변수
    private PlayerState mNowState;

    // 플레이어 현재 무기 상태(1:대검,2:단검,3:활,4:글러브,5:창)
    public int mPlayerNowWeapone;
    // 플레이어 애니매이션을 위한 무기 위치 렉트 트랜스폼 변수
    public RectTransform mPlayerWeaponePosition;
    // 플레이어 공격시 박스의 크기를 결정해줌;
    public BoxCollider2D mWeaponeHitBox;
    public RectTransform mWeaponeHitBoxPosition;
    public float mTime = 0;

    // 플레이어 상태 머신 타입 변경
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
            case global::ActState.STATE_ATTACK_COMBO_ONE:
                mNowState = gameObject.AddComponent<PlayerAttackComboOne>();
                mNowState.Action(state);
                break;
            case global::ActState.STATE_ATTACK_COMBO_TWO:
                mNowState = gameObject.AddComponent<PlayerAttackComboTwo>();
                mNowState.Action(state);
                break;
            case global::ActState.STATE_ATTACK_COMBO_THREE:
                mNowState = gameObject.AddComponent<PlayerAttackComboThree>();
                mNowState.Action(state);
                break;
            case global::ActState.STATE_EVASION:
                mNowState = gameObject.AddComponent<PlayerEvasion>();
                mNowState.Action(state);
                break;
            default:
                break;
        }
    }
    void Awake()
    {

        mPlayerAnimator = GetComponent<Animator>();
        mPlayerRigid = GetComponent<Rigidbody2D>();
        mPlayerHitBox = GetComponent<BoxCollider2D>();
        mPlayerWeaponePosition = transform.GetChild(1).GetComponent<RectTransform>();
        mWeaponeHitBoxPosition = transform.GetChild(0).GetComponent<RectTransform>();
        mWeaponeHitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        SetActionType(ActState.STATE_MOVE);
        mPlayerNowWeapone = 1;
    }

    void Update()
    {
        Debug.Log(mState);
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]))
        {
            if (!mIsCombo)
            {
                mAttackRoll++;
            }


        }
        switch (mState)
        {
            case ActState.STATE_MOVE:
                if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.EVASION]))
                {
                    SetActionType(ActState.STATE_EVASION);
                }
                else if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]))
                {
                    mTime = 0;
                    mIsCombo = true;
                    SetActionType(ActState.STATE_ATTACK_COMBO_ONE);

                }
                PlayerMove();
                break;
            case ActState.STATE_ATTACK_COMBO_ONE:
                mPlayerRigid.velocity = Vector2.zero;
                mTime += Time.deltaTime;
                Debug.Log(mTime);
                if (mTime > 1.3f)
                {
                    mTime = 0;
                    SetActionType(ActState.STATE_MOVE);
                    mPlayerAnimator.SetBool("IsAttack", false);
                }
                if (mIsCombo)
                {
                    mTime = 0;
                    SetActionType(ActState.STATE_ATTACK_COMBO_TWO);
                }
                break;
            case ActState.STATE_ATTACK_COMBO_TWO:
                mTime += Time.deltaTime;
                Debug.Log(mTime);
                if (mTime > 1.3f)
                {
                    mTime = 0;
                    SetActionType(ActState.STATE_MOVE);
                    mPlayerAnimator.SetBool("IsAttack", false);
                }
                if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]))
                {
                    if (mIsCombo)
                    {

                        SetActionType(ActState.STATE_ATTACK_COMBO_THREE);
                    }
                }

                break;
            case ActState.STATE_EVASION:

                break;
        }


    }
    //플레이어 이동 구현
    void PlayerMove()
    {
        float Horizontal = 0;
        float vertical = 0;
        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        {
            mIsMove = true;
            mPlayerAnimator.SetFloat("InputX", 0);
            mPlayerAnimator.SetFloat("InputY", 1);
            mPlayerAnimator.SetBool("IsRun", true);

            mPlayerDirection = 1;
            vertical = 1;
        }
        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.DOWN]))
        {
            mIsMove = true;
            mPlayerAnimator.SetFloat("InputX", 0);
            mPlayerAnimator.SetFloat("InputY", -1);
            mPlayerAnimator.SetBool("IsRun", true);
            // mPlayerAnimator.SetInteger("Run", 2);
            // mPlayerAnimator.SetInteger("Idle", 1);
            mPlayerDirection = 0;
            vertical = -1;
        }
        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.LEFT]))
        {
            mIsMove = true;
            mPlayerAnimator.SetFloat("InputX", -1);
            mPlayerAnimator.SetFloat("InputY", 0);
            mPlayerAnimator.SetBool("IsRun", true);
            // mPlayerAnimator.SetInteger("Run", 3);
            // mPlayerAnimator.SetInteger("Idle", 3);
            mPlayerDirection = 2;
            Horizontal = -1;
        }
        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.RIGHT]))
        {
            mIsMove = true;
            mPlayerAnimator.SetFloat("InputX", +1);
            mPlayerAnimator.SetFloat("InputY", 0);
            mPlayerAnimator.SetBool("IsRun", true);
            // mPlayerAnimator.SetInteger("Run", 4);
            // mPlayerAnimator.SetInteger("Idle", 4);
            mPlayerDirection = 3;
            Horizontal = 1;

        }


        if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        {
            mIsMove = false;
            mPlayerAnimator.SetBool("IsRun", false);
        }
        if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.DOWN]))
        {
            mIsMove = false;
            mPlayerAnimator.SetBool("IsRun", false);
        }
        if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.LEFT]))
        {
            mIsMove = false;
            mPlayerAnimator.SetBool("IsRun", false);
        }
        if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.RIGHT]))
        {
            mIsMove = false;
            mPlayerAnimator.SetBool("IsRun", false);

        }

        float xSpeed = Horizontal * Time.deltaTime * mPlayerSpeed * 1000;
        float ySpeed = vertical * Time.deltaTime * mPlayerSpeed * 1000;

        Vector2 playerVector = new Vector2(xSpeed, ySpeed);
        mPlayerRigid.velocity = playerVector;
        if (!mIsMove)
        {
            mPlayerAnimator.SetBool("IsRun", false);
        }
    }
    //플레이어 회피
    public void PlayerEvasion()
    {
        //0.625(애니메이션 시간)
        // if (is)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))
            {

            }
        }
    }


}
