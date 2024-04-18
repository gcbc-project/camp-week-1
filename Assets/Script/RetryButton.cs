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

    public void GameExit()      // 게임 종료 기능
    {
        Application.Quit(); // 게임 종료 기능 
                            //유니티 에디터도 종료되게 해주는 기능
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    //에디터 상으로 게임이 종료되게 해준다.
#else
            Application.Quit();
#endif
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OnEnterStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
