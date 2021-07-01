using UnityEngine;
using UnityEngine.UI;

namespace ASOIAF {
    public class UnitCardBack : MonoBehaviour, IUnitCardRenderer {
        [Header("Texts")]
        [SerializeField] private new TMPro.TextMeshProUGUI name;
        [SerializeField] private TMPro.TextMeshProUGUI pointValue, lore, requierementNameText, requierementText;

        [Header("Images")]
        [SerializeField] private Image character;
        [SerializeField] private Image unitType, baseImage, requierementNameImage, requierementImage;
        public void UpdateVisual(UnitCardData unit) {
            if(unit == null) {
                name.text = "Null";
                pointValue.text = "-";
                lore.text = "Null";

                character.sprite = null;
                unitType.sprite = null;
                baseImage.sprite = null;

                requierementText.transform.parent.gameObject.SetActive(true);

                requierementNameText.text = "Null";
                requierementText.text = "Null";
                requierementNameImage.sprite = null;
                requierementImage.sprite = null;
                return;
            }
            name.text = unit.Name;
            pointValue.text = unit.PointValue.ToString();
            lore.text = unit.Lore;

            character.sprite = unit.Character;
            unitType.sprite = unit.Type.Sprite;
            baseImage.sprite = unit.House.UnitBack;

            requierementText.transform.parent.gameObject.SetActive(unit.HasRequierement);
            if(unit.HasRequierement) {
                requierementNameText.text = unit.RequierementName;
                requierementText.text = unit.RequierementText;
                requierementNameImage.sprite = unit.House.RequierementNameImage;
                requierementImage.sprite = unit.House.RequierementImage;
            }
        }
    }
}