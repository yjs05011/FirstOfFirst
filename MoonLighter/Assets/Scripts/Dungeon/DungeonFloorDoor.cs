using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFloorDoor : MonoBehaviour
{
    public DungeonDoor mDoor = null;


    public void StartFloorChange()
    {

        // �÷��̾ ���� ��쿡�� ������ �̵� �ڷ�ƾ ����. (�Ϲ������� �ݾƾ��ϴ� ��쵵 �ֱ⶧��)
        if (mDoor.IsPlayerEnterDoor())
        {
            StartCoroutine(mDoor.FloorChange());
        }

    }
}
