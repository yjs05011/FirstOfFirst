using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSkill : PlayerState
{
    private bool mIsSkill = false;
    private int mAttackCount = 0;

    public override void Action(ActState state)
    {
        PlayerAct player = GetComponent<PlayerAct>();

        mIsSkill = false;
        RectTransform Weapon = player.mPlayerWeaponePosition;

        switch (player.mPlayerNowWeapone)
        {
            case 1:
                StartCoroutine(BigSwordSkill(player.mPlayerDirection, player));
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



    IEnumerator BigSwordSkill(int direct, PlayerAct player)
    {
        player.mPlayerAnimator.SetBool("IsSkill", true);
        player.mWeaponeHitBox.gameObject.SetActive(true);
        player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, 0f);
        player.mWeaponeHitBox.size = new Vector2(4, 4);
        yield return new WaitForSeconds(0.1f);
        player.mPlayerRigid.AddForce(new Vector2(0, -90));
        yield return new WaitForSeconds(0.933f);
        player.mWeaponeHitBox.gameObject.SetActive(false);
        player.SetActionType(ActState.State_Move);
        player.mPlayerAnimator.SetBool("IsSkill", false);
        player.mPlayerAnimator.SetBool("IsSKillHoling", false);
        player.mPlayerAnimator.SetBool("IsSkillUse", false);


    }
    //     IEnumerator BigSwordLeftCombo2()
    //     {
    //         yield return new WaitForSeconds(0.1f);
    //         PlayerAct player = GetComponent<PlayerAct>();

    //         player.mPlayerAnimator.SetBool("IsAttackComboTwo", true);
    //         player.mWeaponeHitBox.gameObject.SetActive(true);
    //         player.mWeaponeHitBoxPosition.localPosition = new Vector2(-1.25f, 0);
    //         player.mWeaponeHitBox.size = new Vector2(2.5f, 2.083f);
    //         RectTransform Weapon = player.mPlayerWeaponePosition;

    //         Weapon.localPosition = new Vector2(0.52f, -0.75f);

    //         yield return new WaitForSeconds(FramNumber * 3f);
    //         Weapon.localPosition = new Vector2(-0.729f, -0.416f);
    //         yield return new WaitForSeconds(FramNumber * 2f);
    //         Weapon.localPosition = new Vector2(0.541f, 0.729f);
    //         yield return new WaitForSeconds(FramNumber * 4f);
    //         Weapon.localPosition = new Vector2(0.27f, 0.541f);

    //         if (player.mAttackRoll > 1)
    //         {
    //             player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
    //             Debug.Log("Combo");
    //             player.mAttackRoll = 0;
    //             player.mIsCombo = true;

    //         }
    //         else
    //         {
    //             player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
    //             player.mAttackRoll = 0;
    //             player.mPlayerAnimator.SetBool("IsAttack", false);
    //             player.SetActionType(ActState.State_Move);
    //         }
    //     }
    //     IEnumerator BigSwordRightCombo2()
    //     {

    //         // yield return new WaitForSeconds(0.1f);
    //         PlayerAct player = GetComponent<PlayerAct>();
    //         Debug.Log(player.mPlayerAnimation[2].length);
    //         player.mPlayerAnimator.SetBool("IsAttackComboTwo", true);
    //         player.mWeaponeHitBox.gameObject.SetActive(true);
    //         player.mWeaponeHitBoxPosition.localPosition = new Vector2(1.25f, 0);
    //         player.mWeaponeHitBox.size = new Vector2(2.5f, 2.083f);
    //         RectTransform Weapon = player.mPlayerWeaponePosition;
    //         yield return new WaitForSeconds(player.mPlayerAnimation[1].length);
    //         // Weapon.localPosition = new Vector2(-0.375f, -0.833f);

    //         // yield return new WaitForSeconds(FramNumber);
    //         // Weapon.localPosition = new Vector2(-0.041f, -0.979f);
    //         // yield return new WaitForSeconds(FramNumber * 2f);
    //         // Weapon.localPosition = new Vector2(0.333f, 0.291f);
    //         // yield return new WaitForSeconds(FramNumber * 2f);
    //         // Weapon.localPosition = new Vector2(-0.666f, 0.833f);
    //         // yield return new WaitForSeconds(FramNumber * 2f);
    //         player.SetActionType(ActState.STATE_ATTACK_COMBO_THREE);
    //         player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
    //         mIsAttack = false;
    //         // if (player.mAttackRoll > 1)
    //         // {
    //         //     player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
    //         //     player.mAttackRoll = 0;
    //         //     player.mIsCombo = true;

    //         // }
    //         // else
    //         // {
    //         //     player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
    //         //     player.mAttackRoll = 0;
    //         //     player.mPlayerAnimator.SetBool("IsAttack", false);
    //         //     player.SetActionType(ActState.State_Move);
    //         // }
    //     }
    //     IEnumerator BigSwordUpCombo2()
    //     {
    //         yield return new WaitForSeconds(0.1f);
    //         PlayerAct player = GetComponent<PlayerAct>();

    //         player.mPlayerAnimator.SetBool("IsAttackComboTwo", true);
    //         player.mWeaponeHitBox.gameObject.SetActive(true);
    //         player.mWeaponeHitBoxPosition.localPosition = new Vector2(0, 1.04f);
    //         player.mWeaponeHitBox.size = new Vector2(1.7f, 2.08f);
    //         RectTransform Weapon = player.mPlayerWeaponePosition;

    //         Weapon.localPosition = new Vector2(-1.24f, 0f);
    //         yield return new WaitForSeconds(FramNumber * 3f);
    //         Weapon.localPosition = new Vector2(0.24f, 0.29f);
    //         yield return new WaitForSeconds(FramNumber * 1f);
    //         Weapon.localPosition = new Vector2(0.13f, 0.65f);
    //         yield return new WaitForSeconds(FramNumber * 1f);
    //         Weapon.localPosition = new Vector2(-0.64f, -0.37f);
    //         yield return new WaitForSeconds(FramNumber * 3f);

    //         if (player.mAttackRoll > 1)
    //         {
    //             player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
    //             Debug.Log("Combo");
    //             player.mAttackRoll = 0;
    //             player.mIsCombo = true;

    //         }
    //         else
    //         {
    //             player.mPlayerAnimator.SetBool("IsAttackComboTwo", false);
    //             player.mAttackRoll = 0;
    //             player.mPlayerAnimator.SetBool("IsAttack", false);
    //             player.SetActionType(ActState.State_Move);
    //         }
    //     }
}