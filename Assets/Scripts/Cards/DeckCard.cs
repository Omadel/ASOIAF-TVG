using UnityEngine;
using UnityEngine.UI;

namespace ASOIAF {
    public class DeckCard : Card {
        [SerializeField] private DeckCardData cardData;

        protected override void SetTexts() {
            texts ??= GetComponentsInChildren<TMPro.TextMeshProUGUI>();
            if(cardData != null) {
                texts[0].text = cardData.CardName;
                texts[1].text = cardData.ActivationText;
                texts[2].text = cardData.RuleText;
                texts[1].color = cardData.House.Color;
            }
        }

        protected override void SetImages() {
            backgrounds ??= GetComponentsInChildren<Image>();
            if(cardData != null) {
                backgrounds[1].sprite = cardData.House.DeckFront;
            }
        }

        private TMPro.TextMeshProUGUI[] texts;
        private Image[] backgrounds;
    }
}