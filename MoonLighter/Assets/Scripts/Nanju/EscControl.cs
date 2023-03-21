using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour
{
    public GameObject GameBackButton;
    public GameObject OptionButton;
    public GameObject MainMenuButton;
    public GameObject ExitButton;

    public int mTextCheck;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (mTextCheck == 3)
            {

            }
            else
            {
                mTextCheck++;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (mTextCheck == 0)
            {

            }
            else
            {
                mTextCheck--;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (mTextCheck)
            {
                case 0:
                    gameObject.SetActive(false);
                    break;

                case 1:
                    break;
                case 2:
                    break;
                case 3:
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
                    break;
            }
        }

        switch (mTextCheck)
        {
            case 0:
                GameBackButton.SetActive(true);
                OptionButton.SetActive(false);
                break;

            case 1:
                OptionButton.SetActive(true);
                GameBackButton.SetActive(false);
                MainMenuButton.SetActive(false);
                break;
            case 2:
                MainMenuButton.SetActive(true);
                OptionButton.SetActive(false);
                ExitButton.SetActive(false);
                break;
            case 3:
                ExitButton.SetActive(true);
                MainMenuButton.SetActive(false);
                break;
        }

    }

}
