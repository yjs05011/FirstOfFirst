using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{

    // Start is called before the first frame update
    public override void Action(ActState state)
    {
        PlayerAct player = GetComponent<PlayerAct>();

        float Horizontal = 0;
        float vertical = 0;

        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        {
            player.mIsMove = true;
            player.mPlayerAnimator.SetFloat("InputX", 0);
            player.mPlayerAnimator.SetFloat("InputY", 1);
            player.mPlayerAnimator.SetBool("IsRun", true);
            player.mPlayerDirection = 1;
            vertical = 1;

        }
        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.DOWN]))
        {
            player.mIsMove = true;
            player.mPlayerAnimator.SetFloat("InputX", 0);
            player.mPlayerAnimator.SetFloat("InputY", -1);
            player.mPlayerAnimator.SetBool("IsRun", true);
            // mPlayerAnimator.SetInteger("Run", 2);
            // mPlayerAnimator.SetInteger("Idle", 1);
            player.mPlayerDirection = 0;
            vertical = -1;
        }
        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.LEFT]))
        {
            player.mIsMove = true;
            player.mPlayerAnimator.SetFloat("InputX", -1);
            player.mPlayerAnimator.SetFloat("InputY", 0);
            player.mPlayerAnimator.SetBool("IsRun", true);
            // mPlayerAnimator.SetInteger("Run", 3);
            // mPlayerAnimator.SetInteger("Idle", 3);
            player.mPlayerDirection = 2;
            Horizontal = -1;
        }
        if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.RIGHT]))
        {
            player.mIsMove = true;
            player.mPlayerAnimator.SetFloat("InputX", +1);
            player.mPlayerAnimator.SetFloat("InputY", 0);
            player.mPlayerAnimator.SetBool("IsRun", true);
            // mPlayerAnimator.SetInteger("Run", 4);
            // mPlayerAnimator.SetInteger("Idle", 4);
            player.mPlayerDirection = 3;
            Horizontal = 1;

        }

        if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        {
            player.mIsMove = false;
            player.mPlayerAnimator.SetBool("IsRun", false);
        }
        if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.DOWN]))
        {
            player.mIsMove = false;
            player.mPlayerAnimator.SetBool("IsRun", false);
        }
        if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.LEFT]))
        {
            player.mIsMove = false;
            player.mPlayerAnimator.SetBool("IsRun", false);
        }
        if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.RIGHT]))
        {
            player.mIsMove = false;
            player.mPlayerAnimator.SetBool("IsRun", false);

        }
        float xSpeed = Horizontal * player.mPlayerSpeed;
        float ySpeed = vertical * player.mPlayerSpeed;

        Vector2 playerVector = new Vector2(xSpeed, ySpeed);
        if (Horizontal != 0 && vertical != 0)
        {
            playerVector /= Mathf.Sqrt(2);
        }
        player.mPlayerRigid.velocity = playerVector;
        if (!player.mIsMove)
        {
            player.mPlayerAnimator.SetBool("IsRun", false);
        }
        if (player.mIsMove)
        {
            player.mTime += Time.deltaTime;
            if (player.mTime > 1f)
            {
                player.mTime = 0;

                player.SetPosToFallCheck();


            }
        }

    }
}
