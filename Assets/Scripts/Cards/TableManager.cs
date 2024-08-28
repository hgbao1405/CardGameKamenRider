using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<CardPosition> slotCardPositions;

    public void UpdatePoisiton(CardPosition position)
    {
        slotCardPositions.Add(position);
        LoadSlots();
    }

    public void LoadSlots()
    {
        foreach (CardPosition slotPosition in this.slotCardPositions)
        {
            GameObject slot = Instantiate(slotPrefab, slotPosition.coordinates, Quaternion.identity);

            // Gán keyword cho Slot
            CardPosition cardPosition = slot.GetComponent<CardPosition>();
            if (cardPosition != null)
            {
                cardPosition.slot.keyword = slotPosition.slot.keyword;
            }

            // Gắn Slot vào Table
            slot.transform.SetParent(transform,false);
        }
    }
}

