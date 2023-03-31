using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject mTables;
    public SpriteRenderer mGetItem;
    public Vector3 mTablePosition;
    public Sprite mItem;
    public int mTableNumber;

    public Vector3 mMinimumPosition;
    public Vector3 mMaximumPosition;
    public Vector3 mDoorPosition;
    public Vector3 mCasherPosition;

    private Animator mNpcAni;
    private int mItemPrice;
    private float mSpeed = 1f;
    private int mMoveCount;
    private bool IsGoOut = false;
    private bool IsFirst = true;
    public bool IsCalculate = true;
    private bool IsWaitForCalculate = false;
    private bool IsSendOnManager = true;
    private bool IsOrderIscome = true;
    private Vector3 mPosition;
    // Start is called before the first frame update
    public void Start()
    {
        mTableNumber = 0;
        mMoveCount = 0;
        //mTable = mTables.GetComponent<Tables>();
        mNpcAni = GetComponent<Animator>();
        RandomPosition();
    }
    //public void Awake()
    //{
    //    ShopManager.mShopNPC.Add(this.gameObject);
    //}

    // Update is called once per frame
    public void Update()
    {
        if (IsFirst)
        {
            MoveDirection();
            ShopManager.Instance.mShopNPC.Add(this.gameObject);
            IsFirst = false;
        }
        if (Shop.mIsShopStart || mTablePosition != Vector3.zero)
        {
            
            if (mTablePosition != Vector3.zero)
            {
               
                if (IsOrderIscome)
                {
                    ShopManager.Instance.mWaitShopNPC.Add(this.gameObject);
                    mNpcAni.SetBool("IsWalking", false);
                    MoveDirection();
                    IsOrderIscome = false;
                }

                Vector3 tableDirection = mTablePosition - transform.position;
                transform.Translate(tableDirection.normalized * mSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, mTablePosition) <= 0.2f)
                {
                    mNpcAni.SetBool("IsWalking", false);

                    if (IsGoOut)
                    {
                        GoOutShop();
                    }
                    else
                    {

                       
                        if (IsWaitForCalculate)
                        {
                            if (!IsCalculate)
                            {
                                PlayerManager.Instance.mPlayerStat.Money += mItemPrice;
                                PlayerManager.Instance.mIsMoneyChange = true;
                                ShopManager.Instance.mWaitShopNPC.Remove(this.gameObject);
                                Invoke("GoOut", 0);
                            }

                        }
                        else
                        {

                            mItemPrice = ShopManager.Instance.mItemPrice[mTableNumber];
                            GameObject.Find("Shop").GetComponent<Shop>().SetOutItem(mTableNumber);
                            ShopManager.Instance.mItemTables[mTableNumber].transform.GetChild(0).gameObject.SetActive(false);
                            mGetItem.sprite = mItem;
                            mItem = null;
                            Invoke("GoCash", 0);
                        }
                        if(!IsCalculate)
                        {
                            IsCalculate = true;
                        }
                    }

                }
            }
            else
            {
                Vector3 direction = mPosition - transform.position;
                transform.Translate(direction.normalized * mSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, mPosition) <= 0.2f)
                {
                    mNpcAni.SetBool("IsWalking", false);

                    if (IsGoOut)
                    {
                        ShopManager.Instance.mShopNPC.Remove(this.gameObject);
                        GoOutShop();
                    }
                    if (mMoveCount < 3)
                    {
                        Invoke("RandomPosition",3);
                    }
                    else
                    {
                        Invoke("GoOut", 0);
                    }

                }
            }
        }
        else
        {
            Invoke("GoOut", 0); 
            Vector3 direction = mPosition - transform.position;
            transform.Translate(direction.normalized * mSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, mPosition) <= 0.2f)
            {
                ShopManager.Instance.mShopNPC.Remove(this.gameObject);
                GoOutShop();
            }
        }

    }

    public void RandomPosition()
    {
        CancelInvoke();
        mNpcAni.SetBool("IsWalking", false);
        mMoveCount++;
        mPosition = new Vector3(transform.position.x + Random.Range(-3, 4), transform.position.y + Random.Range(-3, 4), 0);
        if (mPosition.x > mMaximumPosition.x) { mPosition = new Vector3(mMaximumPosition.x, mPosition.y, 0); }
        if (mPosition.x < mMinimumPosition.x) { mPosition = new Vector3(mMinimumPosition.x, mPosition.y, 0); }
        if (mPosition.y > mMaximumPosition.y) { mPosition = new Vector3(mPosition.x, mMaximumPosition.y, 0); }
        if (mPosition.y < mMinimumPosition.y) { mPosition = new Vector3(mPosition.x, mMinimumPosition.y, 0); }
        MoveDirection();
    }

    public void GoOut()
    {
        CancelInvoke();
        mNpcAni.SetBool("IsWalking", false);
        if (mTablePosition != Vector3.zero)
        {
            mTablePosition = mDoorPosition;
        }
        else
        {
            mPosition = mDoorPosition;
        }

        MoveDirection();
        IsGoOut = true;
    }
    public void GoCash()
    {
        CancelInvoke();
        mNpcAni.SetBool("IsWalking", false);
        mTablePosition = mCasherPosition;
        MoveDirection();
        IsWaitForCalculate = true;
        
    }
    public void GoOutShop()
    {

        mMoveCount = 0;
        IsGoOut = false;
        IsCalculate = true;
        IsWaitForCalculate = false;
        IsFirst = true;
        IsSendOnManager = true;
        IsOrderIscome = true;
        mTablePosition = Vector3.zero;
        mTableNumber = 0;
        mGetItem.sprite = null;
        ShopNPCPool.ReturnObject(this);
    }
    public void MoveDirection()
    {
        Vector3 direction;
        if (mTablePosition != Vector3.zero)
        {
            direction = mTablePosition - transform.position;
        }
        else
        {
            direction = mPosition - transform.position;
        }

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0)
            {
                mNpcAni.SetInteger("Direction", 3);
            }
            else
            {
                mNpcAni.SetInteger("Direction", 0);
            }
        }
        else
        {
            if (direction.x > 0)
            {
                mNpcAni.SetInteger("Direction", 2);
            }
            else
            {
                mNpcAni.SetInteger("Direction", 1);
            }
        }
        mNpcAni.SetBool("IsWalking", true);
    }
}
