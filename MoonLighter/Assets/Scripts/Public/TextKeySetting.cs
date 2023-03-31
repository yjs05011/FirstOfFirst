using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class TextKeySetting : MonoBehaviour
{
    [SerializeField]
    Text text;
    // GameKeyManager의 ENUM 타입의 이름을 그대로 가져와 입력하면 키가 변경됩니다.
    public string buttonKey;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        int a = (int)Enum.Parse(typeof(GameKeyManger.KeyAction), buttonKey);
        text.text = GameKeyManger.KeySetting.keys[(GameKeyManger.KeyAction)a].ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
