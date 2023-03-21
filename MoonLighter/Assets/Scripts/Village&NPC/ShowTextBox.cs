using UnityEngine;

public class ShowTextBox : MonoBehaviour
{

    public int mID = default;
    public int mTalkIndex = default;
    public TalkManager mTalkManager;
    public GameObject mText;

    private GameObject talk = default;
    private bool IsPlayerNearby = false;

    // Start is called before the first frame update
    void Start()
    {
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
                string talkData = mTalkManager.GetTalk(mID, mTalkIndex);
                GFunc.SetText(mText, talkData);
                talk.SetActive(false);
            }

        }
        if (talk.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (mID == 600)
                {
                    GFunc.LoadScene("ShopLv1");
                }
                else
                {
                    Talk(mID);
                }
            }
        }
    }
    private void Talk(int id)
    {
        mTalkIndex++;
        string talkData = mTalkManager.GetTalk(id, mTalkIndex);
        if (talkData == null)
        {
            mTalkIndex = 0;
            talkData = mTalkManager.GetTalk(id, mTalkIndex);
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
