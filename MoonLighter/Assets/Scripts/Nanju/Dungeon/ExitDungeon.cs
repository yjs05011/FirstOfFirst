using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDungeon : MonoBehaviour
{
    // ���� �� 
    public GameObject mFirstFloor;
    public GameObject mFirstFloorClear;
    public GameObject mScendFloor;
    public GameObject mScendFloorClear;
    public GameObject mThreeFloor;
    public GameObject mThreeFloorClear;

    // test
    public GameObject monster;
    public Transform Parent;
    public GridLayoutGroup gridGroup;

    // � ���� �������
    public Text mChestCount;

    // � ���� ��Ҵ���
    // public GameObject[] mMonsterList = new GameObject[11];
    public Text mKillMonsterCount;

    // ���� ��������Ʈ �迭
    public Sprite[] mPlayerKillMonsterSprites;
    // �Ҵ�Ʈ ��������Ʈ
    public Sprite mPendantSprite;
    // Ż�� ��� ǥ�� �̹��� (�������� �׾��ų�, �Ҵ�Ʈ ����߰ų�)
    public SpriteRenderer mPlayerKillMonster;
    public int killMonster;


    // Start is called before the first frame update
    void Awake()
    {
        mPlayerKillMonster = GetComponent<SpriteRenderer>();
    }




    // Start is called before the first frame update
    void Start()
    {
        // test
        //ExitDungeonKillMosterIamge();

        SetKillMonsterCount();
        SetDungeonChestCount();
        FloorCheck();
        // ���� ���� üũ �Լ�
        CheckExitReason();

    }

    // Update is called once per frame
    void Update()
    {
        UiInputKeyControl();

    }


    public void UiInputKeyControl()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerManager.Instance.mPlayerStat.isDie = false;
            UiManager.Instance.mIsPlayerUseAnimation = false;
            PlayerManager.Instance.mIsUiActive = false;
            UiManager.Instance.mIsPlayerFinishAnimation = false;
            UiManager.Instance.mIsDungeonCheck = false; 
            this.gameObject.SetActive(false);

            LoadingManager.LoadScene("VillageScene");

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerManager.Instance.mPlayerStat.isDie = false;
            UiManager.Instance.mIsPlayerUseAnimation = false;
            PlayerManager.Instance.mIsUiActive = false;
            this.gameObject.SetActive(false);

            // �׾��� ��  �ٽ��÷��� ��ư ������ ó�� ���� ������ �Ȱ��� ExitDungeon ui ����ä�� ��
            // -> �ε��� �״ٰ� ������  �ٽ� �ҷ������ϱ� ��Ͽ� �����غ��� 
            // �ӽ÷� Ż���Ѱ� ����� ���� Ȯ���ϱ�
            LoadingManager.LoadScene("Dungeon");

        }
    }

    public void CheckExitReason()
    {
        if (PlayerManager.Instance.mPlayerStat.isDie == true)
        {
            PlayerKillMonsterCheck();
        }
        else
        {
            PendantUseExit();
        }

    }


    // [����] �� ������ Ȯ���Ͽ� ������Ʈ Ȱ��ȭ
    public void FloorCheck()
    {
        // ���� �� ��ġ ǥ��
        // �Ϸ�� ���� ���� �������� ������ �ϱ�
        switch (DungeonManager.Instance.mPlayerCurrStage.GetFloor())
        {
            case 1:
                mFirstFloor.SetActive(true);
                break;
            case 2:
                mFirstFloor.SetActive(false);
                mFirstFloorClear.SetActive(true);
                mScendFloor.SetActive(true);
                break;
            case 3:
                if (DungeonManager.Instance.mPlayerCurrStage.GetBoardType() == DungeonBoard.BoardType.DungeonBoss)
                {
                    mFirstFloor.SetActive(false);
                    mFirstFloorClear.SetActive(true);
                    mScendFloor.SetActive(false);
                    mScendFloorClear.SetActive(true);
                    mThreeFloor.SetActive(false);
                    mThreeFloorClear.SetActive(true);


                }
                else
                {
                    mFirstFloor.SetActive(false);
                    mFirstFloorClear.SetActive(true);
                    mScendFloor.SetActive(false);
                    mScendFloorClear.SetActive(true);
                    mThreeFloor.SetActive(true);

                }
                break;

        }
        Debug.Log($"�����̴� : {DungeonManager.Instance.mPlayerCurrStage.GetFloor()}");

    }

    // Ÿ��Ʋ �ؿ� ���׶�� UI --------------------------
    // �÷��̾ ���� ���Ͱ� �������� Ȯ��(ExitObj)
    public void PlayerKillMonsterCheck()
    {
        // �÷��̾ ���� ���͸� üũ�ؼ� ǥ������.

        //if(PlayerManager.Instance.mPlayerWasKilled != 10)
        //{
        //    mPlayerKillMonster.sprite = mPlayerKillMonsterSprites[PlayerManager.Instance.mPlayerWasKilled - 1];
        //}

    }

    public void PendantUseExit()
    {
        // �Ҵ�Ʈ ������� Ż�� UI ������ ���� ǥ������.
        //mPlayerKillMonster.sprite = mPendantSprite;
    }

    // [����] � ���ڸ� �������� Ȯ��
    public void SetDungeonChestCount()
    {
        int chestCount = DungeonManager.Instance.mUnlockChestList.Count;
        mChestCount.text = chestCount.ToString();
    }

    // [����] � ���� ��Ҵ��� Ȯ��
    public void SetKillMonsterCount()
    {
        killMonster = DungeonManager.Instance.mKillMonsterList.Count;
        mKillMonsterCount.text = killMonster.ToString();
        //// ���� �̹��� enum Ÿ�� ��϶� �����ϱ� (���������� �̿�)
        //List<int> killMonsterlist = new List<int>();
        //foreach (var sd in DungeonManager.Instance.mKillMonsterList)
        //{
        //    killMonsterlist.Add((int)sd);
        //}
        //Vector3 FirstPosition = new Vector3(-190, 40, 0);
        //float gap = 380 / (killMonster / 2);
        //for (int i = 0; i < killMonster; i++)
        //{
        //    GameObject MonsterObj = null;
        //    switch (killMonsterlist[i])
        //    {
        //        case 1:
        //            MonsterObj = mMonsterList[1];
        //            break;
        //        case 2:
        //            MonsterObj = mMonsterList[2];
        //            break;
        //        case 3:
        //            MonsterObj = mMonsterList[3];
        //            break;
        //        case 4:
        //            MonsterObj = mMonsterList[4];
        //            break;
        //        case 5:
        //            MonsterObj = mMonsterList[5];
        //            break;
        //        case 6:
        //            MonsterObj = mMonsterList[6];
        //            break;
        //        case 10:
        //            MonsterObj = mMonsterList[10];
        //            break;
        //    }
        //    Instantiate(MonsterObj);
        //    MonsterObj.transform.SetParent(transform, false);
        //    MonsterObj.transform.localPosition = FirstPosition + new Vector3(gap * i, 0, 0);
        //    if (i > (killMonster / 2))
        //    {
        //        FirstPosition += new Vector3(0, -80, 0);
        //    }
        //}
    }

    //public void ExitDungeonKillMosterIamge()
    //{
    //    int MonsterKillCount = (int)(40 * 0.5f);
    //    gridGroup = transform.GetChild(6).GetComponent<GridLayoutGroup>();
    //    gridGroup.cellSize = new Vector2((float)450 / MonsterKillCount, 70);

    //    for (int i = 0; i < MonsterKillCount * 2; i++)
    //    {

    //        Instantiate(monster, Parent);

    //    }


    //    Debug.Log($"킬 몬스터 개수 : {MonsterKillCount}");
    //    // 절반 나눠서 해야되니까 오브젝트 이미지를 만들어야된다.
    //}
}
