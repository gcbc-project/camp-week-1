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
    // 3*6 4*6 5*6
    public GameObject Card;
    public int BoardX = 3;
    void Start()
    {
        int cardAmount = BoardX * 6;
        int numRows = 6;
        float cardSpacing = 1.1f;
        float cardYSpacing = 2.0f;

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

        cardInfos = cardInfos.Skip(0).Take(cardAmount).ToArray();
        cardInfos = cardInfos.OrderBy(x => Random.Range(0f, cardAmount / 2 - 1)).ToArray();

        for (int i = 0; i < cardAmount; i++)
        {
            GameObject tempCard = Instantiate(Card, this.transform);

            float x = (i % BoardX - (BoardX - 1) / 2.0f) * cardSpacing;
            float y = (numRows / 2.0f - i / BoardX) * cardSpacing - cardYSpacing;

            tempCard.transform.position = new Vector2(x, y);
            tempCard.GetComponent<Card>().OnCardSetting(cardInfos[i]);
        }
        GameManager.Instance.CardCount = cardInfos.Length;
    }
}
