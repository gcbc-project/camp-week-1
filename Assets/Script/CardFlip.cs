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
        //게임 시작과 동시에 실행되면 TimeTxt를 비활성화후 카드가 다시 뒤집어질때 활성화
        if (state == 1)
            GameManager.Instance.TimeTxt.gameObject.SetActive(false);
        if (state == 2)
            CanCardFlipNum--;


        //Scene에서 생성되어있는 Card를 모두 배열에 할당       
        GameObject[] cardObjects = GameObject.FindGameObjectsWithTag("Card");

        //배열의 크기를 카드의 개수로 설정
        _cardNum = new Card[cardObjects.Length];

        //카드 오브젝트들의 카드 컴포넌트를 _cardNum배열에 저장
        for (int i = 0; i < cardObjects.Length; i++)
        {
            Card cardComponent = cardObjects[i].GetComponent<Card>();

            _cardNum[i] = cardComponent;
        }
        //카드 앞면으로 뒤집기
        OnAllCardFlipFront(state);
    }
    void OnAllCardFlipFront(int state)
    {
        for (int i = 0; i < _cardNum.Length; i++)
        {
            _cardNum[i].OnCardFlipFront();
        }
        //게임 시작과 동시에 실행되면 TimeTxt를 비활성화후 카드가 다시 뒤집어질때 활성화
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
        GameManager.Instance.time = 0.0f;

    }
}
