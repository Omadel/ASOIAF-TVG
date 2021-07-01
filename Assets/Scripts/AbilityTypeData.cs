using System;
using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "Abilities", menuName = "ASOIAF/Abilities/AbilityType")]
    public class AbilityTypeData : ScriptableObject {
        public Sprite Gold => gold;
        public Sprite GoldOutline => goldOutline;
        public Sprite Silver => silver;
        public Sprite SilverOutline => silverOutline;
        public string Name => name;
        public string Rule => rule;

        [SerializeField] private new string name;
        [SerializeField, TextArea(3, 5)] private string rule;
        [SerializeField, PreviewSprite(50)] private Sprite gold, silver;
        [SerializeField, PreviewSprite(50)] private Sprite goldOutline, silverOutline;

        public static implicit operator AbilityTypesEnum(AbilityTypeData house) => (AbilityTypesEnum)Array.IndexOf(AbilityTypes.GetAbilities, house);
    }
}