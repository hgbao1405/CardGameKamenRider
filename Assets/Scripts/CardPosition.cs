using UnityEngine;

public class CardPosition : MonoBehaviour
{
    public float x;
    public float y;
    public string keyword;       // Lưu keyword của vị trí
    public Vector2 coordinates;

    public CardPosition(float x, float y, string keyword)
    {
        this.x = x;
        this.y = y;
        this.keyword = keyword;
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
