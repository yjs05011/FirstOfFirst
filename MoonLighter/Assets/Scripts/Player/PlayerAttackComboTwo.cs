using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComboTwo : PlayerState
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
                            StartCoroutine(BigSwordDownCombo2());
                        }
                        break;
                    case 1:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordUpCombo2());
                        }
                        break;
                    case 2:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordLeftCombo2());
                        }

                        break;
                    case 3:
                        if (!mIsAttack)
                        {
                            mIsAttack = true;
                            StartCoroutine(BigSwordRightCombo2());
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



    IEnumerator BigSwordDownCombo2()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerAct player = GetComponent<PlayerAct>();

        player.mPlayerAnimator.SetBool("IsAttackComboTwo", true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, -50);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBox.size = new Vector2(80, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;


        Weapon.localPosition = new Vector2(48f, -30f);

        yield return new WaitForSeconds(0.195f);
        Weapon.localPosition = new Vector2(-4f, -24f);

        yield return new WaitForSeconds(0.06f);
        Weapon.localPosition = new Vector2(-13f, -42f);
        yield return new WaitForSeconds(0.04f);
        Weapon.localPosition = new Vector2(-17f, -39f);
        yield return new WaitForSeconds(0.03f);
        Weapon.localPosition = new Vector2(20f, 32f);
        yield return new WaitForSeconds(0.1f);
        Weapon.localPosition = new Vector2(26f, 28f);

        if (player.mAttackRoll > 1)
        {
            player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
            Debug.Log("Combo");
            player.mAttackRoll = 0;
            player.mIsCombo = true;

        }
        else
        {
            player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
            player.mAttackRoll = 0;
            player.mPlayerAnimator.SetBool("IsAttack", false);
            player.SetActionType(ActState.STATE_MOVE);
        }


    }
    IEnumerator BigSwordLeftCombo2()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerAct player = GetComponent<PlayerAct>();

        player.mPlayerAnimator.SetBool("IsAttackComboTwo", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(-60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;

        Weapon.localPosition = new Vector2(25f, -36f);

        yield return new WaitForSeconds(FramNumber * 3f);
        Weapon.localPosition = new Vector2(-35f, -20f);
        yield return new WaitForSeconds(FramNumber * 2f);
        Weapon.localPosition = new Vector2(26f, 35f);
        yield return new WaitForSeconds(FramNumber * 4f);
        Weapon.localPosition = new Vector2(13f, 26f);

        if (player.mAttackRoll > 1)
        {
            player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
            Debug.Log("Combo");
            player.mAttackRoll = 0;
            player.mIsCombo = true;

        }
        else
        {
            player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
            player.mAttackRoll = 0;
            player.mPlayerAnimator.SetBool("IsAttack", false);
            player.SetActionType(ActState.STATE_MOVE);
        }
    }
    IEnumerator BigSwordRightCombo2()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerAct player = GetComponent<PlayerAct>();

        player.mPlayerAnimator.SetBool("IsAttackComboTwo", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;

        Weapon.localPosition = new Vector2(-18f, -40f);

        yield return new WaitForSeconds(FramNumber);
        Weapon.localPosition = new Vector2(-2f, -47f);
        yield return new WaitForSeconds(FramNumber * 2f);
        Weapon.localPosition = new Vector2(16f, 14f);
        yield return new WaitForSeconds(FramNumber * 2f);
        Weapon.localPosition = new Vector2(-32f, 40f);
        yield return new WaitForSeconds(FramNumber * 5f);

        if (player.mAttackRoll > 1)
        {
            player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
            Debug.Log("Combo");
            player.mAttackRoll = 0;
            player.mIsCombo = true;

        }
        else
        {
            player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
            player.mAttackRoll = 0;
            player.mPlayerAnimator.SetBool("IsAttack", false);
            player.SetActionType(ActState.STATE_MOVE);
        }
    }
    IEnumerator BigSwordUpCombo2()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerAct player = GetComponent<PlayerAct>();

        player.mPlayerAnimator.SetBool("IsAttackComboTwo", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(-60, 0);
        player.mWeaponeHitBox.size = new Vector2(120, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;

        Weapon.localPosition = new Vector2(25f, -36f);

        yield return new WaitForSeconds(FramNumber * 3f);
        Weapon.localPosition = new Vector2(-35f, -20f);
        yield return new WaitForSeconds(FramNumber * 2f);
        Weapon.localPosition = new Vector2(26f, 35f);
        yield return new WaitForSeconds(FramNumber * 4f);
        Weapon.localPosition = new Vector2(13f, 26f);

        if (player.mAttackRoll > 1)
        {
            player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
            Debug.Log("Combo");
            player.mAttackRoll = 0;
            player.mIsCombo = true;

        }
        else
        {
            player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
            player.mAttackRoll = 0;
            player.mPlayerAnimator.SetBool("IsAttack", false);
            player.SetActionType(ActState.STATE_MOVE);
        }
    }
}
