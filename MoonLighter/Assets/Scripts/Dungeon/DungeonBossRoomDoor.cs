using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBossRoomDoor : MonoBehaviour
{
    public DungeonDoor mDoor = null;


    public void FinishBossRoomDoorClose()
    {
       
        if (mDoor.IsPlayerEnterDoor())
        {
            mDoor.EnterBossRoom();
        }

    }
}
