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
<<<<<<< HEAD

        SetPosition.Instance.Create();

        GameManager.Instance.Create();
        DataManager.Instance.Create();

        GFunc.LoadScene("VillageScene");
=======
        GameManager.Instance.Create();
        DataManager.Instance.Create();
        //GFunc.LoadScene("VillageScene");
>>>>>>> de76c5c24b466cf605e893e886147b2febaa16ab
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
