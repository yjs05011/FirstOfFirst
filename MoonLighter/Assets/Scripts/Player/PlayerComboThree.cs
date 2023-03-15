using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComboThree : PlayerState
{
    // 코루틴을 반복 실행하는것을 방지 하기 위한 bool 변수
    private bool mIsAttack = false;
    // 3번째 콤보 공격을 나타내는 함수
    public override void Action(ActState state)
    {
        //플레이어의 playerAct의 컴포넌트를 가져온다
        PlayerAct player = GetComponent<PlayerAct>();
        // 플레이어의 무기 타입을 확인하여 그 무기에 알맞은 애니메이션 및 행동을 출력하기 위한 switch
        switch (player.mPlayerNowWeapone)
        {
            // 플레이어가 바라보는 방향을 확인하여 방향에 알맞은 애니메이션 및 행동을 출력하기 위한 switch
            case 1:
                switch (player.mPlayerDirection)
                {
                    case 0:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordDownCombo3());
                        }
                        break;
                    case 1:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordUpCombo3());
                        }
                        break;
                    case 2:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordLeftCombo3());
                        }
                        break;
                    case 3:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordRightCombo3());
                        }
                        break;
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }

    }
    // 아랫 방향을 바라보는 플레이어 대검의 행동을 나타 내기 위한 코루틴 함수
    IEnumerator BigSwordDownCombo3()
    {
        // Time.timeScale = 1f / 24f;
        yield return new WaitForSeconds(0.15f);
        PlayerAct player = GetComponent<PlayerAct>();

        player.mPlayerAnimator.SetBool("IsAttackComboThree", true);
        yield return new WaitForSeconds(0.03f);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, -50);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBox.size = new Vector2(80, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;


        Weapon.localPosition = new Vector2(8f, 35f);
        yield return new WaitForSeconds(0.08f);
        Weapon.localPosition = new Vector2(-13f, 38f);
        yield return new WaitForSeconds(0.008f);
        Weapon.localPosition = new Vector2(5f, -39f);
        yield return new WaitForSeconds(0.13f);
        Weapon.localPosition = new Vector2(46f, -47f);
        yield return new WaitForSeconds(0.3f);
        player.mAttackRoll = 0;
        player.mPlayerAnimator.SetBool("IsAttack", false);
        player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
        player.SetActionType(ActState.STATE_MOVE);
        player.mIsCombo = false;

    }
    //  왼쪽을 바라보는 플레이어 대검의 행동을 나타 내기 위한 코루틴 함수
    IEnumerator BigSwordLeftCombo3()
    {
        yield return new WaitForSeconds(FramNumber * 2f);
        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerAnimator.SetBool("IsAttackComboThree", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(-60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;


        Weapon.localPosition = new Vector2(22f, 21f);
        yield return new WaitForSeconds(FramNumber * 3);
        Weapon.localPosition = new Vector2(-44f, -10f);
        yield return new WaitForSeconds(FramNumber * 2);
        Weapon.localPosition = new Vector2(-27f, -41f);
        yield return new WaitForSeconds(FramNumber * 4);
        player.mAttackRoll = 0;
        player.mPlayerAnimator.SetBool("IsAttack", false);
        player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
        player.SetActionType(ActState.STATE_MOVE);
        player.mIsCombo = false;

    }
    //  왼쪽을 바라보는 플레이어 대검의 행동을 나타 내기 위한 코루틴 함수
    IEnumerator BigSwordRightCombo3()
    {
        yield return new WaitForSeconds(FramNumber * 1f);
        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerAnimator.SetBool("IsAttackComboThree", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;


        Weapon.localPosition = new Vector2(-17f, 22f);
        yield return new WaitForSeconds(FramNumber * 3);
        Weapon.localPosition = new Vector2(47f, -12f);
        yield return new WaitForSeconds(FramNumber * 4);
        Weapon.localPosition = new Vector2(23f, -51f);
        yield return new WaitForSeconds(FramNumber * 7);
        player.mAttackRoll = 0;
        player.mPlayerAnimator.SetBool("IsAttack", false);
        player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
        player.SetActionType(ActState.STATE_MOVE);
        player.mIsCombo = false;

    }
    //  왼쪽을 바라보는 플레이어 대검의 행동을 나타 내기 위한 코루틴 함수
    IEnumerator BigSwordUpCombo3()
    {
        yield return new WaitForSeconds(0.133f);
        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerAnimator.SetBool("IsAttackComboThree", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(-60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;


        Weapon.localPosition = new Vector2(22f, 21f);
        yield return new WaitForSeconds(FramNumber * 3);
        Weapon.localPosition = new Vector2(-44f, -10f);
        yield return new WaitForSeconds(FramNumber * 2);
        Weapon.localPosition = new Vector2(-27f, -41f);
        yield return new WaitForSeconds(FramNumber * 4);
        player.mAttackRoll = 0;
        player.mPlayerAnimator.SetBool("IsAttack", false);
        player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
        player.SetActionType(ActState.STATE_MOVE);
        player.mIsCombo = false;

    }



}

