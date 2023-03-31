using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    private Animator mWitchAni;
    private float mTime;
    private int mWaitForTime;
    // Start is called before the first frame update
    void Start()
    {
        mTime = 0;
        mWaitForTime = Random.Range(6, 10);
        mWitchAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mTime += Time.deltaTime;
        if (mTime > mWaitForTime)
        {
            mTime = 0;
            int mAni = Random.Range(0, 2);
            switch (mAni)
            {
                case 0:
                    mWitchAni.SetTrigger("Cape");
                    break;
                case 1:
                    mWitchAni.SetTrigger("Hair");
                    break;
            }
            mWaitForTime = Random.Range(6, 10);
        }

    }
}
