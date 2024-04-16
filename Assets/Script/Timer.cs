using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimeTxt;
    public Animator TimeTxtAnim;

    public float WarningTime = 20.0f; //애니메이션 시작할 시간

    private GameManager _gameManager; //게임매니저에 접근하기 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); //게임매니저 찾기
    }

    // Update is called once per frame
    void Update()
    {
        float time = _gameManager.GetTime(); //time에 시간 넣기

        if(time >= WarningTime)
        {
            TimeTxtAnim.enabled = true;
        }
    }
}
