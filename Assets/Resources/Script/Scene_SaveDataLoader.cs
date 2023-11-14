using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using static CSVReader;


public class Scene_SaveDataLoader : MonoBehaviour
{
    
    string scene_name;

    string MergeBoard_obj_name;
    Vector2 MergeBoard_obj_pos;


    void Load_MergeBoard_Data() //머지보드씬이 로드 될 
    {
        Debug.Log("머지보드데이터를 로드합니다...");
        string filePath = Application.persistentDataPath + "/MergeTableData.csv";
        StreamReader sr = new StreamReader(filePath);
        
        bool endOfFile = false;
        int headline = 0;
        while (!endOfFile)
        {
            string data_String = sr.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
            if (headline != 0)//헤드를 스킵하기 위해...
            {
                string[] data_values = data_String.Split(';');

                //Debug.LogFormat("오브젝트 이름: {0}\nX: {1}, Y: {2}",
                //    data_values[1], data_values[2], data_values[3]);
                MergeBoard_obj_name = "item_obj/" + data_values[1].ToString().Substring(0, 6);
                MergeBoard_obj_pos.x = float.Parse(data_values[2].ToString());
                MergeBoard_obj_pos.y = float.Parse(data_values[3].ToString());


                Instantiate(Resources.Load(MergeBoard_obj_name), MergeBoard_obj_pos, Quaternion.identity);
            }
            if (headline == 0) headline++;

        }
        sr.Close();

    }
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene(); //함수 안에 선언하여 사용한다.
        scene_name = scene.name;
        Debug.LogFormat("씬 이름 {0}이 로드되었습니다.", scene_name);
        MergeBoard_obj_pos = new Vector2(0.0f, 0.0f);
        if (scene_name == "MergeBoard") 
        {
            Load_MergeBoard_Data();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
