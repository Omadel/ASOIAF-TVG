using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "UnitTypes", menuName = "ASOIAF/UnitTypes/UnitTypeStatic")]
    public class UnitTypes : StaticScriptableObject {
        public static UnitTypeData[] GetUnitTypes { get; private set; }
        [SerializeField, ForceDebugMode] private UnitTypeData[] unitTypes;

        private void OnValidate() {
            GetUnitTypes = unitTypes;
        }

        public override void StaticSetup() {
            OnValidate();
        }
    }

    public enum UnitTypesEnum {
        Infantry,
        Cavalry,
        Monster,
        WarMachine
    }
}