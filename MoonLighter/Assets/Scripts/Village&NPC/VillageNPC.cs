using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageNPC : MonoBehaviour
{
    public GameObject mWaypoints;

    private Rigidbody2D mNpc;
    private Waypoint mWaypoint;
    private Animator mNpcAni;
    private Transform mTarget;
    private float mSpeed = 1f;
    private int mWavepointIndex = 0;


    // Start is called before the first frame update
    public void Start()
    {
        mWaypoint = mWaypoints.GetComponent<Waypoint>();
        mTarget = mWaypoint.points[0];
        mNpcAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 direction = mTarget.position - transform.position;
        transform.Translate(direction.normalized * mSpeed * Time.deltaTime);
        mNpcAni.SetBool("IsWalking",true);
        if (Vector3.Distance(transform.position, mTarget.position) <= 0.2f)
        {
            mNpcAni.SetBool("IsWalking", false);
            GetNextWaypoint();
        }
    }

    public void GetNextWaypoint()
    {
        mWavepointIndex++;
        if (mWavepointIndex >= mWaypoint.points.Length)
        {
            mWavepointIndex = 0;
            
        }
        
        mTarget = mWaypoint.points[mWavepointIndex];

        Vector3 direction = mTarget.position - transform.position;
        
        if(Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0)
            {
                mNpcAni.SetInteger("Direction", 3);
            }
            else
            {
                mNpcAni.SetInteger("Direction", 0);
            }
        }
        else
        {
            if (direction.x > 0)
            {
                mNpcAni.SetInteger("Direction", 2);
            }
            else
            {
                mNpcAni.SetInteger("Direction", 1);
            }
        }
    }

    //IEnumerator MoveNPC()
    //{
    //    while (true)
    //    {
    //        int direction1 = Random.Range(-1, 2);
    //        int direction2 = Random.Range(-1, 2);
    //        Vector3 localPos = gameObject.transform.localPosition;

    //        if (direction1 != 0 && direction2 != 0)
    //        {
    //            mNpc.velocity = new Vector3(direction1, direction2, 0) / Mathf.Sqrt(2);
    //        }
    //        else
    //        {
    //            mNpc.velocity = new Vector3(direction1, direction2, 0);
    //        }

    //        yield return new WaitForSeconds(5);
    //        mNpc.velocity = Vector3.zero;
    //        yield return new WaitForSeconds(5);
    //    }
    //}

}
