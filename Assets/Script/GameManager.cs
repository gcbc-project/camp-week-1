using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Text TimeTxt; // �ð� ��
    float time = 0.0f; // �ð� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 30.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }

        // 30�� ����� ���� ���� �� ���� ���� �ǳ�

        time += Time.deltaTime; // �ð� �帧
        TimeTxt.text = time.ToString("N2"); // �ð� �帥 ��ŭ �ݿ�
    }
}
