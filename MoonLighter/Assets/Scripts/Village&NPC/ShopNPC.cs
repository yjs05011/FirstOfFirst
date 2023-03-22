using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject mTables;
    public SpriteRenderer mGetItem;
    public Vector3 mTablePosition;
    public Sprite mItem;
    public int mTableNumber;


    private Animator mNpcAni;
    private float mSpeed = 1f;
    private int mMoveCount;
    private bool IsGoOut = false;
    private bool IsFirst = true;
    private bool IsCalculate = true;
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
        if (Shop.mIsShopStart)
        {
            if (mTablePosition != Vector3.zero)
            {

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

                        if (!IsCalculate)
                        {
                            Invoke("GoOut", 0);
                        }
                        else
                        {

                            ShopManager.Instance.mItemTables[mTableNumber].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                            mGetItem.sprite = mItem;
                            mItem = null;
                            Invoke("GoCash", 0);
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
                        GoOutShop();
                    }
                    if (mMoveCount < 3)
                    {
                        Invoke("RandomPosition", 3);
                    }
                    else
                    {
                        Invoke("GoOut", 3);
                    }
                }
            }
        }
        else
        {
            Invoke("GoOut", 0);
        }

    }

    public void RandomPosition()
    {
        CancelInvoke();
        mNpcAni.SetBool("IsWalking", false);
        mMoveCount++;
        mPosition = new Vector3(transform.position.x + Random.Range(-3, 4), transform.position.y + Random.Range(-3, 4), 0);
        if (mPosition.x > 3.5) { mPosition = new Vector3(3.5f, mPosition.y, 0); }
        if (mPosition.x < 0) { mPosition = new Vector3(0, mPosition.y, 0); }
        if (mPosition.y > -4) { mPosition = new Vector3(mPosition.x, -4, 0); }
        if (mPosition.y < -6) { mPosition = new Vector3(mPosition.x, -6, 0); }
        MoveDirection();
    }

    public void GoOut()
    {
        CancelInvoke();
        mNpcAni.SetBool("IsWalking", false);
        if (mTablePosition != Vector3.zero)
        {
            mTablePosition = new Vector3(1, -6.5f, 0);
        }
        else
        {
            mPosition = new Vector3(1, -6.5f, 0);
        }

        MoveDirection();
        IsGoOut = true;
    }
    public void GoCash()
    {
        CancelInvoke();
        mNpcAni.SetBool("IsWalking", false);
        mTablePosition = new Vector3(1, -3.9f, 0);
        MoveDirection();
        IsCalculate = false;
    }
    public void GoOutShop()
    {
        ShopManager.Instance.mShopNPC.Remove(this.gameObject);
        mMoveCount = 0;
        IsGoOut = false;
        IsCalculate = true;
        IsFirst = true;
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
