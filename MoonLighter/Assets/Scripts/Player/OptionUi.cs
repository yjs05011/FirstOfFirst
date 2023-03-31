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
    [SerializeField]
    private int selectTitleIdx = 0;
    [SerializeField]
    private int selectOptionIdx = 0;
    [SerializeField]
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
        Debug.Log(corser.transform.localPosition);
        SetActiveOption();
        ColorChange(selectTitleIdx, selectOptionIdx, green);



    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (selectTitleIdx == 2)
            {
                RuningFunc(selectOptionIdx);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (selectTitleIdx >= 2)
            {


            }
            else
            {
                selectTitleIdx++;
                selectOptionIdx = 0;
                CorsetSetGameObject();
                corser.transform.localPosition = CorsorSetPos(0);
                ColorChange(selectTitleIdx, selectOptionIdx, green);
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
                CorsetSetGameObject();
                corser.transform.localPosition = CorsorSetPos(0);
                ColorChange(selectTitleIdx, selectOptionIdx, green);
                StartCoroutine(TileLeftMoving(0.5f));
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectOptionIdx == 2)
            {

            }
            else
            {
                isDirection = false;
                RuningFunc(selectOptionIdx, isDirection);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectOptionIdx == 2)
            {

            }
            else
            {
                isDirection = true;
                RuningFunc(selectOptionIdx, isDirection);
            }

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
                ColorChange(selectTitleIdx, beforeOptionIdx, yellow);
                ColorChange(selectTitleIdx, selectOptionIdx, green);
                corser.transform.localPosition = CorsorSetPos(selectOptionIdx);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (selectOptionIdx >= option[selectTitleIdx].transform.GetChild(0).childCount - 1)
            {

            }
            else
            {
                beforeOptionIdx = selectOptionIdx;
                selectOptionIdx++;
                CorsetSetGameObject();
                ColorChange(selectTitleIdx, beforeOptionIdx, yellow);
                ColorChange(selectTitleIdx, selectOptionIdx, green);
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
    public void ColorChange(int titleidx, int num, Color color)
    {
        if (titleidx == 0)
        {
            int idx = option[selectTitleIdx].transform.GetChild(0).GetChild(num).childCount;
            option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetChild(0).GetComponent<Text>().color = color;
            for (int i = 1; i < idx; i++)
            {
                option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetChild(i).GetComponent<Image>().color = color;
            }
        }
        else if (titleidx == 1)
        {
            int idx = option[selectTitleIdx].transform.GetChild(0).GetChild(num).childCount;
            option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetChild(0).GetComponent<Text>().color = color;
            for (int i = 1; i < idx - 2; i++)
            {
                option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetChild(i).GetComponent<Image>().color = color;
            }
        }
        else
        {

        }

    }
    public void RuningFunc(int num, bool direction)
    {
        option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetComponent<UIController>().Runing(direction);
    }
    public void RuningFunc(int num)
    {
        option[selectTitleIdx].transform.GetChild(0).GetChild(num).GetComponent<UIController>().Runing();
    }
}
