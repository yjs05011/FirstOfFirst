using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventroyMove : MonoBehaviour
{

    [SerializeField]
    int inputX;
    [SerializeField]
    int inputY;
    public GameObject selector;
    public GameObject firstpos;
    Vector3 defaultPos;
    Vector3 moveXPos = new Vector3(72, 0, 0);
    Vector3 moveYPosFist = new Vector3(0, 86, 0);
    Vector3 moveYPos = new Vector3(0, 72, 0);
    ItemStat[] iTemList = new ItemStat[20];
    public Sprite[] itemSprite;
    GameObject[] slot = new GameObject[20];
    public int seletorCountText = 0;
    public int seletorid = 0;
    public bool isSelect = false;
    int count = 0;
    private void Awake()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            slot[i] = transform.GetChild(0).GetChild(i).gameObject;
        }

    }
    private void OnEnable()
    {
        iTemList = GameManager.Instance.mInventory;
        defaultPos = firstpos.transform.localPosition;
        inputX = 0;
        inputY = 0;
        selector.transform.localPosition = defaultPos;
        for (int i = 0; i < iTemList.Length; i++)
        {
            slot[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemSprite[iTemList[i].id];
            if (iTemList[i].count != 0)
            {
                slot[i].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = iTemList[i].count.ToString();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int seletorCount = inputX + inputY * 5;
            if (isSelect)
            {

                if (seletorid == iTemList[seletorCount].id)
                {
                    seletorCountText++;
                    GameManager.Instance.mInventory[seletorCount].count--;
                    if (GameManager.Instance.mInventory[seletorCount].count == 0)
                    {
                        GameManager.Instance.mInventory[seletorCount] = GameManager.Instance.mItemList[0];
                    }
                }
                else
                {
                    Debug.Log(iTemList[seletorCount].id);
                }
            }
            if (GameManager.Instance.mInventory[seletorCount].id == 0 && isSelect)
            {
                isSelect = false;
                Debug.Log("!!");
                if (GameManager.Instance.mInventory[seletorCount].id == 0)
                {
                    GameManager.Instance.mInventory[seletorCount] = GameManager.Instance.mItemList[seletorid];
                    GameManager.Instance.mItemList[seletorid].count = seletorCountText;
                    seletorCountText = 0;
                }
            }
            else if (GameManager.Instance.mInventory.Length > seletorCount && !isSelect)
            {
                isSelect = true;

                GameManager.Instance.mInventory[seletorCount].count--;
                seletorCountText++;
                seletorid = GameManager.Instance.mInventory[seletorCount].id;
                if (GameManager.Instance.mInventory[seletorCount].count == 0)
                {
                    GameManager.Instance.mInventory[seletorCount] = GameManager.Instance.mItemList[0];
                }
            }
            Debug.Log(GameManager.Instance.mItemList[seletorCount].id);
            selector.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = itemSprite[seletorid];
            selector.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = seletorCountText.ToString();
            slot[seletorCount].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemSprite[iTemList[seletorCount].id];
            if (iTemList[seletorCount].count != 0)
            {
                slot[seletorCount].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = iTemList[seletorCount].count.ToString();
            }
            else
            {
                slot[seletorCount].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = " ";
            }

        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene("TitleScene");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (inputX + inputY * 5 <= 0 && inputX <= 0)
            {

            }
            else if (inputY >= 1 && inputX <= 0)
            {
                inputX = 4;
                inputY--;
            }
            else
            {
                inputX--;
            }
            if (inputY == 1)
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX - moveYPosFist * inputY;
            }
            else if (inputY == 0)
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX;

            }
            else
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX - moveYPosFist * 1 - moveYPos * (inputY - 1);
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (inputX + inputY * 5 >= 19 && inputX >= 4)
            {

            }
            else if (inputY <= 3 && inputX >= 4)
            {
                inputX = 0;
                inputY++;
            }
            else
            {
                inputX++;
            }
            if (inputY == 1)
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX - moveYPosFist * inputY;
            }
            else if (inputY == 0)
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX;

            }
            else
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX - moveYPosFist * 1 - moveYPos * (inputY - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (inputY <= 0)
            {
                inputY = 3;
            }
            else
            {
                inputY--;
            }
            if (inputY == 1)
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX - moveYPosFist * inputY;
            }
            else if (inputY == 0)
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX;

            }
            else
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX - moveYPosFist * 1 - moveYPos * (inputY - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (inputY >= 3)
            {
                inputY = 0;
            }
            else
            {
                inputY++;
            }
            if (inputY == 1)
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX - moveYPosFist * inputY;
            }
            else if (inputY == 0)
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX;

            }
            else
            {
                selector.transform.localPosition = defaultPos + moveXPos * inputX - moveYPosFist * 1 - moveYPos * (inputY - 1);
            }
        }

    }
}
