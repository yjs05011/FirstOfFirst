using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSoundControll : UIController
{
    int mGameMusic;
    public GameObject[] mBarGauge;
    RectTransform[] mBarGaugeRect;
    Vector2 mSmallSize = new Vector2(30, 5);
    Vector2 mBigSize = new Vector2(30, 30);
    private void OnEnable()
    {
        mBarGaugeRect = new RectTransform[mBarGauge.Length];
        for (int i = 0; i < mBarGauge.Length; i++)
        {
            mBarGaugeRect[i] = mBarGauge[i].GetComponent<RectTransform>();
        }
        GaugeChage();


    }
    public override void Runing(bool flag)
    {
        if (flag)
        {
            if (SoundManager.Instance.music >= 9)
            {

            }
            else
            {
                SoundManager.Instance.music++;
                GaugeChage();

            }
        }
        else
        {
            if (SoundManager.Instance.music <= 0)
            {

            }
            else
            {
                SoundManager.Instance.music--;
                GaugeChage();

            }
        }
    }
    void GaugeChage()
    {

        mGameMusic = SoundManager.Instance.music;
        for (int i = 0; i < mGameMusic; i++)
        {
            mBarGaugeRect[i].sizeDelta = mBigSize;
        }
        for (int i = mGameMusic; i < 9; i++)
        {
            mBarGaugeRect[i].sizeDelta = mSmallSize;
        }
        transform.GetChild(0).GetComponent<Text>().text = mGameMusic.ToString();
    }

}
