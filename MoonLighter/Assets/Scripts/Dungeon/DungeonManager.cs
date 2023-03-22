using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    // ���� �÷��̾ ��ġ�� �������� 
    public DungeonStage mPlayerCurrStage = null;
    // ���� �������� ���� �� 
   
    public DungeonCameraController mCamera = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // �÷��̾� �������� �̵��� �����ϴ� �Լ�
    public void SetPlayerCurrStage(DungeonStage stage)
    {   
        mPlayerCurrStage = stage; 
    }

    // �÷��̾ �ִ� ���������� ��ȯ�ϴ� �Լ�
    public DungeonStage GetPlayerCurrStage()
    {
        return mPlayerCurrStage;
    }

   
    public void CameraMoveByPos(Vector3 NextPos)
    {
        mCamera.CameraMoveByPos(NextPos);
    }

}
