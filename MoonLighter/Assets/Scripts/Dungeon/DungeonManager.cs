using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<DungeonChest> mUnlockChestList = new List<DungeonChest>();

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

    public void UnlockChestAdd(DungeonChest chest)
    {
        mUnlockChestList.Add(chest);
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


    public int GetKillMonsterCount()
    {
         return mKillMonsterList.Count;
    }

}
