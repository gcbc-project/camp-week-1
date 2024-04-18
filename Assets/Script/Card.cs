using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Animator CardAnim;
    public int Index = 0;
    public string Name = "";        // 카드의 이름을 넣기위해 변수생성
    public SpriteRenderer CardImage;
    public GameObject Front;
    public GameObject Back;
    public Color TwinckleColor;

    public AudioClip FlipClip;

    AudioSource _audioSource;
    bool _isFlip = false;
    SpriteRenderer _cardBackSprite;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _cardBackSprite = Back.GetComponent<SpriteRenderer>();
    }

    public void OnCardSetting(CardInfo cardInfo)//ī�� �迭 ����
    {
        Index = cardInfo.Id;        //Index에 카드 이미지 번호를 넣어준다
        Name = cardInfo.Name;       // Name에 팀원의 이름을 넣어준다
        CardImage.sprite = Resources.Load<Sprite>($"Img{Index}");
    }

    public void OnOpenCard() //ī�� ������
    {
        _audioSource.PlayOneShot(FlipClip);

        OnCardFlipFront();

        if (!_isFlip)
        {
            _cardBackSprite.color = new Color(0.84313725f, 0.86666667f, 0.86274510f, 1f);
        }

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

    public void OnCloseCardInvoke()//ī�带 �ٽ� ������
    {
        CardAnim.SetBool("isOpen", false);
        transform.Find("Front").gameObject.SetActive(false);
        transform.Find("Back").gameObject.SetActive(true);
    }

    public void OnCardFlipFront()
    {
        CardAnim.SetBool("isOpen", true);
        transform.Find("Front").gameObject.SetActive(true);
        transform.Find("Back").gameObject.SetActive(false);
    }

   //카드 색상 랜덤 변경
   public void ChangeRandomColor()
    { 
        _cardBackSprite.color = TwinckleColor;
    }
}
