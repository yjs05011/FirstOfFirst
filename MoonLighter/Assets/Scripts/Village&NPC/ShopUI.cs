using System;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject[,] mInventoryItem = new GameObject[4, 5];
    private GameObject[,] mTableItem = new GameObject[4, 2];
    private int[,] mInventoryItemNumber = new int[4, 5];
    private int[,] mTableItemNumber = new int[2, 2];
    private int[,] mTableItemMoney = new int[2, 2];

    private int mDigits;

    private int mInventoryX;
    private int mInventoryY;
    private int mTableX;
    private int mTableY;
    public int mTableNumber;
    
    private Vector3 mSelectPosition;
    private bool mIsInventory;
    private bool mIsGetPrice;
    private bool mDontShutDownUI;

    public int mSelectItemNumber;
    public GameObject mSelector;
    public GameObject mSelectItem;

    public void OnEnable()
    {
        PlayerManager.Instance.mIsUiActive = true;
        mTableNumber = ShopManager.Instance.mTablesNumber;
        for (int indexX = 0; indexX < 4; indexX++)
        {
            for (int indexY = 0; indexY < 5; indexY++)
            {
                mInventoryItem[indexX, indexY] = transform.Find("Items").GetChild((indexX * 5) + indexY).gameObject;
                mInventoryItem[indexX, indexY].gameObject.SetActive(false);
            }
        }
        for (int indexY = 0; indexY < 2; indexY++)
        {
            for (int indexX = 0; indexX < 4; indexX++)
            {
                mTableItem[indexX, indexY] = transform.Find("Tables").GetChild(indexX + (indexY * 4)).gameObject;
                if (indexX == 0 || indexX == 2)
                {
                    if (ShopManager.Instance.mItems[(mTableNumber*4) + indexX + indexY] != null)
                    {
                        mTableItem[indexX, indexY].SetActive(true);
                        mTableItem[indexX, indexY].GetComponent<SpriteRenderer>().sprite = ShopManager.Instance.mItems[(mTableNumber * 4) + indexX + indexY];
                        if (indexX == 0)
                        {
                            mTableItemNumber[0, indexY] = ShopManager.Instance.mItemsNumber[(mTableNumber * 4) + indexX + indexY];
                            GFunc.SetTmpText(mTableItem[indexX, indexY].transform.GetChild(0).gameObject, mTableItemNumber[0, indexY].ToString());
                        }
                        else
                        {
                            mTableItemNumber[1, indexY] = ShopManager.Instance.mItemsNumber[(mTableNumber * 4) + indexX + indexY];
                            GFunc.SetTmpText(mTableItem[indexX, indexY].transform.GetChild(0).gameObject, mTableItemNumber[1, indexY].ToString());
                        }

                    }
                    else
                    {
                        mTableItem[indexX, indexY].SetActive(false);
                    }

                }
                if (indexX == 1 || indexX == 3)
                {
                    mTableItem[indexX, indexY].transform.GetChild(0).gameObject.SetActive(false);
                    if (ShopManager.Instance.mItems[(mTableNumber * 4) + (indexX - 1) + indexY] != null)
                    {
                        if (indexX == 1)
                        {
                            GFunc.SetTmpText(mTableItem[indexX, indexY].transform.GetChild(3).gameObject, ShopManager.Instance.mItemPrice[(mTableNumber * 4) + (indexX - 1) + indexY].ToString());
                            mTableItemMoney[0, indexY] = ShopManager.Instance.mItemPrice[(mTableNumber * 4) + (indexX - 1) + indexY] / ShopManager.Instance.mItemsNumber[(mTableNumber * 4) + (indexX - 1) + indexY];
                            GFunc.SetTmpText(mTableItem[indexX, indexY].transform.GetChild(1).gameObject, $"{mTableItemMoney[0, indexY]:D7}");
                        }
                        else
                        {
                            GFunc.SetTmpText(mTableItem[indexX, indexY].transform.GetChild(3).gameObject, ShopManager.Instance.mItemPrice[(mTableNumber * 4) + (indexX - 1) + indexY].ToString());
                            mTableItemMoney[1, indexY] = ShopManager.Instance.mItemPrice[(mTableNumber * 4) + (indexX - 1) + indexY] / ShopManager.Instance.mItemsNumber[(mTableNumber * 4) + (indexX - 1) + indexY];
                            GFunc.SetTmpText(mTableItem[indexX, indexY].transform.GetChild(1).gameObject, $"{mTableItemMoney[1, indexY]:D7}");
                        }
                    }
                    else
                    {
                        GFunc.SetTmpText(mTableItem[indexX, indexY].transform.GetChild(1).gameObject, "0000000");
                        GFunc.SetTmpText(mTableItem[indexX, indexY].transform.GetChild(3).gameObject, "0");
                    }
                }

            }
        }
        
        mInventoryX = 0;
        mInventoryY = 0;
        mTableX = 0;
        mTableY = 0;
        mDontShutDownUI = false;
        mIsInventory = true;
        mIsGetPrice = false;
        mSelectPosition = mInventoryItem[mInventoryX, mInventoryY].transform.position;
        mSelectItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsInventory)
        {
            Inventory();
        }
        else
        {
            Table();
        }
        mSelector.transform.position = mSelectPosition;
        if(Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.ATTACK])) 
        {
            if(!mDontShutDownUI)
            {
                PlayerManager.Instance.mIsUiActive = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void Inventory()
    {
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
        {
            if (mInventoryX > 0) { mInventoryX--; }
        }
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.DOWN]))
        {
            if (mInventoryX < 3) { mInventoryX++; }
        }
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.LEFT]))
        {
            if (mInventoryY > 0) { mInventoryY--; }
        }
        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.RIGHT]))
        {
            if (mInventoryY < 4) { mInventoryY++; }
            else { mIsInventory = false; }
        }
        mSelectPosition = mInventoryItem[mInventoryX, mInventoryY].transform.position;

        if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))
        {
            if (mInventoryItem[mInventoryX, mInventoryY].GetComponent<SpriteRenderer>().sprite != null) // �κ��丮 �������� ������ (�������� ���÷��� �Ѵ�)
            {
                // ��� �ִ� �������� ����� ����
                if(!mSelectItem.activeSelf)
                {
                    mDontShutDownUI = true;
                    mSelectItem.SetActive(true);
                    mSelectItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mInventoryItem[mInventoryX, mInventoryY].GetComponent<SpriteRenderer>().sprite;
                    mInventoryItem[mInventoryX, mInventoryY].GetComponent<SpriteRenderer>().sprite = null;
                    mSelectItemNumber = mInventoryItemNumber[mInventoryX, mInventoryY];
                    mInventoryItemNumber[mInventoryX, mInventoryY] = 0;
                    mInventoryItem[mInventoryX, mInventoryY].SetActive(false);
                    GFunc.SetTmpText(mSelectItem.transform.GetChild(0).GetChild(0).GetChild(0).gameObject, mSelectItemNumber.ToString());
                }
            }
            else                                                                                        // �κ��丮 �������� ������ (���ø� �������� ������ �������� �κ��丮�� �ִ´�)
            {
                // ��� �ִ� �������� �־�� ����
                if(mSelectItem.activeSelf)
                {
                    mDontShutDownUI = false;
                    mInventoryItem[mInventoryX, mInventoryY].SetActive(true);
                    mInventoryItem[mInventoryX, mInventoryY].GetComponent<SpriteRenderer>().sprite = mSelectItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                    mSelectItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                    mInventoryItemNumber[mInventoryX, mInventoryY] = mSelectItemNumber;
                    mSelectItemNumber = 0;
                    mSelectItem.SetActive(false);
                    GFunc.SetTmpText(mInventoryItem[mInventoryX, mInventoryY].transform.GetChild(0).gameObject, mInventoryItemNumber[mInventoryX, mInventoryY].ToString());
                }
            }
        }
    }
    public void Table()
    {
        if (mIsGetPrice)
        {
            
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
            {
                if (mTableX == 1)
                {
                    mTableItemMoney[0, mTableY] += (int)Mathf.Pow(10, mDigits);
                    if (mTableItemMoney[0, mTableY] > 9999999)
                    {
                        mTableItemMoney[0, mTableY] = 9999999;
                    }
                    GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(1).gameObject, $"{mTableItemMoney[0, mTableY]:D7}");
                    GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(3).gameObject, $"{mTableItemMoney[0, mTableY] * mTableItemNumber[0, mTableY]}");
                }
                else if (mTableX == 3)
                {
                    mTableItemMoney[1, mTableY] += (int)Mathf.Pow(10, mDigits);
                    if (mTableItemMoney[1, mTableY] > 9999999)
                    {
                        mTableItemMoney[1, mTableY] = 9999999;
                    }
                    GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(1).gameObject, $"{mTableItemMoney[1, mTableY]:D7}");
                    GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(3).gameObject, $"{mTableItemMoney[1, mTableY] * mTableItemNumber[1, mTableY]}");
                }
            }
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.DOWN]))
            {
                if (mTableX == 1)
                {
                    mTableItemMoney[0, mTableY] -= (int)Mathf.Pow(10, mDigits);
                    if (mTableItemMoney[0, mTableY] < 0)
                    {
                        mTableItemMoney[0, mTableY] = 0;
                    }
                    GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(1).gameObject, $"{mTableItemMoney[0, mTableY]:D7}");
                    GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(3).gameObject, $"{mTableItemMoney[0, mTableY] * mTableItemNumber[0, mTableY]}");
                }
                else if (mTableX == 3)
                {
                    mTableItemMoney[1, mTableY] -= (int)Mathf.Pow(10, mDigits);
                    if (mTableItemMoney[1, mTableY] < 0)
                    {
                        mTableItemMoney[1, mTableY] = 0;
                    }
                    GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(1).gameObject, $"{mTableItemMoney[1, mTableY]:D7}");
                    GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(3).gameObject, $"{mTableItemMoney[1, mTableY] * mTableItemNumber[1, mTableY]}");
                }
            }
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.LEFT]))
            {
                if (mDigits < 7)
                {
                    mDigits++;
                    mTableItem[mTableX, mTableY].transform.GetChild(0).localPosition += new Vector3(-8.2f, 0, 0);
                }
            }
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.RIGHT]))
            {
                if (mDigits > 0)
                {
                    mDigits--;
                    mTableItem[mTableX, mTableY].transform.GetChild(0).localPosition += new Vector3(8.2f, 0, 0);
                }
            }
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT])) // ������ �����ְ� ShopManager�� mItemPrice�� ���� ������ ����
            {
                mTableItem[mTableX, mTableY].transform.GetChild(0).gameObject.SetActive(false);
                if (mTableX == 1)
                {
                    ShopManager.Instance.mItemPrice[(mTableNumber * 4) + 0 + mTableY] = mTableItemMoney[0, mTableY] * mTableItemNumber[0, mTableY];
                }
                if (mTableX == 3)
                {
                    ShopManager.Instance.mItemPrice[(mTableNumber * 4) + 2 + mTableY] = mTableItemMoney[1, mTableY] * mTableItemNumber[1, mTableY];
                }
                Shop.SetPrice();
                mDontShutDownUI = false;
                mIsGetPrice = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.UP]))
            {
                if (mTableX > 0) //�� ���� �ƴϴ�
                {
                    mTableX--;

                    if (mTableX == 1 && !(mTableItem[0, mTableY].activeSelf))
                    {
                        mTableX--;
                    }

                }
            }
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.DOWN]))
            {
                if (mTableX < 3)
                {
                    mTableX++;

                    if (mTableX == 3 && !(mTableItem[2, mTableY].activeSelf))
                    {
                        mTableX--;
                    }
                    if (mTableX == 1 && !(mTableItem[0, mTableY].activeSelf))
                    {
                        mTableX++;
                    }
                }
            }
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.LEFT]))
            {
                if (mTableY > 0)
                {
                    mTableY--;
                    if (mTableX == 1 && !(mTableItem[0, mTableY].activeSelf))
                    {
                        mTableX = 0;
                    }
                    if (mTableX == 3 && !(mTableItem[2, mTableY].activeSelf))
                    {
                        mTableX = 2;
                    }
                }

                else { mIsInventory = true; }
            }
            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.RIGHT]))
            {
                if (mTableY < 1)
                {
                    mTableY++;
                    if (mTableX == 1 && !(mTableItem[0, mTableY].activeSelf))
                    {
                        mTableX = 0;
                    }
                    if (mTableX == 3 && !(mTableItem[2, mTableY].activeSelf))
                    {
                        mTableX = 2;
                    }
                }
            }
            mSelectPosition = mTableItem[mTableX, mTableY].transform.position;

            if (Input.GetKeyDown(GameKeyManger.KeySetting.keys[GameKeyManger.KeyAction.INTERRUPT]))
            {
                if (mTableX == 1 || mTableX == 3) // ���� ����
                {
                    mTableItem[mTableX, mTableY].transform.GetChild(0).gameObject.SetActive(true);
                    mTableItem[mTableX, mTableY].transform.GetChild(0).localPosition = Vector3.zero;
                    mDigits = 0;
                    mDontShutDownUI = true;
                    mIsGetPrice = true;
                }
                else  // ������ ��ġ,Ȥ�� ������ ����  (mTableItem�� ��������Ʈ,mTableItem���ڽ� �ؽ�Ʈ ����, mTableItemNumber���� )
                {
                    if (mTableItem[mTableX,mTableY].GetComponent<SpriteRenderer>().sprite != null)  // �������� �ֱ� ������ �������� ���� ����
                    {
                        //���� : �������� ��� ���� �ʴٸ� ���� ����
                        if(!mSelectItem.activeSelf)
                        {
                            mDontShutDownUI = true;
                            mSelectItem.SetActive(true);
                            mSelectItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mTableItem[mTableX, mTableY].GetComponent<SpriteRenderer>().sprite;
                            mTableItem[mTableX, mTableY].GetComponent<SpriteRenderer>().sprite = null;
                            if (mTableX == 0)
                            {
                                mSelectItemNumber = mTableItemNumber[0, mTableY];
                                mTableItemNumber[0, mTableY] = 0;
                                mTableItemMoney[0, mTableY] = 0;
                                GFunc.SetTmpText(mTableItem[mTableX+1, mTableY].transform.GetChild(1).gameObject, $"0000000");
                                GFunc.SetTmpText(mTableItem[mTableX+1, mTableY].transform.GetChild(3).gameObject, $"0");
                            }
                            else 
                            {
                                mSelectItemNumber = mTableItemNumber[1, mTableY];
                                mTableItemNumber[1, mTableY] = 0;
                                mTableItemMoney[1, mTableY] = 0;
                                GFunc.SetTmpText(mTableItem[mTableX + 1, mTableY].transform.GetChild(1).gameObject, $"0000000");
                                GFunc.SetTmpText(mTableItem[mTableX + 1, mTableY].transform.GetChild(3).gameObject, $"0");
                            }
                            GFunc.SetTmpText(mSelectItem.transform.GetChild(0).GetChild(0).GetChild(0).gameObject, mSelectItemNumber.ToString());
                           
                            mTableItem[mTableX, mTableY].SetActive(false);
                            GameObject.Find("Shop").GetComponent<Shop>().SetOutItem((mTableNumber*4) + mTableX + mTableY);
                        }
                        //transform.Find("Shop").GetComponent<Shop>().SetOnItem(mTableX + mTableY,��� �ִ� �������� ��������Ʈ,��� �ִ� �������� ����);
                    }
                    else        // �������� ���� ������ ������ ��ġ ����
                    {
                        //���� : �������� ��� �־�� ���� ����
                        if(mSelectItem.activeSelf)
                        {
                            mDontShutDownUI = false;
                            mTableItem[mTableX, mTableY].SetActive(true);
                            mTableItem[mTableX, mTableY].GetComponent<SpriteRenderer>().sprite = mSelectItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            if (mTableX == 0)
                            {
                                mTableItemNumber[0, mTableY] = mSelectItemNumber;
                                GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(0).gameObject, mTableItemNumber[0, mTableY].ToString());
                            }
                            else
                            {
                                mTableItemNumber[1, mTableY] = mSelectItemNumber;
                                GFunc.SetTmpText(mTableItem[mTableX, mTableY].transform.GetChild(0).gameObject, mTableItemNumber[1, mTableY].ToString());
                            }
                            GameObject.Find("Shop").GetComponent<Shop>().SetOnItem((mTableNumber * 4) + mTableX + mTableY, mSelectItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite, mSelectItemNumber);
                            mSelectItem.SetActive(false);
                        }
                    }
                }
            }
        }

    }

}
