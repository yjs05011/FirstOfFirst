using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonScrollControl : MonoBehaviour
{
    public GameObject mDungeonScroll;
    public bool mIsDungeonCheck = false;
    public bool mIsBossRoomCheck = false;
    public Text mScrollText;
    public Text mLevelText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DungeonCheck();
        LevelCheck();
    }

    // 던전인지 체크하여 DungeonScroll 활성화, 비활성화 하기
    public void DungeonCheck()
    {
        if (mIsDungeonCheck == true)
        {
            mDungeonScroll.SetActive(true);
        }
        else
        {
            mDungeonScroll.SetActive(false);
        }
    }

    // 던전 보스방이면 DungeonScroll 활성화
    public void DungeonBoss()
    {
        if (mIsBossRoomCheck == true)
        {
            mDungeonScroll.SetActive(true);
        }
        else
        {
            mDungeonScroll.SetActive(false);
        }
    }

    // 던전 레벨을 체크하여 Text 변경
    public void LevelCheck()
    {

    }


}
