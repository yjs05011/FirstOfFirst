using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComboOne : PlayerState
{
    private bool mIsAttack = false;
    private int mAttackCount = 0;

    public override void Action(ActState state)
    {
        PlayerAct player = GetComponent<PlayerAct>();
        player.mIsCombo = false;
        RectTransform Weapon = player.mPlayerWeaponePosition;

        switch (player.mPlayerNowWeapone)
        {
            case 1:
                if (!mIsAttack)
                {
                    mIsAttack = true;
                    StartCoroutine(BigSwordCombo1(player.mPlayerDirection, player));
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

    IEnumerator BigSwordCombo1(int direct, PlayerAct player)
    {
        player.mPlayerRigid.velocity = Vector2.zero;
        player.mPlayerAnimator.SetBool("IsAttackComboOne", true);
        player.mPlayerAnimator.SetBool("IsAttack", true);
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
        yield return new WaitForSeconds(player.mPlayerAnimation[0].length);
        player.mWeaponeHitBox.gameObject.SetActive(false);
        if (player.mAttackRoll > 1)
        {
            player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
            player.mAttackRoll = 0;
            player.mIsCombo = true;
        }
        else
        {
            player.mAttackRoll = 0;
            player.mPlayerAnimator.SetBool("IsAttack", false);
            player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
            player.SetActionType(ActState.STATE_MOVE);
        }




    }
    // IEnumerator BigSwordLeftCombo1()
    // {
    //     PlayerAct player = GetComponent<PlayerAct>();
    //     player.mPlayerRigid.velocity = Vector2.zero;
    //     player.mPlayerAnimator.SetBool("IsAttackComboOne", true);
    //     player.mPlayerAnimator.SetBool("IsAttack", true);
    //     player.mWeaponeHitBox.gameObject.SetActive(true);
    //     player.mWeaponeHitBoxPosition.localPosition = new Vector2(-1.25f, 0);
    //     player.mWeaponeHitBox.size = new Vector2(2.5f, 2.08f);
    //     RectTransform Weapon = player.mPlayerWeaponePosition;
    //     Weapon.localPosition = new Vector2(0.52f, 0.395f);
    //     yield return new WaitForSecondsRealtime(FramNumber * 6f);
    //     Weapon.localPosition = new Vector2(-1.104f, -0.2f);
    //     yield return new WaitForSecondsRealtime(FramNumber * 5f);

    //     if (player.mAttackRoll > 1)
    //     {
    //         player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
    //         player.mAttackRoll = 0;
    //         player.mIsCombo = true;
    //     }
    //     else
    //     {
    //         player.mAttackRoll = 0;
    //         player.mPlayerAnimator.SetBool("IsAttack", false);
    //         player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
    //         player.SetActionType(ActState.STATE_MOVE);
    //     }




    // }
    // IEnumerator BigSwordRightCombo1()
    // {

    //     PlayerAct player = GetComponent<PlayerAct>();

    //     Debug.Log(player.mPlayerAnimation[0].length);
    //     player.mPlayerRigid.velocity = Vector2.zero;
    //     player.mPlayerAnimator.SetBool("IsAttackComboOne", true);
    //     player.mPlayerAnimator.SetBool("IsAttack", true);
    //     player.mWeaponeHitBox.gameObject.SetActive(true);
    //     // player.mWeaponeHitBoxPosition.localPosition = new Vector2(1.25f, 0);
    //     // player.mWeaponeHitBox.size = new Vector2(2.5f, 2.08f);
    //     // RectTransform Weapon = player.mPlayerWeaponePosition;
    //     // Weapon.localPosition = new Vector2(-0.416f, 0.416f);
    //     // yield return new WaitForSecondsRealtime(FramNumber * 5f);
    //     // Weapon.localPosition = new Vector2(1f, -0.125f);
    //     // yield return new WaitForSecondsRealtime(FramNumber * 3f);
    //     // Weapon.localPosition = new Vector2(0.583f, -0.895f);
    //     // yield return new WaitForSecondsRealtime(FramNumber * 3f);
    //     yield return new WaitForSeconds(player.mPlayerAnimation[0].length);
    //     player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
    //     player.SetActionType(ActState.STATE_ATTACK_COMBO_TWO);
    //     mIsAttack = false;

    //     // if (player.mAttackRoll > 1)
    //     // {
    //     //     player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
    //     //     player.mAttackRoll = 0;
    //     //     player.mIsCombo = true;
    //     // }
    //     // else
    //     // {
    //     //     player.mAttackRoll = 0;
    //     //     player.mPlayerAnimator.SetBool("IsAttack", false);
    //     //     player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
    //     //     player.SetActionType(ActState.STATE_MOVE);
    //     // }




    // }
    // IEnumerator BigSwordUpCombo1()
    // {
    //     PlayerAct player = GetComponent<PlayerAct>();
    //     player.mPlayerRigid.velocity = Vector2.zero;
    //     player.mPlayerAnimator.SetBool("IsAttackComboOne", true);
    //     player.mPlayerAnimator.SetBool("IsAttack", true);
    //     player.mWeaponeHitBox.gameObject.SetActive(true);
    //     player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, 1.04f);
    //     player.mWeaponeHitBox.size = new Vector2(1.7f, 2.08f);
    //     RectTransform Weapon = player.mPlayerWeaponePosition;
    //     Weapon.localPosition = new Vector2(-0.21f, -0.51f);
    //     yield return new WaitForSecondsRealtime(FramNumber * 4f);
    //     Weapon.localPosition = new Vector2(0.57f, -0.39f);
    //     yield return new WaitForSecondsRealtime(FramNumber * 1f);
    //     Weapon.localPosition = new Vector2(0.21f, 0.31f);
    //     yield return new WaitForSecondsRealtime(FramNumber * 1f);
    //     Weapon.localPosition = new Vector2(-0.1f, 0.54f);
    //     yield return new WaitForSecondsRealtime(FramNumber * 2f);
    //     Weapon.localPosition = new Vector2(-0.91f, 0.42f);

    //     if (player.mAttackRoll > 1)
    //     {
    //         player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
    //         player.mAttackRoll = 0;
    //         player.mIsCombo = true;
    //     }
    //     else
    //     {
    //         player.mAttackRoll = 0;
    //         player.mPlayerAnimator.SetBool("IsAttack", false);
    //         player.mPlayerAnimator.SetBool("IsAttackComboOne", false);
    //         player.SetActionType(ActState.STATE_MOVE);
    //     }




    // }

}

