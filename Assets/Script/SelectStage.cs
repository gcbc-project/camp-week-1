using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage
{
    public int Level { get; }
    public string SceneName { get; }

    public Stage(int level, string sceneName)
    {
        Level = level;
        SceneName = sceneName;
    }
}

public class SelectStage : MonoBehaviour
{
    List<Stage> stageInfo = new List<Stage> { new Stage(1, "StageLevel1"), new Stage(2, "StageLevel2"), new Stage(3, "StageLevel3") };

    [SerializeField] private GameObject SelectStageBtn;

    void Start()
    {
        stageInfo.ForEach(stageInfo =>
        {
            GameObject tempStageButton = Instantiate(SelectStageBtn, this.transform);
            bool isLock = false;
            if (!PlayerPrefs.HasKey("StageLevel"))
            {
                PlayerPrefs.SetInt("StageLevel", 1);
            }
            isLock = PlayerPrefs.GetInt("StageLevel") < stageInfo.Level;

            string bestScoreKey = "Stage" + stageInfo.Level + "BestScore";

            if (!PlayerPrefs.HasKey(bestScoreKey))
            {
                PlayerPrefs.SetInt(bestScoreKey, 0);
            }

            tempStageButton.GetComponent<SelectStageButton>().InitStageButton(stageInfo, PlayerPrefs.GetInt(bestScoreKey), isLock);
        });
    }
}
