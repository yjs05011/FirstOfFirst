using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum State { None, Move, Explosion }

    [Header("Componenet")]
    public Animator mAnimator = null;
    public AnimationEvent mAnimationEvent = null;

    [Header("Projectile Table")]
    [Range(0.1f, 100.0f)]
    public float mSpeed = 1.5f;

    [Range(1.0f, 1000.0f)]
    public float mEffectiveRange = 10.0f;

    [Header("Projectile Data")]
    public State mState = State.None;

    public Monster mOwner = null;
    public Vector3 mDirection = Vector3.zero;

    public Vector3 mStartPosition = Vector3.zero;

    public Transform mProjectile = null;


    public void SetData(Monster owner, Vector3 direction)
    {
        mOwner = owner;
        mDirection = direction;

        mStartPosition = this.transform.position;
        mAnimationEvent.SetDelegate(OnAnimationEvent);
        mState = State.Move;
    }

    public void SetRotation(DungeonUtils.Direction direction)
    {
        if(!mProjectile)
        {
            return;
        }

        switch(direction)
        {
            case DungeonUtils.Direction.Up: { mProjectile.transform.Rotate(new Vector3(0.0f, 0.0f, 180.0f)); break; }
            case DungeonUtils.Direction.Down: { mProjectile.transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f)); break; }
            case DungeonUtils.Direction.Left: { mProjectile.transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f)); break; }
            case DungeonUtils.Direction.Right: { mProjectile.transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f)); break; }
        }
    }

    public void Update()
    {
        if (mState == State.Move)
        {
            Vector3 movement = mDirection * mSpeed * Time.deltaTime;
            this.transform.Translate(movement, Space.World);
            if (!IsEffectiveRange() || !mOwner.IsMovablePosition(this.transform.position))
            {
                // Pool(Push)
                this.Explosion();
            }
        }
    }

    public bool IsEffectiveRange()
    {
        Vector3 from = mStartPosition; //시작위치
        Vector3 to = this.transform.position; //현재위치

        Vector3 distance = from - to; //거리

        if (distance.magnitude <= mEffectiveRange)
        {
            return true;
        }
        return false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            return;
        }
        this.Explosion();

        Debug.Log("OnTriggerEnter2D");
        if (collision.CompareTag("Player"))
        {
            PlayerAct player = collision.GetComponent<PlayerAct>();
            if (player)
            {
                player.OnDamage(mOwner.GetDamage());
            }
        }
    }

    public void Explosion()
    {
        //어딘가 부딪힌 경우, 폭발 애니메이션 출력
        mState = State.Explosion;
        mAnimator.SetTrigger("Explosion");
    }


    public void OnAnimationEvent(string name)
    {
        if ("destroy".Equals(name, System.StringComparison.OrdinalIgnoreCase))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
