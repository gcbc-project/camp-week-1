using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Animator CardAnim;
    public int Index = 0;
    public SpriteRenderer CardImage;
    public GameObject Front;
    public GameObject Back;

    public void OnCardSetting(int num)//ī�� �迭 ����
    {
        Index = num;
        CardImage.sprite = Resources.Load<Sprite>($"Img{Index}");
        Debug.Log("클릭");
    }

    public void OnOpenCard() //ī�� ������
    {
        Debug.Log("클릭");
        CardAnim.SetBool("isOpen", true);
        transform.Find("Front").gameObject.SetActive(true);
        transform.Find("Back").gameObject.SetActive(false);

        if (GameManager.Instance.FirstCard == null)
        {
            GameManager.Instance.FirstCard = this;
        }
        else
        {
            GameManager.Instance.SecondCard = this;
            GameManager.Instance.Matched();
        }
    }
    public void OnDestroyCard()//ī�尡 �´ٸ� ī�������Ʈ�� 1�ʵڿ� �ı�
    {
        Destroy(gameObject, 1.0f);
    }

    public void OnCloseCard()// ī�尡 �����ʴٸ� OnCloseCardInvoke�Լ��� 1�ʵ� ����
    {
        Invoke("OnCloseCardInvoke", 1.0f);
    }

    void OnCloseCardInvoke()//ī�带 �ٽ� ������
    {
        CardAnim.SetBool("isOpen", false);
        transform.Find("Front").gameObject.SetActive(false);
        transform.Find("Back").gameObject.SetActive(true);
    }
}
