using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public DrawCircle[] racers;

    void Start()
    {
        foreach (DrawCircle racer in racers)
        {
            racer.OnFinishLineReached.AddListener(HandleFinishLineReached);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueRace();
        }
        if (racers[0].isLowSpeed()&& racers[1].isLowSpeed())
        {
            foreach (DrawCircle racer in racers)
            {
                racer.SpeedUp();
            }
        }
    }
    void HandleFinishLineReached()
    {
        Debug.Log("A racer reached the finish line!");
        // Thực hiện hành động khác, như dừng tất cả các đối tượng hoặc chuyển cảnh
        // Ví dụ: Dừng tất cả các đối tượng
        foreach (DrawCircle racer in racers)
        {
            racer.ResetTurnMessager();
            racer.StopMovement();
            racer.SpeedDown();
        }
    }
    void ContinueRace()
    {
        Debug.Log("Continue Race!");
        // Thực hiện hành động khác, như dừng tất cả các đối tượng hoặc chuyển cảnh
        // Ví dụ: Dừng tất cả các đối tượng
        foreach (DrawCircle racer in racers)
        {
            racer.Continue();
        }
    }

}
