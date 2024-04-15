using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Animator CardAnim;
    public void OnOpenCard() //ī�� ������
    {
        CardAnim.SetBool("isOpen",true);
        transform.Find("Front").gameObject.SetActive(true);
        transform.Find("Back").gameObject.SetActive(false);

        /* ���ӸŴ��� �̱���ȭ ���� �ּ�����
                if (gameManager.I.FirstCard == null)
                {
                    gameManager.I.FirstCard = gameObject;
                }
                else
                {
                    gameManager.I.SecondCard = gameObject;
                    gameManager.I.IsMatched();
                } 
        */
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
        CardAnim.SetBool("IsOpen", false);
        transform.Find("Front").gameObject.SetActive(false);
        transform.Find("Back").gameObject.SetActive(true);
    }
}
