using UnityEngine;
using UnityEngine.UI;

namespace ASOIAF {
    public class UnitCardSeparator : MonoBehaviour, IUnitCardRenderer {
        [SerializeField] private Image background;

        public void UpdateVisual(UnitCardData unit) {
            background.sprite = unit.House.SeparatorSprite;
        }
    }
}