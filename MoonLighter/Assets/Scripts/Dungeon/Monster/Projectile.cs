using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject mExplosionPreset = null;

    public Monster mOwner = null;
    public Vector3 mDirection = Vector3.zero;

    public Vector3 mStartPosition = Vector3.zero;

    [Range(0.1f, 100.0f)]
    public float mSpeed = 1.5f;

    [Range(1.0f, 1000.0f)]
    public float mEffectiveRange = 10.0f;

    public void SetData(Monster owner, Vector3 direction)
    {
        mOwner = owner;
        mDirection = direction;

        mStartPosition = this.transform.position;
    }

    public void Update()
    {
        Vector3 movement = mDirection * mSpeed * Time.deltaTime;
        this.transform.Translate(movement, Space.World);
        if(!IsEffectiveRange())
        {
            // Pool(Push)
            GameObject.Destroy(this.gameObject);
        }
    }

    public bool IsEffectiveRange()
    {
        Vector3 from = mStartPosition; //시작위치
        Vector3 to = this.transform.position; //현재위치

        Vector3 distance = from - to; //거리

        if(distance.magnitude <= mEffectiveRange)
        {
            return true;
        }
        return false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if(collision.CompareTag("Player"))
        {
            PlayerAct player = collision.GetComponent<PlayerAct>();
            if(player)
            {
                player.OnDamage(mOwner.GetDamage());

                // Pool(Push)
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
