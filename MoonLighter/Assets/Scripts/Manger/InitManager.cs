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
<<<<<<< HEAD

        //GFunc.LoadScene("VillageScene");
=======
        VillageManager.Instance.Create();
<<<<<<< HEAD
        GFunc.LoadScene("TitleScene");




=======
        
>>>>>>> 299fa6c0c042db0fa38f6727d1f429e875431eed
>>>>>>> 994da980975e76d685684d64752445bc48c07135
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
