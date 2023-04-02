using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    public GameObject[] objs;
    ItemStat mitemList;
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


    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            GameManager.Instance.AddItemList(objs[i].GetComponent<ItemStat>());
        }
        GameManager.Instance.DefaultItemAdd();
        // GFunc.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
