using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Animator CardAnim;
    public void OnOpenCard() //카드 뒤집기
    {
        CardAnim.SetBool("isOpen",true);
        transform.Find("Front").gameObject.SetActive(true);
        transform.Find("Back").gameObject.SetActive(false);

        /* 게임매니저 싱글톤화 이후 주석삭제
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
    public void OnDestroyCard()
    {
        Destroy(gameObject, 1.0f);
    }
    public void OnCloseCard()
    {
        Invoke("OnCloseCardInvoke", 1.0f);
    }
    void OnCloseCardInvoke()
    {
        CardAnim.SetBool("IsOpen", false);
        transform.Find("Front").gameObject.SetActive(false);
        transform.Find("Back").gameObject.SetActive(true);
    }
}
