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

    // 퇴장 ui 열린 방식
    // 플레이어가 죽어서 열렸을 때 이미지
    public GameObject mExitUiOpenMethodPlayerDie;
    // 팬던트를 사용해서 열렸을 때 이미지
    public GameObject mExitUiOpenMethodUsePendant;

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
        ExitUiOpenMethodCheck();
        SetKillMonsterCount();
        SetDungeonChestCount();
        FloorCheck();
    }

    // Update is called once per frame
    void Update()
    {
        UiInputKeyControl();


    }

    // 퇴장 ui Open 방식 체크하는 함수
    public void ExitUiOpenMethodCheck()
    {
        if (PlayerManager.Instance.mPlayerStat.isDie == true)
        {
            // 방식에 따라서 이미지 교체
            mExitUiOpenMethodPlayerDie.SetActive(true);
            mExitUiOpenMethodUsePendant.SetActive(false);
            // 처치한 몬스터 수 
            PlayerKillMonsterCheck();
        }
        else
        {
            // 방식에 따라서 이미지 교체
            mExitUiOpenMethodPlayerDie.SetActive(false);
            mExitUiOpenMethodUsePendant.SetActive(true);
        }


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

            LoadingManager.LoadScene("Dungeon");

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
