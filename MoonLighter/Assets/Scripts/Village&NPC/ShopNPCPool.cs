using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCPool : MonoBehaviour
{
    public static ShopNPCPool Instance;
    public int ObjectNumber;
    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<ShopNPC> poolingObjectQueue = new Queue<ShopNPC>();

    private void Awake()
    {
        Instance = this;

        Initialize(ObjectNumber);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private ShopNPC CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<ShopNPC>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static ShopNPC GetObject()
    {

        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            
            return obj;
        }
        else
        {
            //var newObj = Instance.CreateNewObject();
            //newObj.gameObject.SetActive(true);
            //newObj.transform.SetParent(null);
            return null;
        }

    }

    public static void ReturnObject(ShopNPC obj)
    {
        
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
