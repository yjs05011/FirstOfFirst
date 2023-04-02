using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class DungeonBreakable : MonoBehaviour
{
    public GameObject mBreakable  = null;
    public GameObject mBroken = null;

    public enum BreakType { None, Bump, Attack };
    public BreakType mType = BreakType.None;    

    private void Start()
    {
        mBreakable = transform.Find("Breakable").gameObject;
        mBroken = transform.Find("Broken").gameObject;

        mBreakable.SetActive(true);
        mBroken.SetActive(false);
    }



    // 플레이어가 부딪히면 깨지는 오브젝트
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && mType == BreakType.Bump)
        {
            mBreakable.SetActive(false);
            mBroken.SetActive(true);
        }
    }

    // 공격 받아야 깨지는 오브젝트 
    public void OnBreak()
    {
        if (mType == BreakType.Attack)
        {
            mBreakable.SetActive(false);
            mBroken.SetActive(true);
        }
    }
   
}
