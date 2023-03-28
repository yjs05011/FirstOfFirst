using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniInventory : MonoBehaviour
{
    public GameObject mMiniInventory;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            mMiniInventory.SetActive(true);
        }
        // 던전에 들어가 몬스터를 다 잡지않으면 인벤토리 열리지 않음
        if (DungeonManager.Instance.mDungeonBossStage)
        {

        }
        // 다른 던전 맵으로 들어가면 인벤토리 열리지 않음
    }



}
