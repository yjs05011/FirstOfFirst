using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonChest : MonoBehaviour
{
    public GameObject mChest = null;
    private Animator mChestAnim = null;

    public enum ChestID { None = 0, WoodChest = 1, BossChest =2 }
    [SerializeField]
    private ChestID mChestID = ChestID.None;
    public enum ChestState { Lock , Unlock , WaitInput , Open, Close }
    public ChestState mState = ChestState.Lock;
    public GameObject mInteractionMenu = null;
    public TMP_Text mInteractionKey = null;
    public Collider2D mTrriger = null;

    void Awake()
    {
        mChestAnim = mChest.GetComponent<Animator>();
        SetChestState(ChestState.Lock);
        mTrriger = mChest.GetComponent<Collider2D>();
    }

    private void Start()
    {
        mTrriger.enabled = false;
    }

    public void SetChestState(ChestState state)
    {
        mState = state;
    }

    public ChestState GetChestState()
    {
        return mState;
    }
    public ChestID GetChestID()
    {
        return mChestID;
    }

    public void SetInteractionKeyValue(string value)
    {
        mInteractionKey.text = value;
    }

    public void Update()
    {
        // ��� �����̰ų�, ���� �����̸� ����.
        if(mState == ChestState.Lock )//|| mState == ChestState.Open)
        {
            return;
        }
        if (mState == ChestState.Open)
        {
            //if (mChestID == ChestID.BossChest)
            //{
                // �ӽ�> ���� ���� Ż���ϰ� ó��.
                   mState = ChestState.Lock;
            // DungeonManager.Instance.TestDungeonExitInit();
            //DungeonManager.Instance.TestDungeonEnterInit();
            LoadingManager.LoadScene("Dungeon");
            return;
            //}
           // return;
        }
        if(mState == ChestState.Unlock)
        {
            // ����ִϸ��̼� ���
            mChestAnim.SetTrigger("ChestUnlock");
            // ĳ����  ��ó�Ӵ��� üũ�� ���� Ʈ���ſ� �ö��̴� Ȱ��ȭ ó��
            mTrriger.enabled = true;
        }
        if (mState == ChestState.WaitInput)
        {
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))
            {
                // ���� ���� �ִϸ��̼� ���
                mChestAnim.SetTrigger("ChestOpen");
                // ���� ���� ���·� ����
                SetChestState(ChestState.Open);

                // ���� UI ȣ�� 
                Debug.Log("UI : Chest open");

            }
        }
        // ���� UI���� ������ ���¸� close �� ���� ���ְ�, close anim ��������� ������ ���� 
        if (mState == ChestState.Close)
        {
            // ���� �ݴ� �ִϸ��̼� ���
            mChestAnim.SetTrigger("ChestClose");
            SetChestState(ChestState.Unlock);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Ű�Է� ��� ���·� ����
            SetChestState(ChestState.WaitInput);
            
            // �ɼǿ� ������ Ű�� Ű ����
            // ����� �ڵ� �ö󰡸� �ּ� Ǯ��.
            // string keyValue = GameKeyManger.keyString[GameKeyManger.KeyAction.INTERRUPT];
            //SetInteractionKeyValue("keyValue");

            // ���� Ű UI ����
            Debug.Log("UI : Chest open guide UI Active true");
            mInteractionMenu.SetActive(true);

        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Ű�Է� ��� ���� ����. ���� ���·�
            SetChestState(ChestState.Unlock);

            // ���� Ű UI ���� ����
            Debug.Log("UI : Chest open guide UI Active false");
            mInteractionMenu.SetActive(false);

        }
    }

    // ���� UI ���� ���� ���µ� Close �� �ٲ������ (���)


}
