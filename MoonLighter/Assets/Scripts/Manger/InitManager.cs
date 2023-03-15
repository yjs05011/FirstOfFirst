using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    private void Awake()
    {
        GameKeyManger.Instance.Create();
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
