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
=======
        GameManager.Instance.Create();
        DataManager.Instance.Create();
>>>>>>> 0df387b2af46bf52a383692706ec7c5414da0129
        //GFunc.LoadScene("VillageScene");
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
