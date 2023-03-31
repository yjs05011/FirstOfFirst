using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Transform mPlayer;
    public Vector2 mMinCameraBoundary;
    public Vector2 mMaxCameraBoundary;
    
    private float smoothing = 0.2f;

    public void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(mPlayer.position.x, mPlayer.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, mMinCameraBoundary.x, mMaxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, mMinCameraBoundary.y, mMaxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
