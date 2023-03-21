using System.Threading;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject mStartShop;
    public GameObject mOpen;

    private Animator mDoorAni;
    private float mTimer;
    private bool IsPressE = false;
    // Start is called before the first frame update
    void Start()
    {
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
            if(Input.GetKey(KeyCode.E))     // 상점 시작
            {
                mTimer += Time.deltaTime;
                if (mTimer > 3)
                {
                    mOpen.SetActive(false);
                    mStartShop.SetActive(false);
                    mDoorAni.SetBool("OpenTheDoor", true);
                    mTimer = 0;
                }
            }
            if(Input.GetKeyUp(KeyCode.E))       // 상점 나가기
            {
                if (mTimer <= 3)
                {
                    mOpen.SetActive(false);
                    mStartShop.SetActive(false);
                    mDoorAni.SetBool("OpenTheDoor", true);
                    mDoorAni.SetBool("OpenTheDoor", false);
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
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            mStartShop.SetActive(false);
            mOpen.SetActive(false);
        }
    }
}
