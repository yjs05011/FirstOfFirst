using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{
    private Animator blackSmithAni;
    private float time;
    private int waitForTime;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        waitForTime = Random.Range(6, 10);
        blackSmithAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > waitForTime) 
        {
            time = 0;
            int mAni = Random.Range(0, 2);
            switch (mAni)
            {
                case 0:
                    blackSmithAni.SetTrigger("belt");
                    break;
                case 1:
                    blackSmithAni.SetTrigger("gaunlet");
                    break;
            }
            waitForTime = Random.Range(6, 10);
        }

    }
}
