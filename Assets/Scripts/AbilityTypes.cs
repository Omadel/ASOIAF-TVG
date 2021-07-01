using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "Abilities", menuName = "ASOIAF/Abilities/AbilitiesTypesStatic")]
    public class AbilityTypes : StaticScriptableObject {
        public static AbilityTypeData[] GetAbilities { get; private set; }
        [SerializeField, ForceDebugMode] private AbilityTypeData[] abilities;

        private void OnValidate() {
            GetAbilities = abilities;
        }

        public override void StaticSetup() {
            OnValidate();
        }
    }

    public enum AbilityTypesEnum {
        Melee,
        ShortRange,
        LongRange,
        Order,
        Innate,
    }
}