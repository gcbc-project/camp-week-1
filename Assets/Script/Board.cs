using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject Card;
    void Start()//ī�� ���� �迭�� �ε��� ����
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(Card, this.transform);
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().OnCardSetting(arr[i]);
        }
        /* GameManager �̱���ȭ ���� �ּ� ����
        GameManager.instance.cardCount = arr.Length;
        */
    }


}


}
