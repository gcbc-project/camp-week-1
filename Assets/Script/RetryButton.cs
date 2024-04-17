using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnEnterStageSelectScene()    // 스테이지 선택 씬으로 넘어가기
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
