using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniInventory : MonoBehaviour
{
    public GameObject mMiniInventory;
    public GameObject mInventroy;

    public int mUiInventory = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenMiniInventory();
        //UiManager.Instance.mIsInventoryInteraction= true;
        Debug.Log(mUiInventory);
    }

    // �κ��丮 �� ui �� ���� ui ���� �������� Ȯ���ϱ� ���� �ӽ� �Լ�
    public void OpenMiniInventory() 
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            mUiInventory++;
            if (mUiInventory == 1)
            {
                mMiniInventory.SetActive(true);
            }
            else if (mUiInventory == 2)
            {
                mMiniInventory.SetActive(false);

            }
        }
    }



}
