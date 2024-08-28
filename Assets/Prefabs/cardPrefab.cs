using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cardPrefab : TurnBehaviour
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
        if(Card.IsHasCouter && Card.counter.Value > 0)
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

}
