using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [Header("Stage")]
    [Tooltip("마지막 Stage인 경우 True 변경, 아닌 경우 False")]
    [SerializeField] bool IsLastStage = false;
    [Tooltip("다음 Stage Scene 이름을 입력, Next 버튼 클릭 시 해당 씬으로 이동")]
    [SerializeField] string NextStageName;

    [Header("Buttons")]
    [SerializeField] Image RetryBtn;
    [SerializeField] Image HomeBtn;
    [SerializeField] Image NextBtn;

    [Header("Others")]
    [SerializeField] GameObject OverlayPanel;

    void Start()
    {
        if (IsLastStage)
        {
            if (NextBtn != null)
            {
                NextBtn.enabled = false;
                foreach (Transform child in NextBtn.transform)
                {
                    child.gameObject.SetActive(false);
                }

                RetryBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100f, 30);
                HomeBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(100f, 30);
            }
        }
    }

    public void OnClickRetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClickHomeBtn()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
    public void OnClickNextBtn()
    {
        if (NextStageName != null)
        {
            SceneManager.LoadScene(NextStageName);
        }
    }
    public void OnClickPauseBtn()
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(true);
        OverlayPanel.SetActive(true);
    }
    public void OnClickPlayBtn()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        OverlayPanel.SetActive(false);
    }
}