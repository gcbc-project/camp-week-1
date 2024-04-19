using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public static CardFlip Instance;
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
        StartCoroutine(OnAllCardFlipFront(state));
    }
    IEnumerator OnAllCardFlipFront(int state)
    {
        for (int i = 0; i < _cardObjects.Count; i++)
        {
            if (_cardObjects[i] != null)
            {
                _cardObjects[i].OnCardFlipFront();
            }
        }
        if (state == 1)
            StartCoroutine(TimeTextActive(CardFlipTime));
        yield return new WaitForSeconds(CardFlipTime);
        StartCoroutine(OnAllCardFlipBack());

    }
    IEnumerator OnAllCardFlipBack()
    {
        for (int i = 0; i < _cardObjects.Count; i++)
        {
            if (_cardObjects[i] != null)
            {
                _cardObjects[i].OnCloseCardInvoke();
            }
        }
        yield break;
    }

    IEnumerator TimeTextActive(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.TimeTxt.gameObject.SetActive(true);
        GameManager.Instance.InitRunningTime();
    }
}
