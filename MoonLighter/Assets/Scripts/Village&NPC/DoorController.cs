using System.Threading;
using UnityEngine;
using static GameKeyManger;

public class DoorController : MonoBehaviour
{
    public GameObject mStartShop;
    public GameObject mOpen;
    public GameObject mNPCSpawner;

    private Animator mDoorAni;
    private float mTimer;
    private bool IsPressE = false;
    // Start is called before the first frame update
    void Start()
    {
        mNPCSpawner.SetActive(false);
        mTimer = 0;
        mStartShop.SetActive(false);
        mOpen.SetActive(false);
        mDoorAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mOpen.activeSelf)
        {
            if(Input.GetKey(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))     // 상점 시작
            {
                mTimer += Time.deltaTime;
                if (mTimer > 2)
                {
                    Shop.mIsShopStart= true;
                    mNPCSpawner.SetActive(true);
                    mOpen.SetActive(false);
                    mStartShop.SetActive(false);
                    mTimer = 0;
                }
            }
            if(Input.GetKeyUp(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))       // 상점 나가기
            {
                if (mTimer <= 2)
                {
                    mOpen.SetActive(false);
                    mStartShop.SetActive(false);
                    mDoorAni.SetBool("OpenTheDoor", true);
                    mDoorAni.SetBool("OpenTheDoor", false);
                    GFunc.LoadScene("VillageScene");
                }
                mTimer = 0;
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
        if(other.tag  == "Npc")
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
