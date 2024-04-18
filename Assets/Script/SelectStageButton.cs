using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageButton : MonoBehaviour
{
    [SerializeField] private Text StageTitle;
    [SerializeField] private Text BestScoreTxt;
    [SerializeField] private GameObject LockOverlayPanel;

    private string _sceneName;

    public void InitStageButton(Stage stage, int bestScore, bool isLock)
    {
        StageTitle.text = "Stage" + stage.Level.ToString();
        BestScoreTxt.text = bestScore.ToString();
        _sceneName = stage.SceneName;

        LockOverlayPanel.SetActive(isLock);
        gameObject.GetComponent<Button>().interactable = !isLock;
    }

    public void OnClickEnterStage()
    {
        if (_sceneName != null)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
