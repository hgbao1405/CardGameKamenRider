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
    public Transform Avatars;
    public TextMeshProUGUI Name;

    public void GetAvatars()
    {
        List<string> source = KamenRider.GetAvatars();
        foreach (string s in source)
        {
            // Tạo đối tượng GameObject chứa Image
            GameObject avatar = new GameObject("AvatarImage");

            // Thêm Image component vào GameObject
            Image image = avatar.AddComponent<Image>();

            // Gán đối tượng này làm con của Avatars Panel
            avatar.transform.SetParent(Avatars, false);

            RectTransform rectTransform = avatar.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0); // Cố định góc dưới trái
            rectTransform.anchorMax = new Vector2(1, 1); // Cố định góc trên phải
            rectTransform.offsetMin = new Vector2(0, 0); // Không dịch chuyển biên dưới và trái
            rectTransform.offsetMax = new Vector2(0, 0); // Không dịch chuyển biên trên và phải
            
            // Tải Sprite từ Resources
            Sprite newSprite = Resources.Load<Sprite>(s);

            if (newSprite != null)
            {
                image.sprite = newSprite;
            }
            else
            {
                Debug.LogError("Sprite not found at path: " + s);
            }
        }
    }

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
        Name.text=KamenRider.GetName();
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
