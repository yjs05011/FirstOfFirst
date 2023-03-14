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

        if (player.mIsEvasion) { }
        else
        {
            player.mIsEvasion = true;
            Vector2 Evasion = player.mPlayerRigid.velocity;
            switch (player.mPlayerDirection)
            {
                case 0:
                    player.mPlayerAnimator.SetInteger("Evasion", 2);
                    break;
                case 1:
                    player.mPlayerAnimator.SetInteger("Evasion", 1);
                    break;
                case 2:
                    player.mPlayerAnimator.SetInteger("Evasion", 3);
                    break;
                case 3:
                    player.mPlayerAnimator.SetInteger("Evasion", 4);
                    break;
            }
            player.mPlayerRigid.velocity = Evasion;
            yield return new WaitForSeconds(0.5f);
            player.mPlayerAnimator.SetInteger("Evasion", 0);
            player.mIsEvasion = false;
            yield return new WaitForSeconds(0.01f);

            player.SetActionType(ActState.STATE_MOVE);
        }
    }

}
