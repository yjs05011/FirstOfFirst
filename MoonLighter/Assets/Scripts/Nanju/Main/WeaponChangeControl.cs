using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChangeControl : MonoBehaviour
{
    // cire1
    // x -76 y - 95
    // cire2(앞)
    // x -63 y -95 , 색 225

    // 뒤 - sorting Layer -2
    // 앞 - sorting Layer -1

    public GameObject mBigWeaponBase;
    public GameObject mSpearBase;
    public GameObject mBigWeaponSmallCircleBase;
    public GameObject mSpearSmallCircleBase;

    public SpriteRenderer mBigWeaponBaseSprite;
    public SpriteRenderer mSpearBaseSprite;
    public SpriteRenderer mBigWeaponSmallCircleBaseSprite;
    public SpriteRenderer mSpearSmallCircleBaseSprite;
    public SpriteRenderer mBaseSprite;
    public SpriteRenderer mWeaponSprite;


    private Vector2 mFrontPos = new Vector2(-63, -95);
    private Vector2 mBackPos = new Vector2(-76, -95);
    private Vector2 mSmallFrontPos = new Vector2(-54, 13);
    private Vector2 mSmallBackPos = new Vector2(-56, 13);

    private Color mFrontColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color mBackColor = new Color(1.0f, 1.0f, 1.0f, 0.5411765f);
    private Color mSmallFrontColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color mSmallBackColor = new Color(1.0f, 1.0f, 1.0f, 0.5411765f);


    // Start is called before the first frame update
    void Start()
    {
        // //앞이 대검이다.
        // //뒤가 창이다.
        mBigWeaponBaseSprite = mBigWeaponBase.GetComponent<SpriteRenderer>();
        mSpearBaseSprite = mSpearBase.GetComponent<SpriteRenderer>();

        // 스타트시 대검이 앞에 (-63,95) RGB(,,,255);
        mBigWeaponBase.transform.localPosition = mFrontPos;
        mBigWeaponBaseSprite.color = mFrontColor;
        // 대검 sorting Layer 변경
        mBigWeaponBaseSprite.sortingOrder = -1;
        mBigWeaponBase.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 0;

        // 스타트시 창이 뒤(-76,-95) RGB(,,,138);
        mSpearBase.transform.localPosition = mBackPos;
        mSpearBaseSprite.color = mBackColor;
        // 창 sorting Layer 변경
        mSpearBaseSprite.sortingOrder = -2;
        mSpearBase.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = -2;


        // 대검 small Circle
        mBigWeaponSmallCircleBaseSprite = mBigWeaponSmallCircleBase.GetComponent<SpriteRenderer>();
        mSpearSmallCircleBaseSprite = mSpearSmallCircleBase.GetComponent<SpriteRenderer>();
        // 대검 small circle
        mBigWeaponSmallCircleBase.transform.localPosition = mSmallFrontPos;
        mBigWeaponSmallCircleBaseSprite.color = mSmallFrontColor;

        // 대검 small circle Sorting Layer 변경
        mBigWeaponSmallCircleBaseSprite.sortingOrder = -1;
        mBigWeaponSmallCircleBase.transform.GetComponent<SpriteRenderer>().sortingOrder = 0;

        // 창 small circle
        mSpearSmallCircleBase.transform.localPosition = mSmallBackPos;
        mSpearSmallCircleBaseSprite.color = mSmallBackColor;

        // 창 small circle Sorting Layer 변경
        mSpearSmallCircleBaseSprite.sortingOrder = -2;
        mSpearSmallCircleBase.transform.GetComponent<SpriteRenderer>().sortingOrder = -2;



    }

    // Update is called once per frame
    void Update()
    {
        BackAndForthCheck();
    }

    void BackAndForthCheck()
    {
        if (UiManager.Instance.mIsWeaponChange)
        {
            Vector2 posTemp = default;
            Vector2 SmallPosTemp = default;
            Color colorTemp = default;
            Color SmallColorTemp = default;
            int bodyTemp = default;
            int weaponTemp = default;
            int smallBodyTemp = default;
            int smallWeaponTemp = default;

            // 대검 , 창 Pos, Color, Sorting Layer변경
            posTemp = mBigWeaponBase.transform.localPosition;
            mBigWeaponBase.transform.localPosition = mSpearBase.transform.localPosition;
            mSpearBase.transform.localPosition = posTemp;

            colorTemp = mBigWeaponBaseSprite.color;
            mBigWeaponBaseSprite.color = mSpearBaseSprite.color;
            mSpearBaseSprite.color = colorTemp;

            bodyTemp = mBigWeaponBaseSprite.sortingOrder;
            mBigWeaponBaseSprite.sortingOrder = mSpearBaseSprite.sortingOrder;
            mSpearBaseSprite.sortingOrder = bodyTemp;

            weaponTemp = mBigWeaponBase.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder;
            mBigWeaponBase.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = mSpearBase.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder;
            mSpearBase.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = weaponTemp;


            // small Circle Pos, Color, Sorting Layer 변경
            SmallPosTemp = mBigWeaponSmallCircleBase.transform.localPosition;
            mBigWeaponSmallCircleBase.transform.localPosition = mSpearSmallCircleBase.transform.localPosition;
            mSpearSmallCircleBase.transform.localPosition = SmallPosTemp;

            SmallColorTemp = mBigWeaponSmallCircleBaseSprite.color;
            mBigWeaponSmallCircleBaseSprite.color = mSpearSmallCircleBaseSprite.color;
            mSpearSmallCircleBaseSprite.color = SmallColorTemp;

            smallBodyTemp = mBigWeaponSmallCircleBaseSprite.sortingOrder;
            mBigWeaponSmallCircleBaseSprite.sortingOrder = mBigWeaponSmallCircleBaseSprite.sortingOrder;
            mBigWeaponSmallCircleBaseSprite.sortingOrder = smallBodyTemp;

            smallWeaponTemp = mBigWeaponSmallCircleBase.transform.GetComponent<SpriteRenderer>().sortingOrder;
            mBigWeaponSmallCircleBase.transform.GetComponent<SpriteRenderer>().sortingOrder = mSpearSmallCircleBase.transform.GetComponent<SpriteRenderer>().sortingOrder;
            mSpearSmallCircleBase.transform.GetComponent<SpriteRenderer>().sortingOrder = smallWeaponTemp;

            UiManager.Instance.mIsWeaponChange = true;

            // swap 방식(unit에서만 가능)
            // (mBigWeaponBase.transform.localPosition, mSpearBase.transform.localPosition) =
            // (mSpearBase.transform.localPosition, mBigWeaponBase.transform.localPosition);

            // (mBigWeaponBaseSprite.color, mBigWeaponBaseSprite.color) = (mBigWeaponBaseSprite.color, mBigWeaponBaseSprite.color);
        }
    }
}



