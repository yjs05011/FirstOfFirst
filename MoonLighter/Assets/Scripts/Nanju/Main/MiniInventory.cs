using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniInventory : MonoBehaviour
{
    public GameObject mMiniInventory;
    public GameObject mInventroy;

    public int mUiInventory = 0;

    public Text mChangeText;
    public int mCurrentMiniInventoryText;
    public GameObject mForbidden;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LockCheck();
        MiniInventoryCheck();
        OpenMiniInventory();


        Debug.Log(mUiInventory);
    }

    // �κ��丮 �� ui �� ���� ui ���� �������� Ȯ���ϱ� ���� �ӽ� �Լ�
    public void OpenMiniInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerManager.Instance.mIsUiActive = false;

            mUiInventory++;
            if (mUiInventory == 1)
            {
                // Debug.Log(UiManager.Instance.mIsInventoryLock);

                // 락이 아닐때 인벤토리 열기
                if (UiManager.Instance.mIsInventoryLock == false)
                {
                    mMiniInventory.SetActive(true);

                }
                else
                {
                    mUiInventory = 0;

                }
            }
            else if (mUiInventory == 2)
            {
                mMiniInventory.SetActive(false);
                mUiInventory = 0;

            }
        }
    }

    // 인벤토리가 잠겼는지 체크하는 함수
    public void LockCheck()
    {
        if (UiManager.Instance.mIsInventoryLock == true)
        {
            mForbidden.SetActive(true);
        }
        else
        {

            mForbidden.SetActive(false);
        }
    }

    // 지금은 텍스트로 체크하고 있고, 사실은 아이템개수로 체크하여 텍스트 바꿔야됨
    // 지금은 텍스트로 체크하고 있고, 사실은 아이템개수로 체크하여 텍스트 바꿔야됨
    void MiniInventoryCheck()
    {
        mChangeText.text = mCurrentMiniInventoryText.ToString();

        // 가방 가득 안찬 경우
        if (0 <= mCurrentMiniInventoryText && mCurrentMiniInventoryText < 20)
        {
            // 던전일때
            if (UiManager.Instance.mIsDungeonCheck == true)
            {   // 던전에 몬스터가 없는 경우
                if (DungeonManager.Instance.IsClenStage() == true)
                {
                    UiManager.Instance.SetInventoryLock(false);

                }
                else
                {
                    UiManager.Instance.SetInventoryLock(true);

                }
            }
            // 던전이 아닐때
            else
            {
                UiManager.Instance.SetInventoryLock(false);
            }

        }
        else if (mCurrentMiniInventoryText == 20)
        {
            UiManager.Instance.SetInventoryLock(true);
        }
    }



}
