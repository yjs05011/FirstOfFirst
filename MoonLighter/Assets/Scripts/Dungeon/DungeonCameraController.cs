using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraController : MonoBehaviour
{
    // 카메라가 쫒아갈 대상(플레이어)의 transform
    [SerializeField]
    public Transform mTarget;
    [SerializeField] 
    // 카메라 이동 
    private float mSmooth = 0.02f;
    // 카메라 초기 위치
    private Vector3 mInitCameraPos = new Vector3(0, 0, -10);
    // 카메라 경계 설정
    [SerializeField] 
    private Vector2 mCameraBoundaryMin;
    [SerializeField] 
    private Vector2 mCameraBoundaryMax;

    private float mStageWidth = 26.0f;
    private float mStageHeight = 15.0f;

    private bool mIsMove = false;
    public Vector3 mMovePos = Vector3.zero;
    public void Awake()
    {
        this.transform.position = mInitCameraPos;
       
    }

    public void Update()
    {
        if (mIsMove) 
        {
            this.transform.position = Vector3.Lerp(this.transform.position, mMovePos, mSmooth);
        }
       
       
    }
    public void CameraMoveByPos(Vector3 NextPos)
    {

        Vector3 currPos = this.transform.position;

        Vector3 MovePos = new Vector3(NextPos.x, NextPos.y, currPos.z);
        // new Vector3(startX * 26.0f, startY * 15.0f, 0);
        mMovePos = MovePos;
        mIsMove = true;
        //this.transform.position = Vector3.Lerp(currPos, NextPos, mSmooth);
    }
    


}
