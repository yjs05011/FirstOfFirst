using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestInventory : MonoBehaviour
{
    //(4,5)배열 상수
    public const int ARRAY_X = 5;
    public const int ARRAY_Y = 4;

    //(4,7)배열 상수
    public const int CHEST_ARRAY_X = 7;
    public const int CHEST_ARRAY_Y = 4;

    //(4,5)배열 변수
    public int mSelectX = default;
    public int mSelectY = default;
    //(4,7)배열 변수
    public int mSelectChestY = default;
    public int mSelectChestX = default;

    public int mKeyCount = 0;
    public int mSelectCount = 0;

    //인벤토리 활성화 확인. true면 인벤토리가 열림.이동 막음
    public static bool mIsChestActiveCheck = false;
    //마을인지 확인. false면 돋보기 칸 비활성화, true면 활성화
    public static bool mIsVillageCheck = false;

    //인벤토리 키보드 활성화확인
    public static bool mIsChestSelectMove = false;
    public static bool mIsChestCheck = false;
    public bool mChestIsInventoryOpen = false;
    public bool mIsChestSlotCheck = false;

    //드래그앤 드롭으로 게임 오브젝트에 대입
    public GameObject mChangeChestBase;
    public GameObject mInventory;
    public GameObject mChestInventory;

    //배열들의 부모를 찾기 쉽게 하기 위해 만든 GameObject
    public GameObject mInventoryFindSlot;
    public GameObject mChestInventoryFindSlot;
    public GameObject mChestSelectPoint = null;
    public GameObject mChestSelectItem;

    //기본상태 인벤토리 배열
    //같은 인벤토리라서 이름이 같음
    public GameObject[,] mInventoryGameObjectArray;
    public GameObject[,] mChestGameObjectArray;
    public Slot[,] mInventoryArray;
    //상자 인벤토리 배열
    public Slot[,] mChestArray;

    private Coroutine chestSwapFunc = default;

    private void OnEnable()
    {

    }
    private void Awake()
    {
    }
    void Start()
    {
        
        mInventoryArray = new Slot[4, 5];
        mChestArray = new Slot[4, 7];
        mInventoryGameObjectArray = new GameObject[4, 5];
        mChestGameObjectArray = new GameObject[4, 7];

        for (int indexY = 0; indexY < ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < ARRAY_X; indexX++)
            {
                int indexAdd = indexY * ARRAY_X + indexX;

                mInventoryArray[indexY, indexX] = mInventoryFindSlot.transform.GetChild(indexAdd).GetComponent<Slot>();
                //Debug.Log($"mInventoryArray[indexY, indexX] Tag : Y : {indexY} / X : {indexX} / {mInventoryArray[indexY, indexX].tag}");
                mInventoryArray[indexY, indexX].gameObject.SetActive(true);
                InventoryManager.Instance.mChestInventorySlots[indexY, indexX] = mInventoryArray[indexY, indexX].GetComponent<Slot>();
            }
        }
        mChestSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;

        for (int indexY = 0; indexY < CHEST_ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < CHEST_ARRAY_X; indexX++)
            {
                int indexChestAdd = indexY * CHEST_ARRAY_X + indexX;

                mChestArray[indexY, indexX] = mChestInventoryFindSlot.transform.GetChild(indexChestAdd).GetComponent<Slot>();
                mChestArray[indexY, indexX].gameObject.SetActive(true);
                InventoryManager.Instance.mChestSlots[indexY, indexX] = (mChestArray[indexY, indexX].GetComponent<Slot>());
            }
        }



        //bool 초기화
        mIsChestSelectMove = false;
        mIsChestCheck = false;
        SetActiveAll(false);
        mChestSelectPoint.transform.GetChild(0).gameObject.SetActive(false);

       Debug.Log(mChestSelectPoint.transform.localPosition);
        

    }
    void Update()
    {
        // if (InventoryManager.Instance.mIsManagerAddCheck == true)
        // {
        //     LoadItem();
        //     InventoryManager.Instance.mIsManagerAddCheck = false;
        // }

        TryOpenChest();
        if (mChestIsInventoryOpen == true)
        {
            if (!mIsChestCheck)
            {
                InventoryMove();
            }
            else
            {
                ChestMove();
            }
        }
    }

    private void TryOpenChest()
    {
        //KeyManager를 통해 키보드 입력(매니저에서 받는)
        //if(Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        //화요일 머지시 지워야함(i눌러서 인벤토리 나오는 부분)

        if (Input.GetKeyDown(KeyCode.U))
        {
            Invoke("DeleyItemList", 0.001f);
            mIsChestActiveCheck = !mIsChestActiveCheck;

            if (Input.GetKeyDown(KeyCode.U))
            {
                mKeyCount++;
                if (mKeyCount == 1)
                {
                    OpenChest();
                    
                }
                if (mKeyCount == 2)
                {
                    mKeyCount = 0;
                    CloseChest();
                }
            }
        }
    }
    public void SetActiveAll(bool flag)
    {
        transform.GetChild(0).gameObject.SetActive(flag);
        transform.GetChild(1).gameObject.SetActive(flag);
        transform.GetChild(2).gameObject.SetActive(flag);
        transform.GetChild(3).gameObject.SetActive(flag);
    }
    public void OpenChest()
    {
        if (ItemNullCheck())
        {

            //Debug.Log($"{mChestSelectPoint.transform.localPosition}, 포인터");

            // mChestSelectPoint = mInventoryArray[mSelectY, mSelectX].gameObject;
            // mChestSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;
        }
        mInventoryArray = new Slot[4,5];
        mInventoryArray = InventoryManager.Instance.mInventorySlots;
        mSelectY = 0;
        mSelectX = 0;
        //mChestSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;
        mChestIsInventoryOpen = true;
        SetActiveAll(true);
        // 


    }
    public void CloseChest()
    {
        if (ItemNullCheck())
        {
            
            // mChestSelectPoint = mInventoryArray[mSelectY, mSelectX].gameObject;
            // mChestSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;
        }
        mChestIsInventoryOpen = false;
        
        mChestSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;
        

        
        InventoryManager.Instance.mInventorySlots = mInventoryArray;
        SetActiveAll(false);
    }

    //매니저에서 정보 불러오기
    // void LoadItem()
    // {
    //     for (int indexY = 0; indexY < ARRAY_Y; indexY++)
    //     {
    //         for (int indexX = 0; indexX < ARRAY_X; indexX++)
    //         {
    //             mInventoryArray[indexY, indexX] = InventoryManager.Instance.mInventorySlots[indexY, indexX];
                
    //         }
    //     }        

    // }

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
    //매니저로 저장정보 넘겨주기.
    void DeleyItemList()
    {
        if (ItemNullCheck())
        {
            for (int indexY = 0; indexY < ARRAY_Y; indexY++)
            {
                for (int indexX = 0; indexX < ARRAY_X; indexX++)
                {
                    int indexAdd = indexY * ARRAY_X + indexX;
                    if (mInventoryArray[indexY, indexX].mItem != null)
                    {
                        mInventoryFindSlot.transform.GetChild(indexAdd).GetComponent<Slot>().AddItem(mInventoryArray[indexY, indexX].mItem, mInventoryArray[indexY, indexX].mItemCount);
                        mInventoryArray[indexY, indexX].gameObject.SetActive(true);
                        InventoryManager.Instance.mChestInventorySlots[indexY, indexX] = mInventoryArray[indexY, indexX].GetComponent<Slot>();
                        
                    }
                }
            }
        }

    }

    #region 인벤토리와 상자의 이동 함수
    //인벤토리에서 이동
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
            //ItemSelfInsert();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (mSelectX < 4)
            {
                mSelectX++;
                mChestSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
                Debug.Log("!!");
            }
            else
            {
                mIsChestCheck = true;
                mIsChestSlotCheck = true;
                mSelectChestY = mSelectY;
                mSelectChestX = 0;
                mChestSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
                Debug.Log("!");
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (0 < mSelectX)
            {
                mSelectX--;
                mChestSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
            }
            else
            {
                mIsChestCheck = true;
                mIsChestSlotCheck = true;
                mSelectChestY = mSelectY;
                mSelectChestX = 6;
                mChestSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
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
            mChestSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mSelectY++;
            if (3 < mSelectY)
            {
                mSelectY = 0;
            }
            //Debug.Log(mInventoryArray[mSelectY, mSelectX]);
            mChestSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
        }
    }

    //상자 이동

    public void ChestMove()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            SelectSlot();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {           
            if (mSelectChestX < 6)
            {
                mSelectChestX++;
                mChestSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
            }
            else
            {
                mSelectY = mSelectChestY;
                mSelectX = 0;
                mChestSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
                mIsChestCheck = false;
                mIsChestSlotCheck = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (0 < mSelectChestX)
            {
                mSelectChestX--;
                mChestSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
            }
            else
            {
                mSelectY = mSelectChestY;
                mSelectX = 4;
                mChestSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
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
            mChestSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mSelectChestY++;
            if (3 < mSelectChestY)
            {
                mSelectChestY = 0;
            }
            mChestSelectPoint.transform.localPosition = mChestArray[mSelectChestY, mSelectChestX].transform.localPosition;
        }
    }
    #endregion


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
                                if (mChestArray[i, j].GetComponent<Slot>().mItem != null)
                                    InventoryManager.Instance.mEquipmentSlots[i, j] = mChestArray[i, j].GetComponent<Slot>();
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
                                InventoryManager.Instance.mEquipmentSlots[i, j] = mChestArray[i, j].GetComponent<Slot>();
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
        //Debug.Log($"{},템있니");
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
                else //if(!mChestSelectPoint.transform.GetChild(0).gameObject.activeSelf)
                {
                    // FOR DEBUG:
                    // System.TimeSpan initializedTimeDeffer = 
                    //     InventoryManager.Instance.spRendererInitializedTime - 
                    //     InventoryManager.Instance.objActivedTime;
                    // System.TimeSpan itemAddedTimeDeffer = 
                    //     InventoryManager.Instance.afterItemAddedTime - 
                    //     InventoryManager.Instance.objActivedTime;
                    // Debug.Log($"objActivedTime: {InventoryManager.Instance.objActivedTime.Millisecond}");
                    // Debug.Log($"spRendererInitializedTime: {InventoryManager.Instance.spRendererInitializedTime.Millisecond}");
                    // Debug.Log($"afterItemAddedTime: {InventoryManager.Instance.afterItemAddedTime.Millisecond}");
                    // Debug.Log($"initializedTimeDeffer: {initializedTimeDeffer.TotalMilliseconds}");
                    // Debug.Log($"itemAddedTimeDeffer: {itemAddedTimeDeffer.TotalMilliseconds}");
                    SelectSwap();
                }
            }
        }
        //창고에서 포인터 움직일때
        else
        {
            //창 배열에 아이템이 없을때
            if (mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem == null)
            {
                //포인터에 아이템이 없을때
                if (mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem == null)
                {

                }
                //장비창 배열에 아이템이 없고 포인터에 아이템이 있으면
                else
                {
                    WearEquipment();
                    // mSelectCount = 0;
                    // mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().AddItem(
                    // mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem, mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount);
                    // mChestSelectPoint.transform.GetChild(0).gameObject.SetActive(false);
                }

            }
            //장비창 배열에 아이템이 있을때
            else if (mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem != null)
            {
                //포인터에 아이템이 있고
                if (mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem != null)
                { //인벤토리 배열의 아이템과 포인터의 아이템이 같을때
                    if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem)
                    {
                        //아이템 타입이 장비면
                        if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem.mItemType == Item.ItemEnumType.Equiment)
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

    public void WearEquipment()
    {
        mSelectCount = 0;

        mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().AddItem(mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem, mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount);
        mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().ClearSlot();
        mChestSelectPoint.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void SelectSwap()
    {
        //선택된 배열의 아이템의 수를 1씩 빼기
        //선택을 눌렀을때 선택 스프라이트 켜기

        // FOR DEBUG:
        // InventoryManager.Instance.objActivedTime = System.DateTime.Now;

        mChestSelectPoint.transform.GetChild(0).gameObject.SetActive(true);
        
        if(chestSwapFunc != default || chestSwapFunc != null) StopCoroutine(chestSwapFunc);
        chestSwapFunc = StartCoroutine(DoSelectSwap());
        
        // LEGACY:
        // //카운트
        // mSelectCount++;
        // if (!mIsChesmIsChestSlotChecktSlotCheck)
        // {
        //     mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mSelectCount);
            
        //     // FOR DEBUG:
        //     // InventoryManager.Instance.afterItemAddedTime = System.DateTime.Now;

        //     mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().SetSlotCount(-1);
        // }
        // else
        // {
        //     mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem, mSelectCount);

        //     mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().SetSlotCount(-1);
        // }
    }

    private IEnumerator DoSelectSwap()
    {
        yield return null;

        //카운트
        mSelectCount++;
        if (!mIsChestSlotCheck)
        {
            Debug.Log( mInventoryArray[mSelectY, mSelectX].tag);
            mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(
                mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mSelectCount);
            
            // FOR DEBUG:
            // InventoryManager.Instance.afterItemAddedTime = System.DateTime.Now;
            mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().SetSlotCount(-1);
        }
        else
        {
            mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(
                mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem, mSelectCount);

                
            mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().SetSlotCount(-1);
        }
    }       // DoSelectSwap()

    public void Swap()
    {
        Item swapTemp;
        int swapCount;

        swapTemp = mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItem;
        swapCount = mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().mItemCount;
        if (!mIsChestSlotCheck)
        {
            mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem, mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItemCount);
            mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().AddItem(swapTemp, swapCount);
        }
        else
        {
            mChestSelectPoint.transform.GetChild(0).GetComponent<Slot>().AddItem(mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItem, mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().mItemCount);
            mChestArray[mSelectChestY, mSelectChestX].GetComponent<Slot>().AddItem(swapTemp, swapCount);
        }

    }





}
