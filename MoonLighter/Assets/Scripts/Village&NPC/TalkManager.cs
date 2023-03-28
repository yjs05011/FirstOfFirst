using System.Collections.Generic;
using UnityEngine;

public class TalkManager : GSingleton<TalkManager>
{
    Dictionary<int, string[]> mTalkData;

    protected override void Init()
    {
        base.Init();
        mTalkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        mTalkData.Add(100, new string[] { "읽기", "던전 ▲" });      // 던전 ▲ 표지판 
        mTalkData.Add(200, new string[] { "읽기", "던전 ▶" });      // 던전 ▶ 표지판
        mTalkData.Add(300, new string[] { "읽기", "벌컨의 대장간" });       // 대장간 표지판
        mTalkData.Add(400, new string[] { "읽기", "나무 모자" });     // 나무 모자 표지판
        mTalkData.Add(500, new string[] { "읽기", "문라이트" });      // 문라이트 표지판
        mTalkData.Add(600, new string[] { "들어가기" });        // 문라이트
        mTalkData.Add(700, new string[] { "읽기" });            // 게시판
        mTalkData.Add(1000, new string[] { "말하기", "힘차고 강한 아침!!" });     // NPC 
        mTalkData.Add(2000, new string[] { "말하기" , "대장간의 불꽃은 네가 원하는 건\n뭐든지 제작할 준비가 되어 있지!" });        //대장간
        mTalkData.Add(3000, new string[] { "말하기" , "여기선 신선한 물약을 제조하거나\n신기한 마법을 부여할 수 있어" });        //마녀의 집
        mTalkData.Add(110, new string[] { "읽기", "골렘 던전", "상인과 영웅 둘 다 입장할 수 있는\n던전" });                      //골렘 던전
        mTalkData.Add(210, new string[] { "읽기", "숲 던전", "영웅만 입장할 수 있는 던전.\n상인에게는 추천하지 않음" });       // 숲 던전
        mTalkData.Add(310, new string[] { "읽기", "사막 던전", "영웅 전용" });                                                // 사막 던전
        mTalkData.Add(410, new string[] { "읽기", "기술 던전", "입장 불가" });                                                // 기술 던전
        mTalkData.Add(120, new string[] { "배치" });                                                                          // 상점 테이블
        mTalkData.Add(220, new string[] { "길게 눌러 잠자기" });                                                              // 상점 침대Lv1
        mTalkData.Add(320, new string[] { "열기" });                                                                          // 상점 침대 상자
        mTalkData.Add(420, new string[] { "판매" });                                                                          // 계산대
        
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == mTalkData[id].Length)
        {
            return null;
        }
        else
        {
            return mTalkData[id][talkIndex];
        }
    }
}
