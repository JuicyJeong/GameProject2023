using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Text;
using static CSVReader;
using TMPro;

public class global_check : MonoBehaviour
{
    float timer;
    float waitingTime = 0.1f;

    public string current_controlling_object_name;
    Text print_msg;
    TextMeshProUGUI print_msg_TMP;

    RaycastHit hit;
    public List<List<(float x, float y)>> Greed = new List<List<(float x, float y)>>();
    public List<List<bool>> is_Greed_full = new List<List<bool>>();

    public Vector2 positionToCheck = new Vector2(-2.0f, 1.0f); // üũ�Ϸ��� ������

    // ���� ���� �ִ� ��� ������Ʈ�� �����ɴϴ�.
    GameObject[] allObjects;

    void Greed_Initialize() // play only once
    {
        float start_x, start_y, greed_distance;
        //start_x = -6.0f; start_y = 5.0f; greed_distance = 2.0f;
        start_x = -5.58f; start_y = 5.58f; greed_distance = 1.86f;

        // �� �࿡ ���� ����Ʈ�� �����ϰ� �ʱ�ȭ
        for (int row_num = 0; row_num < 7; row_num++)
        {
            List<(float x, float y)> row = new List<(float x, float y)>();
            for (int col_num = 0; col_num < 8; col_num++)
            {
                // ���ϴ� ��ǥ (x, y)�� �Ҵ�
                //row.Add((i, j)); // ���� ���, �� ���Ҵ� (i, j) ��ǥ�� ����
                row.Add((start_x + (row_num * greed_distance), start_y - (col_num * greed_distance)));
            }
            Greed.Add(row);
        }


        //�� ���� �ڸ��� ������Ʈ�� �ִ��� ������ üũ. �Ϻη� �ݺ��� �и��ؼ� �б� ���� ��
        for (int row_num = 0; row_num < 7; row_num++)
        {
            List<bool> innerList = new List<bool>();
            for (int col_num = 0; col_num < 8; col_num++)
            {
                innerList.Add(false);
            }
            is_Greed_full.Add(innerList);
        }
        ////false�� �ʱ�ȭ �ϰ� üũ�ϴ� ����. ��ҿ� �Ⱦ��ϴ�.
        //for (int i = 0; i < is_Greed_Empty.Count; i++)
        //{
        //    for (int j = 0; j < is_Greed_Empty[i].Count; j++)
        //    {
        //        Debug.Log("Value at (" + i + ", " + j + "): " + is_Greed_Empty[i][j]);
        //    }
        //}

    }

    void check_cell_is_empty()
    {
        //�Ź� ���� ��ġ�� ������Ʈ
        for (int row_num = 0; row_num < 7; row_num++)
        {
            List<bool> innerList = new List<bool>();
            for (int col_num = 0; col_num < 8; col_num++)
            {
                //üũ�� ���� ���� �ʱ�ȭ�Ͽ� �Է�
                Vector2 current_check_cell = new Vector2(Greed[row_num][col_num].x, Greed[row_num][col_num].y);

                positionToCheck = current_check_cell;

                RaycastHit2D hit = Physics2D.Raycast(positionToCheck, Vector2.down, 1.0f);
                if (hit.collider != null)
                {
                    is_Greed_full[row_num][col_num] = true;
                }
                else
                {
                    is_Greed_full[row_num][col_num] = false;
                }
            }
        }


    }


    // Start is called before the first frame update
    void Start()
    {

        current_controlling_object_name = "init";
        //print_msg = GameObject.Find("print_msg1").GetComponent<Text>();

        print_msg_TMP = GameObject.Find("print_msg2").GetComponent<TextMeshProUGUI>();
        allObjects = FindObjectsOfType<GameObject>(); // ���� �ִ� ��� ������Ʈ�� ������
        Greed_Initialize(); // �׸����� 8*7 ��Ŀ��� ������ �� �������� �����ϰ� �� ������ �Ҵ�� ������Ʈ�� false�� �ʱ�ȭ

        List<Dictionary<string, object>> test_csv = CSVReader.Read("Script/test");
        string test1 = test_csv[3].ToString();
        Debug.Log(test1);
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //print_msg.text = "���� ��Ʈ������ ������Ʈ:" + current_controlling_object_name;
        print_msg_TMP.text = "���� ��Ʈ������ ������Ʈ:" + current_controlling_object_name;

        check_cell_is_empty();




        if (timer > waitingTime)
        {

            if (Input.GetKey(KeyCode.C))
            {
                Debug.Log("���� ������Ʈ�� �ִ� ���� ������ �����ϴ�.");

                for (int row_num = 0; row_num < 7; row_num++)
                {
                    for (int col_num = 0; col_num < 8; col_num++)
                    {
                        if (is_Greed_full[row_num][col_num])
                        {
                            Debug.LogFormat("{0},{1} ������ ������Ʈ�� �ֽ��ϴ�.", row_num, col_num);
                        }
                    }
                }
                //Debug.LogFormat("���� �ִ� ������Ʈ ��: {0}", allObjects.Length);
            }

            timer = 0;
        }
        
    }
}
