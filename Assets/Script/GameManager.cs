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
    public Text ScoreTxt;

    public Card FirstCard;
    public Card SecondCard;
    public int CardCount = 0;
    public int MatchingCardCount = 0;

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
            GameOver();
            Time.timeScale = 0.0f;
        }

        // 30�� ����� ���� ���� �� ���� ���� �ǳ�

        time += Time.deltaTime; // �ð� �帧
        TimeTxt.text = time.ToString("N2"); // �ð� �帥 ��ŭ �ݿ�
    }

    public void Matched()
    {
        MatchingCardCount++;
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
        }
        FirstCard = null;
        SecondCard = null;
    }

    void GameOver()
    {
        EndTxt.SetActive(true);
        ScoreTxt.text = $"매칭 횟수 : {MatchingCardCount}회";
        Time.timeScale = 0.0f;
    }
}
