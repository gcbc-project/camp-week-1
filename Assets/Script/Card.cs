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

    public void OnCardSetting(int num)//카드 배열 세팅
    {
        Index = num;
        CardImage.sprite = Resources.Load<Sprite> ($"img{Index}");
    }

    public void OnOpenCard() //카드 뒤집기
    {
        CardAnim.SetBool("isOpen",true);
        transform.Find("Front").gameObject.SetActive(true);
        transform.Find("Back").gameObject.SetActive(false);

        /* 게임매니저 싱글톤화 이후 주석삭제
                if (GameManager.I.FirstCard == null)
                {
                    GameManager.I.FirstCard = gameObject;
                }
                else
                {
                    GameManager.I.SecondCard = gameObject;
                    GameManager.I.IsMatched();
                } 
        */
    }
    public void OnDestroyCard()//카드가 맞다면 카드오브젝트를 1초뒤에 파괴
    {
        Destroy(gameObject, 1.0f);
    }

    public void OnCloseCard()// 카드가 맞지않다면 OnCloseCardInvoke함수를 1초뒤 실행
    {
        Invoke("OnCloseCardInvoke", 1.0f);
    }

    void OnCloseCardInvoke()//카드를 다시 뒤집기
    {
        CardAnim.SetBool("IsOpen", false);
        transform.Find("Front").gameObject.SetActive(false);
        transform.Find("Back").gameObject.SetActive(true);
    }
}
