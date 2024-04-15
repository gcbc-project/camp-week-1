using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Text TimeTxt; // 시간 판
    float time = 0.0f; // 시간 단위 설정

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 30.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }

        // 30초 경과시 게임 오버 및 게임 오버 판넬

        time += Time.deltaTime; // 시간 흐름
        TimeTxt.text = time.ToString("N2"); // 시간 흐른 만큼 반영
    }
}
