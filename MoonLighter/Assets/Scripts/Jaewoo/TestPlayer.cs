using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private float mMoveSpeed = 5f;
    private bool misMove = false;
    private bool misPickUp = false;
    private Inventory mInventoryTest;
    public Rigidbody2D mPlayerRigid = default;

    public int mPlayerDamage = 5;

    void Start()
    {
        mPlayerRigid = gameObject.GetComponent<Rigidbody2D>();
        misMove = false;
    }

    void Update()
    {
        if (Inventory.mIsInventoryActiveCheck == false)
        {
            if (!misMove)
            {
                mPlayerRigid.velocity = Vector2.zero;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                misMove = true;
                mPlayerRigid.velocity = Vector2.right * mMoveSpeed;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                misMove = false;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                misMove = true;
                mPlayerRigid.velocity = Vector2.left * mMoveSpeed;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                misMove = false;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                misMove = true;
                mPlayerRigid.velocity = Vector2.up * mMoveSpeed;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                misMove = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                misMove = true;
                mPlayerRigid.velocity = Vector2.down * mMoveSpeed;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                misMove = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ItemManager.Instance.DropItem(Vector3.zero);
        }
    }











}
