using UnityEngine;
using UnityEngine.UI;

namespace ASOIAF {
    public class UnitCardFront : MonoBehaviour, IUnitCardRenderer {
        [Header("Texts"), Space(-10)]
        [SerializeField] private new TMPro.TextMeshProUGUI name;
        [SerializeField] private TMPro.TextMeshProUGUI speed, defense, moral;
        [Header("Images")]
        [SerializeField] private Image character;
        [SerializeField] private Image unitType, baseImage, abilityMask;

        public void UpdateVisual(UnitCardData unit) {
            if(unit == null) {
                name.text = "Null";
                speed.text = "-";
                defense.text = "-";
                moral.text = "-";

                character.sprite = null;
                unitType.sprite = null;
                baseImage.sprite = null;
                abilityMask.sprite = null;
                return;
            }

            name.text = unit.Name;
            speed.text = unit.Speed.ToString();
            defense.text = $"{unit.Defense}+";
            moral.text = $"{unit.Moral}+";

            character.sprite = unit.Character;
            unitType.sprite = unit.Type.Sprite;
            baseImage.sprite = unit.House.UnitFront;
            abilityMask.sprite = unit.House.AbilityMask;
        }
    }
}
