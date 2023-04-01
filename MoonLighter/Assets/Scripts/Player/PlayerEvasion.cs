using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvasion : PlayerState
{

    public override void Action(ActState state)
    {
        StartCoroutine(Evasion());

    }

    IEnumerator Evasion()
    {

        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerPosCheck = player.mPlayerRigid.position;
        if (player.mIsEvasion) { }
        else
        {
            bool isPool = player.mPlayerAnimator.GetBool("IsPool");

            if (isPool)
            {
                player.mPlayerAnimator.SetBool("IsPool", false);

            }
            player.mPlayerAnimator.SetBool("IsEvasion", true);
            player.mIsEvasion = true;

            Vector2 Evasion = player.mPlayerRigid.velocity;
            player.mPlayerRigid.velocity = Evasion;
            player.mPlayerHitBox.isTrigger = true;
            yield return new WaitForSeconds(0.2f);
            player.mPlayerHitBox.isTrigger = false;
            yield return new WaitForSeconds(0.3f);

            player.mIsEvasion = false;
            player.mPlayerAnimator.SetBool("IsEvasion", false);
            // Debug.Log(isPool);
            if (isPool)
            {
                player.mPlayerAnimator.SetBool("IsPool", true);
                player.SetActionType(ActState.State_Enter_Pool);

            }
            else
            {
                player.SetActionType(ActState.State_Move);
            }
            Evasion = Vector2.zero;
            player.mPlayerRigid.velocity = Evasion;
        }
    }

}
