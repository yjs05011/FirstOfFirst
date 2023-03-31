using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject mStartShop;
    public GameObject mOpen;
    public GameObject mNPCSpawner;
    public GameObject mButton1;
    public GameObject mButton2;

    private Animator mDoorAni;
    private float mTimer;
    private bool mIsShutDown = false;
    // Start is called before the first frame update
    void Start()
    {
        mNPCSpawner.SetActive(false);
        mTimer = 0;
        mStartShop.SetActive(false);
        mOpen.SetActive(false);
        mDoorAni = GetComponent<Animator>();
        GFunc.SetTmpText(mButton1, GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT].ToString());
        GFunc.SetTmpText(mButton2, GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT].ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(!mIsShutDown)
        {
            if (mOpen.activeSelf)
            {
                if (Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))     // 상점 시작
                {
                    mTimer += Time.deltaTime;
                    if (!SetPosition.Instance.mIsNight)
                    {

                        if (mTimer > 2)
                        {
                            if (!Shop.mIsShopStart)
                            {
                                Shop.mIsShopStart = true;
                                mNPCSpawner.SetActive(true);
                                mOpen.SetActive(false);
                                mStartShop.SetActive(false);
                                mTimer = 0;
                            }
                            else
                            {
                                Shop.mIsShopStart = false;
                                mNPCSpawner.SetActive(false);
                                mOpen.SetActive(false);
                                mStartShop.SetActive(false);
                                mTimer = 0;
                                mIsShutDown= true;
                                SetPosition.Instance.mIsNight = true;
                            }
                        }
                    }
                }
                if (Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))       // 상점 나가기
                {
                    if (!Shop.mIsShopStart)
                    {
                        if (mTimer <= 2)
                        {
                            mOpen.SetActive(false);
                            mStartShop.SetActive(false);
                            mDoorAni.SetBool("OpenTheDoor", true);
                            mDoorAni.SetBool("OpenTheDoor", false);
                            //PlayerManager.Instance.mPlayerBeforPos = SetPosition.Instance.mSettingPosition;
                            SetPosition.Instance.mSettingPosition = new Vector3(8, 10, 0);
                            LoadingManager.LoadScene("VillageScene");
                            //GFunc.LoadScene("VillageScene");
                        }
                        mTimer = 0;
                    }

                }

            }
        }
        else
        {
            if(ShopManager.Instance.mWaitShopNPC == null || ShopManager.Instance.mWaitShopNPC.Count == 0)
            {
                mIsShutDown = false;
                LoadingManager.LoadScene(VillageManager.Instance.WillHouse);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            mStartShop.SetActive(true);
            mOpen.SetActive(true);
        }
        if (other.tag == "Npc")
        {
            mDoorAni.SetBool("OpenTheDoor", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            mStartShop.SetActive(false);
            mOpen.SetActive(false);
        }
        if (other.tag == "Npc")
        {
            mDoorAni.SetBool("OpenTheDoor", false);

        }
    }
}
