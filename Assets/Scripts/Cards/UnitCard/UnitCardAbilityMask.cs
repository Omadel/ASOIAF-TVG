using UnityEngine;

namespace ASOIAF {
    public class UnitCardAbilityMask : MonoBehaviour {
        private const float TopOffset = .07f;

        [SerializeField] private RectTransform rect;

        [ContextMenu("Resize")]
        public void Resize() {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, 1 - TopOffset - rect.rect.height);
        }

        private RectTransform rectTransform;
    }
}