using UnityEngine;
using UnityEngine.UIElements;

public class ShowTextBox : MonoBehaviour
{

    public int mID = default;
    public int mTalkIndex = default;
    public GameObject mText;
    public static Vector3 mBedPosition = new Vector3(3, 3, 0);
    public GameObject mShopUI;
    private float mTimer;
    private GameObject mTalk = default;
    private GameObject mButton = default;
    private bool IsTalking = false;
    private bool IsPlayerNearby = false;
    private bool IsPlayerGoToBed = false;
    private UiManager mUiManager = default;
    // Start is called before the first frame update
    void Start()
    {
        IsPlayerGoToBed = false;
        mTimer = 0;
        mTalkIndex = 0;
        mTalk = transform.Find("Talk").gameObject;
        mButton = mTalk.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        mTalk.SetActive(false);
        mUiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerNearby)
        {
            if (!mTalk.activeSelf)
            {
                mTalkIndex = 0;
                string talkData = TalkManager.Instance.GetTalk(mID, mTalkIndex);
                GFunc.SetText(mText, talkData);
            }
            mTalk.SetActive(true);
            GFunc.SetText(mButton, GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT].ToString());

        }
        else if (!IsPlayerNearby)
        {
            if (mTalk.activeSelf)
            {
                mTalkIndex = 0;
                IsTalking = false;
                string talkData = TalkManager.Instance.GetTalk(mID, mTalkIndex);
                GFunc.SetText(mText, talkData);
                mTalk.SetActive(false);
            }

        }
        if (mTalk.activeSelf)
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
                            mTalk.SetActive(false);
                            SetPosition.Instance.mSettingPosition = mBedPosition;

                            if (SetPosition.Instance.mIsNight)
                            {
                                SetPosition.Instance.mIsNight = false;
                            }
                            else
                            {
                                SetPosition.Instance.mIsNight = true;
                            }
                            GameManager.Instance.mIsNight = SetPosition.Instance.mIsNight;
                            GameManager.Instance.mIsShop = VillageManager.Instance.IsWillHouseUpgrade;
                            GameManager.Instance.mPlayerSpeed = PlayerManager.Instance.mPlayerStat.Speed;
                            GameManager.Instance.mPlayerStr = PlayerManager.Instance.mPlayerStat.Str;
                            GameManager.Instance.mPlayerDef = PlayerManager.Instance.mPlayerStat.Def;
                            GameManager.Instance.mPlayerMoney = PlayerManager.Instance.mPlayerStat.Money;
                            GameManager.Instance.mPlayerMaxHp = PlayerManager.Instance.mPlayerStat.MaxHp;
                            GameManager.Instance.mTableNumber = ShopManager.Instance.mTableNumber;
                            GameManager.Instance.mItemPrice = ShopManager.Instance.mItemPrice;
                            GameManager.Instance.mItemsNumber = ShopManager.Instance.mItemsNumber;
                            GameManager.Instance.mBedPosition = ShopManager.Instance.mBedPosition;
                            GameManager.Instance.mIsBlackSmithBuild = VillageManager.Instance.IsBlackSmithBuild;
                            GameManager.Instance.mIsWitchHouseBuild = VillageManager.Instance.IsWitchHouseBuild;
                            GameKeyManger.Instance.SaveKeyData();
                            //GameManager.Instance.mInventorySlots = InventoryManager.Instance.mInventorySlots;
                            //GameManager.Instance.mEquipmentSlots = InventoryManager.Instance.mEquipmentSlots;
                            DataManager.Instance.JsonSave();

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

                        LoadingManager.LoadScene(VillageManager.Instance.WillHouse);
                    }
                    else if (mID == 120)
                    {
                        ShopManager.Instance.mTablesNumber = 0;
                        mUiManager.GetItemTableOpen(true);
                        mShopUI.SetActive(true);
                        // ������ ���̺�
                    }
                    else if (mID == 130)
                    {
                        ShopManager.Instance.mTablesNumber = 1;
                        mUiManager.GetItemTableOpen(true);
                        mShopUI.SetActive(true);

                    }
                    else if (mID == 700)
                    {
                        mUiManager.GetVillageNoticeBoardOpen(true);
                        // �Խ��� UI
                    }

                    else if (mID == 2000 && mTalkIndex == 1)
                    {
                        mUiManager.GetBlacksmithTalk(true);
                        // ���尣 UI
                    }
                    else if (mID == 3000 && mTalkIndex == 1)
                    {
                        mUiManager.GetWitchTalk(true);
                        // ���� UI
                    }
                    else if (mID == 420)
                    {
                        //����
                        if (ShopManager.Instance.mWaitShopNPC != null)
                        {
                            ShopManager.Instance.mWaitShopNPC[0].GetComponent<ShopNPC>().IsCalculate = false;
                            //ShopManager.Instance.mWaitShopNPC.RemoveAt(0);
                        }
                    }
                    else if (mID == 900)
                    {

                    }
                    else if (mID == 910)
                    {

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
