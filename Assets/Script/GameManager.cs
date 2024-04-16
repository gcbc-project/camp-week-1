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
        if (time >= 30.0f)
        {
            GameOver();
        }

        time += Time.deltaTime; // 시간초
        TimeTxt.text = time.ToString("N2"); // 시간초를 시간판에 반영
    }

    public void Matched()
    {
        if (FirstCard.Index == SecondCard.Index)
        {
            FirstCard.OnDestroyCard();
            SecondCard.OnDestroyCard();
            CardCount -= 2;

            // 본래 게임 오버 함수를 출력하나, 카드 카운터가 0일시 시간이 30초 보다 적어도 30.00으로 만드므로, 게임오버를 호출하는게 아닌 따로 게임 끝 판넬과 시간 멈춤을
            // 부여
            if (CardCount == 0)
            {
                EndTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            FirstCard.OnCloseCard();
            SecondCard.OnCloseCard();

            // 시간 패널티
            time += 5.0f;

            FirstCard = null;
            SecondCard = null;
        }
    }

    // 게임오버 함수를 밖으로 빼냄, 이를 통해 윗 구간에서 게임오버를 호출 할 수 있도록 바꿈
    void GameOver()
    {
        // 게임 오버 호출 시 시간은 무조건 30.00f로 바꾼다. (32초든 31초든)
        time = 30.00f;

        // 바꾼 시간을 시간판에 반영한다.
        TimeTxt.text = time.ToString("N2");

        // 게임 끝 판넬을 불러온다
        EndTxt.SetActive(true);

        // 시간을 멈춘다
        Time.timeScale = 0.0f;
    }

}
