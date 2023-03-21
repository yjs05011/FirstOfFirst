using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DungeonHealingPool : MonoBehaviour
{
    public List<GameObject> mBubbles = new List<GameObject>();
    public GameObject mPoolLine = null;
    public GameObject mPoolWater = null;
    public Animator mPoolAnimator = null;

    private SpriteRenderer mPoolWaterSprite = null;
    
    private float mBubbleIntervalTime= 1.0f;
    private float mTimer = 0.0f;
    private int mBubbleCounter = 0;
    private bool mIsHealing = false;

    // 힐링 풀 보유 힐량 변수
    public float mHealPoint = 0;

    public void Awake()
    {
        for (int i = 0; i < mBubbles.Count; ++i)
        {
            //mBubbleAnimators[i] = mBubbles[i].GetComponent<Animator>();
        }

        mPoolAnimator= mPoolLine.GetComponent<Animator>();
        mPoolWaterSprite = mPoolWater.GetComponent<SpriteRenderer>();
  
    }



    public void Update()
    {

         if (mTimer > mBubbleIntervalTime)
         {
             mTimer = 0.0f;
             mBubbles[mBubbleCounter].SetActive(true);

             if (mBubbleCounter == mBubbles.Count - 1)
             {
                 mBubbleCounter = 0;
             }
             else
             {
                 ++mBubbleCounter;
             }
         }

        if (mIsHealing)
        {
            Debug.LogFormat("힐 포인트 : {0}", GetPoolHealPoint());

            if (mHealPoint <= 0)
            {
                Debug.LogFormat("힐 empty");
                mIsHealing = false;
                SetPoolHealEmpty();
            }
        }
    }
    public void InitPoolHeal()
    {
        // 보유 힐량 설정 
        mHealPoint = DungeonGenerator.POOL_MAX_HEAL_POINT;
        // pool water 이미지 컬러 기본 컬러 적용
        mPoolWaterSprite.color = Color.white;
    }
    public void SetPoolHealEmpty()
    { 
        // pool water 이미지 컬러 어둡게처리
        mPoolWaterSprite.color = new Color(125.0f, 125.0f, 125.0f);
    }
    
    public float GetPoolHealPoint()
    {
        return mHealPoint;
    }
    public void SetPoolHealPoint(float value)
    {
        mHealPoint += value;
    }


      
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mIsHealing = true;

            StartCoroutine(Healing(other));
            
            Debug.Log("Player Healing");

        }
    }



    IEnumerator Healing(Collider2D other)
    {
        float delay = 0.05f;
        float totalTime = 1.0f;
        PlayerAct player = other.GetComponent<PlayerAct>();
        
       while (player.GetPlayerMaxHp() != player.GetPlayerHp() || GetPoolHealPoint() <= 0)
       {
           SetPoolHealPoint(-1.0f);
           //player.OnHealing(1.0f); 

           totalTime -= delay;
           yield return new WaitForSeconds(totalTime - delay);
       }
       
        
    }

    private void OntrrigerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           mIsHealing = false;
           StopCoroutine(Healing(other));
        }
    }
    
}
