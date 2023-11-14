using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CSVReader;
using System.IO;
using System;



public class Scene_change : MonoBehaviour
{
    //////////////////////////////VAR//////////////////////////////
    ///
    Vector2 mousePosition;

    bool mouseButtonReleased = true;
    bool mouseButtonPressed = false;
    bool is_cursor_on = false;
    string when_pressed_obj, when_realesed_obj;
    string scene_name;



    GameObject[] allObjects;

    //////////////////////////////VAR//////////////////////////////



    public void SaveCSV_MergeBoard()
    {
        //string filePath = "Assets/Resources/SaveData/MergeTableData.csv";
        string filePath = Application.persistentDataPath + "/MergeTableData.csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);

        int obj_no = 0;
        string CSV_HEAD = "No,ObjectName,PosX,PosY";

        DateTime currentTime = DateTime.Now;
        string formattedTime = currentTime.ToString("yyyy-MM-dd-HH-mm-ss");

        outStream.WriteLine(CSV_HEAD);
        //outStream.WriteLine(formattedTime);

        allObjects = FindObjectsOfType<GameObject>(); // 씬에 있는 모든 오브젝트를 저장함
        foreach (GameObject obj in allObjects) 
        {
            string obj_name = obj.ToString();
            if (obj_name.Contains("c1") || obj_name.Contains("c2") ||
                obj_name.Contains("c3") || obj_name.Contains("c4") ||
                obj_name.Contains("c5") || obj_name.Contains("c6") ||
                obj_name.Contains("c7")) 
            {
                if (!obj_name.Contains("t0")) 
                {
                    string obj_Xpos = obj.transform.position.x.ToString();
                    string obj_Ypos = obj.transform.position.y.ToString();

                    string add_line = obj_no.ToString() +
                        ";" + obj_name.Substring(0,7) + ";" + obj_Xpos + ";" + obj_Ypos;
                    //Debug.Log("추가할 라인 내용:\n" + add_line);
                    outStream.WriteLine(add_line);

                    obj_no++;
                }
            }
        }
            outStream.Close();
    }

    private void OnMouseDown()
    {
        mouseButtonReleased = false;
        mouseButtonPressed = true;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.down, 1.0f);
        if (hit.collider == null)
        {
            //Debug.LogFormat("감지된 버튼이 없습니다.");
            when_pressed_obj = "";
        }
        else 
        {
            when_pressed_obj = hit.collider.name;
            //Debug.LogFormat("마우스가 눌렸을 때의 클릭된 오브젝트:{0}", when_pressed_obj);
            if (scene_name == "MergeBoard") 
            {
                Debug.LogFormat("클릭 감지. 씬 데이터 저장.");
                SaveCSV_MergeBoard();
            }

        }
 

    }

    private void OnMouseDrag()
    {
        mouseButtonReleased = false;
        mouseButtonPressed = true;


    }


    private void OnMouseUp()
    {
        mouseButtonReleased = true;
        mouseButtonPressed = false;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.down, 1.0f);
        if (hit.collider == null)
        {
            //Debug.LogFormat("감지된 버튼이 없습니다.");
            when_realesed_obj = "";
        }
        else
        {
            when_realesed_obj = hit.collider.name;
            //Debug.LogFormat("마우스가 눌렸을 때의 클릭된 오브젝트:{0}", when_realesed_obj);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene(); //함수 안에 선언하여 사용한다.
        scene_name = scene.name;

        if (when_pressed_obj == when_realesed_obj) 
        {
            //////////////////////////////머지보드////////////////////////////////////////
            if (when_pressed_obj == "Merge_icon_Arrow_Left" && when_realesed_obj == "Merge_icon_Arrow_Left")
            {
                Debug.Log("왼쪽버튼을 클릭했습니다. 퀘스트창을 띄웁니다.");
            }
            if (when_pressed_obj == "Merge_item_Box" && when_realesed_obj == "Merge_item_Box") 
            {
                Debug.Log("박스버튼을 클릭했습니다. 어셈블리씬으로 이동합니다.");
                SceneManager.LoadScene("Assembly");
            }
            if (when_pressed_obj == "Merge_icon_Arrow_Right" && when_realesed_obj == "Merge_icon_Arrow_Right")
            {
                Debug.Log("오른쪽버튼을 클릭했습니다. 카운터로 이동합니다.");
                SceneManager.LoadScene("Counter");
            }

            //////////////////////////////어셈블리////////////////////////////////////////
            if (when_pressed_obj == "Assembly_icon_Arrow_Left" && when_realesed_obj == "Assembly_icon_Arrow_Left")
            {
                Debug.Log("왼쪽버튼을 클릭했습니다. 퀘스트창을 띄웁니다.");
            }
            if (when_pressed_obj == "Assembly_item_Box" && when_realesed_obj == "Assembly_item_Box")
            {
                Debug.Log("박스버튼을 클릭했습니다. 머지보드씬으로 이동합니다.");
                SceneManager.LoadScene("MergeBoard");
            }
            if (when_pressed_obj == "Assembly_icon_Arrow_Right" && when_realesed_obj == "Assembly_icon_Arrow_Right")
            {
                Debug.Log("오른쪽버튼을 클릭했습니다. 카운터로 이동합니다.");
                SceneManager.LoadScene("Counter");
            }

            //////////////////////////////카운터////////////////////////////////////////
            if (when_pressed_obj == "Counter_icon_Arrow_Left" && when_realesed_obj == "Counter_icon_Arrow_Left")
            {
                Debug.Log("왼쪽버튼을 클릭했습니다. 퀘스트창을 띄웁니다.");
            }
            if (when_pressed_obj == "Counter_item_Box" && when_realesed_obj == "Counter_item_Box")
            {
                Debug.Log("박스버튼을 클릭했습니다. 어셈블리씬으로 이동합니다.");
                SceneManager.LoadScene("Assembly");
            }
            if (when_pressed_obj == "Counter_icon_Arrow_Right" && when_realesed_obj == "Counter_icon_Arrow_Right")
            {
                Debug.Log("오른쪽버튼을 클릭했습니다. 머지보드로 이동합니다.");
                SceneManager.LoadScene("MergeBoard");
            }

            when_pressed_obj = "";
            when_realesed_obj = "";
        }

    }
}
