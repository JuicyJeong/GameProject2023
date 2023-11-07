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
            //Debug.LogFormat("감지된 버튼이 없습니다.");
            when_pressed_obj = "";
        }
        else 
        {
            when_pressed_obj = hit.collider.name;
            //Debug.LogFormat("마우스가 눌렸을 때의 클릭된 오브젝트:{0}", when_pressed_obj);
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
        if (when_pressed_obj == when_realesed_obj) 
        {
            //////////////////////////////머지보드////////////////////////////////////////
            if (when_pressed_obj == "MergeBoard_left_btn" && when_realesed_obj == "MergeBoard_left_btn")
            {
                Debug.Log("왼쪽버튼을 클릭했습니다. 퀘스트창을 띄웁니다.");
            }
            if (when_pressed_obj == "MergeBoard_center_btn" && when_realesed_obj == "MergeBoard_center_btn") 
            {
                Debug.Log("박스버튼을 클릭했습니다. 어셈블리씬으로 이동합니다.");
                SceneManager.LoadScene("Assembly");
            }
            if (when_pressed_obj == "MergeBoard_right_btn" && when_realesed_obj == "MergeBoard_right_btn")
            {
                Debug.Log("오른쪽버튼을 클릭했습니다. 카운터로 이동합니다.");
                SceneManager.LoadScene("Counter");
            }

            //////////////////////////////어셈블리////////////////////////////////////////
            if (when_pressed_obj == "Assembly_left_btn" && when_realesed_obj == "Assembly_left_btn")
            {
                Debug.Log("왼쪽버튼을 클릭했습니다. 퀘스트창을 띄웁니다.");
            }
            if (when_pressed_obj == "Assembly_center_btn" && when_realesed_obj == "Assembly_center_btn")
            {
                Debug.Log("박스버튼을 클릭했습니다. 머지보드씬으로 이동합니다.");
                SceneManager.LoadScene("MergeBoard");
            }
            if (when_pressed_obj == "Assembly_right_btn" && when_realesed_obj == "Assembly_right_btn")
            {
                Debug.Log("오른쪽버튼을 클릭했습니다. 카운터로 이동합니다.");
                SceneManager.LoadScene("Counter");
            }

            //////////////////////////////카운터////////////////////////////////////////
            if (when_pressed_obj == "Counter_left_btn" && when_realesed_obj == "Counter_left_btn")
            {
                Debug.Log("왼쪽버튼을 클릭했습니다. 퀘스트창을 띄웁니다.");
            }
            if (when_pressed_obj == "Counter_center_btn" && when_realesed_obj == "Counter_center_btn")
            {
                Debug.Log("박스버튼을 클릭했습니다. 어셈블리씬으로 이동합니다.");
                SceneManager.LoadScene("Assembly");
            }
            if (when_pressed_obj == "Counter_right_btn" && when_realesed_obj == "Counter_right_btn")
            {
                Debug.Log("오른쪽버튼을 클릭했습니다. 머지보드로 이동합니다.");
                SceneManager.LoadScene("MergeBoard");
            }

            when_pressed_obj = "";
            when_realesed_obj = "";
        }

    }
}
