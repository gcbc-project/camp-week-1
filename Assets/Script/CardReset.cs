using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CardReset : MonoBehaviour
{
    List<Card> _cardArr = new List<Card>();
    Vector3[] _cardOriginalPositions;

    public void GetCardPosition()
    {
        CardFlip.Instance.FindAllCard(3);

        _cardArr = Board.CardObject.Where(card => card != null).ToList();

        _cardOriginalPositions = new Vector3[_cardArr.Count];
        for (int i = 0; i < _cardArr.Count; i++)
        {
            _cardOriginalPositions[i] = _cardArr[i].transform.position;
        }

        //all cards move to center
        PlayMoveCenterAnimation();

        Invoke("OnCardShuffle", 0.2f);
    }
    void PlayMoveCenterAnimation()
    {        
        Vector3 centerPos = new Vector3(0, -1.5f, 0);
        for (int i = 0; i < _cardOriginalPositions.Length; i++)
        {
            _cardArr[i].transform.position = centerPos;
            _cardArr[i].CardAnim.SetBool("isMoveCenter", true);
        }
    }

    private void OnCardShuffle()
    {
        //arr shuffle logic
        int n = _cardArr.Count;
        while (n > 1)
        {
            int k = UnityEngine.Random.Range(0, n);
            n--;
            Card temp = _cardArr[n];
            _cardArr[n] = _cardArr[k];
            _cardArr[k] = temp;
        }

        //move cards
        for (int i = 0; i < _cardArr.Count; i++)
        {
            // �̵��ϱ� ���� ī���� �ʱ� ��ġ�� �ǵ���
            _cardArr[i].transform.position = _cardOriginalPositions[i];
        }
    }


}
