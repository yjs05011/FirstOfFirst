using Unity.VisualScripting;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject mTables;
    public SpriteRenderer mGetItem;

    private Sprite mItem;
    private Tables mTable;
    private Animator mNpcAni;
    private float mSpeed = 1f;
    public int mMoveCount;
    private bool IsGoOut = false;
    private bool IsFirst = true;
    private Vector3 mPosition;
    private Vector3 mTablePosition;
    // Start is called before the first frame update
    public void Start()
    {
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
        if(IsFirst)
        {
            MoveDirection();
            ShopManager.mShopNPC.Add(this.gameObject);
            IsFirst= false;
        }
        
        //for (int index = 0; index < mTable.mTables.Length; index++)
        //{
        //    mItem = mTable.mTables[index].Find("Item").GetComponent<SpriteRenderer>().sprite;
        //    if (mItem != null)
        //    {
        //        mTablePosition = mTable.mTables[index].position;
        //        break;
        //    }
        //    else
        //    {
        //        mTablePosition = Vector3.zero;
        //    }
        //}
        if (mTablePosition != Vector3.zero)
        {
            mNpcAni.SetBool("IsWalking", true);
            Vector3 tableDirection = mTablePosition - transform.position;
            transform.Translate(tableDirection.normalized * mSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, mTablePosition) <= 0.2f)
            {
                mGetItem.sprite = mItem;
                mItem = null;
            }
        }
        else
        {
            Vector3 direction = mPosition - transform.position;
            transform.Translate(direction.normalized * mSpeed * Time.deltaTime);
            mNpcAni.SetBool("IsWalking", true);

            if (Vector3.Distance(transform.position, mPosition) <= 0.2f)
            {
                if(IsGoOut)
                {
                    mMoveCount = 0;
                    IsGoOut= false;
                    IsFirst = true;
                    ShopNPCPool.ReturnObject(this);
                }
                if (mMoveCount < 3)
                {
                    mNpcAni.SetBool("IsWalking", false);
                    
                    Invoke("RandomPosition", 3);
                }
                else
                {
                    mNpcAni.SetBool("IsWalking", false);
                    Invoke("GoOut", 3);
                }
            }
        }

    }

    public void RandomPosition()
    {
        CancelInvoke();
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
        mPosition = new Vector3(1, -6.5f, 0);
        IsGoOut = true;
        MoveDirection();
    }
    public void MoveDirection()
    {
        Vector3 direction = mPosition - transform.position;

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
    }
}
