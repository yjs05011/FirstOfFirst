using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour
{

    public GameObject mStartScreenText;
    public GameObject mStartScreenLine;
    public GameObject mStartButton;
    public GameObject mMainNenus;
    public GameObject mTitleButtons;


    public GameObject mStartScreenLeftDoor;
    public GameObject mStartScreenRightDoor;

    Vector3 mStartLeftPosition;
    Vector3 mStartRightPosition;
    Vector3 mLeftDoorsDestination = new Vector3(-750, 0, 0);
    Vector3 mRightDoorsDestination = new Vector3(750, 0, 0);

    float mSpeed = 0f;


    void Update()
    {
        DoorMove();

    }

    void DoorMove()
    {
        if (Input.anyKeyDown)
        {
            // Debug.Log("아무키 입력 확인");

            mStartScreenText.SetActive(false);
            mStartScreenLine.SetActive(false);

            mMainNenus.SetActive(true);
            mTitleButtons.SetActive(true);
            // Debug.Log("활성화 하기");


            mSpeed = 10f;
        }
        mStartScreenLeftDoor.transform.localPosition = Vector3.MoveTowards(mStartScreenLeftDoor.transform.localPosition, mLeftDoorsDestination, mSpeed);
        mStartScreenRightDoor.transform.localPosition = Vector3.MoveTowards(mStartScreenRightDoor.transform.localPosition, mRightDoorsDestination, mSpeed);
        //Debug.Log($"포지션 확인 : {mStartScreenLeftDoor.transform.localPosition}, {mStartScreenRightDoor.transform.localPosition}");
        // mStartButton.SetActive(false);

    }
}
