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
    public GameObject TeamName; // 팀네임 텍스트 생성
    public Card FirstCard;
    public Card SecondCard;
    public int CardCount = 0;
    float _runningTime = 0.0f;

    private int _matchingCardCount = 0;
    private int _cardMatchScore = 0;
    private int _timeScore = 0;
    private int _finalScore = 0;

    [Header("Stage")]
    [Tooltip("현재 Stage Level 입력")]
    [SerializeField] int StageLevel;
    // time를 public로 아에 빼냄
    [Header("게임 종료")]
    [Tooltip("GameOverPanel Prefab을 넣는다")]
    [SerializeField] public GameObject GameOverPanel;

    [Header("시간 조정")]
    [Tooltip("전체 시간 조정")]
    [SerializeField] public float GameTime = 60.0f;

    [Tooltip("보너스 시간 조정")]
    [SerializeField] float PlusTime = 0.0f;

    [Tooltip("패널티 시간 조정")]
    [SerializeField] float FailTime = 0.0f;

    [Header("오디오")]
    public AudioClip MatchClip;
    public AudioClip MatchFailClip;
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;

        CardFlip.Instance.OnFlipCard(1);
        InitRunningTime();
    }

    void Update()
    {
        if (_runningTime >= 0.0f)
        {
            _runningTime -= Time.deltaTime;
        }

        else
        {
            GameOver();
            OverTime();
        }

        TimeTxt.text = _runningTime.ToString("N2");
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
            _runningTime += PlusTime;

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

            _runningTime -= FailTime;
        }
        FirstCard = null;
        SecondCard = null;
        Invoke("OnClosedTeamName", 0.5f);   // 0.5초 동안 텍스트 UI를 보여준뒤 다시 UI꺼주기
    }

    // 게임오버 함수를 밖으로 빼냄, 이를 통해 윗 구간에서 게임오버를 호출 할 수 있도록 바꿈
    void GameOver()
    {
        CalculatedFinalScore();
        Time.timeScale = 0.0f;
        GameOverPanel.GetComponent<GamePanel>().SetFinalScore(_matchingCardCount, _finalScore, CardCount == 0, StageLevel);
        GameOverPanel.SetActive(true);
    }

    void CalculatedFinalScore()
    {
        _timeScore = Convert.ToInt32(Mathf.Round(_runningTime - 30) * 5);
        _finalScore = _timeScore + _cardMatchScore - _matchingCardCount;
    }

    public float GetTime()
    {
        return _runningTime;
    }

    public void OnClosedTeamName()  // 텍스트 UI를 꺼주기 위한 함수 생성
    {
        TeamName.SetActive(false);      // 텍스트 UI 꺼주기
    }

    void OverTime()
    {
        // 시간을 무조건 0초로 맞춘다
        _runningTime = 0.0f;

        // 바꾼 시간을 시간판에 반영한다.
        TimeTxt.text = _runningTime.ToString("N2");
    }

    public void InitRunningTime()
    {
        _runningTime = GameTime;
    }
}
