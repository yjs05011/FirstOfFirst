using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCameraController : MonoBehaviour
{
    public Transform mPlayer;
    public float mSwitchPosition;
    public Vector3 mShopPosition;
    public Vector3 mRoomPOsition;

    private Vector3 mPosition;
    private float smoothing = 0.002f;
    // Update is called once per frame
    void Update()
    {
        if(mPlayer.position.y > mSwitchPosition)
        {
            mPosition = mShopPosition;
        }
        else
        {
            mPosition = mRoomPOsition;
        }
        transform.position = Vector3.Lerp(transform.position, mPosition, smoothing);
    }
}
