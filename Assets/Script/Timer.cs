using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimeTxt;
    public Animator TimeTxtAnim;
    public float WarningTime = 20.0f; //타이머 애니메이션 시작할 시간
    public float WarningBackground = 25.0f; //애니메이션 시작할 시간

    private GameManager _gameManager; //게임매니저에 접근하기 위한 변수
    private Camera _mainCamera; //메인카메라

    public Color StartColor;
    public Color EndColor;
    public float ColorChangeDuration = 10.0f; //배경색 변경에 걸리는 시간

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); //게임매니저 찾기
        _mainCamera = Camera.main; //메인카메라 찾기
    }

    // Update is called once per frame
    void Update()
    {
        float time = _gameManager.GetTime(); //time에 시간 넣기

        if(time >= WarningTime)
        {
            TimeTxtAnim.enabled = true;
        }
        
        //배경색 변경
        float t = Mathf.Clamp01((time - WarningBackground) / ColorChangeDuration * 2); //보간에 사용될 시간 값을 계산
        _mainCamera.backgroundColor = Color.Lerp(StartColor, EndColor, t);
    }
}
