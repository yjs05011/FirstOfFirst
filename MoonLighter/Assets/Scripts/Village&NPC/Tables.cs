using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Tables : MonoBehaviour
{
    public Transform[] mTables;
    public SpriteRenderer[] mSprites;
    void Awake()
    {
        mTables = new Transform[transform.childCount - 1];
        mSprites= new SpriteRenderer[transform.childCount - 1];
        for (int i = 0; i < mTables.Length; i++)
        {
            mTables[i] = transform.GetChild(i);
            mSprites[i] = mTables[i].gameObject.FindChildObj("Item").GetComponent<SpriteRenderer>();
            mSprites[i].sprite = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetItemOnTable(Sprite item,int tableNum)
    {
        mSprites[tableNum].sprite = item;
    }
    public void GetItemOnTable(int tableNum)
    {
        mSprites[tableNum].sprite = null;
    }
}
