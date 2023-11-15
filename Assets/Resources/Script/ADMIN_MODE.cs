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
        // Ű �Է��� Ȯ��
        if (Input.anyKeyDown)
        {
            // ���� ���Ǵ� Ű�� �Էµ� Ű�� ��ġ�ϴ��� Ȯ��
            if (Input.GetKeyDown(konamiCodeKeys[currentIndex]))
            {
                currentIndex++;
                // ��� Ű�� �ùٸ��� �ԷµǾ��� ��
                if (currentIndex == konamiCodeKeys.Length)
                {
                    // �ڳ��� �ڵ尡 ���������� �ԷµǾ��� �� ������ ����
                    Debug.Log("������ ��� ����.");
                    // ���⿡ ���ϴ� ������ �߰��� �� �ֽ��ϴ�.
                    ADMIN_MODE_ACTIVATED = true;

                    // �ڵ带 �缳���Ͽ� �ٽ� �Է��� ���
                    currentIndex = 0;
                }
            }
            else
            {
                // �߸��� Ű�� �ԷµǾ��� �� �ε����� �缳��
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
