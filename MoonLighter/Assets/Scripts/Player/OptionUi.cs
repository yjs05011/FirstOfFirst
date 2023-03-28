using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OptionUi : MonoBehaviour
{
    // public Image Test;
    public Text Test;
    public GameObject selectTitle;
    public GameObject scrollTile;

    public List<Vector2> titlePos = default;
    public List<GameObject> option = new List<GameObject>();
    public GameObject corser;
    public Color yellow = new Color(0.9254903f, 0.9254903f, 0.7529413f, 1f);
    public Color green = new Color(0.1176471f, 0.5372549f, 0.3921569f, 1f);
    private int selectTitleIdx = 0;
    private int selectOptionIdx = 0;
    private int beforeOptionIdx = default;
    // 방향을 나타내는 변수 (false = 왼쪽, true = 오른쪽)
    private bool isDirection = default;

    // Start is called before the first frame update
    void Start()
    {
        titlePos = new List<Vector2>();
        titlePos.Add(new Vector2(-427, 0));
        titlePos.Add(new Vector2(0, 0));
        titlePos.Add(new Vector2(427, 0));
        CorsetSetGameObject();
        corser.transform.localPosition = CorsorSetPos(0);
        SetActiveOption();
        ColorChange(selectOptionIdx, yellow);



    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (selectTitleIdx >= 2)
            {

            }
            else
            {
                selectTitleIdx++;
                selectOptionIdx = 0;
                StartCoroutine(TileRightMoving(0.5f));
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (selectTitleIdx <= 0)
            {

            }
            else
            {
                selectTitleIdx--;
                selectOptionIdx = 0;
                StartCoroutine(TileLeftMoving(0.5f));
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isDirection = false;
            RuningFunc(selectOptionIdx, isDirection);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isDirection = true;
            RuningFunc(selectOptionIdx, isDirection);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectOptionIdx <= 0)
            {

            }
            else
            {
                beforeOptionIdx = selectOptionIdx;
                selectOptionIdx--;
                CorsetSetGameObject();
                ColorChange(beforeOptionIdx, green);
                ColorChange(selectOptionIdx, yellow);
                corser.transform.localPosition = CorsorSetPos(selectOptionIdx);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log(option[selectOptionIdx].transform.childCount);
            if (selectOptionIdx >= option[selectOptionIdx].transform.childCount)
            {

            }
            else
            {
                beforeOptionIdx = selectOptionIdx;
                selectOptionIdx++;
                CorsetSetGameObject();
                ColorChange(beforeOptionIdx, green);
                ColorChange(selectOptionIdx, yellow);
                corser.transform.localPosition = CorsorSetPos(selectOptionIdx);
            }
        }

    }
    IEnumerator TileRightMoving(float Delay)
    {
        float time = Delay / 1280;
        Vector3 pos = new Vector2(titlePos[selectTitleIdx].x, 240);
        selectTitle.gameObject.SetActive(false);
        selectTitle.transform.localPosition = titlePos[selectTitleIdx];
        while (Vector3.Distance(scrollTile.transform.localPosition, pos) >= 10)
        {
            scrollTile.transform.localPosition += Vector3.right * 6;

            yield return new WaitForSeconds(time);
        }
        scrollTile.transform.localPosition = pos;
        selectTitle.gameObject.SetActive(true);
        SetActiveOption();


    }
    IEnumerator TileLeftMoving(float Delay)
    {
        float time = Delay / 1280;
        Vector3 pos = new Vector2(titlePos[selectTitleIdx].x, 240);
        selectTitle.gameObject.SetActive(false);
        selectTitle.transform.localPosition = titlePos[selectTitleIdx];
        while (Vector3.Distance(scrollTile.transform.localPosition, pos) >= 10)
        {
            scrollTile.transform.localPosition += Vector3.left * 6;

            yield return new WaitForSeconds(time);
        }
        scrollTile.transform.localPosition = pos;
        selectTitle.gameObject.SetActive(true);
        SetActiveOption();


    }
    public void SetActiveOption()
    {
        switch (selectTitleIdx)
        {
            case 0:
                option[0].SetActive(true);
                option[1].SetActive(false);
                option[2].SetActive(false);
                break;
            case 1:
                option[0].SetActive(false);
                option[1].SetActive(true);
                option[2].SetActive(false);
                break;
            case 2:
                option[0].SetActive(false);
                option[1].SetActive(false);
                option[2].SetActive(true);
                break;
        }
    }
    public void CorsetSetGameObject()
    {
        corser = option[selectTitleIdx].transform.GetChild(1).gameObject;
    }
    public Vector3 CorsorSetPos(int num)
    {
        return option[selectTitleIdx].transform.GetChild(0).GetChild(num).localPosition;
    }
    public void ColorChange(int num, Color color)
    {
        Debug.Log(num);
        int idx = option[selectTitleIdx].transform.GetChild(0).GetChild(num).childCount;
        option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetChild(0).GetComponent<Text>().color = color;
        for (int i = 1; i < idx; i++)
        {
            option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetChild(i).GetComponent<Image>().color = color;
        }
    }
    public void RuningFunc(int num, bool direction)
    {
        option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetComponent<UIController>().Runing(direction);
    }
}
