using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public Animator HintAnim;

    [SerializeField] private float MatchTimeout = 5f; //���� ��Ī�� ���� �ð�
    private float activeHintTime = 0.0f;
    private bool isHint = false;
    private GameManager _gameManager; //���ӸŴ����� �����ϱ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponent<GameManager>();//���ӸŴ��� ã��
    }

    // Update is called once per frame
    void Update()
    {
        float time = _gameManager.GlobalTime;
        //_lastMatchTime = _gameManager.MatchedTime;

        // CardCount�� ��ȭ���� ���� ���
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

        int randomIdx = Random.Range(0, Board.CardObject.Count); // Board.CardObject ����Ʈ�� �ε��� �� �ϳ��� �������� ����

        int tempCardIdx = Board.CardObject[randomIdx].Index; //1���� �ε����� �� ī�� �ε����� ������

        List<Card> cardList = Board.CardObject.Where(card => card.Index == tempCardIdx).ToList(); //�������� ���� ���� ���� �ε��� ī�� 2���� ������

        Color rColor = new Color(Random.value, Random.value, Random.value);

        cardList.ForEach(card =>
        {
            card.TwinckleColor = rColor;
            card.CardAnim.SetBool("isHint", true);
        });
    }
}
