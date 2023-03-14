using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerState
{
    private bool mIsAttack = false;
    private int mAttackCount = 0;

    public override void Action(ActState state)
    {
        PlayerAct player = GetComponent<PlayerAct>();
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
                        player.mPlayerAnimator.SetInteger("WeaponDirection", 0);
                        break;
                    case 2:
                        player.mPlayerAnimator.SetInteger("WeaponDirection", 0);
                        break;
                    case 3:
                        player.mPlayerAnimator.SetInteger("WeaponDirection", 0);
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
        player.mPlayerAnimator.SetInteger("IsWeaponType", 1);
        player.mPlayerAnimator.SetInteger("WeaponDirection", 0);
        player.mPlayerAnimator.SetInteger("WeaPonCombo", 1);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, -50);
        player.mWeaponeHitBox.size = new Vector2(80, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;
        Weapon.localPosition = new Vector2(14f, 42f);
        yield return new WaitForSeconds(0.242f);
        Weapon.localPosition = new Vector2(-12f, 37f);
        yield return new WaitForSeconds(0.068f);
        Weapon.localPosition = new Vector2(7f, -40f);
        yield return new WaitForSeconds(0.35f);
        // yield return new WaitForSeconds(0.225f);
        // Weapon.localPosition = new Vector2(48f, -30f);


        if (player.mAttackRoll > 1)
        {
            player.mAttackRoll = 0;

            player.mPlayerAnimator.SetInteger("WeaPonCombo", 2);
            StartCoroutine(BigSwordDownCombo2());
        }
        else
        {
            mIsAttack = false;
            player.mAttackRoll = 0;
            player.mPlayerAnimator.SetInteger("IsWeaponType", 0);
            player.SetActionType(ActState.STATE_MOVE);
        }




    }
    IEnumerator BigSwordDownCombo2()
    {

        PlayerAct player = GetComponent<PlayerAct>();

        player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, -50);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBox.size = new Vector2(80, 100);
        RectTransform Weapon = player.mPlayerWeaponePosition;
        Debug.Log(Weapon.localPosition);
        yield return new WaitForSeconds(0.375f);

        Weapon.localPosition = new Vector2(-2f, -25f);
        Debug.Log(Weapon.localPosition);
        yield return new WaitForSeconds(0.126f);
        Weapon.localPosition = new Vector2(47f, -20f);
        Debug.Log(Weapon.localPosition);
        yield return new WaitForSeconds(0.126f);
        Weapon.localPosition = new Vector2(-15f, -43f);
        Debug.Log(Weapon.localPosition);
        yield return new WaitForSeconds(0.15f);
        Weapon.localPosition = new Vector2(20f, 19f);
        Debug.Log(Weapon.localPosition);

        if (player.mAttackRoll > 1)
        {
            mIsAttack = false;
            player.mAttackRoll = 0;
            player.mPlayerAnimator.SetInteger("IsWeaponType", 0);
            //StartCoroutine(BigSwordDownCombo3());
        }
        else
        {
            mIsAttack = false;
            player.mAttackRoll = 0;
            player.mPlayerAnimator.SetInteger("IsWeaponType", 0);
            player.SetActionType(ActState.STATE_MOVE);
        }
        // IEnumerator BigSwordDownCombo3()
        // {

        //     PlayerAct player = GetComponent<PlayerAct>();
        //     player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, -50);
        //     player.mWeaponeHitBox.gameObject.SetActive(true);
        //     player.mWeaponeHitBox.size = new Vector2(80, 100);
        //     RectTransform Weapon = player.mPlayerWeaponePosition;
        //     Weapon.localPosition = new Vector2(46f, -27f);
        //     yield return new WaitForSeconds(0.2f);
        //     Weapon.localPosition = new Vector2(-2f, -25f);
        //     yield return new WaitForSeconds(0.0375f);
        //     Weapon.localPosition = new Vector2(-15f, -43f);
        //     yield return new WaitForSeconds(0.15f);
        //     Weapon.localPosition = new Vector2(20f, 19f);
        //     player.SetActionType(ActState.STATE_MOVE);
        //     player.mPlayerAnimator.SetInteger("IsWeaponType", 0);
        //     if (mAttackCount > 1)
        //     {
        //         mAttackCount = 0;
        //         StartCoroutine(BigSwordDownCombo3());
        //     }
        //     else
        //     {
        //         mIsAttack = false;
        //     }




        // }
    }
}
