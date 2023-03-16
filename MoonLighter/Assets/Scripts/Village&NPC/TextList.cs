using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextList : MonoBehaviour
{
    Dictionary<string, string[]> textData;

    void Awake()
    {
        textData= new Dictionary<string, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        textData.Add("Village_Sign_DungeonUp", new string[] {"´øÀü ¡ã"});
    }

    public string GetTalk(string mName, int mTextIndex)
    {
        return textData[mName][mTextIndex];
    }
}
