using Assets;
using UnityEngine;

public class CardPosition : MonoBehaviour
{
    public SlotCard slot;
    public Vector2 coordinates;

    public CardPosition(float x, float y, string keyword)
    {
        this.slot.x = x;
        this.slot.y = y;
        this.slot.keyword = keyword;
        // Khởi tạo tọa độ dựa trên vị trí của Transform
        this.coordinates = new Vector2(x, y);
    }

    public void Highlight(bool highlight)
    {
        // Đổi màu hoặc hiệu ứng để làm sáng vị trí khi highlight = true
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = highlight ? Color.yellow : Color.white;
        }
    }
}
