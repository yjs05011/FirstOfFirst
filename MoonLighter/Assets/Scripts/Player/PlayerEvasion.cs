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

        Debug.Log("!!!");
        PlayerAct player = GetComponent<PlayerAct>();
        player.mPlayerAnimator.SetBool("IsEvasion", true);
        if (player.mIsEvasion) { }
        else
        {
            player.mIsEvasion = true;
            Vector2 Evasion = player.mPlayerRigid.velocity;
            player.mPlayerRigid.velocity = Evasion;
            yield return new WaitForSeconds(0.5f);

            player.mIsEvasion = false;
            yield return new WaitForSeconds(0.01f);
            player.mPlayerAnimator.SetBool("IsEvasion", false);
            player.SetActionType(ActState.STATE_MOVE);
        }
    }

}
