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

        if (player.mIsEvasion) { }
        else
        {
            bool isPool = player.mPlayerAnimator.GetBool("IsPool");

            if (isPool)
            {
                Debug.Log(isPool);
                player.mPlayerAnimator.SetBool("IsPool", false);

            }
            player.mPlayerAnimator.SetBool("IsEvasion", true);
            player.mIsEvasion = true;
            Vector2 Evasion = player.mPlayerRigid.velocity;
            Debug.Log(Evasion);
            player.mPlayerRigid.velocity = Evasion;
            yield return new WaitForSeconds(0.5f);
            player.mIsEvasion = false;
            player.mPlayerAnimator.SetBool("IsEvasion", false);
            // Debug.Log(isPool);
            if (isPool)
            {
                player.mPlayerAnimator.SetBool("IsPool", true);
                player.SetActionType(ActState.State_Enter_Pool);

            }
            else { player.SetActionType(ActState.State_Move); }
            Evasion = Vector2.zero;
            player.mPlayerRigid.velocity = Evasion;
        }
    }

}
