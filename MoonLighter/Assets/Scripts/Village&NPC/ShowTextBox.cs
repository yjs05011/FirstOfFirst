using UnityEngine;

public class ShowTextBox : MonoBehaviour
{

    public int mID = default;
    public int mTalkIndex = default;
    public GameObject mText;
    public static Vector3 mBedPosition = new Vector3(3, 3, 0);
    public GameObject mShopUI;
    private float mTimer;
    private GameObject talk = default;
    private bool IsTalking = false;
    private bool IsPlayerNearby = false;
    private bool IsPlayerGoToBed = false;
    // Start is called before the first frame update
    void Start()
    {
        IsPlayerGoToBed = false;
        mTimer = 0;
        mTalkIndex = 0;
        talk = transform.Find("Talk").gameObject;
        talk.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerNearby)
        {
            talk.SetActive(true);
        }
        else
        {
            if (talk.activeSelf)
            {
                mTalkIndex = 0;
                IsTalking = false;
                string talkData = TalkManager.Instance.GetTalk(mID, mTalkIndex);
                GFunc.SetText(mText, talkData);
                talk.SetActive(false);
            }

        }
        if (talk.activeSelf)
        {
            if (mID == 220)
            {
                if (!IsPlayerGoToBed)
                {
                    if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))
                    {
                        mTimer += Time.deltaTime;
                        if (mTimer > 2)
                        {
                            IsPlayerGoToBed = true;
                            talk.SetActive(false);
                            SetPosition.Instance.mSettingPosition = mBedPosition;

                            if (SetPosition.Instance.mIsNight)
                            {
                                SetPosition.Instance.mIsNight = false;
                            }
                            else
                            {
                                SetPosition.Instance.mIsNight = true;
                            }
                            //DataManager.Instance.JsonSave();

                            LoadingManager.LoadScene(VillageManager.Instance.WillHouse);

                        }
                    }
                }

            }

            else
            {
                if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))
                {

                    if (mID == 600)
                    {
                        //SetPosition.Instance.mSettingPosition = new Vector3(8, 10, 0);
                        LoadingManager.LoadScene(VillageManager.Instance.WillHouse);
                    }
                    else if (mID == 120)
                    {
                        mShopUI.SetActive(true);
                        mShopUI.GetComponent<ShopUI>().mTableNumber = 0;
                        // 상점의 테이블
                    }
                    else if(mID == 130)
                    {
                        mShopUI.SetActive(true);
                        mShopUI.GetComponent<ShopUI>().mTableNumber = 1;
                    }
                    else if (mID == 700)
                    {
                        // 게시판 UI
                    }
                    else if (mID == 2000 && mTalkIndex == 1)
                    {
                        // 대장간 UI
                    }
                    else if (mID == 3000 && mTalkIndex == 1)
                    {
                        // 마녀 UI
                    }
                    else if(mID == 420)
                    {
                        //계산대
                        if(ShopManager.Instance.mWaitShopNPC != null)
                        {
                            ShopManager.Instance.mWaitShopNPC[0].GetComponent<ShopNPC>().IsCalculate = false;
                            ShopManager.Instance.mWaitShopNPC.RemoveAt(0);
                        }
                    }
                    else
                    {
                        Talk(mID);
                    }
                }
            }

        }
    }
    private void Talk(int id)
    {
        mTalkIndex++;
        string talkData = TalkManager.Instance.GetTalk(id, mTalkIndex);
        if (talkData == null)
        {
            mTalkIndex = 0;
            talkData = TalkManager.Instance.GetTalk(id, mTalkIndex);
        }
        GFunc.SetText(mText, talkData);

    }
    private void OnTriggerEnter2D(Collider2D mCollision)
    {
        if (mCollision.tag == "Player")
        {
            IsPlayerNearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D mCollision)
    {
        if (mCollision.tag == "Player")
        {
            IsPlayerNearby = false;
        }
    }
}
