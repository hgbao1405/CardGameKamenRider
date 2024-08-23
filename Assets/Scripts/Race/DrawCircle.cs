using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    public int segments = 100; // Số lượng đoạn để tạo đường tròn
    public float radius = 50.0f; // Bán kính của đường tròn
    public float width = 10f;
    private LineRenderer lineRenderer; 
    public GameObject finishLine; // Tham chiếu đến vạch đích
    public GameObject movingObject; // Tham chiếu đến đối tượng di chuyển
    public float startAngle = 0f; // Góc bắt đầu của đối tượng di chuyển

    public float speed = 10.0f;   // Tốc độ di chuyển
    public float maxSpeed = 50.0f;
    private float currentAngle;   // Góc hiện tại trên vòng tròn

    public TextMeshProUGUI speedtext;
    public TextMeshProUGUI TurnText;

    public bool isMoving = true;

    public UnityEvent OnFinishLineReached;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false; // Sử dụng không gian local của GameObject
        lineRenderer.positionCount = segments + 1; // Đặt số điểm cần vẽ
        lineRenderer.loop = true; // Đóng vòng để tạo đường tròn
        lineRenderer.startWidth = width; // Độ dày bắt đầu
        lineRenderer.endWidth = width;   // Độ dày kết thúc

        currentAngle = startAngle;
        TurnText.text = "";

        CreatePoints();
        PlaceMarkers();
        BindText();
    }

    void Update()
    {
        if (isMoving)
        {
            // Cập nhật góc hiện tại
            currentAngle += speed * Time.deltaTime;
            if (currentAngle >= 360f)
            {
                currentAngle -= 360f;
                isMoving = false;
                OnFinishLineReached?.Invoke();
                TurnText.text = "My turn";
            }

            UpdatePosition();
        }
    }

    void BindText()
    {
        speedtext.text = string.Format("{0} km/s",speed);
    }

    void UpdatePosition()
    {
        float radianAngle = Mathf.Deg2Rad * currentAngle;
        float x = Mathf.Cos(radianAngle) * radius;
        float y = Mathf.Sin(radianAngle) * radius;
        movingObject.transform.localPosition = new Vector2(x, y);
    }
    
    void CreatePoints()
    {
        float angle = 0f;
        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }


    void PlaceMarkers()
    {
        // Đặt vị trí vạch đích
        if (finishLine != null)
        {
            SetObjectOnCircle(finishLine, 360f);
        }

        // Đặt vị trí đối tượng di chuyển
        if (movingObject != null)
        {
            SetObjectOnCircle(movingObject, startAngle);
        }
    }

    // Hàm đặt đối tượng trên vòng tròn
    void SetObjectOnCircle(GameObject obj, float angle)
    {
        // Chuyển đổi góc độ sang radian
        float radianAngle = Mathf.Deg2Rad * angle;

        // Tính toán vị trí dựa trên bán kính và góc
        float x = Mathf.Cos(radianAngle) * radius;
        float y = Mathf.Sin(radianAngle) * radius;

        // Đặt vị trí cho đối tượng trong không gian 2D
        obj.transform.localPosition = new Vector2(x, y);
    }

    public void StopMovement()
    {
        isMoving = false;
    }
    public void Continue()
    {
        isMoving = true;
    }
    public void UpdateSpeed(float speednew)
    {
        speed = speednew;
        BindText();
    }

}
