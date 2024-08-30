using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPoisitionEvent : MonoBehaviour
{
    public Image Slot;
    public Image Border;
    private void Start()
    {
        RemoveHighlight();
    }
    public void HighlightSlot()
    {
        Border.gameObject.SetActive(true);
    }

    public void RemoveHighlight()
    {
        Border.gameObject.SetActive(false);
    }
}
