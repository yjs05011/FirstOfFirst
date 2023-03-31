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
        mTalkData.Add(100, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t던전 ▲\n" });      // 던전 ▲ 표지판 
        mTalkData.Add(200, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t던전 ▶\n" });      // 던전 ▶ 표지판
        mTalkData.Add(300, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t벌컨의 대장간\n" });       // 대장간 표지판
        mTalkData.Add(400, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t나무 모자\n" });     // 나무 모자 표지판
        mTalkData.Add(500, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t문라이트\n" });      // 문라이트 표지판
        mTalkData.Add(600, new string[] { "\n\t\t\t들어가기\n" });        // 문라이트
        mTalkData.Add(700, new string[] { "\n\t\t\t읽기\n" });            // 게시판
        mTalkData.Add(1000, new string[] { "\n\t\t\t말하기\n", "\n\t\t\t힘차고 강한 아침!!\n" });     // NPC 
        mTalkData.Add(2000, new string[] { "\n\t\t\t말하기\n", "\n\t\t\t대장간의 불꽃은 네가 원하는 건\n\t\t\t뭐든지 제작할 준비가 되어 있지!\n" });        //대장간
        mTalkData.Add(3000, new string[] { "\n\t\t\t말하기\n", "\n\t\t\t여기선 신선한 물약을 제조하거나\n\t\t\t신기한 마법을 부여할 수 있어\n" });        //마녀의 집
        mTalkData.Add(110, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t골렘 던전\n", "\n\t\t\t상인과 영웅 둘 다 입장할 수 있는\n\t\t\t던전\n" });                      //골렘 던전
        mTalkData.Add(210, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t숲 던전\n", "\n\t\t\t영웅만 입장할 수 있는 던전.\n\t\t\t상인에게는 추천하지 않음\n" });       // 숲 던전
        mTalkData.Add(310, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t사막 던전\n", "\n\t\t\t영웅 전용" });                                                // 사막 던전
        mTalkData.Add(410, new string[] { "\n\t\t\t읽기\n", "\n\t\t\t기술 던전\n", "\n\t\t\t입장 불가" });                                                // 기술 던전
        mTalkData.Add(120, new string[] { "\n\t\t\t배치\n" });                                                                          // 상점 테이블(Lv1,Lv2)
        mTalkData.Add(130, new string[] { "\n\t\t\t배치\n" });                                                                          // 상점 테이블(Lv2)
        mTalkData.Add(220, new string[] { "\n\t\t\t길게 눌러 잠자기\n" });                                                              // 상점 침대Lv1
        mTalkData.Add(320, new string[] { "\n\t\t\t열기\n" });                                                                          // 상점 침대 상자
        mTalkData.Add(420, new string[] { "\n\t\t\t판매\n" });                                                                          // 계산대
        mTalkData.Add(900, new string[] { "\n\t\t\t열기\n" });                                                                          // 상점 창고 Lv1,Lv2
        mTalkData.Add(910, new string[] { "\n\t\t\t열기\n" });                                                                          // 상점 창고 Lv2                                                                      
        
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
