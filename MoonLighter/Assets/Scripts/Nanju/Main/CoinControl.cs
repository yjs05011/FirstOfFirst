using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinControl : MonoBehaviour
{
    public Text mGoldText;
    private int mCurrentGoldText;
    public Sprite[] mGoldSprites;
    public SpriteRenderer mGold;




    // Start is called before the first frame update
    void Start()
    {

        // mCurrentGoldText = int.Parse(mGoldText.text);
        mCurrentGoldText = (int)PlayerManager.Instance.mPlayerStat.Money;
        mGoldText.text = $"{mCurrentGoldText}";
    }

    // Update is called once per frame
    void Update()
    {
        MoneyChage();

    }
    void MoneyChage()
    {
        if (PlayerManager.Instance.mIsMoneyChange)
        {
            mCurrentGoldText = (int)PlayerManager.Instance.mPlayerStat.Money;
            mGoldText.text = $"{mCurrentGoldText}";
            // 게임오브젝트 . 
            // PlayerManager.Instance.mIsMoneyChange = false;
            if (0 <= mCurrentGoldText && mCurrentGoldText <= 10000)
            {
                mGold.sprite = mGoldSprites[0];
            }
            else if (11000 <= mCurrentGoldText && mCurrentGoldText <= 20000)
            {
                mGold.sprite = mGoldSprites[1];
            }
            else if (21000 <= mCurrentGoldText && mCurrentGoldText <= 30000)
            {
                mGold.sprite = mGoldSprites[2];
            }
            else if (31000 <= mCurrentGoldText && mCurrentGoldText <= 40000)
            {
                mGold.sprite = mGoldSprites[3];
            }
            else if (41000 <= mCurrentGoldText && mCurrentGoldText <= 50000)
            {
                mGold.sprite = mGoldSprites[4];
            }
            else if (51000 <= mCurrentGoldText && mCurrentGoldText <= 60000)
            {
                mGold.sprite = mGoldSprites[5];
            }
            else
            {
                mGold.sprite = mGoldSprites[5];
            }

            PlayerManager.Instance.mIsMoneyChange = false;
        }

    }
}
