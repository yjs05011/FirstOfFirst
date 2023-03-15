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
                switch (player.mPlayerDirection)
                {
                    case 0:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordDownCombo1());
                        }
                        break;
                    case 1:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordUpCombo1());
                        }
                        break;
                    case 2:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordLeftCombo1());
                        }

                        break;
                    case 3:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordRightCombo1());
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

    IEnumerator BigSwordDownCombo1()
    {

        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerRigid.velocity = Vector2.zero;
        player.mPlayerAnimator.SetBool("IsAttackComboOne", true);
        player.mPlayerAnimator.SetBool("IsAttack", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, -50);
        player.mWeaponeHitBox.size = new Vector2(80, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;
        Weapon.localPosition = new Vector2(14f, 42f);
        yield return new WaitForSeconds(FramNumber * 4f);
        Weapon.localPosition = new Vector2(-12f, 37f);
        yield return new WaitForSeconds(FramNumber);
        Weapon.localPosition = new Vector2(7f, -40f);
        yield return new WaitForSeconds(FramNumber * 4f);
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
    IEnumerator BigSwordLeftCombo1()
    {
        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerRigid.velocity = Vector2.zero;
        player.mPlayerAnimator.SetBool("IsAttackComboOne", true);
        player.mPlayerAnimator.SetBool("IsAttack", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(-60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;
        Weapon.localPosition = new Vector2(25f, 19f);
        yield return new WaitForSeconds(FramNumber * 6f);
        Weapon.localPosition = new Vector2(-53f, -12f);
        yield return new WaitForSeconds(FramNumber * 5f);

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
    IEnumerator BigSwordRightCombo1()
    {
        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerRigid.velocity = Vector2.zero;
        player.mPlayerAnimator.SetBool("IsAttackComboOne", true);
        player.mPlayerAnimator.SetBool("IsAttack", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;
        Weapon.localPosition = new Vector2(-20f, 20f);
        yield return new WaitForSeconds(FramNumber * 5f);
        Weapon.localPosition = new Vector2(47f, -6f);
        yield return new WaitForSeconds(FramNumber * 3f);
        Weapon.localPosition = new Vector2(28f, -43f);
        yield return new WaitForSeconds(FramNumber * 3f);

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
    IEnumerator BigSwordUpCombo1()
    {
        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerRigid.velocity = Vector2.zero;
        player.mPlayerAnimator.SetBool("IsAttackComboOne", true);
        player.mPlayerAnimator.SetBool("IsAttack", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(-60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;
        Weapon.localPosition = new Vector2(25f, 19f);
        yield return new WaitForSeconds(FramNumber * 6f);
        Weapon.localPosition = new Vector2(-53f, -12f);
        yield return new WaitForSeconds(FramNumber * 5f);

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

}

