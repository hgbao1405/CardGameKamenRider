using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfor : MonoBehaviour
{
    private Card Card;
    public Image Avatar;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Decription;

    public void SetCard(Card Card)
    {
        this.Card = Card;
    }

    public void Load()
    {
        if (Card == null) return;

        Title.text = Card.Name;
        Decription.text = Card.Description;
        Sprite newSprite = Resources.Load<Sprite>(Card.Avatar);
        transform.localPosition = Vector2.zero;
        if (newSprite != null)
        {
            Avatar.sprite = newSprite;
        }
        else
        {
            Debug.LogError("Sprite not found at path: " + Card.Avatar);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyCardInfor()
    {
        Destroy(gameObject);
    }
}
