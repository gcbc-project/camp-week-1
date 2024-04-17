using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public static CardFlip Instance;

    private Card[] _cardNum;
    [SerializeField]
    private float CardFlipTime = 2.0f;
    [SerializeField]
    private int CanCardFlipNum = 5;

    void Awake()
    {
        Instance = this;
    }

    public void OnFlipCard(int state)
    {
        switch (state)
        {
            case 1:
                FindAllCard(state);
                break;
            case 2:
                if (CanCardFlipNum <= 0)
                    break;
                else
                    FindAllCard(state);
                break;
        }
    }

    void FindAllCard(int state)
    {
        //���� ���۰� ���ÿ� ����Ǹ� TimeTxt�� ��Ȱ��ȭ�� ī�尡 �ٽ� ���������� Ȱ��ȭ
        if (state == 1)
            GameManager.Instance.TimeTxt.gameObject.SetActive(false);
        if (state == 2)
            CanCardFlipNum--;


        //Scene���� �����Ǿ��ִ� Card�� ��� �迭�� �Ҵ�       
        GameObject[] cardObjects = GameObject.FindGameObjectsWithTag("Card");

        //�迭�� ũ�⸦ ī���� ������ ����
        _cardNum = new Card[cardObjects.Length];

        //ī�� ������Ʈ���� ī�� ������Ʈ�� _cardNum�迭�� ����
        for (int i = 0; i < cardObjects.Length; i++)
        {
            Card cardComponent = cardObjects[i].GetComponent<Card>();

            _cardNum[i] = cardComponent;
        }
        //ī�� �ո����� ������
        OnAllCardFlipFront(state);
    }
    void OnAllCardFlipFront(int state)
    {
        for (int i = 0; i < _cardNum.Length; i++)
        {
            _cardNum[i].OnCardFlipFront();
        }
        //���� ���۰� ���ÿ� ����Ǹ� TimeTxt�� ��Ȱ��ȭ�� ī�尡 �ٽ� ���������� Ȱ��ȭ
        if (state == 1)
            Invoke("TimeTextActive", CardFlipTime);
        Invoke("OnAllCardFlipBack", CardFlipTime);

    }
    void OnAllCardFlipBack()
    {
        for (int i = 0; i < _cardNum.Length; i++)
        {
            _cardNum[i].OnCloseCardInvoke();
        }
    }

    void TimeTextActive()
    {
        GameManager.Instance.TimeTxt.gameObject.SetActive(true);
        GameManager.Instance.InitRunningTime();
    }
}
