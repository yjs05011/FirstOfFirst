using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public SaveData(List<KeyCode> _mKeySave, float _playerHp, float _playerMaxHp, float _playerSpeed, float _playerStr, float _playerDef, float _playerMoney, bool _isNight,
    List<int> _tableNumber, int[] _itemPrice, int[] _itemsNumber, Vector3 _bedPostion, bool _isBlackSmithBuild, bool _isWitchHouseBuild)
    {
        mPlayerMaxHp = _playerMaxHp;
        mPlayerHp = _playerHp;
        mPlayerMoney = _playerMoney;
        mPlayerDef = _playerDef;
        mPlayerStr = _playerStr;
        mPlayerSpeed = _playerSpeed;
        mKeySave = _mKeySave;
        mIsNight = _isNight;
        mTableNumber = _tableNumber;
        mItemPrice = _itemPrice;
        mItemsNumber = _itemsNumber;
        mBedPosition = _bedPostion;
        mIsBlackSmithBuild = _isBlackSmithBuild;
        mIsWitchHouseBuild = _isWitchHouseBuild;

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
    public PlayerStat mPlayerStat;
    public float mPlayerHp;
    public float mPlayerSpeed;
    public float mPlayerStr;
    public float mPlayerDef;
    public float mPlayerMoney;
    public float mPlayerMaxHp;
    public bool mIsNight;
    [SerializeField]
    public List<int> mTableNumber;
    [SerializeField]
    public int[] mItemPrice;
    [SerializeField]
    public int[] mItemsNumber;
    [SerializeField]
    public Vector3 mBedPosition;
    [SerializeField]
    public bool mIsBlackSmithBuild;
    [SerializeField]
    public bool mIsWitchHouseBuild;
    public List<KeyCode> mKeySave;
}
public class DataManager : GSingleton<DataManager>
{
    string path;
    // Start is called before the first frame update
    public override void Awake()
    {

        path = Path.Combine(Application.dataPath, "MoonLightSaveFile.json");
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
                GameManager.Instance.mIsNight = saveData.mIsNight;
                GameManager.Instance.mTableNumber = saveData.mTableNumber;
                GameManager.Instance.mItemPrice = saveData.mItemPrice;
                GameManager.Instance.mItemsNumber = saveData.mItemsNumber;
                GameManager.Instance.mBedPosition = saveData.mBedPosition;
                GameManager.Instance.mIsBlackSmithBuild = saveData.mIsBlackSmithBuild;
                GameManager.Instance.mIsWitchHouseBuild = saveData.mIsWitchHouseBuild;
                GameKeyManger.Instance.SaveKeyList = saveData.mKeySave;
                GameKeyManger.Instance.LoadKeyData();

            }

        }
    }

    public void JsonSave()
    {
        Debug.Log(path);
        SaveData saveData = new SaveData(GameKeyManger.Instance.SaveKeyList, GameManager.Instance.mPlayerHp, GameManager.Instance.mPlayerMaxHp, GameManager.Instance.mPlayerSpeed,
        GameManager.Instance.mPlayerStr, GameManager.Instance.mPlayerDef, GameManager.Instance.mPlayerMoney, GameManager.Instance.mIsNight, GameManager.Instance.mTableNumber, GameManager.Instance.mItemPrice,
        GameManager.Instance.mItemsNumber, GameManager.Instance.mBedPosition, GameManager.Instance.mIsBlackSmithBuild, GameManager.Instance.mIsWitchHouseBuild);

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }

    public bool FileCheck()
    {
        if (!File.Exists(path))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
