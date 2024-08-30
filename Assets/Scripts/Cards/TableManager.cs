using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public GameObject slotPrefab;
    private List<GameObject> gameObjects;
    private void Start()
    {
    }

    public void UpdatePoisiton(CardPosition position)
    {
        GameObject slot = Instantiate(slotPrefab, position.coordinates, Quaternion.identity);

        // Gán keyword cho Slot
        CardPosition cardPosition = slot.GetComponent<CardPosition>();
        if (cardPosition != null)
        {
            cardPosition.slot=new Assets.SlotCard(position.slot.x,position.slot.y,position.slot.keyword);
        }

        slot.transform.SetParent(transform, false);

        if(gameObjects==null)
        {
            gameObjects = new List<GameObject>();
        }
        // Gắn Slot vào Table
        gameObjects.Add(slot);
    }

    public List<CardPoisitionEvent> GetSlotPositionEventByKeyWords(List<string> word)
    {
        List<CardPoisitionEvent> slots=new List<CardPoisitionEvent>();
        foreach (GameObject slot in gameObjects)
        {
            CardPosition cardPosition = slot.GetComponent<CardPosition>();
            if(cardPosition != null)
            {
                if (cardPosition.slot != null)
                {
                    if(word!=null)
                        if (cardPosition != null && word.Contains(cardPosition.slot.keyword))
                        {
                            CardPoisitionEvent e = slot.GetComponent<CardPoisitionEvent>();
                            slots.Add(e);
                        }
                }
            }
            
        }
        return slots;
    }
}

