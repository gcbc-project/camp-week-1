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

    public Text TimeTxt; // �ð� ��
    public GameObject EndTxt;
    public GameObject TeamName; // 팀네임 텍스트 생성
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
        }

        // 30�� ����� ���� ���� �� ���� ���� �ǳ�

        time += Time.deltaTime; // �ð� �帧
        TimeTxt.text = time.ToString("N2"); // �ð� �帥 ��ŭ �ݿ�
    }

   
    public void Matched()
    {
        TeamName.SetActive(true);  // 텍스트 UI 켜주기
        if (FirstCard.Index == SecondCard.Index)
        {          
            FirstCard.OnDestroyCard();
            SecondCard.OnDestroyCard();
            CardCount -= 2;

            TeamName.GetComponent<Text>().text = FirstCard.Name.ToString();       //켜준 텍스트 UI에 이미지에 맞는 팀원 이름 띄워주기
            
            if (CardCount == 0)
            {
                GameOver();
            }
        }
        else
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
        Time.timeScale = 0.0f;
    }

    public void OnClosedTeamName()  // 텍스트 UI를 꺼주기 위한 함수 생성
    {
        TeamName.SetActive(false);      // 텍스트 UI 꺼주기
    }

}
