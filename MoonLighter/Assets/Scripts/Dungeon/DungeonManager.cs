using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    // 현재 플레이어가 위치한 스테이지 
    public DungeonStage mPlayerCurrStage = null;
    // 현재 던전의 보스 스테이지 
    public DungeonStage mDungeonBossStage = null;
    // 던전 카메라 제어 스크립트
    public DungeonCameraController mCamera = null;

    // 던전 층 이동 로딩 씬 
    public DungeonUIFadeInOutTransition mTransitionUI = null;

    // 처치한 몬스터 리스트 
    public List<Monster.MonsterID> mKillMonsterList = new List<Monster.MonsterID>();

    // 언락한 상자 리스트
    public List<DungeonChest.ChestID> mUnlockChestList = new List<DungeonChest.ChestID>();

    

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

    public void Start()
    {
        mKillMonsterList.Clear();
        mUnlockChestList.Clear();
    }
 

    public void KillMonsterAdd(Monster.MonsterID monsterID)
    {
        mKillMonsterList.Add(monsterID);
    }

    public void UnlockChestAdd(DungeonChest.ChestID chestID)
    {
        mUnlockChestList.Add(chestID);
    }

    public DungeonUIFadeInOutTransition GetTransitionUI()
    {
        return mTransitionUI;
    }
 
    public DungeonCameraController GetDungeonCamera()
    {
        return mCamera;
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

    // 던전 보스 스테이지 설정 함수
    public void SetDungeonBossRoom(DungeonStage bossStage)
    {
        mDungeonBossStage = bossStage;
    }

    // 던전 보스 스테이지 반환 함수
    public DungeonStage GetDungeonBossRoom()
    {
        return mDungeonBossStage;
    }


    // 카메라 좌표 이동 
    public void CameraMoveByPos(Vector3 NextPos)
    {
        mCamera.CameraMoveByPos(NextPos);
    }


    public int GetKillMonsterCount()
    {
         return mKillMonsterList.Count;
    }

    // 던전 나가기 UI 연결 전 던전 탈출 테스트 함수 
    public void TestDungeonExitInit()
    {
       
        mKillMonsterList.Clear();
        mUnlockChestList.Clear();
        LoadingManager.LoadScene("DungeonEntrance");
        DungeonGenerator.Instance.OnDestroyMySelf();


    }


    // 던전 나가기 UI 연결 전 재진입  테스트 함수 
    public void TestDungeonEnterInit()
    {
        
        mKillMonsterList.Clear();
        mUnlockChestList.Clear();
        
    }
}
