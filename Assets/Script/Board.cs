using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject Card;
    void Start()//ī�� ���� �迭�� �ε��� ����
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14 };
        arr = arr.OrderBy(x => Random.Range(0f, 14f)).ToArray();

        for (int i = 0; i < 30; i++)
        {
            GameObject tempCard = Instantiate(Card, this.transform);

            float x = (i % 5) * 1.1f - 2.2f;
            float y = (i / 5) * 1.1f - 4.0f;

            tempCard.transform.position = new Vector2(x, y);
            Debug.Log(arr[i]);
            tempCard.GetComponent<Card>().OnCardSetting(arr[i]);
        }
        // GameManager.Instance.cardCount = arr.Length;
    }
}
