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
   
    public Vector2 mCameraBoundaryMin;

    public Vector2 mCameraBoundaryMax;

    private float mBoundaryAddValueX = 10.8f;
    private float mBoundaryAddValueY = 8.8f;

    private bool mIsMove = false;
    public Vector3 mMovePos = Vector3.zero;

    public enum CameraMoveType { Default, Immediately , Follow};
    public CameraMoveType mType = CameraMoveType.Default;

    public bool mIsBossRoom = false;


    public void Awake()
    {
        this.transform.position = mInitCameraPos;
        SetCameraType(CameraMoveType.Default);
        SetIsBossRoom(false);
    }

    public void Update()
    {
        if (mIsMove)
        {
            if (mType == CameraMoveType.Default)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, mMovePos, mSmooth);

                if (this.transform.position == mMovePos)
                {
                    mIsMove = false;
                }
            }
            if (mType == CameraMoveType.Immediately)
            {
                this.transform.position = mMovePos;

                if (this.transform.position == mMovePos)
                {
                    if (mIsBossRoom)
                    {
                        Debug.Log("ī�޶� ��� �̵� �Ϸ�, ���� Ÿ�� fllow ���� ����");
                        SetCameraType(CameraMoveType.Follow);
                    }
                    else
                    {
                        Debug.Log("ī�޶� ��� �̵� �Ϸ�, ���� Ÿ�� Default ���� ����");
                        SetCameraType(CameraMoveType.Default);
                    }
                }
            }
            if(mType == CameraMoveType.Follow)
            {
                Vector3 targetPos = new Vector3(mTarget.position.x, mTarget.position.y, -10);

                targetPos.x = Mathf.Clamp(targetPos.x, mCameraBoundaryMin.x, mCameraBoundaryMax.x);
                targetPos.y = Mathf.Clamp(targetPos.y, mCameraBoundaryMin.y, mCameraBoundaryMax.y);

                this.transform.position = Vector3.Lerp(this.transform.position, targetPos, mSmooth);
            }
        }
    }


    public void SetCameraType(CameraMoveType type)
    {
        mType = type;
        if(type == CameraMoveType.Follow)
        {
            mIsMove= true;
        }
    }

    public void CameraMoveByPos(Vector3 NextPos)
    {

        Vector3 currPos = this.transform.position;
        Vector3 MovePos = new Vector3(NextPos.x, NextPos.y, currPos.z);
        
        if(mType == CameraMoveType.Immediately)
        {
            if (mIsBossRoom)
            {
                DungeonStage bossStage = DungeonManager.Instance.GetDungeonBossRoom();
                SetCameraBoundary(bossStage);
            }
        }
        mMovePos = MovePos;
        mIsMove = true;
    }

    public void SetCameraBoundary(DungeonStage bossStage)
    {
        mCameraBoundaryMin.x = bossStage.transform.position.x - mBoundaryAddValueX;
        mCameraBoundaryMin.y = bossStage.transform.position.y - mBoundaryAddValueY;
        mCameraBoundaryMax.x = bossStage.transform.position.x + mBoundaryAddValueX;
        mCameraBoundaryMax.y = bossStage.transform.position.y + mBoundaryAddValueY;
    }
 
    public void SetIsBossRoom(bool value)
    {
        mIsBossRoom = value;
    }

}
