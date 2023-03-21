using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscControl : MonoBehaviour
{
    public GameObject GameBackCursorImage;
    public GameObject OptionCursorImage;
    public GameObject MainMenuCursorImage;
    public GameObject ExitCursorImage;

    // public KeyCode mUpKey= KeyCode.W;

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
                GameBackCursorImage.SetActive(true);
                OptionCursorImage.SetActive(false);
                break;

            case 1:
                OptionCursorImage.SetActive(true);
                GameBackCursorImage.SetActive(false);
                MainMenuCursorImage.SetActive(false);
                break;
            case 2:
                MainMenuCursorImage.SetActive(true);
                OptionCursorImage.SetActive(false);
                ExitCursorImage.SetActive(false);
                break;
            case 3:
                ExitCursorImage.SetActive(true);
                MainMenuCursorImage.SetActive(false);
                break;
        }

    }

}
