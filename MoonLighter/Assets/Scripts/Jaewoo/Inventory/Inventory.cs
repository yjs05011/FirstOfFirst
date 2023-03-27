
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int ARRAY_X = 5;
    public const int ARRAY_Y = 4;
    public int mSelectX = 0;
    public int mSelectY = 0;
    public int mKeyCodeICount = 0;
    //인벤토리 활성화 확인. true면 인벤토리가 열림.이동 막음
    public static bool mIsInventoryActiveCheck = false;
    //마을인지 확인. false면 돋보기 칸 비활성화, true면 활성화
    public static bool mIsVillageCheck = false;
    //인벤토리 키보드 활성화확인
    public static bool mIsSelectMove = false;
    public GameObject mSelectPoint = null;
    

    [SerializeField]
    //인벤토리 base 이미지
    private GameObject mChangeInventoryBase;
    [SerializeField]
    private GameObject mChangeSlotGrideSetting;
    [SerializeField]
    private GameObject mChnageEquipmentSlotGrideSetting;

    public GameObject[,] mInventoryArray;
    public GameObject[,] mSlotValue;
    public GameObject mInventoryFindSlot;
    public GameObject mSelectPointPrefab = null;

    public Slot[] mSlot;
    public Slot[] mSlotEquip;

    private void Awake()
    {
        mInventoryArray = new GameObject[4, 5];
        mSlotValue = new GameObject[4, 5];
        
        transform.GetChild(0).gameObject.SetActive(true);        
    }
    void Start()
    {
        mIsSelectMove = false;
        transform.GetChild(0).gameObject.SetActive(false);
        mSlot = mChangeSlotGrideSetting.GetComponentsInChildren<Slot>();
        mSlotEquip = mChnageEquipmentSlotGrideSetting.GetComponentsInChildren<Slot>();
        mSelectPoint =mInventoryFindSlot.transform.GetChild(1).gameObject;
       
        //4,5 배열 초기화 및 선언
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
        mSelectPoint.transform.localPosition = mInventoryArray[0,0].transform.localPosition;
    }

    void Update()
    {
          
        TryOpenInventory();       
        if(mIsSelectMove == true)
        {

        if (Input.GetKeyDown(KeyCode.D))
        {
            mSelectX++;
            if( 4 < mSelectX )
            {
                mSelectX = 0;
            }
            Debug.Log(mInventoryArray[mSelectY, mSelectX]);              
            mSelectPoint.transform.localPosition = mInventoryArray[mSelectY,mSelectX].transform.localPosition;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {                   
            mSelectX --;
            if(mSelectX < 0 )
            {
                mSelectX = 4;
            }
            Debug.Log(mInventoryArray[mSelectY, mSelectX]);              
            mSelectPoint.transform.localPosition = mInventoryArray[mSelectY,mSelectX].transform.localPosition;
            // Debug.Log(mSeletX);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {                   
            mSelectY --;
            if(mSelectY < 0 )
            {
                mSelectY = 3;
            }                
            Debug.Log(mInventoryArray[mSelectY, mSelectX]);             
            mSelectPoint.transform.localPosition = mInventoryArray[mSelectY, mSelectX].transform.localPosition;   

        }
        if (Input.GetKeyDown(KeyCode.S))
        {                   
            mSelectY ++;
            if(3 < mSelectY )
            {
                mSelectY = 0;
            }
            Debug.Log(mInventoryArray[mSelectY, mSelectX]);            
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
            if(Input.GetKeyDown(KeyCode.I))
            {                   
                mKeyCodeICount ++;
                if(mKeyCodeICount ==1)
                {                                       
                    OpenInventory();
                }   
                if(mKeyCodeICount == 2)
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
        mChangeInventoryBase.SetActive(true);   
    }
    //GameObject 인벤토리 베이스 비활성화 
    private void CloseInventory()
    {                     
        mChangeInventoryBase.SetActive(false);
        mIsSelectMove = false;        
    }
   
    public void AcpuireItem(Item item, int itemCount)
    {
        //Debug.Log(Item.ItemE)
        //얻은 아이템이 장비 타입과 다를때
        if (Item.ItemEnumType.Equiment != item.mItemType)
        {
            for (int index = 0; index < mSlot.Length; index++)
            {
                if (mSlot[index].mItem != null)
                {
                    //슬롯[index]의 이름과 아이템의 이름이 같으면
                    if (mSlot[index].mItem.mItemName == item.mItemName)
                    {
                        //슬롯 개수 올림
                        //mSlot[index].SetSlotCount(itemCount += itemCount);
                        mSlot[index].SetSlotCount(itemCount++);
                        if ( itemCount < 10 )
                        {
                            return;

                        }
                        
                        
                    }
                }
            }
        }

        for (int index = 0; index < mSlot.Length; index++)
        {
            if (mSlot[index].mItem == null)
            {
                mSlot[index].AddItem(item, itemCount);
                return;
            }
        }
    }


}
