using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardReset : MonoBehaviour
{
    GameObject[] cardArr;
    Vector3[] cardOriginalPositions;

    public void GetCardPosition()
    {
        CardFlip.Instance.FindAllCard(3);

        cardArr = new GameObject[CardFlip.Instance.CardObjects.Length];

        Array.Copy(CardFlip.Instance.CardObjects, cardArr, CardFlip.Instance.CardObjects.Length);
        
        cardOriginalPositions = new Vector3[cardArr.Length];
        for (int i = 0; i < cardArr.Length; i++)
        {
            cardOriginalPositions[i] = cardArr[i].transform.position;
        }

        //all cards move to center
        Vector3 centerPos = new Vector3(0, -1.5f, 0);
        for (int i = 0; i < cardArr.Length; i++)
        {           
            cardArr[i].transform.position = centerPos;
        }

        Invoke("OnCardShuffle", 0.2f);


    }
    private void OnCardShuffle()
    {
        //arr shuffle logic
        int n = cardArr.Length;
        while (n > 1)
        {
            int k = UnityEngine.Random.Range(0, n);
            n--;
            GameObject temp = cardArr[n];
            cardArr[n] = cardArr[k];
            cardArr[k] = temp;
        }

        //move cards
        for (int i = 0; i < cardArr.Length; i++)
        {
            // 이동하기 전에 카드의 초기 위치로 되돌림
            cardArr[i].transform.position = cardOriginalPositions[i];
        }
    }


}
