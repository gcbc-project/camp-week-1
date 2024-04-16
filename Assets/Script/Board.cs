using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CardInfo       // 카드에 이미지와 이름을 넣기위한 클래스 생성
{
    public CardInfo(int id, string name)        //CardInfos에 이미지와 이름을 넣기위한 함수 생성 및 매개변수 생성
    {
        _id = id;           //_id에 id를 넣어줌
        _name = name;       //_name에 name을 넣어줌
    }

    private int _id;        
    private string _name;

    public int Id
    {
        get => _id;
        set => _id = value;
    }
    public string Name
    {
        get => _name;
        set => _name = value;
    }
}
public class Board : MonoBehaviour
{
    public GameObject Card;
    void Start()//ī�� ���� �迭�� �ε��� ����
    {   // 30장의 카드에 이미지 번호와 팀원 이름을 넣어준다
        CardInfo[] cardInfos = {new CardInfo(0, "박재민"), new CardInfo(0, "박재민"),
        new CardInfo(1, "이종윤"),new CardInfo(1, "이종윤"),
        new CardInfo(2, "박재민"),new CardInfo(2, "박재민"),
        new CardInfo(3, "김태형"),new CardInfo(3, "김태형"),
        new CardInfo(4, "박재민"),new CardInfo(4, "박재민"),
        new CardInfo(5, "유승아"),new CardInfo(5, "유승아"),
        new CardInfo(6, "유승아"),new CardInfo(6, "유승아"),
        new CardInfo(7, "유승아"),new CardInfo(7, "유승아"),
        new CardInfo(8, "이종윤"),new CardInfo(8, "이종윤"),
        new CardInfo(9, "이종윤"),new CardInfo(9, "이종윤"),
        new CardInfo(10, "김태형"),new CardInfo(10, "김태형"),
        new CardInfo(11, "이인호"),new CardInfo(11, "이인호"),
        new CardInfo(12, "이인호"),new CardInfo(12, "이인호"),
        new CardInfo(13, "이인호"),new CardInfo(13, "이인호"),
        new CardInfo(14, "김태형"),new CardInfo(14, "김태형")};

        cardInfos = cardInfos.OrderBy(x => Random.Range(0f, 14f)).ToArray();    // 그 30장의 카드를 랜덤으로 섞어준 다음 다시 배열로 나타낸다

        for (int i = 0; i < 30; i++)
        {
            GameObject tempCard = Instantiate(Card, this.transform);

            float x = (i % 5) * 1.1f - 2.2f;
            float y = (i / 5) * 1.1f - 4.0f;

            tempCard.transform.position = new Vector2(x, y);
            tempCard.GetComponent<Card>().OnCardSetting(cardInfos[i]);
        }
        // GameManager.Instance.cardCount = arr.Length;
    }
}
