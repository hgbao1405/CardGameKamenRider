using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cardPrefab : MonoBehaviour
{
    public Card Card;
    public Image Avatar;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Decription;

    public cardPrefab(Card card)
    {
        Card = card;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
