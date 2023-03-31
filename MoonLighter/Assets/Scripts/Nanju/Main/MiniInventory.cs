using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniInventory : MonoBehaviour
{
    public GameObject mMiniInventory;
    public GameObject mInventroy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            mMiniInventory.SetActive(true);
            mInventroy.SetActive(true);
        }

    }



}
