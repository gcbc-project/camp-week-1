using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimeTxt;
    public Animator TimeTxtAnim;
    public float WarningTime = 20.0f; //Ÿ�̸� �ִϸ��̼� ������ �ð�
    public float WarningBackground = 25.0f; //�ִϸ��̼� ������ �ð�

    private GameManager _gameManager; //���ӸŴ����� �����ϱ� ���� ����
    private Camera _mainCamera; //����ī�޶�

    public Color StartColor;
    public Color EndColor;
    public float ColorChangeDuration = 10.0f; //���� ���濡 �ɸ��� �ð�

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); //���ӸŴ��� ã��
        _mainCamera = Camera.main; //����ī�޶� ã��
    }

    // Update is called once per frame
    void Update()
    {
        float time = _gameManager.GetTime(); //time�� �ð� �ֱ�

        if(time >= WarningTime)
        {
            TimeTxtAnim.enabled = true;
        }
        
        //���� ����
        float t = Mathf.Clamp01((time - WarningBackground) / ColorChangeDuration * 2); //������ ���� �ð� ���� ���
        _mainCamera.backgroundColor = Color.Lerp(StartColor, EndColor, t);
    }
}
