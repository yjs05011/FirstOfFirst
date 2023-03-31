using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    // ���� �÷��̾ ��ġ�� �������� 
    public DungeonStage mPlayerCurrStage = null;
    // ���� ������ ���� �������� 
    public DungeonStage mDungeonBossStage = null;
    // ���� ī�޶� ���� ��ũ��Ʈ
    public DungeonCameraController mCamera = null;

    // ���� �� �̵� �ε� �� 
    public DungeonUIFadeInOutTransition mTransitionUI = null;

    // óġ�� ���� ����Ʈ 
    public List<Monster.MonsterID> mKillMonsterList = new List<Monster.MonsterID>();

    // ����� ���� ����Ʈ
    public List<DungeonChest.ChestID> mUnlockChestList = new List<DungeonChest.ChestID>();

    // ��� ������ ����Ʈ
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
        // ������ ����
        if(Input.GetKeyUp(KeyCode.F1))
        {
            LoadingManager.LoadScene("DungeonEntrance");
        }

        // ���� ����
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
        // �������� �ε���� üũ�Ѵ�.
        // ������ ��� ���� �ε�� ���Ŀ� ������ �����ϱ� ������ ������ LoadingManager �� �����ϰ� ��� �Ұ�
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

    // �÷��̾� �������� �̵��� �����ϴ� �Լ�
    public void SetPlayerCurrStage(DungeonStage stage)
    {   
        mPlayerCurrStage = stage; 
    }

    // �÷��̾ �ִ� ���������� ��ȯ�ϴ� �Լ�
    public DungeonStage GetPlayerCurrStage()
    {
        return mPlayerCurrStage;
    }

    // ���� ���� �������� ���� �Լ�
    public void SetDungeonBossRoom(DungeonStage bossStage)
    {
        mDungeonBossStage = bossStage;
    }

    // ���� ���� �������� ��ȯ �Լ�
    public DungeonStage GetDungeonBossRoom()
    {
        return mDungeonBossStage;
    }


    // ī�޶� ��ǥ �̵� 
    public void CameraMoveByPos(Vector3 NextPos)
    {
        mCamera.CameraMoveByPos(NextPos);
    }

    // �÷��̾ óġ�� ���� ī��Ʈ ��ȯ �Լ�
    public int GetKillMonsterCount()
    {
         return mKillMonsterList.Count;
    }

    // ��� ������ ����Ʈ�� ������ �߰� �ϴ� �Լ�.
    public void DungeonDropItemAdd(Item dropItem)
    {
        mDungeonDropItemList.Add(dropItem);
    }
    // ��� ������ ����Ʈ���� ������ ���� �ϴ� �Լ�
    public void DungeonDropItemDelete(Item dropItem)
    {
        mDungeonDropItemList.Remove(dropItem);
    }

    // ��� ������ ����Ʈ �ʱ�ȭ 
    public void ClearDungeonDropItemList()
    {
        mDungeonDropItemList.Clear();
    }
}
