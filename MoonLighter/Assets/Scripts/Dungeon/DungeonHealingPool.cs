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

    // 힐링 풀 보유 힐량 변수
    public int mHealPoint = 0;

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
        mTimer += Time.deltaTime;

        if(mTimer > mBubbleIntervalTime)
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

        if(mHealPoint <= 0)
        {
            Debug.LogFormat("힐 포인트 : {0}", GetPoolHealPoint());

            SetPoolHealEmpty();
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
    
    public int GetPoolHealPoint()
    {
        return mHealPoint;
    }
    public void SetPoolHealPoint(int value)
    {
        mHealPoint = value;
    }



    // [ 테스트 코드 ] ===============================================================================
    // 일단 힐 풀 기능 확인용으로 넣어둠
    // 플레이어가 입장시 플레이어 HP 증가 시키고, pool 의 보유 hp 차감 필요
    // 플레이어 exit or pool 보유 hp 0 이되면 더이상 힐안되게 처리 필요
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("Player Healing");

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (mHealPoint > 0)
            {
                mHealPoint -= 1;
            }
        }
    }
    // [ 테스트 코드 ] ===============================================================================
}
