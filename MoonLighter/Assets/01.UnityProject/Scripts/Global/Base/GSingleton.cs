using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSingleton<T> : GComponent where T : GSingleton<T>
{
    private static T _instance = default;

    public static T Instance 
    {
        get
        {
            if(GSingleton<T>._instance == default || _instance == default)
            {
                GSingleton<T>._instance = 
                    GFunc.CreateObj<T>(typeof(T).ToString());
                DontDestroyOnLoad(_instance.gameObject);
            }       // if: ?¥í?????? ??? ???? ?? ???? ?¥í????? ???

            // ???? ????? ?¥í?????? ???? ??????? ???????
            return _instance;
        }
    }

    public override void Awake()
    {
        base.Awake();
    }       // Awake()

    public void Create()
    {
        this.Init();
    }       // Create()

    protected virtual void Init()
    {
        /* Do something */
    }
    
    public static T CreateObj<K>(string objName) where K : Component
    {
        GameObject newObj = new GameObject(objName);
        return newObj.AddComponent<T>();
    }
}
