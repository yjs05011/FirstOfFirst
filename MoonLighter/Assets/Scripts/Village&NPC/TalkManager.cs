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
        mTalkData.Add(100, new string[] { "�б�", "���� ��" });      // ���� �� ǥ���� 
        mTalkData.Add(200, new string[] { "�б�", "���� ��" });      // ���� �� ǥ����
        mTalkData.Add(300, new string[] { "�б�", "������ ���尣" });       // ���尣 ǥ����
        mTalkData.Add(400, new string[] { "�б�", "���� ����" });     // ���� ���� ǥ����
        mTalkData.Add(500, new string[] { "�б�", "������Ʈ" });      // ������Ʈ ǥ����
        mTalkData.Add(600, new string[] { "����" });        // ������Ʈ
        mTalkData.Add(700, new string[] { "�б�" });            // �Խ���
        mTalkData.Add(1000, new string[] { "���ϱ�", "������ ���� ��ħ!!" });     // NPC 
        mTalkData.Add(2000, new string[] { "���ϱ�" , "���尣�� �Ҳ��� �װ� ���ϴ� ��\n������ ������ �غ� �Ǿ� ����!" });        //���尣
        mTalkData.Add(3000, new string[] { "���ϱ�" , "���⼱ �ż��� ������ �����ϰų�\n�ű��� ������ �ο��� �� �־�" });        //������ ��
        mTalkData.Add(110, new string[] { "�б�", "�� ����", "���ΰ� ���� �� �� ������ �� �ִ�\n����" });                      //�� ����
        mTalkData.Add(210, new string[] { "�б�", "�� ����", "������ ������ �� �ִ� ����.\n���ο��Դ� ��õ���� ����" });       // �� ����
        mTalkData.Add(310, new string[] { "�б�", "�縷 ����", "���� ����" });                                                // �縷 ����
        mTalkData.Add(410, new string[] { "�б�", "��� ����", "���� �Ұ�" });                                                // ��� ����
        mTalkData.Add(120, new string[] { "��ġ" });                                                                          // ���� ���̺�
        mTalkData.Add(220, new string[] { "��� ���� ���ڱ�" });                                                              // ���� ħ��Lv1
        mTalkData.Add(320, new string[] { "����" });                                                                          // ���� ħ�� ����
        mTalkData.Add(420, new string[] { "�Ǹ�" });                                                                          // ����
        
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
