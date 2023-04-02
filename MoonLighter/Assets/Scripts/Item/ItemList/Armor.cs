using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : ItemStat
{
    int itemId = 2;
    int itemType = 2;
    int itemCount = 1;
    int itemMaxCount = 1;
    public override void Awake()
    {
        base.Awake();
        base.SetStat(itemId, itemType, itemCount, itemMaxCount);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        bool duplicationCheck = false;
        int count = 0;
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < GameManager.Instance.mInventory.Length; i++)
            {
                if (GameManager.Instance.mInventory[i].id == id &&
                 GameManager.Instance.mInventory[i].count < GameManager.Instance.mInventory[i].MaxCount)
                {
                    duplicationCheck = true;
                    count = i;
                    i = GameManager.Instance.mInventory.Length;

                }
            }
            if (duplicationCheck == false)
            {
                ItemStat item = new ItemStat();
                item = GameManager.Instance.mItemList[id];
                item.count = 1;
                GameManager.Instance.mInventory[count] = item;
                gameObject.SetActive(false);
                Debug.Log(GameManager.Instance.mInventory[0]);
            }
            else
            {
                GameManager.Instance.mInventory[count].count++;
                gameObject.SetActive(false);
            }

        }
    }
}