using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public SaveData(float _playerHp, float _playerMaxHp, float _playerSpeed, float _playerStr, float _playerDef, float _playerMoney)
    {
        mPlayerMaxHp = _playerMaxHp;
        mPlayerHp = _playerHp;
        mPlayerMoney = _playerMoney;
        mPlayerDef = _playerDef;
        mPlayerStr = _playerStr;
        mPlayerSpeed = _playerSpeed;
    }
    public SaveData()
    {
        mPlayerHp = default;
        mPlayerSpeed = default;
        mPlayerStr = default;
        mPlayerDef = default;
        mPlayerMoney = default;
        mPlayerMaxHp = default;
    }
    public float mPlayerHp;
    public float mPlayerSpeed;
    public float mPlayerStr;
    public float mPlayerDef;
    public float mPlayerMoney;
    public float mPlayerMaxHp;

}
public class DataManager : GSingleton<DataManager>
{
    string path;
    // Start is called before the first frame update
    public override void Awake()
    {

        path = Path.Combine(Application.dataPath, "test.json");
        JsonLoad();
        base.Init();
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();
        if (!File.Exists(path))
        {

        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            if (saveData != null)
            {
                GameManager.Instance.mPlayerHp = saveData.mPlayerHp;
                GameManager.Instance.mPlayerMaxHp = saveData.mPlayerMaxHp;
                GameManager.Instance.mPlayerDef = saveData.mPlayerDef;
                GameManager.Instance.mPlayerStr = saveData.mPlayerStr;
                GameManager.Instance.mPlayerMoney = saveData.mPlayerMoney;
                GameManager.Instance.mPlayerSpeed = saveData.mPlayerSpeed;
            }

        }
    }

    public void JsonSave()
    {
        Debug.Log(path);
        SaveData saveData = new SaveData();
        saveData.mPlayerHp = GameManager.Instance.mPlayerHp;
        saveData.mPlayerMaxHp = GameManager.Instance.mPlayerMaxHp;
        saveData.mPlayerDef = GameManager.Instance.mPlayerDef;
        saveData.mPlayerStr = GameManager.Instance.mPlayerStr;
        saveData.mPlayerMoney = GameManager.Instance.mPlayerMoney;
        saveData.mPlayerSpeed = GameManager.Instance.mPlayerSpeed;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }
}
