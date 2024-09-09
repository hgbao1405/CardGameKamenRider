using Assets;
using UnityEngine;

public class CardPosition : MonoBehaviour
{
    public SlotCard slot;
    public Vector2 coordinates;

    public void SetCardPosition(float x, float y, string keyword)
    {
        this.slot=new SlotCard(x,y,keyword);
        // Khởi tạo tọa độ dựa trên vị trí của Transform
        this.coordinates = new Vector2(x, y); 
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            // Đặt vị trí anchoredPosition
            rectTransform.anchoredPosition = new Vector2(coordinates.x, coordinates.y);
        }
    }
}
