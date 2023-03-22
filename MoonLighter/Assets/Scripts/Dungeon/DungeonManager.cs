using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    // 현재 플레이어가 위치한 스테이지 
    public DungeonStage mPlayerCurrStage = null;
    // 현재 진행중인 던전 층 
   
    public DungeonCameraController mCamera = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // 플레이어 스테이지 이동시 설정하는 함수
    public void SetPlayerCurrStage(DungeonStage stage)
    {   
        mPlayerCurrStage = stage; 
    }

    // 플레이어가 있는 스테이지를 반환하는 함수
    public DungeonStage GetPlayerCurrStage()
    {
        return mPlayerCurrStage;
    }

   
    public void CameraMoveByPos(Vector3 NextPos)
    {
        mCamera.CameraMoveByPos(NextPos);
    }

}
