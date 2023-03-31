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
        mTalkData.Add(100, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t���� ��\n" });      // ���� �� ǥ���� 
        mTalkData.Add(200, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t���� ��\n" });      // ���� �� ǥ����
        mTalkData.Add(300, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t������ ���尣\n" });       // ���尣 ǥ����
        mTalkData.Add(400, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t���� ����\n" });     // ���� ���� ǥ����
        mTalkData.Add(500, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t������Ʈ\n" });      // ������Ʈ ǥ����
        mTalkData.Add(600, new string[] { "\n\t\t\t����\n" });        // ������Ʈ
        mTalkData.Add(700, new string[] { "\n\t\t\t�б�\n" });            // �Խ���
        mTalkData.Add(1000, new string[] { "\n\t\t\t���ϱ�\n", "\n\t\t\t������ ���� ��ħ!!\n" });     // NPC 
        mTalkData.Add(2000, new string[] { "\n\t\t\t���ϱ�\n", "\n\t\t\t���尣�� �Ҳ��� �װ� ���ϴ� ��\n\t\t\t������ ������ �غ� �Ǿ� ����!\n" });        //���尣
        mTalkData.Add(3000, new string[] { "\n\t\t\t���ϱ�\n", "\n\t\t\t���⼱ �ż��� ������ �����ϰų�\n\t\t\t�ű��� ������ �ο��� �� �־�\n" });        //������ ��
        mTalkData.Add(110, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t�� ����\n", "\n\t\t\t���ΰ� ���� �� �� ������ �� �ִ�\n\t\t\t����\n" });                      //�� ����
        mTalkData.Add(210, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t�� ����\n", "\n\t\t\t������ ������ �� �ִ� ����.\n\t\t\t���ο��Դ� ��õ���� ����\n" });       // �� ����
        mTalkData.Add(310, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t�縷 ����\n", "\n\t\t\t���� ����" });                                                // �縷 ����
        mTalkData.Add(410, new string[] { "\n\t\t\t�б�\n", "\n\t\t\t��� ����\n", "\n\t\t\t���� �Ұ�" });                                                // ��� ����
        mTalkData.Add(120, new string[] { "\n\t\t\t��ġ\n" });                                                                          // ���� ���̺�(Lv1,Lv2)
        mTalkData.Add(130, new string[] { "\n\t\t\t��ġ\n" });                                                                          // ���� ���̺�(Lv2)
        mTalkData.Add(220, new string[] { "\n\t\t\t��� ���� ���ڱ�\n" });                                                              // ���� ħ��Lv1
        mTalkData.Add(320, new string[] { "\n\t\t\t����\n" });                                                                          // ���� ħ�� ����
        mTalkData.Add(420, new string[] { "\n\t\t\t�Ǹ�\n" });                                                                          // ����
        mTalkData.Add(900, new string[] { "\n\t\t\t����\n" });                                                                          // ���� â�� Lv1,Lv2
        mTalkData.Add(910, new string[] { "\n\t\t\t����\n" });                                                                          // ���� â�� Lv2                                                                      
        
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
