using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cardPrefab : TurnBehaviour, IPointerExitHandler, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public KamenRider KamenRider;
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
    public bool isOpen=false;
    private Vector3 originalPosition;
    private List<CardPoisitionEvent> SlotEvent;

    public void SetSlotEvent(List<CardPoisitionEvent> e)
    {
        SlotEvent = e;
    }

    public cardPrefab(Card card)
    {
        Card = card;
    }

    public override void OnTurn()
    {
        base.OnTurn();
        if (isOnTable)
        {
            if(Card.OnTurn.Count > 0)
            {
                Card.OnTurn.Active(KamenRider,null);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Card == null)
        {
            Debug.LogError("Card is not assigned!");
            return;
        }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isOnHand)
        {
            originalPosition = transform.localPosition;
            // Thực hiện "nhảy" lên một chút
            transform.localPosition += new Vector3(0, 20, 0);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isOnHand)
            transform.localPosition = originalPosition;
    }

    private Vector3 originalPositionVt;
    private Transform originalParent;
    // Bắt đầu kéo
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPositionVt = transform.localPosition;
        originalParent = transform.parent;
    }


    // Khi kéo
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Di chuyển đối tượng theo vị trí của con trỏ chuột
        foreach(CardPoisitionEvent cardevent in SlotEvent)
        {
            cardevent.HighlightSlot();
        }
    }

    // Kết thúc kéo
    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset lại đối tượng nếu không thả vào vị trí hợp lệ
        transform.localPosition = originalPositionVt;
        transform.SetParent(originalParent);
        foreach (CardPoisitionEvent cardevent in SlotEvent)
        {
            cardevent.RemoveHighlight();
        }
    }
}
