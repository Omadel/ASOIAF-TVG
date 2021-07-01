using UnityEditor;
using UnityEngine;

namespace ASOIAF {
    [InitializeOnLoad]
    public class UnitCard : Card {
        [SerializeField] private UnitCardData cardData;

        protected override void SetTexts() {
            unitCardRenderers ??= GetComponentsInChildren<IUnitCardRenderer>();
            if(unitCardRenderers != null) {
                foreach(IUnitCardRenderer renderer in unitCardRenderers) {
                    renderer.UpdateVisual(cardData);
                }
            }
        }

        protected override void SetImages() { }

        private IUnitCardRenderer[] unitCardRenderers;
    }
}