using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBoardStaff : MonoBehaviour
{
    public GameObject mBlacksmithSelector;
    public GameObject mWitchSelector;
    public GameObject mRivalSelector;

    public int mSelectCheck;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 키보드 눌렀을때 Select 되게 하기

        // 키보드 오른쪽 눌렀을때
        if (Input.GetKeyDown(KeyCode.D))
        {
            mSelectCheck++;
            if (mSelectCheck > 2)
            {
                mSelectCheck = 0;
            }


        }

        // 키보드 왼쪽 눌렀을때
        if (Input.GetKeyDown(KeyCode.A))
        {
            mSelectCheck--;
            if (mSelectCheck < 0)
            {
                mSelectCheck = 2;
            }
        }

        // 어떤 Select가 되었는지 체크하여 활성화, 비활성화 하기
        switch (mSelectCheck)
        {
            case 0:
                mBlacksmithSelector.SetActive(true);
                mWitchSelector.SetActive(false);
                mRivalSelector.SetActive(false);
                break;
            case 1:
                mWitchSelector.SetActive(true);
                mBlacksmithSelector.SetActive(false);
                mRivalSelector.SetActive(false);
                break;
            case 2:
                mRivalSelector.SetActive(true);
                mBlacksmithSelector.SetActive(false);
                mWitchSelector.SetActive(false);
                break;
        }

    }
}
