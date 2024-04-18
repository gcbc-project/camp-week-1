using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CardReset : MonoBehaviour
{
    List<GameObject> _cardArr = new List<GameObject>();
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
        Vector3 centerPos = new Vector3(0, -1.5f, 0);
        for (int i = 0; i < _cardArr.Count; i++)
        {
            _cardArr[i].transform.position = centerPos;
        }

        Invoke("OnCardShuffle", 0.2f);


    }
    private void OnCardShuffle()
    {
        //arr shuffle logic
        int n = _cardArr.Count;
        while (n > 1)
        {
            int k = UnityEngine.Random.Range(0, n);
            n--;
            GameObject temp = _cardArr[n];
            _cardArr[n] = _cardArr[k];
            _cardArr[k] = temp;
        }

        //move cards
        for (int i = 0; i < _cardArr.Count; i++)
        {
            // 이동하기 전에 카드의 초기 위치로 되돌림
            _cardArr[i].transform.position = _cardOriginalPositions[i];
        }
    }


}
