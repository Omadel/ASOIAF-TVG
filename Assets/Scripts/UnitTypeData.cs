using System;
using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "UnitType", menuName = "ASOIAF/UnitTypes/UnitType")]
    public class UnitTypeData : ScriptableObject {
        public Sprite Sprite => sprite;
        public float Angle => angle;
        public float ArrowOffset => arrowOffset;
        public float Center => center;
        public Vector3 Size => size;

        [SerializeField, PreviewSprite] private Sprite sprite;
        [SerializeField, Range(0, 360)] private float angle;
        [SerializeField] private float arrowOffset;
        [SerializeField] private float center;
        [SerializeField] private Vector3 size;

        public static implicit operator UnitTypesEnum(UnitTypeData type) {
            return (UnitTypesEnum)Array.IndexOf(UnitTypes.GetUnitTypes, type);
        }
    }
}
