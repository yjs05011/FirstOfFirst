using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDungeon : MonoBehaviour
{
    // 던전 층 
    public GameObject mFirstFloor;
    public GameObject mFirstFloorClear;
    public GameObject mScendFloor;
    public GameObject mScendFloorClear;
    public GameObject mThreeFloor;
    public GameObject mThreeFloorClear;

    // 몇개 상자 얻었는지
    public Text mChestCount;

    // 몇개 몬스터 잡았는지
    // public GameObject[] mMonsterList = new GameObject[11];
    public Text mKillMonsterCount;

    // 몬스터 스프라이트 배열
    public Sprite[] mPlayerKillMonsterSprites;
    // 팬던트 스프라이트
    public Sprite mPendantSprite;
    // 탈출 방식 표시 이미지 (몬스터한테 죽었거나, 팬던트 사용했거나)
    public SpriteRenderer mPlayerKillMonster;


    // Start is called before the first frame update
    void Awake()
    {
        mPlayerKillMonster = GetComponent<SpriteRenderer>();
    }

  
    

    // Start is called before the first frame update
    void Start()
    {
        SetKillMonsterCount();
        SetDungeonChestCount();
        FloorCheck();
        // 퇴장 이유 체크 함수
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

            gameObject.SetActive(false);

            LoadingManager.LoadScene("VillageScene");

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerManager.Instance.mPlayerStat.isDie = false;

            gameObject.SetActive(false);

            // 죽었을 때  다시플레이 버튼 누르면 처음 던전 씬으로 안가고 ExitDungeon ui 켜진채로 감
            // -> 로딩씬 켰다가 던전씬  다시 불러오게하기 언니와 상의해보기 
            // 임시로 탈출한거 만들어 논거 확인하기
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


    // [던전] 몇 층인지 확인하여 오브젝트 활성화
    public void FloorCheck()
    {
        // 현재 층 위치 표시
        // 완료시 지금 현재 층까지만 켜지게 하기
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
        Debug.Log($"몇층이니 : {DungeonManager.Instance.mPlayerCurrStage.GetFloor()}");

    }

    // 타이틀 밑에 동그라미 UI --------------------------
    // 플레이어를 죽인 몬스터가 무었인지 확인(ExitObj)
    public void PlayerKillMonsterCheck()
    {
        // 플레이어를 죽인 몬스터를 체크해서 표시해줌.

        //if(PlayerManager.Instance.mPlayerWasKilled != 10)
        //{
        //    mPlayerKillMonster.sprite = mPlayerKillMonsterSprites[PlayerManager.Instance.mPlayerWasKilled - 1];
        //}

    }

    public void PendantUseExit()
    {
        // 팬던트 사용으로 탈출 UI 오픈한 것을 표시해줌.
        mPlayerKillMonster.sprite = mPendantSprite;
    }

    // [던전] 몇개 상자를 열었는지 확인
    public void SetDungeonChestCount()
    {
        int chestCount = DungeonManager.Instance.mUnlockChestList.Count;
        mChestCount.text = chestCount.ToString();
    }

    // [던전] 몇개 몬스터 잡았는지 확인
    public void SetKillMonsterCount()
    {
        int killMonster = DungeonManager.Instance.mKillMonsterList.Count;
        mKillMonsterCount.text = killMonster.ToString();

        //// 몬스터 이미지 enum 타입 언니랑 상의하기 (프리팹으로 이용)
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
}
