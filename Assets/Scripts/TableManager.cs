using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public GameObject slotPrefab; // Tham chiếu đến Prefab Slot
    public List<CardPosition> slotCardPositions;

    void Start()
    {
        LoadSlots();
    }
    void Awake()
    {
        // Khởi tạo các tham chiếu hoặc biến nếu cần

        List<CardPosition> list = new List<CardPosition>();
        list.Add(new CardPosition(-226, 300, "CoreHeadMedal"));
        list.Add(new CardPosition(-155, 300, "CoreArmMedal"));
        list.Add(new CardPosition(-84, 300, "CoreLegMedal"));

        slotCardPositions = list;
    }
    void LoadSlots()
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

