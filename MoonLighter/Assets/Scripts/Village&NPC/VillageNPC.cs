using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageNPC : MonoBehaviour
{

    private Animator mNpcAni;
    private float mSpeed = 1f;
    private Vector3 mPosition;

    // Start is called before the first frame update
    public void Start()
    {
        mNpcAni = GetComponent<Animator>();
        RandomPosition();
    }

    // Update is called once per frame
    public void Update()
    {
        
        Vector3 direction = mPosition - transform.position;
        transform.Translate(direction.normalized * mSpeed * Time.deltaTime);
        mNpcAni.SetBool("IsWalking",true);
        if (Vector3.Distance(transform.position, mPosition) <= 0.2f)
        {
            mNpcAni.SetBool("IsWalking", false);
            
            Invoke("RandomPosition",3);
            
        }
    }

    public void RandomPosition()
    {
        CancelInvoke();
        mPosition = new Vector3(transform.position.x + Random.Range(-3, 4), transform.position.y + Random.Range(-3, 4),0);
        if(mPosition.x > 13) { mPosition = new Vector3(13, mPosition.y, 0); }
        if(mPosition.x < -13) { mPosition = new Vector3(-13, mPosition.y, 0); }
        if(mPosition.y > 6) { mPosition = new Vector3(mPosition.x, 6, 0); }
        if(mPosition.y < -6) { mPosition = new Vector3(mPosition.x, -6, 0); }

        Vector3 direction = mPosition - transform.position;

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
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
    
}
