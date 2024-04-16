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

    public Text TimeTxt; 
    public GameObject EndTxt;
    public Text ScoreTxt;

    public Card FirstCard;
    public Card SecondCard;
    public int CardCount = 0;
    public int MatchingCardCount = 0;
    public int CardMatchScore = 0;
    public float TimeScore = 0f;

    public float FinalScore = 0.0f;
    float time = 0.0f;

    void Start()
    {
        Time.timeScale = 1.0f;
    }
    
    void Update()
    {
        if (time > 30.0f)
        {
            GameOver();
            Time.timeScale = 0.0f;
        }

        time += Time.deltaTime; 
        TimeTxt.text = time.ToString("N2");
    }

    public void Matched()
    {
        MatchingCardCount++;
        if (FirstCard.Index == SecondCard.Index) //Matched
        {
            FirstCard.OnDestroyCard();
            SecondCard.OnDestroyCard();
            CardCount -= 2;
            CardMatchScore += 5;
            if (CardCount == 0)
            {
                GameOver();
            }
        }
        else//Not Matched
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
        CalculatedFinalScore();
        ScoreTxt.text = $"매칭시도 횟수 : {MatchingCardCount}회 \n 점수 : {FinalScore}";
        Time.timeScale = 0.0f;
    }

    void CalculatedFinalScore()
    {
        TimeScore = Mathf.Round(time - 30) * 5;
        FinalScore = TimeScore + CardMatchScore - MatchingCardCount;
    }
}
