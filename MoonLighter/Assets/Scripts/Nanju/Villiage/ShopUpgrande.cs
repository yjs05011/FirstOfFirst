using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrande : MonoBehaviour
{
    public GameObject mShop1Selector;
    public GameObject mShop2Selector;
    public GameObject mShop3Selector;
    public GameObject mShop4Selector;
    public GameObject mSaleBoxSelector;
    public GameObject mCashRegisterSelector; 
    public GameObject mBedSelector; 
    public GameObject mBedChestSelector; 

    public int mSelectCheck;
    public int mFloorCheck;

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
            // ShopLevelFloor에서 선택 되게 하기
            if (mFloorCheck == 1 && mSelectCheck > 3)
            {
                mSelectCheck = 0;
            }
            // GuiFloor에서 선택 되게 하기
            if( mFloorCheck == 2 && (4 < mSelectCheck && mSelectCheck > 7))
            {
                mSelectCheck = 4;
            }
        }

        // 키보드 왼쪽 눌렀을때
        if (Input.GetKeyDown(KeyCode.A))
        {
            mSelectCheck--;
            // ShopLevelFloor에서 선택 되게 하기
            if (mFloorCheck == 1 && mSelectCheck < 0)
            {
                mSelectCheck = 3;
            }
            // GuiFloor에서 선택 되게 하기
            if (mFloorCheck == 2 && (mSelectCheck < 4 && mSelectCheck < 7))
            {
                mSelectCheck = 7;
            }
        }

        // 키보드  위 눌렀을때
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            if (mFloorCheck == 1)
            {
                
            }
            else
            {
                mSelectCheck -= 4;
            }
        }
        // 키보드  아래 눌렀을때
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            if (mFloorCheck == 2 )
            {
                
            }
            else
            {
                mSelectCheck += 4;
            }
        }
        


            

        // 어떤 Select가 되었는지 체크하여 활성화, 비활성화 하기
        switch (mSelectCheck)
        {
            case 0:
                mFloorCheck=1;

                mShop1Selector.SetActive(true);
                mShop2Selector.SetActive(false);
                mShop3Selector.SetActive(false);
                mShop4Selector.SetActive(false);
                mSaleBoxSelector.SetActive(false);
                mCashRegisterSelector.SetActive(false);
                mBedSelector.SetActive(false);
                mBedChestSelector.SetActive(false);
                break;
            case 1:
                mFloorCheck=1;

                mShop2Selector.SetActive(true);
                mShop1Selector.SetActive(false);
                mShop3Selector.SetActive(false);
                mShop4Selector.SetActive(false);
                mSaleBoxSelector.SetActive(false);
                mCashRegisterSelector.SetActive(false);
                mBedSelector.SetActive(false);
                mBedChestSelector.SetActive(false);
                break;
            case 2:
                mFloorCheck=1;

                mShop3Selector.SetActive(true);
                mShop1Selector.SetActive(false);
                mShop2Selector.SetActive(false);
                mShop4Selector.SetActive(false);
                mSaleBoxSelector.SetActive(false);
                mCashRegisterSelector.SetActive(false);
                mBedSelector.SetActive(false);
                mBedChestSelector.SetActive(false);
                break;
            case 3:
                mFloorCheck=1;

                mShop4Selector.SetActive(true);
                mShop1Selector.SetActive(false);
                mShop2Selector.SetActive(false);
                mShop3Selector.SetActive(false);
                mSaleBoxSelector.SetActive(false);
                mCashRegisterSelector.SetActive(false);
                mBedSelector.SetActive(false);
                mBedChestSelector.SetActive(false);
                break;
            case 4:
                mFloorCheck=2;

                mSaleBoxSelector.SetActive(true);
                mShop1Selector.SetActive(false);
                mShop2Selector.SetActive(false);
                mShop3Selector.SetActive(false);
                mShop4Selector.SetActive(false);
                mCashRegisterSelector.SetActive(false);
                mBedSelector.SetActive(false);
                mBedChestSelector.SetActive(false);
                break;
            case 5:
                mFloorCheck=2;

                mCashRegisterSelector.SetActive(true);
                mShop1Selector.SetActive(false);
                mShop2Selector.SetActive(false);
                mShop3Selector.SetActive(false);
                mShop4Selector.SetActive(false);
                mSaleBoxSelector.SetActive(false);
                mBedSelector.SetActive(false);
                mBedChestSelector.SetActive(false);
                break;
            case 6:
                mFloorCheck=2;

                mBedSelector.SetActive(true);
                mShop1Selector.SetActive(false);
                mShop2Selector.SetActive(false);
                mShop3Selector.SetActive(false);
                mShop4Selector.SetActive(false);
                mSaleBoxSelector.SetActive(false);
                mCashRegisterSelector.SetActive(false);
                mBedChestSelector.SetActive(false);
                break;
            case 7:
                mFloorCheck=2;

                mBedChestSelector.SetActive(true);
                mShop1Selector.SetActive(false);
                mShop2Selector.SetActive(false);
                mShop3Selector.SetActive(false);
                mShop4Selector.SetActive(false);
                mSaleBoxSelector.SetActive(false);
                mCashRegisterSelector.SetActive(false);
                mBedSelector.SetActive(false);
                break;
        }
    }
}
