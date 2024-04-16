using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public Text TimeTxt; // �ð� ��
    public GameObject EndTxt;

    public Card FirstCard;
    public Card SecondCard;
    public int CardCount = 0;

    float time = 0.0f; // �ð� ���� ����

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
            EndTxt.SetActive(true);
            Time.timeScale = 0.0f;


            // 시간이 30초를 초과하거나 같으면 게임오버 처리
            if (time >= 30.0f)
            {
                GameOver();
            }
        }

        // 30�� ����� ���� ���� �� ���� ���� �ǳ�

        time += Time.deltaTime; // �ð� �帧
        TimeTxt.text = time.ToString("N2"); // �ð� �帥 ��ŭ �ݿ�
    }

    public void Matched()
    {
        if (FirstCard.Index == SecondCard.Index)
        {
            FirstCard.OnDestroyCard();
            SecondCard.OnDestroyCard();
            CardCount -= 2;
            if (CardCount == 0)
            {
                GameOver();
            }
        }
        else
        {
            FirstCard.OnCloseCard();
            SecondCard.OnCloseCard();

            // 30초 이하일 때만 패널티 적용
            if (time < 30.0f)
            {
                float newTime = time + 5.0f;

                // 30초를 초과하지 않도록 수정
                time = Math.Min(newTime, 30.0f);
            }
            FirstCard = null;
            SecondCard = null;
        }
    }

    // 게임오버 함수를 밖으로 빼냄, 이를 통해 윗 구간에서 게임오버를 호출 할 수 있도록 바꿈
    void GameOver()
    {
        // 시간을 정확히 30초로 설정
        time = 30.00f;
        TimeTxt.text = time.ToString("N2");
        EndTxt.SetActive(true);
        Time.timeScale = 0.0f;
    }

}
