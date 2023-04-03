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

    // 플레이어가 죽인 몬스터 뜨게 하기
    public GameObject mPlayerKillMonsterImage;
    public Transform mPlayerKillMonsterImageBoard;
    private GridLayoutGroup gridGroup;

    // 플레이어를 죽인 몬스터 뜨게 하기
    public Image mPlayerWasKillMonster;
    // 몬스터에게 죽을 시 텍스트 뜨게 하기
    public GameObject mPlayerDieText;
    // 팬던트 사용시 이미지 뜨게 하기
    public GameObject mUsePendant;
    // 팬던트 사용시 텍스트 뜨게 하기
    public GameObject mPendantUseText;



    // 상자 이미지 뜨게 하기
    public Sprite[] mChestSprites;
    public GameObject mChest;
    public Transform mChestBoard;

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

    public int killMonster;


    // Start is called before the first frame update
    void Start()
    {
        // test

        ExitUiOpenMethodCheck();
        SetKillMonsterCount();
        SetDungeonChestCount();
        FloorCheck();
        ExitDungeonKillMosterIamge();
        ExitDungeonChestIamge();
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
            PlayerWasKillMonsterCheck();
            mPlayerWasKillMonster.gameObject.SetActive(true);
            mPlayerDieText.SetActive(true);
        }
        else
        {
            // 방식에 따라서 이미지 교체
            mExitUiOpenMethodPlayerDie.SetActive(false);
            mExitUiOpenMethodUsePendant.SetActive(true);
            mUsePendant.SetActive(true);
            mPendantUseText.SetActive(true);
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
            UiManager.Instance.mIsSceneChaged = true;
            this.gameObject.SetActive(false);

            LoadingManager.LoadScene("VillageScene");

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerManager.Instance.mPlayerStat.isDie = false;
            UiManager.Instance.mIsPlayerUseAnimation = false;
            PlayerManager.Instance.mIsUiActive = false;
            UiManager.Instance.mIsPlayerFinishAnimation = false;
            UiManager.Instance.mIsSceneChaged = true;
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
        Debug.Log($" 몇 층 : {DungeonManager.Instance.mPlayerCurrStage.GetFloor()}");

    }


    // 플레이어를 죽인 몬스터 뜨게 하기
    public void PlayerWasKillMonsterCheck()
    {
        mPlayerWasKillMonster.sprite = mPlayerKillMonsterSprites[PlayerManager.Instance.mPlayerWasKilled];
        Debug.Log($"팬던트를 사용 했나요? : {UiManager.Instance.mIsPlayerUseAnimation}");

    }


    // [����] � ���ڸ� �������� Ȯ��
    public void SetDungeonChestCount()
    {
        int chestCount = DungeonManager.Instance.mUnlockChestList.Count;
        mChestCount.text = chestCount.ToString();
    }

    // 얻은 상자 이미지 나오게 하기
    public void ExitDungeonChestIamge()
    {
        int chestcount = (int)(DungeonManager.Instance.mUnlockChestList.Count);

        for (int i = 0; i < chestcount; i++)
        {
            GameObject tempObj = Instantiate(mChest, mChestBoard);
            tempObj.transform.GetChild(0).GetComponent<Image>().sprite = mChestSprites[(int)DungeonManager.Instance.mUnlockChestList[i]];
            tempObj.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = mChestSprites[(int)DungeonManager.Instance.mUnlockChestList[i]].textureRect.size;
        }
        // Debug.Log($"상자 몇개? {DungeonManager.Instance.mUnlockChestList.Count}");
    }

    // [����] � ���� ��Ҵ��� Ȯ��
    public void SetKillMonsterCount()
    {
        killMonster = DungeonManager.Instance.mKillMonsterList.Count;
        mKillMonsterCount.text = killMonster.ToString();


        // Debug.Log($"죽은 몬스터 수 : {DungeonManager.Instance.mKillMonsterList.Count}");
    }

    // 플레이어가 죽인 몬스터 뜨게 하기
    public void ExitDungeonKillMosterIamge()
    {
        float monsterKillCount = (float)(DungeonManager.Instance.mKillMonsterList.Count * 0.5f);
        gridGroup = transform.GetChild(6).GetComponent<GridLayoutGroup>();
        gridGroup.cellSize = new Vector2((float)460 / monsterKillCount, 70);

        for (int i = 0; i < monsterKillCount * 2; i++)
        {
            GameObject tempObj = Instantiate(mPlayerKillMonsterImage, mPlayerKillMonsterImageBoard);
            tempObj.transform.GetChild(0).GetComponent<Image>().sprite = mPlayerKillMonsterSprites[(int)DungeonManager.Instance.mKillMonsterList[i]];
            tempObj.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = mPlayerKillMonsterSprites[(int)DungeonManager.Instance.mKillMonsterList[i]].textureRect.size;
        }


        Debug.Log($"킬 몬스터 개수 : {monsterKillCount}");
        // 절반 나눠서 해야되니까 오브젝트 이미지를 만들어야된다.
    }
}
