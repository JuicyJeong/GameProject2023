using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Text;
using static CSVReader;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;


public class global_check : MonoBehaviour
{
    float timer;
    float waitingTime = 0.1f;

    public string current_controlling_object_name;
    Text print_msg;
    TextMeshProUGUI Merge_msg_Description;

    RaycastHit hit;
    public List<List<(float x, float y)>> Greed = new List<List<(float x, float y)>>();
    public List<List<bool>> is_Greed_full = new List<List<bool>>();

    public Vector2 positionToCheck = new Vector2(-2.0f, 1.0f); // 체크하려는 포지션


    // 현재 씬에 있는 모든 오브젝트를 가져옵니다.
    GameObject[] allObjects;

    void Greed_Initialize() // play only once
    {
        float start_x, start_y, greed_distance;
        //start_x = -6.0f; start_y = 5.0f; greed_distance = 2.0f;
        start_x = -5.56f; start_y = 3.06f; greed_distance = 1.86f;
        // 각 행에 대한 리스트를 생성하고 초기화
        for (int row_num = 0; row_num < 7; row_num++)
        {
            List<(float x, float y)> row = new List<(float x, float y)>();
            for (int col_num = 0; col_num < 8; col_num++)
            {
                // 원하는 좌표 (x, y)를 할당
                //row.Add((i, j)); // 예를 들어, 각 원소는 (i, j) 좌표를 가짐
                row.Add((start_x + (row_num * greed_distance), start_y - (col_num * greed_distance)));
            }
            Greed.Add(row);
        }


        //각 행의 자리에 오브젝트가 있는지 없는지 체크. 일부러 반복문 분리해서 읽기 쉽게 함
        for (int row_num = 0; row_num < 7; row_num++)
        {
            List<bool> innerList = new List<bool>();
            for (int col_num = 0; col_num < 8; col_num++)
            {
                innerList.Add(false);
            }
            is_Greed_full.Add(innerList);
        }
        ////false로 초기화 하고 체크하는 구간. 평소엔 안씁니다.
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
        //매번 셀의 위치를 업데이트
        for (int row_num = 0; row_num < 7; row_num++)
        {
            List<bool> innerList = new List<bool>();
            for (int col_num = 0; col_num < 8; col_num++)
            {
                //체크할 셀을 새로 초기화하여 입력
                Vector2 current_check_cell = new Vector2(Greed[row_num][col_num].x, Greed[row_num][col_num].y);

                positionToCheck = current_check_cell;

                RaycastHit2D hit = Physics2D.Raycast(positionToCheck, Vector2.down, 1.0f);
                if (hit.collider != null)
                {
                    is_Greed_full[row_num][col_num] = true;
                    string what_is_name  = hit.collider.gameObject.name;


                }
                else
                {
                    is_Greed_full[row_num][col_num] = false;
                }
            }
        }


    }
    //해당 함수 다른 디비 
    string Print_Selected_Object_Description(List<Dictionary<string, object>> object_description_csv, string print_category, string print_tier) 
    {

        string final_description = "";
        for (int row = 0; row < object_description_csv.Count; row++)
        {
            string check_category = object_description_csv[row]["Category"].ToString();
            string check_tier = object_description_csv[row]["tier"].ToString();
            
            if (check_category == print_category && check_tier == print_tier)
            {
                //Debug.LogFormat("해당 스크립트의 내용을 출력합니다: {0}", object_description_csv[row]["Script"]);
                final_description = object_description_csv[row]["Script"].ToString();
                //Debug.LogFormat(final_description);
            }
        }
        return final_description;
    }
    //


    // Start is called before the first frame update
    void Start()
    {

        current_controlling_object_name = "initial_text";
        //print_msg = GameObject.Find("print_msg1").GetComponent<Text>();

        Merge_msg_Description = GameObject.Find("MSG_Description").GetComponent<TextMeshProUGUI>();
        Merge_msg_Description.text = current_controlling_object_name;
        allObjects = FindObjectsOfType<GameObject>(); // 씬에 있는 모든 오브젝트를 저장함
        Greed_Initialize(); // 그리드의 8*7 행렬에서 각각의 셀 포지션을 선언하고 그 셀마다 할당된 오브젝트를 false로 초기화


    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        //Merge_msg_Description.text = current_controlling_object_name + " 오브젝트에 대한 설명을 작성합니다.오브젝트에 대한 설명을 작성합니다.오브젝트에 대한 설명을 작성합니다.오브젝트에 대한 설명을 작성합니다.오브젝트에 대한 설명을 작성합니다.오브젝트에 대한 설명을 작성합니다.";
        string current_category = current_controlling_object_name.Substring(0,2);
        string current_tier = current_controlling_object_name.Substring(3, 2);
        List<Dictionary<string, object>> object_description_csv = CSVReader.Read("DataBase/Object_description");

        string print_obj_msg = Print_Selected_Object_Description(object_description_csv, current_category, current_tier);
        Merge_msg_Description.text = print_obj_msg;


        check_cell_is_empty();




        if (timer > waitingTime)
        {

            if (Input.GetKey(KeyCode.C))
            {
                Debug.Log("현재 오브젝트가 있는 셀은 다음과 같습니다.");
                for (int row_num = 0; row_num < 7; row_num++)
                {
                    for (int col_num = 0; col_num < 8; col_num++)
                    {
                        if (is_Greed_full[row_num][col_num])
                        {
                            Debug.LogFormat("{0},{1} 셀에는 오브젝트가 있습니다.", row_num, col_num);
                        }
                    }
                }
                //Debug.LogFormat("현재 있는 오브젝트 수: {0}", allObjects.Length);
            }

            timer = 0;
        }
        
    }
}
