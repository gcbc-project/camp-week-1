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
    public GameObject TransTime; // 시간 변화 표시

    public Card FirstCard;
    public Card SecondCard;
    public int CardCount = 0;

    public float MatchedTime = 0.0f; //매칭된 시간 저장용
    public float GlobalTime = 0.0f; //게임의 절대 시간

    float _runningTime = 0.0f;

    private int _matchingCardCount = 0;

    private int _notMatchingCardCount = 0; // 실패 횟 수

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
    public AudioClip AutoDestroySE;
    AudioSource _audioSource;

    [Header("난이도 조절")]
    [Tooltip("자동 파괴 배수 설정")]
    [SerializeField] int SetCount = 0;

    GameObject cardManager;

    CardReset cardReset;
    bool isCardReset = false;
    [Header("카드 셔플 여부")]
    [SerializeField] bool IsCardSuffle = true;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;

        cardManager = GameObject.Find("CardManager");
        cardReset = cardManager.GetComponent<CardReset>();
        CardFlip.Instance.OnFlipCard(1);
        InitRunningTime();
    }

    void Update()
    {
        if (_runningTime >= 0.0f)
        {
            _runningTime -= Time.deltaTime;
            GlobalTime += Time.deltaTime;

            if (IsCardSuffle)
            {
                if ((_runningTime < GameTime / 2) && !isCardReset)
                {
                    cardReset.GetCardPosition();
                    isCardReset = true;
                }
            }


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

        SetRandomPositionForTransTime(); 
        TransTime.SetActive(true);

        if (FirstCard != null && SecondCard != null)

            if (FirstCard.Index == SecondCard.Index)
            {
                _audioSource.PlayOneShot(MatchClip);

                FirstCard.OnDestroyCard();
                SecondCard.OnDestroyCard();

                CardCount -= 2;
                _runningTime += PlusTime;

                TeamName.GetComponent<Text>().text = FirstCard.Name.ToString();       //켜준 텍스트 UI에 이미지에 맞는 팀원 이름 띄워주기

                TransTime.GetComponent<Text>().text = $"{PlusTime}초 증가"; // 시간초 증가 출력

                _cardMatchScore += 5;

                if (CardCount == 0)
                {
                    GameOver();
                }

                //변수를 하나 만들어서 현재 time을 변수에 저장하고, 이 변수를 Hint 스크립트로 가져가기
                MatchedTime = GlobalTime;

            }
            else//Not Matched
            {
                _audioSource.PlayOneShot(MatchFailClip);
                TeamName.GetComponent<Text>().text = "실패";      //켜준 텍스트 UI에 실패 문구 띄워주기

                TransTime.GetComponent<Text>().text = $"{FailTime}초 감소";

                FirstCard.OnCloseCard();
                SecondCard.OnCloseCard();

                _runningTime -= FailTime;

                FindAndDestroyMatch();
            }

        FirstCard = null;
        SecondCard = null;
        Invoke("OnClosedTeamName", 0.5f);   // 0.5초 동안 텍스트 UI를 보여준뒤 다시 UI꺼주기

        Invoke("OnClosedTransTime", 0.5f); 
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
        GlobalTime = 0.0f;
    }

    public void OnClosedTransTime()  // 텍스트 UI를 꺼주기 위한 함수 생성
    {
        Debug.Log("시간 알림 껏당");
        TransTime.SetActive(false);      // 텍스트 UI 꺼주기
    }

    // 랜덤 위치 생성
    void SetRandomPositionForTransTime()
    {
        RectTransform rt = TransTime.GetComponent<RectTransform>();
        if (rt != null)
        {
            // 무작위 위치 설정
            float randomX = UnityEngine.Random.Range(-330.0f, 330f);  // X 범위 설정
            float randomY = UnityEngine.Random.Range(295.0f, 590.0f);  // Y 범위 설정
            rt.anchoredPosition = new Vector2(randomX, randomY);
        }
    }

    // 카드 자동파괴 로직
    public void FindAndDestroyMatch()
    {
        _notMatchingCardCount++;
        if (SetCount > 0 && _notMatchingCardCount % SetCount == 0)
        {
            _audioSource.PlayOneShot(AutoDestroySE);

            // 모든 카드를 비교해서 일치하는 쌍을 찾는다
            for (int i = Board.CardObject.Count - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    // 리스트크기를 줄이는 대신, 리스트 내에서 null 값이 아닌 것을 찾아서 삭제 함,
                    if (Board.CardObject[i] != null && Board.CardObject[j] != null)
                    {
                        Card firstCard = Board.CardObject[i];
                        Card SecondCard = Board.CardObject[j];

                        if (firstCard != null && SecondCard != null && firstCard.Index == SecondCard.Index)
                        {
                            firstCard.OnDestroyCard();
                            SecondCard.OnDestroyCard();

                            Board.CardObject.RemoveAt(i);
                            Board.CardObject.RemoveAt(j); // i와 j를 제거

                            CardCount -= 2;
                            return;
                        }
                    }
                }
            }
        }

    }
}
