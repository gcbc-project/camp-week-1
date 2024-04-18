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
        //_lastMatchTime = _gameManager.MatchedTime;

        // CardCount가 변화하지 않은 경우
        if (time - _gameManager.MatchedTime >= MatchTimeout && time - activeHintTime >= MatchTimeout && isHint == false)
        {
            //_iastCardCountChangeTime = Time.time;
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

        int tempCardIdx = Board.CardObject[randomIdx].GetComponent<Card>().Index; //1개의 인덱스가 들어간 카드 인덱스를 가져옴

        List<GameObject> cardList = Board.CardObject.Where(card => card.GetComponent<Card>().Index == tempCardIdx).ToList(); //랜덤으로 뽑은 같은 값의 인덱스 카드 2개를 가져옴

        Color rColor = new Color(Random.value, Random.value, Random.value);

        cardList.ForEach(card => {
            card.GetComponent<Card>().TwinckleColor = rColor;
            card.GetComponent<Animator>().SetBool("isHint", true);
        });



        //randomIdx 문제가 해결되면
        /*if(randomIdx == 2번째 선택된 것){ //Board.CardObject _id
            해당 카드에 HintAnim.SetBool("isHint", true); 적용
        } else {
            2번째 선택된 것 다시 선택하기
        }
        
         */



        /*        GameObject randomCard = Board.CardObject[randomIdx]; // 선택된 무작위 카드 오브젝트

                // 선택된 카드 오브젝트에 애니메이션을 실행하는 코드를 여기에 작성
                randomCard.GetComponent<Animator>().SetBool("isHint", true);*/
    }


}
