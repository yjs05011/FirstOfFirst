using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
    // 상점 UI에서 게시판과 업그레이드를 저장한 배열
    public GameObject[] mObjs;
    //상점 UI를 변경하기 위한 bool 변수 (false일때 Notice Board 활성화, True일때 shopUpgrade활성화)
    private bool mObjIndex;
    private int mMainIndex;
    private Vector2 mBoardCorserSize = new Vector2(160, 180);
    private Vector2 mUpgradeCorserSize = new Vector2(100, 100);
    public GameObject[] mBoardSelectPosition;
    public GameObject[] mUpgradeSelectPosition;
    public int[] mUsingMoney;
    public GameObject mCorser;
    private RectTransform mCorserPos;
    public Text mBoardCoinText;
    public int mBeforeMainIndex;
    public Text mUpgradeCoinText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        mUsingMoney = new int[4] { 500, 500, 5500, 0 };
        mObjIndex = false;
        mMainIndex = 0;
        mObjs[0].SetActive(true);
        mObjs[1].SetActive(false);
        mCorserPos = mCorser.GetComponent<RectTransform>();
        mCorserPos.localPosition = mBoardSelectPosition[0].transform.localPosition;
        mCorserPos.sizeDelta = mBoardCorserSize;

    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
    }

    void InputKey()
    {
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INVENTORY]) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            PlayerManager.Instance.mIsUiActive = false;
            UiManager.Instance.GetVillageNoticeBoardOpen(false);
        }
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.TOGGLEUPRIGHT]))
        {
            if (mObjIndex)
            {
                mUsingMoney = new int[4] { 500, 500, 5500, 0 };
                mObjIndex = false;
                mObjs[0].SetActive(true);
                mObjs[1].SetActive(false);
                mCorserPos.localPosition = mBoardSelectPosition[0].transform.localPosition;
                for (int i = 0; i < 4; i++)
                {
                    mBoardCoinText.text = mUsingMoney[0].ToString();
                    mBoardSelectPosition[0].transform.GetChild(i).gameObject.SetActive(true);
                    if (i != 3)
                    {
                        mUpgradeSelectPosition[mMainIndex].transform.GetChild(i).gameObject.SetActive(false);
                    }

                }
                mMainIndex = 0;
                mCorserPos.sizeDelta = mBoardCorserSize;

            }


        }
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.TOGGLEUPLEFT]))
        {
            if (!mObjIndex)
            {
                mUsingMoney = new int[4] { 8000, 60000, 120000, 250000 };
                mObjIndex = true;
                mObjs[0].SetActive(false);
                mObjs[1].SetActive(true);
                mCorserPos.localPosition = new Vector2(mUpgradeSelectPosition[0].transform.localPosition.x, mUpgradeSelectPosition[0].transform.localPosition.y - 36);
                for (int i = 0; i < 3; i++)
                {
                    mUpgradeCoinText.text = mUsingMoney[0].ToString();
                    mUpgradeSelectPosition[0].transform.GetChild(i).gameObject.SetActive(true);
                    if (i == 2)
                    {
                        mBoardSelectPosition[mMainIndex].transform.GetChild(i).gameObject.SetActive(false);
                        mBoardSelectPosition[mMainIndex].transform.GetChild(i + 1).gameObject.SetActive(false);
                    }

                }
                mMainIndex = 0;

                mCorserPos.sizeDelta = mUpgradeCorserSize;
            }

        }
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.LEFT]))
        {
            if (mMainIndex > 0)
            {
                mBeforeMainIndex = mMainIndex;
                mMainIndex--;
                Moving(mMainIndex, mBeforeMainIndex);
            }

        }
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.RIGHT]))
        {
            if (!mObjIndex)
            {
                if (mMainIndex < 2)
                {
                    mBeforeMainIndex = mMainIndex;
                    mMainIndex++;
                    Moving(mMainIndex, mBeforeMainIndex);
                }
            }
            else
            {
                if (mMainIndex < 3)
                {
                    mBeforeMainIndex = mMainIndex;
                    mMainIndex++;
                    Moving(mMainIndex, mBeforeMainIndex);
                }
            }


        }
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK]))
        {
            Choice(mMainIndex);
        }
    }
    void Moving(int idx, int beforidx)
    {
        int childLength;
        if (!mObjIndex)
        {
            childLength = 4;
            mCorserPos.localPosition = mBoardSelectPosition[idx].transform.localPosition;
            for (int i = 0; i < childLength; i++)
            {
                mBoardCoinText.text = mUsingMoney[idx].ToString();
                mBoardSelectPosition[idx].transform.GetChild(i).gameObject.SetActive(true);
                mBoardSelectPosition[beforidx].transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            childLength = 3;
            mCorserPos.localPosition = new Vector2(mUpgradeSelectPosition[idx].transform.localPosition.x, mUpgradeSelectPosition[idx].transform.localPosition.y - 36);
            for (int i = 0; i < childLength; i++)
            {
                mUpgradeCoinText.text = mUsingMoney[idx].ToString();
                mUpgradeSelectPosition[idx].transform.GetChild(i).gameObject.SetActive(true);
                mUpgradeSelectPosition[beforidx].transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void Choice(int idx)
    {
        if (!mObjIndex)
        {
            if (mUsingMoney[idx] <= PlayerManager.Instance.mPlayerStat.Money)
            {
                mBoardSelectPosition[idx].GetComponent<VillageUpgrade>().Buy(mUsingMoney[idx]);
            }
            else
            {
                Debug.Log("NO");
            }
        }
        else
        {
            if (mUsingMoney[idx] <= PlayerManager.Instance.mPlayerStat.Money)
            {
                mUpgradeSelectPosition[idx].GetComponent<VillageUpgrade>().Buy(mUsingMoney[idx]);
            }
            else
            {
                Debug.Log("NO");
            }
        }

    }
}
