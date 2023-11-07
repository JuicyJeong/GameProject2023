using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // Start is called before the first frame update

    float timer;
    float waitingTime = 0.01667f;

    int press_counter = 0;
    int create_delay_counter = 0;
    string file_name;

    string object_name;

    Vector2 mousePosition;
    Vector3 object_pos;
    float offsetX, offsetY;

    bool mouseButtonReleased = false;
    bool is_once_played = false;
    bool is_create_delay = false;

    //얘네둘은 글로벌 체크 스크립트에서 따옵니다.
    List<List<(float x, float y)>> Greed = new List<List<(float x, float y)>>();
    public List<List<bool>> is_Greed_full = new List<List<bool>>();




    private void OnMouseDown()// 눌렀을때
    {
        press_counter = 0;
        mouseButtonReleased = false;
        //Debug.Log("누름");
    }
    private void OnMouseDrag()// 누르고 유지할 때
    {
        mouseButtonReleased = false;
    }

    private void OnMouseUp()//누르고 뗄 때
    {
        is_once_played = true;
        mouseButtonReleased = true;
        //Debug.Log("뗐음");

    }
    void object_create()
    {
        //여기에 물려있는 오브젝트의 이름을 불러옴. 그 불러온 이름의 번호에 따라 생성하는 아이템을 다르게 함
        // 지금 오브젝트의 포지션을 구함
        // 포지션에서 가장 가까운 순의 위치로 오브젝트를 생성함
        // 여기서 누르고 드래그의 시간 늘어나면 여기 다 비활성화 시키고 드래그 및 머지로 빠질 수 있도록 하자.
        //확률로 생성될 오브젝트의 티어. 1티어는 80%, 2티어는 20% 이런식으로?
        
        //string load_tier = "c0_t1_";
        string create_name = "item_obj/" + object_name + "1_";
        Vector3 create_pos = GameObject.Find("dummy1").GetComponent<DragObject>().find_empty_short_distance_cell(this.gameObject.transform.position);
        //Debug.Log("생성될 오브젝트의 위치:" + create_pos.x +create_pos.y);
        Instantiate(Resources.Load(create_name), create_pos, Quaternion.identity);

    }



    void Start()
    {
        file_name = this.gameObject.name;
        object_name = file_name.Substring(0,4);
        //Debug.LogFormat("file_name: {0}", file_name);
        //Debug.LogFormat("object_name: {0}", object_name);

        object_pos = this.gameObject.transform.position;
        Greed = GameObject.Find("dummy1").GetComponent<global_check>().Greed;
        is_Greed_full = GameObject.Find("dummy1").GetComponent<global_check>().is_Greed_full;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            Greed = GameObject.Find("dummy1").GetComponent<global_check>().Greed;
            is_Greed_full = GameObject.Find("dummy1").GetComponent<global_check>().is_Greed_full;


            if (!mouseButtonReleased) { press_counter++; }


            if (mouseButtonReleased && press_counter < 20 && is_once_played)
            {
                //Debug.Log("드래그가 아닌 클릭으로 판정. 오브젝트를 생성합니다.");
                object_create();
                press_counter = 0;
                is_once_played = false;
                is_create_delay = true;
            }

            if (is_create_delay)// 오브젝트 생성에 딜레이를 줍니다. 연속으로 생성하면 같은 공간에 오브젝트가 생성되서...
            {
                create_delay_counter++;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;// 딜레이 도는 동안 클릭 비활성화
                if (create_delay_counter > 20) // 20프레임만큼 딜레이를 줄게요.
                {
                    create_delay_counter = 0;
                    is_create_delay = false;
                    this.gameObject.GetComponent<BoxCollider2D>().enabled = true;// 딜레이 끝나서 다시 활성화

                }
            }







            timer = 0;
        }
    }
}


