using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CardReset : MonoBehaviour
{
    List<Card> _cardArr = new List<Card>();
    Vector3[] _cardOriginalPositions;

    IEnumerator CardShuffleRoutine()
    {
        // 중앙으로 이동
        PlayMoveCenterAnimation();
        yield return new WaitForSeconds(0.5f);

        // 카드 섞기
        OnCardShuffle();

        // 애니메이션 상태 리셋
        ResetAnimationStates();
    }

    void ResetAnimationStates()
    {
        foreach (var card in _cardArr)
        {
            card.CardAnim.SetBool("isMoveCenter", false);
            // 다른 애니메이션 상태들도 여기에서 리셋할 수 있습니다.
        }
    }

    public void GetCardPosition()
    {
        // CardFlip.Instance.FindAllCard(3);

        _cardArr = Board.CardObject.Where(card => card != null).ToList();

        _cardOriginalPositions = new Vector3[_cardArr.Count];
        for (int i = 0; i < _cardArr.Count; i++)
        {
            _cardOriginalPositions[i] = _cardArr[i].transform.position;
        }

        StartCoroutine(CardShuffleRoutine());
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
