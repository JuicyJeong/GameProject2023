using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Text;

public class DragObject : MonoBehaviour
{
    string object_name;

    Vector2 object_position;
    Vector2 mousePosition;
    float offsetX, offsetY;

    bool mouseButtonReleased = false;
    bool is_once_played = false;
    bool is_merge = false;
    public Text print_msg1;

    float defalut_obj_scale = 0.15f;
    
    float timer;
    float waitingTime = 0.0166667f;

    List<List<(float x, float y)>> Greed = new List<List<(float x, float y)>>();

    GameObject thisobj;
    GameObject collobj;
    GameObject[] allObjects;

    // �� �࿡ ���� ����Ʈ�� �����ϰ� �ʱ�ȭ



    void Greed_Initialize() // play onlt once
    {
        float start_x, start_y, greed_distance;
        start_x = -6.0f; start_y = 5.0f; greed_distance = 2.0f;
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
    }

    void SNAP() 
    {
        //���� ������Ʈ�� ������ ����
        Vector2 current_object_position = object_position;
        Vector2 close_point = new Vector2(0.0f, 0.0f); // ���� �����- �̵��� ���� ���⿡ �����Ұ���
        float short_distance = 100000.0f;// ����� ū ������ ��
        float distance;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                distance = Mathf.Sqrt(Mathf.Pow(current_object_position.x - Greed[i][j].x, 2) + Mathf.Pow(current_object_position.y - Greed[i][j].y, 2));
                if (distance < short_distance)
                {
                    short_distance = distance; // ��ĵ�� �͵� �� ���� ���� �Ÿ� ��
                    close_point.x = Greed[i][j].x; close_point.y = Greed[i][j].y;

                }

            }
        }
        this.gameObject.transform.position = new Vector3(close_point.x, close_point.y, 0);
    }




    private void OnMouseDown()
    {
        mouseButtonReleased = false;
        is_once_played = false;
        GameObject.Find("dummy1").GetComponent<global_check>().current_controlling_object_name = object_name;

        //print_msg1.text = "���� ��Ʈ������ ������Ʈ:" + current_control_object_name;
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

        //print_msg1.text = "���� ������Ʈ�� ������: (" + this.gameObject.transform.position.x + ", " + this.gameObject.transform.position.y + ")";
        mouseButtonReleased = true;

    }


    void pop_and_merge(GameObject this_obj, GameObject coll_obj) 
    {
        /*�̽�: 
         */
        if (is_merge) 
        {
            string collision_obj_name = coll_obj.gameObject.name.Substring(0, 4);
            string my_object = this_obj.name.Substring(0, 4);

            int collision_obj_tier = int.Parse(coll_obj.name.Substring(4, 1));
            int my_object_tier = int.Parse(this.gameObject.name.Substring(4, 1));

            int my_object_categoey = 0; //���߿� ���� ����ؼ� ī�װ����� ������ �� �ֵ��� �ٲ�� �մϴ�.

            if (collision_obj_name == my_object)
            {
                Vector2 current_pos = new Vector2(this_obj.transform.position.x, this_obj.transform.position.y);
                //���߿� ī�װ��� ���� �� ���⿡�ٰ� �߰��ؼ� �־��ּ���.

                string load_tier = "c" + my_object_categoey.ToString() + "_t" + (collision_obj_tier + 1).ToString() + "_";
                //Debug.LogFormat("�� Ƽ� �����ϴ�. �����մϴ�.�����Ǵ� Ƽ��:{0}", load_tier);

                //�۰� ������� �߿� ������Ʈ�� �ű� ���� ����. ����� ���� �ڽ��ö��̴��� ����
                this_obj.GetComponent<BoxCollider2D>().enabled = false;
                coll_obj.GetComponent<BoxCollider2D>().enabled = false;



                if (defalut_obj_scale > 0.01)
                {
                    defalut_obj_scale = defalut_obj_scale - 0.005f;//  �Լ��׷����� ũ�� �˾� ������ ���߿� �����ؾ���.
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
    //ó�� �ε�� ���� ��Ÿ���� ����� ǥ��...
    void first_appearance()
    {
        //for test
        for (int i = 0; i < 100; i++) { }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {   // ���콺 ��ư�� ������, ���� �÷��� ���� ������, ���� Ŭ���Ǽ� ���������� �ִ� ������Ʈ�� �� ������Ʈ�� �����Ҷ�(��Ʈ������ ������Ʈ�� Ȱ��ȭ�ϰ�)
        if (mouseButtonReleased && !is_once_played && object_name == GameObject.Find("dummy1").GetComponent<global_check>().current_controlling_object_name)
        {
            
            
            string collision_obj_name = collision.gameObject.name.Substring(0, 5);
            string my_object = this.gameObject.name.Substring(0, 5);
            if (collision_obj_name == my_object) 
            {
                //Debug.Log("�� ������Ʈ�� �����մϴ�."+ my_object + collision_obj_name);
                collobj = collision.gameObject;// �浹�� ������Ʈ�� ������ �۷ι��� ��´�.
                is_merge = true;
            }
            if (collision_obj_name != my_object)
            {
                Debug.Log("�� ������Ʈ�� �ٸ��ϴ�!!."+ my_object + collision_obj_name);
                // ���� ������� ���� ����� �׸��� ��ġ�ؼ� ��ġ�ϴ� �Լ� �ۼ�.

            }
            is_once_played = true;

        }
        //if (mouseButtonReleased && !is_once_played && object_name == GameObject.Find("dummy1").GetComponent<global_check>().current_controlling_object_name)
        //{
        //    string collision_obj_name = collision.gameObject.name.Substring(0, 2);
        //    string my_object = this.gameObject.name.Substring(0, 2);

        //    collobj = collision.gameObject;// �浹�� ������Ʈ�� ������ �۷ι��� ��´�.
        //    is_merge = true;
        //    //////////////////////////////////pop_and_merge �Լ��� �̵�///////////////////////////
        //    //////////////////////////////////pop_and_merge �Լ��� �̵�///////////////////////////

        //    //int collision_obj_tier = int.Parse( collision.gameObject.name.Substring(1, 1));
        //    //int my_object_tier = int.Parse(this.gameObject.name.Substring(1, 1));

        //    ////Debug.LogFormat("������Ʈ�� ������ �ֽ��ϴ�. ������ ������Ʈ �̸��� '{0}' �Դϴ�.", collision_obj_name);
        //    ////Debug.LogFormat("��Ʈ���ϰ� �ִ� ������Ʈ �̸��� '{0}' �Դϴ�.", my_object);
        //    //if (collision_obj_name == my_object) 
        //    //{
        //    //    Vector2 current_pos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        //    //    string load_tier = "t" + (collision_obj_tier +1).ToString() +"_";
        //    //    Debug.LogFormat("�� Ƽ� �����ϴ�. �����մϴ�.�����Ǵ� Ƽ��:{0}", load_tier);





        //    //    //��Ÿ Ÿ�� �̿��ؼ� �������� 0���� ����� �� �ʿ���>> ��� ��ũ��Ʈ�� ���� ¥�� �ۼ��ؾ��� �� ����.
        //    //    //Instantiate(Resources.Load(load_tier), current_pos, Quaternion.identity);

        //    //    //Destroy(collision.gameObject);
        //    //    //Destroy(this.gameObject);

        //    //}
        //    //////////////////////////////////pop_and_merge �Լ��� �̵�///////////////////////////
        //    is_once_played = true;

        //}

    }
    string get_tier_name_number(string obj_name)
    {
        //�Էµ� ���� ī�װ��� Ƽ� �Է��ϸ� �� ī�װ��� Ƽ�� �� ����ִ� n��° ���� �̸��� �ʾ

        //�� Ƽ� �ִ� ī�װ��� ��ȣ�� ������ �迭
        List<int> category_tier_number = new List<int>();

        //string search_obj = "c" + category_num.ToString() + "_t" + tier_num.ToString();
        string search_obj = obj_name.Substring(0,5);

        string category_tier_str;
        string ret_number = "";
        allObjects = FindObjectsOfType<GameObject>(); // ���� �ִ� ��� ������Ʈ�� ������
        bool is_number_full = true;

        foreach (GameObject obj in allObjects)
        {
            category_tier_str = obj.name.Substring(0, 5);

            if (category_tier_str == search_obj)//�Է��� Ƽ��� �װŶ� ���� ���
            {
                //Debug.LogFormat("�տ� ī�װ�, Ƽ�: {0} \n ������ ��: {1}", category_tier_str, search_obj);
                if (!obj.name.Contains("Clone")) //Ŭ���̶�� ��Ʈ���� �迭�� ���� �ȵǴϱ� �ѹ� üũ
                {
                    //Debug.LogFormat("�迭�� �Էµ� ��Ʈ ��: {0}", obj.name.Substring(6));
                    int input_num = int.Parse(obj.name.Substring(6));
                    category_tier_number.Add(input_num);
                }

            }
        }
        string result = string.Join(",", category_tier_number);
        //Debug.LogFormat("���� �Ҵ�� ��ȣ���� {0}.", result);

        int number_counter = 0;
        while (is_number_full)
        {
            if (category_tier_number.Contains(number_counter))//������ ������Ʈ ��ȣ �߿� �ߺ��̶�� ++�ϰ� ���� ��ȣ��
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

        print_msg1 = GameObject.Find("print_msg1").GetComponent<Text>();
        print_msg1.text = "init message";

        Greed_Initialize();
        thisobj = this.gameObject;

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        object_name = this.gameObject.name;
        object_position = this.gameObject.transform.position;



        if (timer > waitingTime) 
        {

            pop_and_merge(thisobj, collobj);

            if (this.gameObject.name.Contains("Clone")) 
            {
                //Debug.Log("�̸��� Ŭ���� ���ԵǾ��ֽ��ϴ�.");
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

//�ؾ��Ұ�
//���� �ȵɶ� ������Ʈ ������ ���°�