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
        Quest_Dialog.text = "퀘스트를 위한 대화창입니다. 해당 데이터베이스 추가 예정입니다.";

    }
}
