
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
    public bool mIsChestSelectCheck = false;

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

    public Slot[,] mChestInventoryArray;



    #region 상자용 코드
    [Header("Chest")]
    public const int CHEST_ARRAY_X = 7;
    public const int CHEST_ARRAY_Y = 4;
    public int mKeyCount = 0;
    public int mSelectChestY = default;
    public int mSelectChestX = default;

    public static bool mIsChestActiveCheck = false;
    public static bool mIsChestSelectMove = false;

    public bool mChestIsInventoryOpen = false;
    public bool mIsChestSlotCheck = false;

    //드래그앤 드롭으로 게임 오브젝트에 대입


    //창고 이미지
    public GameObject mChangeChestBase;
    //인벤토리 이미지
    public GameObject mInventory;
    public GameObject mChestInventory;

    //배열들의 부모를 찾기 쉽게 하기 위해 만든 GameObject
    public GameObject mInventoryFind;
    public GameObject mChestInventoryFind;
    public GameObject mChestSelectPoint = null;
    public GameObject mChestSelectItem;


    public Slot[,] mChestArray;


    #endregion



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

        for (int indexY = 0; indexY < ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < ARRAY_X; indexX++)
            {
                int indexAdd = indexY * ARRAY_X + indexX;
                mInventoryArray[indexY, indexX] = mInventoryFindSlot.transform.GetChild(indexAdd).gameObject.GetComponent<Slot>();

            }

        }
       
        mSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;
        mEquipmentArray = new Slot[4, 2];
        for (int indexY = 0; indexY < EQUIPMENT_ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < EQUIPMENT_ARRAY_X; indexX++)
            {
                int indexEquipmentAdd = indexY * EQUIPMENT_ARRAY_X + indexX;

                mEquipmentArray[indexY, indexX] = mEquipmentFindSlot.transform.GetChild(indexEquipmentAdd).gameObject.GetComponent<Slot>();


            }
        }

        #region 상자용 코드
        mChestArray = new Slot[4, 7];
        for (int indexY = 0; indexY < CHEST_ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < CHEST_ARRAY_X; indexX++)
            {
                int indexChestAdd = indexY * CHEST_ARRAY_X + indexX;

                mChestArray[indexY, indexX] = mChestInventoryFind.transform.GetChild(indexChestAdd).GetComponent<Slot>();


            }
        }
        #endregion


        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);

        transform.GetChild(3).gameObject.SetActive(false);

        mInventory.SetActive(false);
        mChestInventory.SetActive(false);
        mSelectPoint.transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);

        for (int indexY = 0; indexY < ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < ARRAY_X; indexX++)
            {

                Debug.Log($"{mInventoryArray[indexY, indexX]}, 스타트 위치");

            }

        }

    }

    void Update()
    {
        for (int indexY = 0; indexY < ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < ARRAY_X; indexX++)
            {

                Debug.Log($"{mInventoryArray[indexY, indexX]}, 업데이트 위치");

            }

        }

        //false 이면  

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

        TryOpenChest();
        if (mChestIsInventoryOpen == true)
        {
            if (!mIsChestCheck)
            {
                ChestInventoryMove();
            }
            else
            {
                ChestMove();
            }
        }

    }
    #region 상자용 코드
    private void TryOpenChest()
    {
        //KeyManager를 통해 키보드 입력(매니저에서 받는)
        //if(Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        //화요일 머지시 지워야함(i눌러서 인벤토리 나오는 부분)

        if (Input.GetKeyDown(KeyCode.U))
        {

            mIsChestActiveCheck = !mIsChestActiveCheck;

            if (Input.GetKeyDown(KeyCode.U))
            {
                mKeyCount++;
                if (mKeyCount == 1)
                {
                    OpenChest();
                    // LoadItem();      

                }
                if (mKeyCount == 2)
                {
                    mKeyCount = 0;
                    CloseChest();
                }
            }
        }
    }
    public void OpenChest()
    {
        mChestIsInventoryOpen = true;
        mInventoryArray = InventoryManager.Instance.mInventorySlots;
        mSelectY = 0;
        mSelectX = 0;
        mInventoryFindSlot.SetActive(true);
        mInventory.SetActive(true);
        mChestInventory.SetActive(true);
        mSelectPoint.SetActive(true);
        for (int indexY = 0; indexY < 4; indexY++)
        {
            for (int indexX = 0; indexX < 7; indexX++)
            {
                if (InventoryManager.Instance.mChestSlots[indexY, indexX] != null)
                {
                    mChestArray[indexY, indexX].mItem = InventoryManager.Instance.mChestSlots[indexY, indexX].mItem;
                    mChestArray[indexY, indexX].mItemCount = InventoryManager.Instance.mChestSlots[indexY, indexX].mItemCount;
                }

            }
        }

    }
    public void CloseChest()
    {
        mChestIsInventoryOpen = false;

        mChestSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;

        mInventoryFindSlot.SetActive(false);

        mInventory.SetActive(false);
        mChestInventory.SetActive(false);
        mSelectPoint.SetActive(false);
        InventoryManager.Instance.mInventorySlots = mInventoryArray;
        InventoryManager.Instance.mChestSlots = mChestArray;


        //(false);
    }
    #endregion

    #region 상자용 코드
    //인벤토리에서 이동
    public void ChestInventoryMove()
    {
        //슬롯에서 아이템 선택
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChestSelectSlot();
        }       


        if (Input.GetKeyDown(KeyCode.D))
        {
            if (mSelectX < 4)
            {
                mSelectX++;

                Debug.Log(mInventoryArray[mSelectY, mSelectX]);
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;

            }
            else
            {
                mIsChestCheck = true;
                mIsChestSlotCheck = true;
                mSelectChestY = mSelectY;
                mSelectChestX = 0;
                mSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;

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
                mIsChestCheck = true;
                mIsChestSlotCheck = true;
                mSelectChestY = mSelectY;
                mSelectChestX = 6;
                mSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            mSelectY--;
            if (mSelectY < 0)
            {
                mSelectY = 3;
            }
            
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

    //상자 이동

    public void ChestMove()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            ChestSelectSlot();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (mSelectChestX < 6)
            {
                mSelectChestX++;
                mSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
            }
            else
            {
                mSelectY = mSelectChestY;
                mSelectX = 0;
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
                mIsChestCheck = false;
                mIsChestSlotCheck = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (0 < mSelectChestX)
            {
                mSelectChestX--;
                mSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
            }
            else
            {
                mSelectY = mSelectChestY;
                mSelectX = 4;
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
                mIsChestCheck = false;
                mIsChestSlotCheck = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            mSelectChestY--;

            if (mSelectChestY < 0)
            {
                mSelectChestY = 3;

            }
            mSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mSelectChestY++;
            if (3 < mSelectChestY)
            {
                mSelectChestY = 0;
            }
            mSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
        }
    }
    #endregion
    #region 상자용 선택시 코드
    public void ChestSelectSlot()
    {

        //인벤토리에서 포인터 움직일때
        if (!mIsChestSlotCheck)
        {
            //인벤토리 배열에 아이템이 없을때
            if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == null)
            {
                //포인터에 아이템이 없을때
                if (mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem == null)
                {

                }
                //인벤토리 배열에 아이템이 없고 포인터에 아이템이 있으면
                else
                {
                    mSelectCount = 0;

                    mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().AddItem(
                        mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem, mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount);

                    mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().ClearSlot();
                    mChestSelectPoint.transform.GetChild(0).gameObject.SetActive(false);
                }

            }
            //인벤토리 배열에 아이템이 있고
            else if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem != null)
            {
                //포인터에 켜져있고
                if (mChestSelectPoint.transform.GetChild(0).gameObject.activeSelf)
                {
                    //인벤토리 배열의 아이템과 포인터의 아이템이 같을때
                    if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem)
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
                else
                {

                    SelectSwap();
                }
            }
        }
        //창고에서 포인터 움직일때
        else
        {
            //창고 배열에 아이템이 없을때
            if (mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem == null)
            {
                //포인터에 아이템이 없을때
                if (mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem == null)
                {

                }
                //창고 배열에 아이템이 없고 포인터에 아이템이 있으면
                else
                {
                    
                    mSelectCount = 0;
                    mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().AddItem(
                    mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem, mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount);

                    mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().ClearSlot();
                    mChestSelectPoint.transform.GetChild(0).gameObject.SetActive(false);
                }

            }
            //창고 배열에 아이템이 있을때
            else if (mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem != null)
            {
                //포인터에 아이템이 있고
                if (mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem != null)
                { //인벤토리 배열의 아이템과 포인터의 아이템이 같을때
                    if (mChestArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem)
                    {
                        //아이템 타입이 장비면
                        if (mChestArray[mSelectY, mSelectX].GetComponent<Slot>().mItem.mItemType == Item.ItemEnumType.Equiment)
                        {
                            SelectSwap();
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
                else
                {
                    SelectSwap();

                }
            }
        }
    }
    #endregion
    //인벤토리 켰을때
    private void TryOpenInventory()
    {

        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INVENTORY]))
        {
            mIsInventoryActiveCheck = !mIsInventoryActiveCheck;

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
            if (mSelectX < 4)
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            WeaponSwap();
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

    private void OpenInventory()
    {

        mSelectY = 0;
        mSelectX = 0;
        mIsInventoryOpen = true;
        mIsSelectMove = true;
        mChangeInventoryBase.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
        for (int indexY = 0; indexY < 4; indexY++)
        {
            for (int indexX = 0; indexX < 5; indexX++)
            {
                if (InventoryManager.Instance.mInventorySlots[indexY, indexX] != null)
                {
                    mInventoryArray[indexY, indexX].mItem = InventoryManager.Instance.mInventorySlots[indexY, indexX].mItem;
                    mInventoryArray[indexY, indexX].mItemCount = InventoryManager.Instance.mInventorySlots[indexY, indexX].mItemCount;
                }

            }
        }
        for (int indexY = 0; indexY < 4; indexY++)
        {
            for (int indexX = 0; indexX < 2; indexX++)
            {
                if (InventoryManager.Instance.mEquipmentSlots[indexY, indexX] != null)
                {
                    mEquipmentArray[indexY, indexX].mItem = InventoryManager.Instance.mEquipmentSlots[indexY, indexX].mItem;
                    mEquipmentArray[indexY, indexX].mItemCount = InventoryManager.Instance.mEquipmentSlots[indexY, indexX].mItemCount;
                }

            }
        }


    }
    bool ItemNullCheck()
    {
        foreach (var slotDate in mInventoryArray)
        {
            if (slotDate != null)
            {
                return true;
            }
        }
        return false;
    }
    
    private void CloseInventory()
    {

        for (int indexY = 0; indexY < 4; indexY++)
        {
            for (int indexX = 0; indexX < 5; indexX++)
            {
                InventoryManager.Instance.mInventorySlots[indexY, indexX] = mInventoryArray[indexY, indexX];
            }
        }
        for (int indexY = 0; indexY < 4; indexY++)
        {
            for (int indexX = 0; indexX < 2; indexX++)
            {

                InventoryManager.Instance.mEquipmentSlots[indexY, indexX] = (mEquipmentArray[indexY, indexX]);

            }
        }
        for (int indexY = 0; indexY < 4; indexY++)
        {
            for (int indexX = 0; indexX < 7; indexX++)
            {

                InventoryManager.Instance.mChestSlots[indexY, indexX] = (mChestArray[indexY, indexX].GetComponent<Slot>());

            }
        }


        mSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;
        mChangeInventoryBase.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);

        mIsEquipmentCheck = false;
        mIsSelectMove = false;
        mIsInventoryOpen = false;
    }

    public void AcpuireItem(Item item, int itemCount)
    {
      
        
        if (Item.ItemEnumType.Equiment != item.mItemType)
        {

            for (int indexY = 0; indexY < ARRAY_Y; indexY++)
            {
                for (int indexX = 0; indexX < ARRAY_X; indexX++)
                {

                    if (mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem != null)
                    {
                        
                        if (mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem.mItemName == item.mItemName)
                        {
                                 
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
                        
                        else
                        {
                            
                        }
                    }
                   
                }
            }

           
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


    }

    public void SelectSlot()
    {
        
        if (!mIsEquipmentCheck)
        {
            
            if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == null)
            {
                
                if (mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem == null)
                {

                }
                
                else
                {
                    mSelectCount = 0;

                    mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().AddItem(
                        mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem, mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount);

                    mSelectPoint.transform.GetChild(0).GetComponent<Slot>().ClearSlot();
                    mSelectPoint.transform.GetChild(0).gameObject.SetActive(false);
                }

            }
           
            else if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem != null)
            {
                
                if (mSelectPoint.transform.GetChild(0).gameObject.activeSelf)
                {
                    
                    if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem)
                    {
                        
                        if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem.mItemType == Item.ItemEnumType.Equiment)
                        {

                        }
                       
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
        
        else
        {
            
            if (mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem == null)
            {
               
                if (mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem == null)
                {

                }
                
                else
                {
                    
                    if (mSelectEquipmentY == 0 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Weapon)
                    {
                        WearEquipment();
                    }

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
            
            else if (mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem != null)
            {
                
                if (mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem != null)
                {
                    if (mSelectEquipmentY == 0 && mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem.mEquipmentType == Item.EquimentEnumType.Weapon)
                    {
                        Swap();
                    }
                    
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
                }
                else
                {
                    SelectSwap();
                }

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
        
        mSelectPoint.transform.GetChild(0).gameObject.SetActive(true);
        
        mSelectCount++;
        if (mIsInventoryOpen)
        {
            if (!mIsEquipmentCheck)
            {
                mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mSelectCount);

                mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().SetSlotCount(-1);
            }
            else if (mIsEquipmentCheck)
            {
                mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem, mSelectCount);

                mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().SetSlotCount(-1);
            }
        }
        else if (mChestIsInventoryOpen)
        {
            if (!mIsChestCheck)
            {
                mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mSelectCount);

                mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().SetSlotCount(-1);
            }
            else if (mIsChestCheck)
            {
                mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem, mSelectCount);

                mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().SetSlotCount(-1);
            }

        }


    }

    public void Swap()
    {
        Item swapTemp;
        int swapCount;

        swapTemp = mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem;
        swapCount = mSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount;

        if (mIsInventoryOpen)
        {
            if (!mIsEquipmentCheck)
            {
                mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItemCount);
                mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().AddItem(swapTemp, swapCount);
            }
            else if (mIsEquipmentCheck)
            {
                mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItem, mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().mItemCount);
                mEquipmentArray[mSelectEquipmentY, mSelectEquipmentX].GetComponent<Slot>().AddItem(swapTemp, swapCount);
            }
        }
        else if (mChestIsInventoryOpen)
        {
             if (!mIsChestCheck)
            {
                mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItemCount);
                mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().AddItem(swapTemp, swapCount);               
            }
            else if (mIsChestCheck)
            {
                mSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem, mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItemCount);

                mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().AddItem(swapTemp, swapCount);
            }
        }



    }



    public void WeaponSwap()
    {
        Item swapTemp;
        int swapCount;

        swapTemp = mEquipmentArray[0, 0].mItem;
        swapCount = mEquipmentArray[0, 0].mItemCount;

        mEquipmentArray[0, 0].mItem = mEquipmentArray[0, 1].mItem;
        mEquipmentArray[0, 1].mItem = swapTemp;


        mEquipmentArray[0, 0].mItemCount = mEquipmentArray[0, 1].mItemCount;
        mEquipmentArray[0, 1].mItemCount = swapCount;

    }

    
    public void ItemSelfInsert()
    {
        if (!mIsEquipmentCheck)
        {

        }
    }

    public void Bagcount()
    {
        
    }
}
