using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static CSVReader;


public class Counter_Dialog : MonoBehaviour
{

    TextMeshProUGUI Quest_Dialog;

    // Start is called before the first frame update
    void Start()
    {
        Quest_Dialog = GameObject.Find("MSG_Dialog").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Quest_Dialog.text = "����Ʈ�� ���� ��ȭâ�Դϴ�. �ش� �����ͺ��̽� �߰� �����Դϴ�.";

    }
}
