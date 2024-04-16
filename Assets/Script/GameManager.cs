using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    public GameObject TeamName; // 팀네임 텍스트 생성
    public Card FirstCard;
    public Card SecondCard;
    public int CardCount = 0;
   

    private int _matchingCardCount = 0;
    private int _cardMatchScore = 0;
    private float _timeScore = 0f;
    private float _finalScore = 0.0f;

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
        _matchingCardCount++;
        TeamName.SetActive(true);  // 텍스트 UI 켜주기
        if (FirstCard.Index == SecondCard.Index)
        {          
            FirstCard.OnDestroyCard();
            SecondCard.OnDestroyCard();
            CardCount -= 2;

            TeamName.GetComponent<Text>().text = FirstCard.Name.ToString();       //켜준 텍스트 UI에 이미지에 맞는 팀원 이름 띄워주기
            _cardMatchScore += 5;
            if (CardCount == 0)
            {
                GameOver();
            }
        }
        else//Not Matched
        {
            TeamName.GetComponent<Text>().text = "실패";      //켜준 텍스트 UI에 실패 문구 띄워주기
            FirstCard.OnCloseCard();
            SecondCard.OnCloseCard();          
        }
        FirstCard = null;
        SecondCard = null;
        Invoke("OnClosedTeamName", 0.5f);   // 0.5초 동안 텍스트 UI를 보여준뒤 다시 UI꺼주기
    }

    void GameOver()
    {
        EndTxt.SetActive(true);
        CalculatedFinalScore();
        ScoreTxt.text = $"매칭시도 횟수 : {_matchingCardCount}회 \n 점수 : {_finalScore}";
        Time.timeScale = 0.0f;
    }

    void CalculatedFinalScore()
    {
        _timeScore = Mathf.Round(time - 30) * 5;
        _finalScore = _timeScore + _cardMatchScore - _matchingCardCount;
    }
    
    public float GetTime()
    {
        return time;
    }
    
    public void OnClosedTeamName()  // 텍스트 UI를 꺼주기 위한 함수 생성
    {
        TeamName.SetActive(false);      // 텍스트 UI 꺼주기
    }
}
