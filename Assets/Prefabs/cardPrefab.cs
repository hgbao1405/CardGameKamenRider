using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// Kéo thả lá bài vào đúng vị trí
// Thay đổi hình dạng lá bài
// Lật,úp lá bài
public class CardPrefab : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public KamenRider KamenRider1;
    public Card Card;
    public Image Avatar;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Decription;
    public Image AvatarCounter;
    public Image BackImage;
    public TextMeshProUGUI NumberCounter;
    public bool isOnTable;
    public bool isOnHand;
    public bool isOnDeck;
    private bool isOpen;
    private Vector3 originalPosition;
    private List<CardPoisitionEvent> SlotEvent;
    private Transform Table;
    private Transform Hand;
    private Vector3 normalScale; // Kích thước ban đầu
    private Vector3 dragScale; // Kích thước khi kéo
    private bool IsHover;
    public GameObject cardInforPrefab;
    private Canvas canvas;

    void Awake()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>(); // Hoặc loại Collider phù hợp
        }
    }
    /// <summary>
    /// Hàm để cập nhật các vị trí để khi kéo sẽ có các vị trí thích hợp hiện lên
    /// </summary>
    public void UpdateTable(Transform Table)
    {
        this.Table = Table;
        GetSlot();
    }
    public void UpdateHand(Transform Hand)
    {
        this.Hand = Hand;
    }
    public CardPrefab(Card card)
    {
        Card = card;
    }

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        if (Card == null)
        {
            Debug.LogError("Card is not assigned!");
            return;
        }
        IsHover = false;
        isOpen = false;
        normalScale = Vector3.one;
        dragScale = new Vector3(0.006468854f, 0.006468854f, 1);
        Load();
    }

    public void Load()
    {
        isOnDeck = true;
        isOnHand = false;
        isOnTable = false;

        Title.text = Card.Name;
        Decription.text = Card.Description;
        Sprite newSprite = Resources.Load<Sprite>(Card.Avatar);

        if (newSprite != null)
        {
            Avatar.sprite = newSprite;
        }
        else
        {
            Debug.LogError("Sprite not found at path: " + Card.Avatar);
        }

        //NumberCounter.gameObject.SetActive(false);
        //AvatarCounter.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //Nếu có Couter thì hiển thị
        if(isOnTable && Card.IsHasCouter && Card.counter.Value > 0)
        {
            string imageURL = "Images/OOO/Couter/cellmedal.psd";
            NumberCounter.text= "x"+Card.counter.Value.ToString();

            Sprite newSprite = Resources.Load<Sprite>(imageURL);
            if (newSprite != null)
            {
                AvatarCounter.sprite = newSprite;
            }
            else
            {
                Debug.LogError("Sprite not found at path: " + imageURL);
            }
            NumberCounter.gameObject.SetActive(true);
            AvatarCounter.gameObject.SetActive(true);
        }
        else
        {
            NumberCounter.gameObject.SetActive(false);
            AvatarCounter.gameObject.SetActive(false);
        }

        BackImage.gameObject.SetActive(isOpen);
    }
    
    public void OnDraw()
    {
        isOnTable = false;
        isOnHand = true;
        isOnDeck = false;

    }
    public void SetTable()
    {
        isOnTable = true;
        isOnHand=false;
        isOnDeck = false;
    }
    public void ReturnHand()
    {
        isOnTable = false;
        isOnHand = true;
        isOnDeck = false;
    }
    public void ReturnDeck() 
    {
        isOnTable = false;
        isOnHand = false;
        isOnDeck = true;
    }

    private GameObject cardInforInstance;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isOnHand)
        {
            IsHover = true;
            originalPosition = transform.localPosition;
            // Thực hiện "nhảy" lên một chút
            transform.localPosition += new Vector3(0, 20, 0);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isOnHand)
        {
            transform.localPosition = originalPosition;
            IsHover = false;
        }
    }

    private Vector3 originalPositionVt;
    private Transform originalParent;

    private void GetSlot()
    {
        SlotEvent = new List<CardPoisitionEvent>();
        foreach (Transform Object in Table.GetComponentsInChildren<Transform>())
        {
            CardPosition cardPosition = Object.GetComponent<CardPosition>();
            if (cardPosition != null)
            {
                if (cardPosition.slot != null)
                {
                    if (Card.Keywords != null)
                        if (cardPosition != null && Card.Keywords.Contains(cardPosition.slot.keyword))
                        {
                            CardPoisitionEvent e = Object.GetComponent<CardPoisitionEvent>();
                            SlotEvent.Add(e);
                        }
                }
            }
        }
    }

    // Bắt đầu kéo
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPositionVt = transform.localPosition;

        //if (IsHover)
        //{
        //    originalPositionVt -= new Vector3(0, 20, 0);
        //}
        //transform.localScale = dragScale;
    }


    // Khi kéo
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Di chuyển đối tượng theo vị trí của con trỏ chuột
        foreach(CardPoisitionEvent cardevent in SlotEvent)
        {
            //Đánh đấu các vị trí hợp lệ
            cardevent.HighlightSlot();
        }
    }

    // Kết thúc kéo
    public void OnEndDrag(PointerEventData eventData)
    {
        //Nếu ở vị trí hợp lệ thì thu nhỏ lá bài, để dưới vị trí đó.
        CardPoisitionEvent cardPoisition = SlotEventNearDraggingCard();
        if (cardPoisition!=null)
        {
            SetCardToPosition(cardPoisition);
        }
        else
        {
            // Reset lại đối tượng nếu không thả vào vị trí hợp lệ
            DragingReturnHand();
            foreach (CardPoisitionEvent cardevent in SlotEvent)
            {
                cardevent.RemoveHighlight();
            }
        }
    }

    public void SetCardToPosition(CardPoisitionEvent cardPoisition)
    {
        if (cardPoisition != null)
        {
            GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f); // Pivot ở giữa
            transform.SetParent(cardPoisition.transform, false);
            transform.localPosition = new Vector2(0, 0);
            ScaleCard(true);
            SetTable();
        }
    }

    public void ScaleCard(bool isSlot)
    {
        if (isSlot)
        {
            Vector2 objectSize = GetComponent<RectTransform>().rect.size;
            Vector2 slotSize = new Vector2(0.8f, 1.4f);

            // Tính toán tỉ lệ cho cả hai chiều
            float widthScale = slotSize.x / objectSize.x;
            float heightScale = slotSize.y / objectSize.y;


            // Chọn tỉ lệ nhỏ hơn để đảm bảo đối tượng vừa với ô
            float scale = Mathf.Min(widthScale, heightScale);

            // Áp dụng tỉ lệ đó cho localScale
            GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
        }
    }

    public void DragingReturnHand()
    {
        transform.position = Hand.position;
        transform.SetParent(Hand);
        transform.localPosition = Vector2.zero;
        transform.localScale = normalScale;
        ReturnHand();
    }

    /// <summary>
    /// Kiểm tra có vị trí hợp lệ nào gần lá bài đang kéo
    /// </summary>
    /// <returns>
    ///     Trả về null nếu không có vị trí nào gần vị trí lá bài đang kéo.
    ///     Trả về CardPoisitionEvent nếu có vị trí gần trong khoảng cách xác định.
    /// </returns>
    private CardPoisitionEvent SlotEventNearDraggingCard()
    {
        float distanceThreshold = 50f;

        foreach (CardPoisitionEvent cardEvent in SlotEvent)
        {
            float distance = Vector2.Distance(cardEvent.transform.position, transform.position);

            if (distance <= distanceThreshold)
            {
                return cardEvent;
            }
        }

        return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CardPrefab clicked: " + gameObject.name);
        if (cardInforInstance != null)
        {
            CardInfor infor = cardInforInstance.GetComponentInChildren<CardInfor>();
            infor.DestroyCardInfor();
        }
        if (!isOpen)
        {
            cardInforInstance = Instantiate(cardInforPrefab, canvas.transform);
            cardInforInstance.transform.position =new Vector2(960,-540);
            CardInfor infor = cardInforInstance.GetComponentInChildren<CardInfor>();
            infor.SetCard(Card);
            infor.Load();
        }
    }

    //Thu nhỏ lá bài khi kéo và khi đang trên bàn
}
