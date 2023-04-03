using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : PlayerState
{
    // Start is called before the first frame update
    public override void Action(ActState state)
    {
        StartCoroutine(OnFalling(0.5f));

    }


    // Update is called once per frame
    IEnumerator OnFalling(float Delay)
    {
        PlayerAct player = GetComponent<PlayerAct>();
        if (player.mIsDelay || player.mState == ActState.State_Die)
        {
        }
        else
        {
            player.mIsDelay = true;
            player.mPlayerHp -= Mathf.Floor(player.mPlayerMaxHp / 10);
            PlayerManager.Instance.mPlayerStat.Hp -= Mathf.Floor(player.mPlayerMaxHp / 10);
            player.mPlayerAnimator.SetTrigger("IsFalling");
            UiManager.Instance.mIsHpChange = true;
            player.mPlayerRigid.velocity *= 0.1f;
            yield return new WaitForSeconds(Delay);
            transform.position = DungeonManager.Instance.GetPlayerCurrStage().GetEntryPosition();
            if (player.mPlayerHp <= 0)
            {
                player.mPlayerAnimator.SetTrigger("IsDie");
                player.SetActionType(ActState.State_Die);

                PlayerManager.Instance.mPlayerStat.isDie = true;
            }
            else
            {
                player.SetActionType(ActState.State_Move);
            }

            player.mIsDelay = false;
            player.mIsFall = false;
        }


    }
}
