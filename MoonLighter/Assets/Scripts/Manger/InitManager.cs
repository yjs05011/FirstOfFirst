using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    private void Awake()
    {
        GameKeyManger.Instance.Create();
        PlayerManager.Instance.Create();
        ShopManager.Instance.Create();
        TalkManager.Instance.Create();

        SetPosition.Instance.Create();
        GameManager.Instance.Create();
        DataManager.Instance.Create();
        InventoryManager.Instance.Create();
        ItemManager.Instance.Create();

        VillageManager.Instance.Create();
        // GFunc.LoadScene("TitleScene");

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
