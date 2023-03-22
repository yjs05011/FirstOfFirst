using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraController : MonoBehaviour
{
    // ī�޶� �i�ư� ���(�÷��̾�)�� transform
    [SerializeField]
    public Transform mTarget;
    [SerializeField] 
    // ī�޶� �̵� 
    private float mSmooth = 0.02f;
    // ī�޶� �ʱ� ��ġ
    private Vector3 mInitCameraPos = new Vector3(0, 0, -10);
    // ī�޶� ��� ����
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
