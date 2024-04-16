using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimeTxt;
    public Animator TimeTxtAnim;

    public float WarningTime = 20.0f; //�ִϸ��̼� ������ �ð�

    private GameManager _gameManager; //���ӸŴ����� �����ϱ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); //���ӸŴ��� ã��
    }

    // Update is called once per frame
    void Update()
    {
        float time = _gameManager.GetTime(); //time�� �ð� �ֱ�

        if(time >= WarningTime)
        {
            TimeTxtAnim.enabled = true;
        }
    }
}
