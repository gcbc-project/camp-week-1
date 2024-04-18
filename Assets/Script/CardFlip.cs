using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public static CardFlip Instance;

    private Card[] _cardNum;

    [SerializeField] private float CardFlipTime = 2.0f;
    [SerializeField] private int CanCardFlipNum = 5;
    List<Card> _cardObjects = new List<Card>();

    public bool IsFlipCard = true;

    void Awake()
    {
        Instance = this;
    }

    public void OnFlipCard(int state)
    {
        if (IsFlipCard)
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
                case 3:
                    FindAllCard(state);
                    break;
            }
        }
        
    }

    public void FindAllCard(int state)
    {

        if (state == 1)
            GameManager.Instance.TimeTxt.gameObject.SetActive(false);
        else if (state == 2)
            CanCardFlipNum--;

        _cardObjects = Board.CardObject.Where(card => card != null).ToList();

        _cardNum = new Card[_cardObjects.Count];

        for (int i = 0; i < _cardObjects.Count; i++)
        {
            _cardNum[i] = _cardObjects[i];
        }
        OnAllCardFlipFront(state);
    }
    void OnAllCardFlipFront(int state)
    {
        for (int i = 0; i < _cardNum.Length; i++)
        {
            _cardNum[i].OnCardFlipFront();
        }
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
