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

    //��׵��� �۷ι� üũ ��ũ��Ʈ���� ���ɴϴ�.
    List<List<(float x, float y)>> Greed = new List<List<(float x, float y)>>();
    public List<List<bool>> is_Greed_full = new List<List<bool>>();




    private void OnMouseDown()// ��������
    {
        press_counter = 0;
        mouseButtonReleased = false;
        //Debug.Log("����");
    }
    private void OnMouseDrag()// ������ ������ ��
    {
        mouseButtonReleased = false;
    }

    private void OnMouseUp()//������ �� ��
    {
        is_once_played = true;
        mouseButtonReleased = true;
        //Debug.Log("����");

    }
    void object_create()
    {
        //���⿡ �����ִ� ������Ʈ�� �̸��� �ҷ���. �� �ҷ��� �̸��� ��ȣ�� ���� �����ϴ� �������� �ٸ��� ��
        // ���� ������Ʈ�� �������� ����
        // �����ǿ��� ���� ����� ���� ��ġ�� ������Ʈ�� ������
        // ���⼭ ������ �巡���� �ð� �þ�� ���� �� ��Ȱ��ȭ ��Ű�� �巡�� �� ������ ���� �� �ֵ��� ����.
        //Ȯ���� ������ ������Ʈ�� Ƽ��. 1Ƽ��� 80%, 2Ƽ��� 20% �̷�������?
        
        //string load_tier = "c0_t1_";
        string create_name = "item_obj/" + object_name + "1_";
        Vector3 create_pos = GameObject.Find("dummy1").GetComponent<DragObject>().find_empty_short_distance_cell(this.gameObject.transform.position);
        //Debug.Log("������ ������Ʈ�� ��ġ:" + create_pos.x +create_pos.y);
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
                //Debug.Log("�巡�װ� �ƴ� Ŭ������ ����. ������Ʈ�� �����մϴ�.");
                object_create();
                press_counter = 0;
                is_once_played = false;
                is_create_delay = true;
            }

            if (is_create_delay)// ������Ʈ ������ �����̸� �ݴϴ�. �������� �����ϸ� ���� ������ ������Ʈ�� �����Ǽ�...
            {
                create_delay_counter++;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;// ������ ���� ���� Ŭ�� ��Ȱ��ȭ
                if (create_delay_counter > 20) // 20�����Ӹ�ŭ �����̸� �ٰԿ�.
                {
                    create_delay_counter = 0;
                    is_create_delay = false;
                    this.gameObject.GetComponent<BoxCollider2D>().enabled = true;// ������ ������ �ٽ� Ȱ��ȭ

                }
            }







            timer = 0;
        }
    }
}


