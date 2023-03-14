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
            if (GSingleton<T>._instance == default || _instance == default)
            {
                GSingleton<T>._instance =
                    CreateObj<T>(typeof(T).ToString());
                DontDestroyOnLoad(_instance.gameObject);
            }       // if: �ν��Ͻ��� ��� ���� �� ���� �ν��Ͻ�ȭ �Ѵ�

            // ���⼭ ���ʹ� �ν��Ͻ��� ���� ������� ������?
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
    public static T CreateObj<T>(string objName) where T : Component
    {
        GameObject newObj = new GameObject(objName);
        return newObj.AddComponent<T>();
    }
}
