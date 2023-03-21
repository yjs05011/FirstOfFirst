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

    // ���� Ǯ ���� ���� ����
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
            Debug.LogFormat("�� ����Ʈ : {0}", GetPoolHealPoint());

            SetPoolHealEmpty();
        }
    }
    public void InitPoolHeal()
    {
        // ���� ���� ���� 
        mHealPoint = DungeonGenerator.POOL_MAX_HEAL_POINT;
        // pool water �̹��� �÷� �⺻ �÷� ����
        mPoolWaterSprite.color = Color.white;
    }
    public void SetPoolHealEmpty()
    { 
        // pool water �̹��� �÷� ��Ӱ�ó��
        mPoolWaterSprite.color = new Color(125.0f, 125.0f, 125.0f);
    }
    
    public float GetPoolHealPoint()
    {
        return mHealPoint;
    }
    public void SetPoolHealPoint(float value)
    {
        mHealPoint = value;
    }


      
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(Healing(other));
            Debug.Log("Player Healing");

        }
    }

    IEnumerator Healing(Collider2D other)
    {
        float delay = 0.005f;
        float totalTime = 0.1f;
        PlayerAct player = other.GetComponent<PlayerAct>();
        
      // while (player.GetPlayerMaxHp() != player.GetPlayerHp() || GetPoolHealPoint() <= 0)
      // {
      //     SetPoolHealPoint(-1.0f);
      //     player.OnHealing(1.0f);
      //    
      //     totalTime -= delay;
      //     yield return new WaitForSeconds(totalTime - delay);
      // }
        yield return null;
        
    }

    private void OntrrigerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           StopCoroutine(Healing(other));
        }
    }
    
}
