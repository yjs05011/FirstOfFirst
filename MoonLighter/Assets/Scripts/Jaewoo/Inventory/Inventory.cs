
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    //(4,5)배열 상수
    public const int ARRAY_X = 5;
    public const int ARRAY_Y = 4;
    //(4,2)배열 상수
    public const int EQUIPMENT_ARRAY_X = 2;
    public const int EQUIPMENT_ARRAY_Y = 4;

    //(4,5)배열 변수
    public int mSelectX = default;
    public int mSelectY = default;
    //(4,2)배열 변수
    public int mSelectEquipmentY = default;
    public int mSelectEquipmentX = default;

    public int mKeyCodeICount = 0;
    public int mSelectCount = 0;

    //인벤토리 활성화 확인. true면 인벤토리가 열림.이동 막음
    public static bool mIsInventoryActiveCheck = false;
    //마을인지 확인. false면 돋보기 칸 비활성화, true면 활성화
    public static bool mIsVillageCheck = false;
    //인벤토리 키보드 활성화확인
    public static bool mIsSelectMove = false;
    public static bool mIsChestCheck = false;
    public static bool mIsInventoryOpen = false;
    public bool mIsEquipmentCheck = false;

    //인벤토리 base 이미지
    public GameObject mChangeInventoryBase;
    public GameObject mChangeSlotGrideSetting;
    public GameObject mChnageEquipmentSlotGrideSetting;

    //배열들의 부모를 찾기 쉽게 하기 위해 만든 GameObject
    public GameObject mInventoryFindSlot;
    public GameObject mEquipmentFindSlot;
    public GameObject mSelectPoint = null;
    public GameObject mSelectItem;
    //기본상태의 인벤토리배열
    public Slot[,] mInventoryArray;
    //기본상태의 인벤토리 장비창 배열
    public Slot[,] mEquipmentArray;


    //public Slot mSlotGet;
    //public Slot[] mSlot;


    private void Awake()
    {

    }
    void OnEnable()
    {

    }
    void Start()
    {
        //bool 초기화
        mIsSelectMove = false;
        mIsChestCheck = false;

        mInventoryArray = new Slot[4, 5];
        mEquipmentArray = new Slot[4, 2];
        for (int indexY = 0; indexY < ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < ARRAY_X; indexX++)
            {
                int indexAdd = indexY * ARRAY_X + indexX;

                mInventoryArray[indexY, indexX] = mInventoryFindSlot.transform.GetChild(indexAdd).GetComponent<Slot>();
                mInventoryArray[indexY, indexX].gameObject.SetActive(true);
                InventoryManager.Instance.mInventorySlots[indexY, indexX] = (mInventoryArray[indexY, indexX]);
            }

        }

        mSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;

        for (int indexY = 0; indexY < EQUIPMENT_ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < EQUIPMENT_ARRAY_X; indexX++)
            {
                int indexEquipmentAdd = indexY * EQUIPMENT_ARRAY_X + indexX;

                mEquipmentArray[indexY, indexX] = mEquipmentFindSlot.transform.GetChild(indexEquipmentAdd).GetComponent<Slot>();
                mEquipmentArray[indexY, indexX].gameObject.SetActive(true);
                InventoryManager.Instance.mEquipmentSlots[indexY, indexX] = (mEquipmentArray[indexY, indexX]);
            }
        }

        //Awake와 Start에서 인벤토리 On/Off를 통해 기능활성화

        //mSlotGet = mChangeSlotGrideSetting.GetComponentInChildren<Slot>();
        //mSlot = mChangeSlotGrideSetting.GetComponentsInChildren<Slot>();
        //



        transform.GetChild(0).gameObject.SetActive(false);
        mSelectPoint.transform.GetChild(0).gameObject.SetActive(false);


    }

    void Update()
    {
       
        if(InventoryManager.Instance.mIsManagerAddCheck == true)
        {
            for (int indexY = 0; indexY < ARRAY_Y; indexY++)
            {
                for (int indexX = 0; indexX < ARRAY_X; indexX++)
                {
                    int indexAdd = indexY * ARRAY_X + indexX;

                    mInventoryArray[indexY, indexX] = mInventoryFindSlot.transform.GetChild(indexAdd).GetComponent<Slot>();
                    mInventoryArray[indexY, indexX].gameObject.SetActive(true);
                    InventoryManager.Instance.mInventorySlots[indexY, indexX] = (mInventoryArray[indexY, indexX]);
                }

            }
        }
        
        
        //인벤토리 On/Off를 계속 확인
        TryOpenInventory();
        if (mIsInventoryOpen == true)
        {
            if (!mIsEquipmentCheck)
            {
                InventoryMove();                
            }
            else
            {                
                EquipmentMove();
            }
        }
    }

    //인벤토리 켰을때
    private void TryOpenInventory()
    {
        //KeyManager를 통해 키보드 입력(매니저에서 받는)
        //if(Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        //화요일 머지시 지워야함(i눌러서 인벤토리 나오는 부분)

        if (Input.GetKeyDown(KeyCode.I))
        {
            mIsInventoryActiveCheck = !mIsInventoryActiveCheck;

            if (Input.GetKeyDown(KeyCode.I))
            {
                mKeyCodeICount++;
                if (mKeyCodeICount == 1)
                {
                    OpenInventory();
                }
                if (mKeyCodeICount == 2)
                {
                    mKeyCodeICount = 0;
                    CloseInventory();
                }
            }
        }
    }

    public void InventoryMove()
    {
        //슬롯에서 아이템 선택
        if (Input.GetKeyDown(KeyCode.J))
        {
            SelectSlot();
        }
        //빠른 위치로 넣기
        if (Input.GetKeyDown(KeyCode.C))
        {
            ItemSelfInsert();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (4 > mSelectX)
            {
                mSelectX++;
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
            }
            else
            {
                mIsEquipmentCheck = true;
                mSelectEquipmentY = mSelectY;
                mSelectEquipmentX = 0;
                mSelectPoint.transform.localPosition = mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].transform.localPosition;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (0 < mSelectX)
            {
                mSelectX--;
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
            }
            else
            {
                mIsEquipmentCheck = true;
                mSelectEquipmentY = mSelectY;
                mSelectEquipmentX = 1;
                mSelectPoint.transform.localPosition = mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].transform.localPosition;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            mSelectY--;
            if (mSelectY < 0)
            {
                mSelectY = 3;
            }
            //Debug.Log(mInventoryArray[mSelectY, mSelectX]);
            mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mSelectY++;
            if (3 < mSelectY)
            {
                mSelectY = 0;
            }
            //Debug.Log(mInventoryArray[mSelectY, mSelectX]);
            mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
        }
    }

    public void EquipmentMove()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SelectSlot();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (mSelectEquipmentX < 1)
            {
                mSelectEquipmentX++;
                mSelectPoint.transform.localPosition = mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].transform.localPosition;
            }
            else
            {
                mSelectY = mSelectEquipmentY;
                mSelectX = 0;
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
                mIsEquipmentCheck = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (0 < mSelectEquipmentX)
            {
                mSelectEquipmentX--;
                mSelectPoint.transform.localPosition = mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].transform.localPosition;
            }
            else
            {
                mSelectY = mSelectEquipmentY;
                mSelectX = 4;
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
                mIsEquipmentCheck = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            mSelectEquipmentY--;

            if (mSelectEquipmentY < 0)
            {
                mSelectEquipmentY = 3;

            }
            mSelectPoint.transform.localPosition = mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].transform.localPosition;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mSelectEquipmentY++;
            if (3 < mSelectEquipmentY)
            {
                mSelectEquipmentY = 0;
            }
            mSelectPoint.transform.localPosition = mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].transform.localPosition;
        }
    }

    //GameObject 인벤토리 베이스 활성화 
    private void OpenInventory()
    {
        mSelectY = 0;
        mSelectX = 0;
        mIsInventoryOpen = true;
        mIsSelectMove = true;
        mChangeInventoryBase.SetActive(true);
    }
    //GameObject 인벤토리 베이스 비활성화 
    private void CloseInventory()
    {
        mIsInventoryOpen = false;

        mSelectY = 0;
        mSelectX = 0;
        mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
        mChangeInventoryBase.SetActive(false);
        mIsSelectMove = false;
    }

    public void AcpuireItem(Item item, int itemCount)
    {
        //Debug.Log(Item.ItemE)
        //얻은 아이템이 장비 타입과 다를때
        if (Item.ItemEnumType.Equiment != item.mItemType)
        {
            for (int indexY = 0; indexY < ARRAY_Y; indexY++)
            {
                for (int indexX = 0; indexX < ARRAY_X; indexX++)
                {
                    //아이템이 들어있는 칸일때
                    if (mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem != null)
                    {
                        //슬롯[index]의 이름과 아이템의 이름이 같으면
                        if (mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem.mItemName == item.mItemName)
                        {
                            //슬롯 개수 올림      
                            if (9 < mInventoryArray[indexY, indexX].GetComponent<Slot>().mItemCount && Item.ItemEnumType.Ingredient == item.mItemType)
                            {

                            }
                            else if (4 < mInventoryArray[indexY, indexX].GetComponent<Slot>().mItemCount && Item.ItemEnumType.Potion == item.mItemType)
                            {

                            }
                            else
                            {
                                mInventoryArray[indexY, indexX].GetComponent<Slot>().SetSlotCount(itemCount);
                                return;
                            }
                        }
                        //슬롯의 이름과 아이템의 이름이 다르면
                        else
                        {
                            //넘어간다.
                        }
                    }
                }
            }

            //InventoryManager에 배열의 정보를 넣어줌
            for (int indexY = 0; indexY < ARRAY_Y; indexY++)
            {
                for (int indexX = 0; indexX < ARRAY_X; indexX++)
                {
                    if (mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem == null)
                    {
                        mInventoryArray[indexY, indexX].GetComponent<Slot>().AddItem(item, itemCount);

                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                if (mInventoryArray[i, j].GetComponent<Slot>().mItem != null)
                                {
                                    InventoryManager.Instance.mInventorySlots[i, j] = mInventoryArray[i, j].GetComponent<Slot>();
                                }

                            }
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                if (mEquipmentArray[i, j].GetComponent<Slot>().mItem != null)
                                    InventoryManager.Instance.mEquipmentSlots[i, j] = mEquipmentArray[i, j].GetComponent<Slot>();
                            }
                        }
                        return;
                    }
                }
            }
        }
        //Item.ItemEnumType이 장비타입 일때
        else
        {
            for (int indexY = 0; indexY < ARRAY_Y; indexY++)
            {
                for (int indexX = 0; indexX < ARRAY_X; indexX++)
                {
                    if (mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem == null)
                    {
                        mInventoryArray[indexY, indexX].GetComponent<Slot>().AddItem(item, itemCount);
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                InventoryManager.Instance.mInventorySlots[i, j] = mInventoryArray[i, j].GetComponent<Slot>();
                            }
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                InventoryManager.Instance.mEquipmentSlots[i, j] = mEquipmentArray[i, j].GetComponent<Slot>();
                            }
                        }
                        return;
                    }
                }

            }
        }


        // for(int indexY = 0; indexY < ARRAY_Y; indexY ++)
        // {
        //     for(int indexX = 0; indexX < ARRAY_X; indexX ++)
        //     {
        //         InventoryManager.Instance.mInventorySlots[indexY,indexX] =  mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem;
        //     }
        // }
        // for(int indexY = 0; indexY < EQUIPMENT_ARRAY_Y; indexY ++)
        // {
        //     for(int indexX = 0; indexX < EQUIPMENT_ARRAY_X; indexX ++)
        //     {
        //         InventoryManager.Instance.mEquipmentSlots[indexY,indexX] =  mEquipmentArray[indexY, indexX].GetComponent<Slot>().mItem;
        //     }
        // }
        //InventoryManager.Instance.mInventorySlots = mInventoryArray;
        //InventoryManager.Instance.mEquipmentSlots = mEquipmentArray;
    }

    public void SelectSlot()
    {
        //인벤토리에서 포인터 움직일때
        if (!mIsEquipmentCheck)
        {
            //인벤토리 배열에 아이템이 없을때
            if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == null)
            {
                //포인터에 아이템이 없을때
                if (mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem == null)
                {

                }
                //인벤토리 배열에 아이템이 없고 포인터에 아이템이 있으면
                else
                {
                    mSelectCount = 0;

                    mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().AddItem(
                        mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem, mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount);

                    mSelectPoint.transform.GetChild(0).GetComponent<Slot>().ClearSlot();
                    mSelectPoint.transform.GetChild(0).gameObject.SetActive(false);
                }

            }
            //인벤토리 배열에 아이템이 있고
            else if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem != null)
            {
                //포인터에 켜져있고
                if (mSelectPoint.transform.GetChild(0).gameObject.activeSelf)
                {
                    //인벤토리 배열의 아이템과 포인터의 아이템이 같을때
                    if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem)
                    {
                        //아이템 타입이 장비면
                        if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem.mItemType == Item.ItemEnumType.Equiment)
                        {

                        }
                        //아이템 타입이 장비가 아니면
                        else
                        {
                            SelectSwap();
                        }
                    }
                    else
                    {
                        Swap();
                    }
                }
                else if (!mSelectPoint.transform.GetChild(0).gameObject.activeSelf)
                {
                    SelectSwap();
                }
            }
        }
        //장비창에서 포인터 움직일때
        else
        {
            //장비창 배열에 아이템이 없을때
            if (mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem == null)
            {
                //포인터에 아이템이 없을때
                if (mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem == null)
                {

                }
                //장비창 배열에 아이템이 없고 포인터에 아이템이 있으면
                else
                {
                    //mEquipment[0,0]과  [0, 1]에는 무기
                    if (mSelectEquipmentY == 0 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Weapon)
                    {
                        WearEquipment();
                    }
                    //mEquipment[1,0] [2,0] [3,0]자리에는 헬멧 아머 부츠
                    else if (mSelectEquipmentX == 0)
                    {
                        if (mSelectEquipmentY == 1 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Helmet)
                        {
                            WearEquipment();
                        }
                        else if (mSelectEquipmentY == 2 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Armor)
                        {
                            WearEquipment();
                        }
                        else if (mSelectEquipmentY == 3 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Boots)
                        {
                            WearEquipment();
                        }
                    }
                    else if (mSelectEquipmentY == 2 && mSelectEquipmentX == 1 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mItemType == Item.ItemEnumType.Potion)
                    {
                        WearEquipment();
                    }
                    else if (mSelectEquipmentX == 1)
                    {
                        if ((mSelectEquipmentY == 1 || mSelectEquipmentY == 3) && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Ring)
                        {
                            WearEquipment();
                        }
                    }
                }

            }
            //장비창 배열에 아이템이 있을때
            else if (mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem != null)
            {
                //포인터에 아이템이 있고
                if (mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem != null)
                {
                    if (mSelectEquipmentY == 0 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Weapon)
                    {
                        Swap();
                    }
                    //mEquipment[1,0] [2,0] [3,0]자리에는 헬멧 아머 부츠
                    else if (mSelectEquipmentX == 0)
                    {
                        if (mSelectEquipmentY == 1 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Helmet)
                        {
                            Swap();

                        }
                        else if (mSelectEquipmentY == 2 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Armor)
                        {
                            Swap();
                        }
                        else if (mSelectEquipmentY == 3 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Boots)
                        {
                            Swap();
                        }
                    }
                    else if (mSelectEquipmentY == 2 && mSelectEquipmentX == 1 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mItemType == Item.ItemEnumType.Potion)
                    {
                        SelectSwap();
                    }
                    else if (mSelectEquipmentX == 1)
                    {
                        if ((mSelectEquipmentY == 1 || mSelectEquipmentY == 3) && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Ring)
                        {
                            Swap();
                        }
                    }
                    else
                    {

                    }


                }
                else
                {
                    SelectSwap();
                }






                //     //장비창 배열의 아이템과 포인터의 아이템이 같을때
                //     if (mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem == mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem)
                //     {
                //         //아이템 타입이 장비면
                //         if (mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem.mItemType == Item.ItemEnumType.Equiment)
                //         {

                //         }
                //         //아이템 타입이 장비가 아니면
                //         else
                //         {
                //             SelectSwap();
                //         }
                //     }
                //     else
                //     {
                //         Swap();

                //     }
                // }
                // else if (!mSelectPoint.transform.GetChild(0).gameObject.activeSelf)
                // {
                //     SelectSwap();
                // }
            }
        }
    }

    public void WearEquipment()
    {
        mSelectCount = 0;

        mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().AddItem(mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem, mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount);
        mSelectPoint.transform.GetChild(0).GetComponent<Slot>().ClearSlot();
        mSelectPoint.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void SelectSwap()
    {
        //선택된 배열의 아이템의 수를 1씩 빼기
        //선택을 눌렀을때 선택 스프라이트 켜기
        mSelectPoint.transform.GetChild(0).gameObject.SetActive(true);
        //카운트
        mSelectCount++;
        if (!mIsEquipmentCheck)
        {
            mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mSelectCount);

            mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().SetSlotCount(-1);
        }
        else
        {
            mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem, mSelectCount);

            mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().SetSlotCount(-1);
        }
    }

    public void Swap()
    {
        Item swapTemp;
        int swapCount;

        swapTemp = mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem;
        swapCount = mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount;
        if (!mIsEquipmentCheck)
        {
            mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItemCount);
            mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().AddItem(swapTemp, swapCount);
        }
        else
        {
            mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem, mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItemCount);
            mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().AddItem(swapTemp, swapCount);
        }


    }

    



    //C눌렀을때 아이템이 장비면 장비칸에 알아서 가고 
    //장비에서 누르면 인벤토리 빈칸이나 칸은 칸에
    public void ItemSelfInsert()
    {
        if (!mIsEquipmentCheck)
        {

        }
    }
}
