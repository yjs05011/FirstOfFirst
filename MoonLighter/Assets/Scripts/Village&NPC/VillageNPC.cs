using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageNPC : MonoBehaviour
{

    private Rigidbody2D mNpc;
    
    // Start is called before the first frame update
    public void Start()
    {
        mNpc = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    IEnumerator MoveNPC()
    {
        while (true)
        {
            int direction1 = Random.Range(-1, 2);
            int direction2 = Random.Range(-1, 2);
            Vector3 localPos = gameObject.transform.localPosition;

            if (direction1 != 0 && direction2 != 0)
            {
                mNpc.velocity = new Vector3(direction1, direction2, 0) / Mathf.Sqrt(2);
            }
            else
            {
                mNpc.velocity = new Vector3(direction1, direction2, 0);
            }

            yield return new WaitForSeconds(5);
            mNpc.velocity = Vector3.zero;
            yield return new WaitForSeconds(5);
        }
    }

}
