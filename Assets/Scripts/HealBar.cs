using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    public Image[] healthBarForegrounds;
    public KamenRider KamenRider;

    public void UpdateHealthBar()
    {
        float healthPercentage = 0;

        if (KamenRider.FormMaxHP == 0)
        {
            healthBarForegrounds[0].fillAmount = 0;
        }
        else
        {
            healthPercentage = KamenRider.CurrentFormHP / KamenRider.FormMaxHP;
            healthBarForegrounds[0].fillAmount = healthPercentage;
        }
        // Tính phần trăm máu còn lại của nhân vật
        float healthPercentageMain = KamenRider.CurrentPlayerHP / KamenRider.PlayerMaxHP;

        // Nếu máu còn lại lớn hơn 50%, cây máu đầu tiên sẽ giảm
        if (healthPercentage > 0.5f)
        {
            healthBarForegrounds[1].fillAmount = 1.0f; // Cây máu đầu tiên vẫn đầy
            healthBarForegrounds[2].fillAmount = (healthPercentageMain - 0.5f) * 2.0f; // Cây máu thứ hai bắt đầu giảm
        }
        else
        {
            healthBarForegrounds[1].fillAmount = healthPercentageMain * 2.0f; // Cây máu đầu tiên giảm dần về 0
            healthBarForegrounds[2].fillAmount = 1.0f; // Cây máu thứ hai vẫn đầy
        }

    }
}
