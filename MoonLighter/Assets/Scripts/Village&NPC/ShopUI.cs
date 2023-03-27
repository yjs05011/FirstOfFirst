using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer[,] mItemInventory;
    public SpriteRenderer[,] mItemTable;
    public int mInventoryX;
    public int mInventoryY;
    public int mTableX;
    public int mTableY;
    public Vector3 mSelectPosition;
    public bool mIsInventory;
    public GameObject mSelector;
    void Start()
    {
        mItemInventory = new SpriteRenderer[4, 5];
        mItemTable = new SpriteRenderer[2, 2];
        for (int indexX = 0; indexX < 4; indexX++)
        {
            for(int indexY = 0; indexY < 5; indexY++ )
            {
                mItemInventory[indexX, indexY] = transform.Find("Items").GetChild((indexX * 5) + indexY).GetComponent<SpriteRenderer>();
            }
        }
        for (int indexX = 0; indexX < 2; indexX++)
        {
            for (int indexY = 0; indexY < 2; indexY++)
            {
                mItemTable[indexX, indexY] = transform.Find("Tables").GetChild((indexX * 2) + indexY).GetComponent<SpriteRenderer>();
            }
        }
        mInventoryX = 0;
        mInventoryY= 0;
        mTableX= 0;
        mTableY= 0;
        mIsInventory= true;
        mSelectPosition= mItemInventory[mInventoryX,mInventoryY].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(mIsInventory)
        {
            Inventory();
        }
        else
        {
            Table();
        }
        mSelector.transform.position = mSelectPosition;
    }

    public void Inventory()
    {
        if(Input.GetKeyDown(KeyCode.W)) 
        {
            if(mInventoryX > 0) {mInventoryX--;}
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (mInventoryX < 3) { mInventoryX++; }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (mInventoryY > 0) { mInventoryY--; }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (mInventoryY < 4) { mInventoryY++; }
            else { mIsInventory= false; }
        }
        mSelectPosition = mItemInventory[mInventoryX, mInventoryY].transform.position;
    }
    public void Table()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (mTableX > 0) { mTableX--; }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (mTableX < 1) { mTableX++; }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (mTableY > 0) { mTableY--; }
            else { mIsInventory = true; }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (mTableY < 1) { mTableY++; }
        }
        mSelectPosition = mItemTable[mTableX, mTableY].transform.position;
    }
}
