using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{
    //////////////////////////////VAR//////////////////////////////
    ///
    Vector2 mousePosition;

    bool mouseButtonReleased = true;
    bool mouseButtonPressed = false;
    bool is_cursor_on = false;
    string when_pressed_obj, when_realesed_obj;

    //////////////////////////////VAR//////////////////////////////

    private void OnMouseDown()
    {
        mouseButtonReleased = false;
        mouseButtonPressed = true;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.down, 1.0f);
        if (hit.collider == null)
        {
            //Debug.LogFormat("������ ��ư�� �����ϴ�.");
            when_pressed_obj = "";
        }
        else 
        {
            when_pressed_obj = hit.collider.name;
            //Debug.LogFormat("���콺�� ������ ���� Ŭ���� ������Ʈ:{0}", when_pressed_obj);
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
            //Debug.LogFormat("������ ��ư�� �����ϴ�.");
            when_realesed_obj = "";
        }
        else
        {
            when_realesed_obj = hit.collider.name;
            //Debug.LogFormat("���콺�� ������ ���� Ŭ���� ������Ʈ:{0}", when_realesed_obj);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (when_pressed_obj == when_realesed_obj) 
        {
            //////////////////////////////��������////////////////////////////////////////
            if (when_pressed_obj == "MergeBoard_left_btn" && when_realesed_obj == "MergeBoard_left_btn")
            {
                Debug.Log("���ʹ�ư�� Ŭ���߽��ϴ�. ����Ʈâ�� ���ϴ�.");
            }
            if (when_pressed_obj == "MergeBoard_center_btn" && when_realesed_obj == "MergeBoard_center_btn") 
            {
                Debug.Log("�ڽ���ư�� Ŭ���߽��ϴ�. ����������� �̵��մϴ�.");
                SceneManager.LoadScene("Assembly");
            }
            if (when_pressed_obj == "MergeBoard_right_btn" && when_realesed_obj == "MergeBoard_right_btn")
            {
                Debug.Log("�����ʹ�ư�� Ŭ���߽��ϴ�. ī���ͷ� �̵��մϴ�.");
                SceneManager.LoadScene("Counter");
            }

            //////////////////////////////�����////////////////////////////////////////
            if (when_pressed_obj == "Assembly_left_btn" && when_realesed_obj == "Assembly_left_btn")
            {
                Debug.Log("���ʹ�ư�� Ŭ���߽��ϴ�. ����Ʈâ�� ���ϴ�.");
            }
            if (when_pressed_obj == "Assembly_center_btn" && when_realesed_obj == "Assembly_center_btn")
            {
                Debug.Log("�ڽ���ư�� Ŭ���߽��ϴ�. ������������� �̵��մϴ�.");
                SceneManager.LoadScene("MergeBoard");
            }
            if (when_pressed_obj == "Assembly_right_btn" && when_realesed_obj == "Assembly_right_btn")
            {
                Debug.Log("�����ʹ�ư�� Ŭ���߽��ϴ�. ī���ͷ� �̵��մϴ�.");
                SceneManager.LoadScene("Counter");
            }

            //////////////////////////////ī����////////////////////////////////////////
            if (when_pressed_obj == "Counter_left_btn" && when_realesed_obj == "Counter_left_btn")
            {
                Debug.Log("���ʹ�ư�� Ŭ���߽��ϴ�. ����Ʈâ�� ���ϴ�.");
            }
            if (when_pressed_obj == "Counter_center_btn" && when_realesed_obj == "Counter_center_btn")
            {
                Debug.Log("�ڽ���ư�� Ŭ���߽��ϴ�. ����������� �̵��մϴ�.");
                SceneManager.LoadScene("Assembly");
            }
            if (when_pressed_obj == "Counter_right_btn" && when_realesed_obj == "Counter_right_btn")
            {
                Debug.Log("�����ʹ�ư�� Ŭ���߽��ϴ�. ��������� �̵��մϴ�.");
                SceneManager.LoadScene("MergeBoard");
            }

            when_pressed_obj = "";
            when_realesed_obj = "";
        }

    }
}
