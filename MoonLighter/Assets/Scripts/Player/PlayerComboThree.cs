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
            case 1:
                if (!mIsAttack)
                {
                    mIsAttack = true;
                    StartCoroutine(BigSwordCombo3(player.mPlayerDirection, player));
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
    // direct 방향을 바라보는 플레이어 대검의 행동을 나타 내기 위한 코루틴 함수
    IEnumerator BigSwordCombo3(int direct, PlayerAct player)
    {
        player.mPlayerAnimator.SetBool("IsAttackComboThree", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        switch (direct)
        {

            case 0:
                player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, -1.041f);
                player.mWeaponeHitBox.size = new Vector2(1.666f, 2.083f);
                break;
            case 1:
                player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, 1.04f);
                player.mWeaponeHitBox.size = new Vector2(1.7f, 2.08f);
                break;
            case 2:
                player.mWeaponeHitBoxPosition.localPosition = new Vector2(-1.25f, 0);
                player.mWeaponeHitBox.size = new Vector2(2.5f, 2.083f);
                break;
            case 3:
                player.mWeaponeHitBoxPosition.localPosition = new Vector2(1.25f, 0);
                player.mWeaponeHitBox.size = new Vector2(2.5f, 2.083f);
                break;
        }
        // Time.timeScale = 1f / 24f;
        yield return new WaitForSeconds(player.mPlayerAnimation[2].length);
        player.mWeaponeHitBox.gameObject.SetActive(false);
        player.mAttackRoll = 0;
        player.mPlayerAnimator.SetBool("IsAttack", false);
        player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
        player.SetActionType(ActState.STATE_MOVE);
        player.mIsCombo = false;

    }
    // //  왼쪽을 바라보는 플레이어 대검의 행동을 나타 내기 위한 코루틴 함수
    // IEnumerator BigSwordLeftCombo3()
    // {
    //     yield return new WaitForSeconds(FramNumber * 2f);
    //     PlayerAct player = GetComponent<PlayerAct>();
    //     player.mPlayerAnimator.SetBool("IsAttackComboThree", true);
    //     player.mWeaponeHitBox.gameObject.SetActive(true);

    //     RectTransform Weapon = player.mPlayerWeaponePosition;


    //     Weapon.localPosition = new Vector2(0.458f, 0.4375f);
    //     yield return new WaitForSeconds(FramNumber * 3);
    //     Weapon.localPosition = new Vector2(-0.916f, -0.208f);
    //     yield return new WaitForSeconds(FramNumber * 2);
    //     Weapon.localPosition = new Vector2(-0.5625f, -0.854f);
    //     yield return new WaitForSeconds(FramNumber * 4);
    //     player.mAttackRoll = 0;
    //     player.mPlayerAnimator.SetBool("IsAttack", false);
    //     player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
    //     player.SetActionType(ActState.STATE_MOVE);
    //     player.mIsCombo = false;

    // }
    // //  왼쪽을 바라보는 플레이어 대검의 행동을 나타 내기 위한 코루틴 함수
    // IEnumerator BigSwordRightCombo3()
    // {

    //     // yield return new WaitForSeconds(FramNumber * 1f);
    //     PlayerAct player = GetComponent<PlayerAct>();
    //     // Debug.Log(player.mPlayerAnimation[2].length);
    //     player.mPlayerAnimator.SetBool("IsAttackComboThree", true);
    //     // player.mWeaponeHitBox.gameObject.SetActive(true);
    //     // player.mWeaponeHitBoxPosition.localPosition = new Vector2(1.25f, 0);
    //     // player.mWeaponeHitBox.size = new Vector2(2.5f, 2.083f);
    //     // RectTransform Weapon = player.mPlayerWeaponePosition;


    //     // Weapon.localPosition = new Vector2(-0.354f, 0.458f);
    //     // yield return new WaitForSeconds(FramNumber * 3);
    //     // Weapon.localPosition = new Vector2(0.979f, -0.25f);
    //     // yield return new WaitForSeconds(FramNumber * 4);
    //     // Weapon.localPosition = new Vector2(0.479f, -1.0625f);
    //     // yield return new WaitForSeconds(FramNumber * 7);
    //     // player.mAttackRoll = 0;
    //     // player.mPlayerAnimator.SetBool("IsAttack", false);
    //     // player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
    //     yield return new WaitForSeconds(player.mPlayerAnimation[2].length);
    //     player.SetActionType(ActState.STATE_MOVE);
    //     player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
    //     player.mPlayerAnimator.SetBool("IsAttack", false);
    //     mIsAttack = false;
    //     player.mIsCombo = false;

    // }
    // //  왼쪽을 바라보는 플레이어 대검의 행동을 나타 내기 위한 코루틴 함수
    // IEnumerator BigSwordUpCombo3()
    // {
    //     yield return new WaitForSeconds(FramNumber * 1f);
    //     PlayerAct player = GetComponent<PlayerAct>();
    //     player.mPlayerAnimator.SetBool("IsAttackComboThree", true);
    //     player.mWeaponeHitBox.gameObject.SetActive(true);

    //     RectTransform Weapon = player.mPlayerWeaponePosition;


    //     Weapon.localPosition = new Vector2(-0.22f, -0.66f);
    //     yield return new WaitForSeconds(FramNumber * 3);
    //     Weapon.localPosition = new Vector2(0.27f, 0.22f);
    //     yield return new WaitForSeconds(FramNumber * 1);
    //     Weapon.localPosition = new Vector2(-0.04f, 0.49f);
    //     yield return new WaitForSeconds(FramNumber * 1);
    //     Weapon.localPosition = new Vector2(-1.02f, 0.61f);
    //     yield return new WaitForSeconds(FramNumber * 6);
    //     player.mAttackRoll = 0;
    //     player.mPlayerAnimator.SetBool("IsAttack", false);
    //     player.mPlayerAnimator.SetBool("IsAttackComboThree", false);
    //     player.SetActionType(ActState.STATE_MOVE);
    //     player.mIsCombo = false;

    // }



}

