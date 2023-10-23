using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Text;
using TMPro;

public class DragObject : MonoBehaviour
{
    string object_name;

    Vector2 object_position;
    Vector2 mousePosition;
    float offsetX, offsetY;

    bool mouseButtonReleased = false;
    bool is_once_played = false;
    bool is_merge = false;
    bool is_play_first;

    public Text print_msg1;

    float defalut_obj_scale = 0.27f;

    float timer;
    float waitingTime = 0.0166667f;

    List<List<(float x, float y)>> Greed = new List<List<(float x, float y)>>();

    Dictionary<string, int> object_tier = new Dictionary<string, int>() 
    {
        { "c0", 5},
        { "c1", 7}
    };

    GameObject thisobj;
    GameObject collobj;
    GameObject[] allObjects;
    TextMeshProUGUI print_msg_TMP;

    // 각 행에 대한 리스트를 생성하고 초기화



    void Greed_Initialize() // play onlt once
    {
        float start_x, start_y, greed_distance;
        //start_x = -6.0f; start_y = 5.0f; greed_distance = 2.0f;
        start_x = -5.58f; start_y = 5.58f; greed_distance = 1.86f;

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
    }

    void SNAP() 
    {
        //현재 오브젝트의 포지션 저장
        Vector2 current_object_position = object_position;
        Vector2 close_point = new Vector2(0.0f, 0.0f); // 가장 가까운- 이동할 점을 여기에 저장할거임
        float short_distance = 100000.0f;// 충분히 큰 값으로 비교
        float distance;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                distance = Mathf.Sqrt(Mathf.Pow(current_object_position.x - Greed[i][j].x, 2) + Mathf.Pow(current_object_position.y - Greed[i][j].y, 2));
                if (distance < short_distance)
                {
                    short_distance = distance; // 스캔한 것들 중 가장 작은 거리 값
                    close_point.x = Greed[i][j].x; close_point.y = Greed[i][j].y;

                }

            }
        }
        //for (int i = 0; i < 100; i++) 
        //{
        //    this.gameObject.transform.position = new Vector3(0.01f*i, 0.01f * i, 0);

        //}
        this.gameObject.transform.position = new Vector3(close_point.x, close_point.y, 0);
    }




    private void OnMouseDown()
    {
        mouseButtonReleased = false;
        is_once_played = false;
        GameObject.Find("dummy1").GetComponent<global_check>().current_controlling_object_name = object_name;

        //print_msg1.text = "현재 컨트롤중인 오브젝트:" + current_control_object_name;
        offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }
    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x - offsetX, mousePosition.y - offsetY);

        mouseButtonReleased = false;
    }

    private void OnMouseUp()
    {
        SNAP();

        //print_msg1.text = "현재 오브젝트의 포지션: (" + this.gameObject.transform.position.x + ", " + this.gameObject.transform.position.y + ")";
        mouseButtonReleased = true;

    }


    void pop_and_merge(GameObject this_obj, GameObject coll_obj) 
    {
        /*이슈: 
         */
        if (is_merge) 
        {
            string collision_obj_name = coll_obj.gameObject.name.Substring(0, 4);
            string my_object = this_obj.name.Substring(0, 4);

            int collision_obj_tier = int.Parse(coll_obj.name.Substring(4, 1));
            int my_object_tier = int.Parse(this.gameObject.name.Substring(4, 1));

            int my_object_categoey = 0; //나중에 변수 취급해서 카테고리별로 대응될 수 있도록 바꿔야 합니다.

            if (collision_obj_name == my_object)
            {
                Vector2 current_pos = new Vector2(this_obj.transform.position.x, this_obj.transform.position.y);
                //나중에 카테고리도 넣을 때 여기에다가 추가해서 넣어주세요.

                string load_tier = "c" + my_object_categoey.ToString() + "_t" + (collision_obj_tier + 1).ToString() + "_";
                //Debug.LogFormat("두 티어가 같습니다. 머지합니다.머지되는 티어:{0}", load_tier);

                //작게 사라지는 중에 오브젝트를 옮길 수가 있음. 사라질 때는 박스컬라이더를 끄기
                this_obj.GetComponent<BoxCollider2D>().enabled = false;
                coll_obj.GetComponent<BoxCollider2D>().enabled = false;



                if (defalut_obj_scale > 0.01)
                {
                    defalut_obj_scale = defalut_obj_scale - 0.02f;//  함수그래프로 크게 팝업 사이즈 나중에 조절해야함.
                    this_obj.transform.localScale = new Vector3(defalut_obj_scale, defalut_obj_scale, 1);
                    coll_obj.transform.localScale = new Vector3(defalut_obj_scale, defalut_obj_scale, 1);

                }
                if (defalut_obj_scale < 0.01) 
                {
                    defalut_obj_scale = 0.27f;

                    Destroy(coll_obj);
                    Destroy(this_obj);
                    Instantiate(Resources.Load(load_tier), current_pos, Quaternion.identity);
                }

            }
        }
    }
    //처음 로드될 때의 나타나는 방식을 표현...

    public Vector3 find_empty_short_distance_cell(Vector3 obj_position) 
    {
        List<List<bool>> is_Greed_full = GameObject.Find("dummy1").GetComponent<global_check>().is_Greed_full;
        List<(int row, int column)> empty_cell_list = new List<(int row, int column)>();
        for (int row_num = 0; row_num < 7; row_num++)
        {
            for (int col_num = 0; col_num < 8; col_num++)
            {
                if (!is_Greed_full[row_num][col_num])
                {
                    empty_cell_list.Add((row_num, col_num));
                }
            }
        }


        Vector3 current_object_position = obj_position;
        Vector3 close_point = new Vector3(0.0f, 0.0f, 0.0f); // 가장 가까운- 이동할 점을 여기에 저장할거임
        float short_distance = 100000.0f;// 충분히 큰 값으로 비교
        float distance;

        for (int count = 0; count < empty_cell_list.Count; count++)
        {
            int row = empty_cell_list[count].row;
            int column = empty_cell_list[count].column;
            distance = Mathf.Sqrt(Mathf.Pow(current_object_position.x - Greed[row][column].x, 2) + Mathf.Pow(current_object_position.y - Greed[row][column].y, 2));
            if (distance < short_distance)
            {
                short_distance = distance; // 스캔한 것들 중 가장 작은 거리 값
                close_point.x = Greed[row][column].x; close_point.y = Greed[row][column].y;
            }
        }
        return close_point;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {   // 마우스 버튼이 떼였고, 아직 플레이 된적 없으며, 현재 클릭되서 움직여지고 있는 오브젝트가 내 오브젝트랑 동일할때(컨트롤중인 오브젝트만 활성화하게)
        if (mouseButtonReleased && !is_once_played && object_name == GameObject.Find("dummy1").GetComponent<global_check>().current_controlling_object_name)
        {
            
            
            string collision_obj_name = collision.gameObject.name.Substring(0, 5);
            string my_object = this.gameObject.name.Substring(0, 5);
            int object_max_tier = object_tier["c0"];
            int current_object_tier = int.Parse(this.gameObject.name.Substring(4, 1));
            if (collision_obj_name == my_object && current_object_tier < object_max_tier)// 합쳐져 있는 오브젝트랑 현재 오브젝트랑 같은 종류인가? AND 그 오브젝트들이 최고 티어가 아닌가?
            {
                //Debug.Log("두 오브젝트가 동일합니다."+ my_object + collision_obj_name);
                //Debug.LogFormat("이 오브젝트가 가질 수 있는 최대 티어: {0}", object_max_tier);
                //Debug.LogFormat("파싱 테스트: {0}",current_object_tier);

                collobj = collision.gameObject;// 충돌한 오브젝트의 변수를 글로벌로 담는다.
                is_merge = true;
            }


            else
            {
                Debug.Log("두 오브젝트가 다릅니다!!."+ my_object + collision_obj_name);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // 이제 여기부터 가장 가까운 그리드 서치해서 배치하는 함수 작성.>>>>>>>>>>>>>>>>>>> 서치 알고리즘을 바꿔야합니다... 다시 작성해보기
                List<List<bool>> is_Greed_full = GameObject.Find("dummy1").GetComponent<global_check>().is_Greed_full;
                List<(int row, int column)> empty_cell_list = new List<(int row, int column)>();
                for (int row_num = 0; row_num < 7; row_num++)
                {
                    for (int col_num = 0; col_num < 8; col_num++)
                    {
                        if (!is_Greed_full[row_num][col_num]) 
                        {
                            empty_cell_list.Add((row_num, col_num));
                        }
                    }
                }


                Vector2 current_object_position = object_position;
                Vector2 close_point = new Vector2(0.0f, 0.0f); // 가장 가까운- 이동할 점을 여기에 저장할거임
                float short_distance = 100000.0f;// 충분히 큰 값으로 비교
                float distance;

                for (int count = 0; count < empty_cell_list.Count; count++) 
                {
                    int row = empty_cell_list[count].row;
                    int column = empty_cell_list[count].column;
                    distance = Mathf.Sqrt(Mathf.Pow(current_object_position.x - Greed[row][column].x, 2) + Mathf.Pow(current_object_position.y - Greed[row][column].y, 2));
                    if (distance < short_distance)
                    {
                        short_distance = distance; // 스캔한 것들 중 가장 작은 거리 값
                        close_point.x = Greed[row][column].x; close_point.y = Greed[row][column].y;
                    }
                }
                this.gameObject.transform.position = new Vector3(close_point.x, close_point.y, 0);
                // 이제 여기부터 가장 가까운 그리드 서치해서 배치하는 함수 작성.>>>>>>>>>>>>>>>>>>> 서치 알고리즘을 바꿔야합니다... 다시 작성해보기
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            }
            is_once_played = true;

        }


    }
    public string get_tier_name_number(string obj_name)
    {
        //입력된 현재 카테고리와 티어를 입력하면 그 카테고리와 티어 중 비어있는 n번째 파일 이름을 벹어냄

        //그 티어에 있는 카테고리의 번호를 저장할 배열
        List<int> category_tier_number = new List<int>();

        //string search_obj = "c" + category_num.ToString() + "_t" + tier_num.ToString();
        string search_obj = obj_name.Substring(0,5);

        string category_tier_str;
        string ret_number = "";
        allObjects = FindObjectsOfType<GameObject>(); // 씬에 있는 모든 오브젝트를 저장함
        bool is_number_full = true;

        foreach (GameObject obj in allObjects)
        {
            category_tier_str = obj.name.Substring(0, 5);

            if (category_tier_str == search_obj)//입력한 티어랑 그거랑 같을 경우
            {
                //Debug.LogFormat("앞에 카테고리, 티어값: {0} \n 비교중인 값: {1}", category_tier_str, search_obj);
                if (!obj.name.Contains("Clone")) //클론이라는 스트링이 배열에 들어가면 안되니깐 한번 체크
                {
                    //Debug.LogFormat("배열에 입력될 인트 값: {0}", obj.name.Substring(6));
                    int input_num = int.Parse(obj.name.Substring(6));
                    category_tier_number.Add(input_num); 
                }

            }
        }
        string result = string.Join(",", category_tier_number);
        //Debug.LogFormat("현재 할당된 번호들은 {0}.", result);

        int number_counter = 0;
        while (is_number_full)
        {
            if (category_tier_number.Contains(number_counter))//생성된 오브젝트 번호 중에 중복이라면 ++하고 다음 번호로
            {
                number_counter++;
            }
            if (!category_tier_number.Contains(number_counter))
            {
                ret_number = number_counter.ToString();
                is_number_full = false;
            }
        }
        return ret_number;
    }

    // Start is called before the first frame update
    void Start()
    {

        //print_msg1 = GameObject.Find("print_msg1").GetComponent<Text>();
        //print_msg1.text = "init message";

        Greed_Initialize();
        thisobj = this.gameObject;

        //모든 오브젝트는 처음 스케일 0에서 점점 커집니다. 그거 구현하기 전에 초기화
        is_play_first = false; // 처음 생성될 때 사용하는(스케일 0에서 커지는거)
        defalut_obj_scale = 0.0f;
        this.gameObject.transform.localScale = new Vector3(defalut_obj_scale, defalut_obj_scale, defalut_obj_scale);

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        object_name = this.gameObject.name;
        object_position = this.gameObject.transform.position;



        if (timer > waitingTime) 
        {
            //1. 오브젝트 처음 생성될때 팝업되게 생성하기.
            if (!is_play_first) // 초기 스케일이 0에서부터 점점 커지는 효과를 갖게 만듭니다.
            {
                //컬라이더 꺼서 마우스로 못옮기게 하기
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                //scale up to linear
                defalut_obj_scale = defalut_obj_scale + 0.02f;
                this.gameObject.transform.localScale = new Vector3(defalut_obj_scale, defalut_obj_scale, defalut_obj_scale);
                if (defalut_obj_scale >= 0.27) 
                {
                    is_play_first = true;
                    this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    this.gameObject.transform.localScale = new Vector3(0.27f, 0.27f, 0.27f);//혹시 큰 수 더하다가 0.27 넘으면 안되니깐 여기서 잡아주기


                }
            }

            pop_and_merge(thisobj, collobj);

            if (this.gameObject.name.Contains("Clone")) 
            {
                //Debug.Log("이름에 클론이 포함되어있습니다.");
                string nth_number = get_tier_name_number(this.gameObject.name.Substring(0,5));
                this.gameObject.name = this.gameObject.name.Substring(0,6) + nth_number;
            }

            if (Input.GetKey(KeyCode.W))
            {

            }
            timer = 0;
        }


    }
}
// 어차피 고정할거니깐 상관없나????

