using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // 드랍 아이템 리스트
    public List<Item> mDungeonDropItemList = new List<Item>(); 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Init();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
#if UNITY_EDITOR
        // 빌리지 가기
        if(Input.GetKeyUp(KeyCode.F1))
        {
            LoadingManager.LoadScene("DungeonEntrance");
        }

        // 던전 가기
        if(Input.GetKeyUp(KeyCode.F2))
        {
            LoadingManager.LoadScene("Dungeon");
        }

        if(Input.GetKeyUp(KeyCode.F3))
        {
            if(DungeonGenerator.Instance.mLastRoom)
            {
                DungeonStage prevStage = DungeonGenerator.Instance.mLastRoom.GetPrevStage();

                GameObject player = GameObject.FindWithTag("Player");
                if (player)
                {
                    player.transform.position = prevStage.transform.position;
                }
            }
        }
#endif
    }

    private void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 던전씬이 로드된지 체크한다.
        // 던전의 경우 씬이 로드된 이후에 던전을 생성하기 때문에 현재의 LoadingManager 는 적합하게 사용 불가
        Debug.Log(scene.name);
        if(scene.name == "Dungeon")
        {
            DungeonGenerator.Instance.DungeonGenerate();
        }
    }


    public void Start()
    {
        
        mKillMonsterList.Clear();
        mUnlockChestList.Clear();
        mDungeonDropItemList.Clear();
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

    // 플레이어가 처치한 몬스터 카운트 반환 함수
    public int GetKillMonsterCount()
    {
         return mKillMonsterList.Count;
    }

    // 드랍 아이템 리스트에 아이템 추가 하는 함수.
    public void DungeonDropItemAdd(Item dropItem)
    {
        mDungeonDropItemList.Add(dropItem);
    }
    // 드랍 아이템 리스트에서 아이템 제거 하는 함수
    public void DungeonDropItemDelete(Item dropItem)
    {
        mDungeonDropItemList.Remove(dropItem);
    }

    // 드랍 아이템 리스트 초기화 
    public void ClearDungeonDropItemList()
    {
        mDungeonDropItemList.Clear();
    }
}
