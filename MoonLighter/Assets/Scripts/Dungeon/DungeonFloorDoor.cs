using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFloorDoor : MonoBehaviour
{
    public DungeonDoor mDoor = null;


    public void StartFloorChange()
    {

        // 플레이어가 들어온 경우에만 다음층 이동 코루틴 시작. (일반적으로 닫아야하는 경우도 있기때문)
        if (mDoor.IsPlayerEnterDoor())
        {
            StartCoroutine(mDoor.FloorChange());
        }

    }
}
