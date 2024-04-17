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
    
    
    public float time = 0.0f;

    public AudioClip MatchClip;
    public AudioClip MatchFailClip;
    AudioSource _audioSource;
  
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;

        CardFlip.Instance.OnFlipCard(1);
    }

    void Update()
    {
        if (time >= 30.0f)
        {
            OverTime();
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
            _audioSource.PlayOneShot(MatchClip);

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
            _audioSource.PlayOneShot(MatchFailClip);
            TeamName.GetComponent<Text>().text = "실패";      //켜준 텍스트 UI에 실패 문구 띄워주기

            FirstCard.OnCloseCard();
            SecondCard.OnCloseCard();
            time += 5.0f;
        }
        FirstCard = null;
        SecondCard = null;
        Invoke("OnClosedTeamName", 0.5f);   // 0.5초 동안 텍스트 UI를 보여준뒤 다시 UI꺼주기
    }

    // 게임오버 함수를 밖으로 빼냄, 이를 통해 윗 구간에서 게임오버를 호출 할 수 있도록 바꿈
    void GameOver()
    {
        // 게임 끝 판넬을 불러온다
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

    void OverTime()
    {
        // 시간을 무조건 30초로 맞춘다
        time = 30.00f;

        // 바꾼 시간을 시간판에 반영한다.
        TimeTxt.text = time.ToString("N2");
    }

    
}
