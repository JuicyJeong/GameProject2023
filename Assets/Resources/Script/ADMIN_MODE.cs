using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADMIN_MODE : MonoBehaviour
{

    private KeyCode[] konamiCodeKeys = {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };
    private int currentIndex = 0;
    private bool ADMIN_MODE_ACTIVATED = false;
    void ENTER_ADMIN_MODE() 
    {
        // 키 입력을 확인
        if (Input.anyKeyDown)
        {
            // 다음 기대되는 키와 입력된 키가 일치하는지 확인
            if (Input.GetKeyDown(konamiCodeKeys[currentIndex]))
            {
                currentIndex++;
                // 모든 키가 올바르게 입력되었을 때
                if (currentIndex == konamiCodeKeys.Length)
                {
                    // 코나미 코드가 성공적으로 입력되었을 때 수행할 동작
                    Debug.Log("관리자 모드 진입.");
                    // 여기에 원하는 동작을 추가할 수 있습니다.
                    ADMIN_MODE_ACTIVATED = true;

                    // 코드를 재설정하여 다시 입력을 대기
                    currentIndex = 0;
                }
            }
            else
            {
                // 잘못된 키가 입력되었을 때 인덱스를 재설정
                currentIndex = 0;
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ENTER_ADMIN_MODE();
        if (ADMIN_MODE_ACTIVATED) 
        {
            
        }
    }
}
