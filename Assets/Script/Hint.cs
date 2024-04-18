using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public Animator HintAnim;

    [SerializeField] private float MatchTimeout = 5f; //성공 매칭이 없는 시간
    private float activeHintTime = 0.0f;
    private bool isHint = false;
    private GameManager _gameManager; //게임매니저에 접근하기 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponent<GameManager>();//게임매니저 찾기
    }

    // Update is called once per frame
    void Update()
    {
        float time = _gameManager.GlobalTime;

        if (time - _gameManager.MatchedTime >= MatchTimeout && time - activeHintTime >= MatchTimeout && isHint == false)
        {
            isHint = true;
            HintTwinkle();

            activeHintTime = _gameManager.GlobalTime;
            isHint = false;
        }
    }

    void HintTwinkle()
    {
        Board.CardObject = Board.CardObject.Where(card => card != null).ToList();

        int randomIdx = Random.Range(0, Board.CardObject.Count); // Board.CardObject 리스트의 인덱스 중 하나를 무작위로 선택

        int tempCardIdx = Board.CardObject[randomIdx].Index; //1개의 인덱스가 들어간 카드 인덱스를 가져옴

        List<Card> cardList = Board.CardObject.Where(card => card.Index == tempCardIdx).ToList(); //랜덤으로 뽑은 같은 값의 인덱스 카드 2개를 가져옴

        Color rColor = new Color(Random.value, Random.value, Random.value);

        cardList.ForEach(card =>
        {
            card.TwinckleColor = rColor;
            card.CardAnim.SetBool("isHint", true);
        });
    }
}
