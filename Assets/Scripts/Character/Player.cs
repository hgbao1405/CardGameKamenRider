using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class Player: MonoBehaviour
    {
        //Deck hand chưa code
        public GameObject cardPrefab;
        public Transform deckPanel;
        public Transform handPanel;
        public HealBar HealBar;
        public TableManager tableManager;
        public DrawCircle Race;
        public KamenRider KamenRider;
        public float speed;
        public float moveDuration = 0.5f;
        public float cardSpacing = 20.0f;

        private void Start()
        {
            KamenRider = Seeding.KamenRiderOOOSeed();

            foreach (SlotCard card in KamenRider.CardSlot)
            {
                CardPosition cardPosition = new CardPosition();
                cardPosition.SetCardPosition(card.x, card.y, card.keyword);
                tableManager.UpdatePoisiton(cardPosition);
            }
            //Load bộ bài
            List<Card> deck = Seeding.SeedCardsOOO();
            foreach (Card card in deck)
            {
                GameObject cardObject = Instantiate(cardPrefab, deckPanel);
                cardPrefab cardDisplay = cardObject.GetComponent<cardPrefab>();
                cardDisplay.Card = card;
                if (cardDisplay != null)
                {
                    cardDisplay.Load();
                }
                cardDisplay.SetSlotEvent(tableManager.GetSlotPositionEventByKeyWords(card.Keywords));
            }

            //Load Speed
            speed = KamenRider.GetSpeed();
            Race.UpdateSpeed(speed);

            //Load thanh máu
            HealBar.KamenRider = KamenRider;
            HealBar.UpdateHealthBar();
            HealBar.GetAvatars();
            //DrawCard();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DrawCard();
            }
        }

        public void DrawManyCard(int number)
        {
            for (int i = 0;i < number;i++)
            {
                DrawCard();
            }
        }
        public void DrawCard()
        {
            // Bắt đầu di chuyển lá bài
            StartCoroutine(MoveCardToHand());   
        }
        // Coroutine di chuyển lá bài từ deck đến hand
        private IEnumerator MoveCardToHand()
        {
            if (deckPanel.childCount > 0)
            {
                // Lấy thẻ bài cuối cùng từ deckPanel
                Transform card = deckPanel.GetChild(deckPanel.childCount - 1);

                RectTransform cardRect = card.GetComponent<RectTransform>();
                Vector3 startPos = cardRect.position;
                Vector3 endPos = handPanel.position;

                float elapsedTime = 0;

                while (elapsedTime < moveDuration)
                {
                    cardRect.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                cardRect.position = endPos;
                card.transform.SetParent(handPanel);
                card.GetComponent<cardPrefab>().OnDraw();
                ArrangeCards();
            }
        }
        private float maxSpacing = -20.0f; // Khoảng cách tối đa giữa các thẻ bài
        private float overlapSpacing = -650.0f;
        private float cardWidth = 255.3102f;
        private float panelWidth = 840f;
        private void ArrangeCards()
        {
            HorizontalLayoutGroup layoutGroup = handPanel.GetComponent<HorizontalLayoutGroup>();
            // Lấy tổng số thẻ bài trong handPanel
            int cardCount = handPanel.childCount;

            if (cardCount <= 1) return; // Không cần tính toán nếu có ít hơn 2 thẻ bài

            // Tính toán khoảng cách giữa các thẻ bài
            float totalCardWidth = cardCount * cardWidth;
            if (totalCardWidth < panelWidth)
            {
                // Sử dụng khoảng cách chồng lên nhau nếu tổng chiều rộng thẻ bài nhỏ
                layoutGroup.spacing = overlapSpacing;
            }
            else
            {
                // Tính toán khoảng cách giữa các thẻ bài
                float totalSpacing = panelWidth - totalCardWidth;
                float spacing = totalSpacing / (cardCount - 1);

                // Đảm bảo spacing không vượt quá maxSpacing
                layoutGroup.spacing = Mathf.Min(spacing, maxSpacing);
            }
        }
    }
}
