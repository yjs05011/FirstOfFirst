
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //인벤토리 활성화 확인. true면 인벤토리가 열림.이동 막음
    public static bool mIsInventoryActiveCheck = false;
    //마을인지 확인. false면 돋보기 칸 비활성화, true면 활성화
    public static bool mIsVillageCheck = false;
    //인벤토리 키보드 활성화확인
    public static bool mIsSelectMove = false;
    public static bool mIsChestCheck = false;
    public bool mIsInventoryOpen = false;
    public GameObject mSelectPoint = null;


    [SerializeField]
    //인벤토리 base 이미지
    private GameObject mChangeInventoryBase;
    [SerializeField]
    private GameObject mChangeSlotGrideSetting;
    [SerializeField]
    private GameObject mChnageEquipmentSlotGrideSetting;


    //기본상태의 인벤토리배열
    public GameObject[,] mInventoryArray;
    //기본상태의 인벤토리 장비창 배열
    public GameObject[,] mEquipmentArray;
    public GameObject mInventoryFindSlot;
    public GameObject mEquipmentFindSlot;
    public GameObject mSelectPointPrefab = null;

    //public Slot mSlotGet;
    //public Slot[] mSlot;
    public Slot[] mSlotEquip;

    private void Awake()
    {
        mInventoryArray = new GameObject[4, 5];
        mEquipmentArray = new GameObject[4, 2];
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void Start()
    {
        //bool 초기화
        mIsSelectMove = false;
        mIsChestCheck = false;

        //Awake와 Start에서 인벤토리 On/Off를 통해 기능활성화
        transform.GetChild(0).gameObject.SetActive(false);

        //mSlotGet = mChangeSlotGrideSetting.GetComponentInChildren<Slot>();
        //mSlot = mChangeSlotGrideSetting.GetComponentsInChildren<Slot>();
        mSlotEquip = mChnageEquipmentSlotGrideSetting.GetComponentsInChildren<Slot>();

        mSelectPoint = mInventoryFindSlot.transform.GetChild(1).gameObject;

        //(4,5) 배열 초기화 및 선언
        for (int indexY = 0; indexY < ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < ARRAY_X; indexX++)
            {
                int indexAdd = indexY * ARRAY_X + indexX;

                mInventoryArray[indexY, indexX] = mInventoryFindSlot.transform.Find("InventorySlot").GetChild(indexAdd).gameObject;
                mInventoryArray[indexY, indexX].SetActive(true);
                InventoryManager.Instance.mSlots.Add(mInventoryArray[indexY, indexX]);
            }
        }
        mSelectPoint.transform.localPosition = mInventoryArray[0, 0].transform.localPosition;

        for (int indexY = 0; indexY < EQUIPMENT_ARRAY_Y; indexY++)
        {
            for (int indexX = 0; indexX < EQUIPMENT_ARRAY_X; indexX++)
            {
                int indexEquipmentAdd = indexY * EQUIPMENT_ARRAY_X + indexX;

                mEquipmentArray[indexY, indexX] = mEquipmentFindSlot.transform.Find("EquipmentSlot").GetChild(indexEquipmentAdd).gameObject;
                mEquipmentArray[indexY, indexX].SetActive(true);
                InventoryManager.Instance.mEquipmentSlots.Add(mEquipmentArray[indexY, indexX]);
            }
        }
    }

    void Update()
    {
        //인벤토리 On/Off를 계속 확인
        TryOpenInventory();
        if (mIsInventoryOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {

                SelectSlot();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                mSelectX++;
                if (4 < mSelectX)
                {
                    mSelectX = 0;
                }
                //Debug.Log(mInventoryArray[mSelectY, mSelectX]);
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                mSelectX--;
                if (mSelectX < 0)
                {
                    mSelectX = 4;
                }
                //Debug.Log(mInventoryArray[mSelectY, mSelectX]);
                mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;
                // Debug.Log(mSeletX);
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
    }

    //인벤토리 켰을때
    private void TryOpenInventory()
    {
        //KeyManager를 통해 키보드 입력(매니저에서 받는)
        //if(Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        //화요일 머지시 지워야함(i눌러서 인벤토리 나오는 부분)

        if (Input.GetKeyDown(KeyCode.I))
        {
            // mSelectPoint = Instantiate(mSelectPointPrefab);
            // mSelectPoint.transform.SetParent(mInventoryFindSlot.transform.GetChild(0));
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
            // if (mIsInventoryActiveCheck)
            // {
            //     OpenInventory();
            // }
            // else
            // {                
            //     CloseInventory();
            // }  
        }
    }

    //GameObject 인벤토리 베이스 활성화 
    private void OpenInventory()
    {
        mIsInventoryOpen = true;
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
                            //mSlot[index].SetSlotCount(itemCount += itemCount);

                            Debug.Log(itemCount);
                            if ( 9 < mInventoryArray[indexY, indexX].GetComponent<Slot>().mItemCount  && Item.ItemEnumType.Ingredient == item.mItemType )
                            {
                                
                            }
                            else if( 4 <mInventoryArray[indexY, indexX].GetComponent<Slot>().mItemCount && Item.ItemEnumType.Potion == item.mItemType)
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
            for (int indexY = 0; indexY < ARRAY_Y; indexY++)
            {
                for (int indexX = 0; indexX < ARRAY_X; indexX++)
                {
                    if (mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem == null)
                    {
                        mInventoryArray[indexY, indexX].GetComponent<Slot>().AddItem(item, itemCount);
                        return;
                    }
                }

            }

        }
        //Enum타입이 나머지 일때
        else
        {
            for (int indexY = 0; indexY < ARRAY_Y; indexY++)
            {
                for (int indexX = 0; indexX < ARRAY_X; indexX++)
                {
                    if (mInventoryArray[indexY, indexX].GetComponent<Slot>().mItem == null)
                    {
                        mInventoryArray[indexY, indexX].GetComponent<Slot>().AddItem(item, itemCount);
                        return;
                    }
                }
            }
        }
    }

    public void SelectSlot()
    {
        Debug.Log(mInventoryArray[mSelectY, mSelectX]);
        if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem == null)
        {
            //빈칸을 선택하면 아무것도 안함
        }
        if (mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().mItem != null)
        {
            Debug.Log(mInventoryArray[mSelectY, mSelectX]);
            // Debug.Log(mSlotGet.SetSlotCount(-1));
            mInventoryArray[mSelectY, mSelectX].GetComponent<Slot>().SetSlotCount(-1);
        }
    }

    public void SelectSwap()
    {
        mSelectPoint.transform.GetChild(0).gameObject.SetActive(true);
        mSelectPoint.transform.GetChild(0).transform.GetChild(0);
    }
}
