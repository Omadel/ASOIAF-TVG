using UnityEngine;
using UnityEngine.UI;

namespace ASOIAF {
    public class UnitCardAbility : MonoBehaviour {
        [SerializeField] private bool isOrderUsed = false;
        [SerializeField] private TMPro.TextMeshProUGUI nameText, ruleText;
        [SerializeField] private Image abilityType, abilityOutline;

        public void UpdateStats(Ability ability, HouseData house) {
            nameText.text = ability.Name;
            nameText.color = house.Color;
            ruleText.text = ability.Rule;

            switch(house.Material) {
                case Material.Gold:
                    abilityType.sprite = ability.Type.Gold;
                    abilityOutline.sprite = ability.Type.GoldOutline;
                    break;
                case Material.Silver:
                    abilityType.sprite = ability.Type.Silver;
                    abilityOutline.sprite = ability.Type.SilverOutline;
                    break;
            }
            if(ability.Type == AbilityTypesEnum.Order) {

                abilityType.sprite = isOrderUsed ? ability.Type.Silver : ability.Type.Gold;
            }
        }
    }
}