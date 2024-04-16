using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimeTxt;
    public Animator TimeTxtAnim;
    public Color StartColor;
    public Color EndColor;

    private GameManager _gameManager; //���ӸŴ����� �����ϱ� ���� ����
    private Camera _mainCamera; //����ī�޶�
    private float _warningTime = 20.0f; //Ÿ�̸� �ִϸ��̼� ������ �ð�
    private float _warningBackground = 25.0f; //�ִϸ��̼� ������ �ð�
    private float _colorChangeDuration = 10.0f; //���� ���濡 �ɸ��� �ð�

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); //���ӸŴ��� ã��
        _mainCamera = Camera.main; //����ī�޶� ã��
    }

    // Update is called once per frame
    void Update()
    {
        float time = _gameManager.GetTime(); //time�� �ð� �ֱ�

        if(time >= _warningTime)
        {
            TimeTxtAnim.enabled = true;
        }

        //���� ����           (time - ���� �ð�) / ���濡 �ɸ��� �ð�
        float t = Mathf.Clamp01((time - _warningBackground) / _colorChangeDuration * 2); //������ ���� �ð� ���� ���
        _mainCamera.backgroundColor = Color.Lerp(StartColor, EndColor, t);
    }
}
