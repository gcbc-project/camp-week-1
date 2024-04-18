using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{   
    [Header("해금 기능")]
    [Tooltip("2단계 잠금해제")]
    public GameObject Lock1;
    [Tooltip("3단계 잠금해제")]
    public GameObject Lock2;

    [Header("스테이지 이름 및 최고 점수 표시")]        
    public GameObject StageName1;

    public GameObject StageName2;

    public GameObject StageName3;

    [Header("스테이지 이름")]
     [SerializeField] string _stageName1 = "Stage 1";
     
     [SerializeField] string _stageName2 = "Stage 2";

     [SerializeField] string _stageName3 = "Stage 3";

    // Start is called before the first frame update
    void Start()
    { 
          // 스테이지 클리어 시 해금 기능 확인 위해 임시로 만든 코드
        PlayerPrefs.SetInt("StageLevel", 3);   // StageLevel 2를 저장  나중에 이부분은 없앨 예정
        PlayerPrefs.Save();                 //  나중에 이부분은 없앨 예정
        int _stageLevel = PlayerPrefs.GetInt("StageLevel"); // _stageLevel이라는 변수에 가져온 StageLevel 데이터를 초기화해준다.
        
        if(_stageLevel >= 2)    //Stage 1 클리어 했을 때  >=로 한 이유는 Stage2를 클리어 했을 때도 잠금 해제 되게 하기 위함
        {
            Lock1.SetActive(false);     //Lock1 잠금 해제
        }
        if(_stageLevel == 3)        //Stage 2 클리어 했을 때 
        {
            Lock2.SetActive(false);   //Lock2 잠금 해제
        } 
        //스테이지 이름 및 최고 점수 표시 임시 코드
        PlayerPrefs.SetInt("BestScore1", 301);     //Stage 1 의 최고점수  나중에 이부분은 없앨 예정
        PlayerPrefs.SetInt("BestScore2", 20);     //Stage 2 의 최고점수  나중에 이부분은 없앨 예정
        PlayerPrefs.SetInt("BestScore3", 10);
        float _bestScore1 = PlayerPrefs.GetInt("BestScore1");     //Stage 1 의 최고점수 가져오기
        float _bestScore2 = PlayerPrefs.GetInt("BestScore2");     //Stage 2 의 최고점수 가져오기
        float _bestScore3 = PlayerPrefs.GetInt("BestScore3");     //Stage 3 의 최고점수 가져오기
        //스테이지 1의 최고점수가 있을 시
        if(PlayerPrefs.HasKey("BestScore1"))
        {   
            //스테이지 이름과 최고 점수 표시
            StageName1.GetComponent<Text>().text = "   " + _stageName1 +"    " + "최고 점수: " + _bestScore1;    
        }
        else    // 최고 점수가 없을 시
        {
            //아무것도 출력하지 않는다.
            StageName1.GetComponent<Text>().text = "";          
        }
        //스테이지 2의 최고점수가 있을 시
        if(PlayerPrefs.HasKey("BestScore2"))
        {
            //스테이지 이름과 최고 점수 표시
            StageName2.GetComponent<Text>().text = "   " + _stageName2 +"    " + "최고 점수: " + _bestScore2;
        }
        else    //최고 점수가 없을 시
        {
            StageName2.GetComponent<Text>().text = "";
        }

        if(PlayerPrefs.HasKey("BestScore3"))
        {
            StageName3.GetComponent<Text>().text = "   " + _stageName3 +"    " + "최고 점수: " + _bestScore3; 
        }
        else
        {
            StageName3.GetComponent<Text>().text = "";
        }
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
