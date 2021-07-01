using System;
using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "UnitType", menuName = "ASOIAF/UnitTypes/UnitType")]
    public class UnitTypeData : ScriptableObject {
        public Sprite Sprite => sprite;
        [SerializeField, PreviewSprite] private Sprite sprite;

        public static implicit operator UnitTypesEnum(UnitTypeData type) => (UnitTypesEnum)Array.IndexOf(UnitTypes.GetUnitTypes, type);
    }
}
