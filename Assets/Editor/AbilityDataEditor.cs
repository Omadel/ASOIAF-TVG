using UnityEditor;
using UnityEngine;

namespace ASOIAF {
    [CustomPropertyDrawer(typeof(AbilityTypeData))]
    public class AbilityDataEditor : PropertyDrawer {
        private AbilityTypesEnum abilityType = AbilityTypesEnum.Melee;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            abilityType = property.objectReferenceValue as AbilityTypeData;
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            EditorGUI.BeginChangeCheck();
            abilityType = (AbilityTypesEnum)EditorGUI.EnumPopup(position, abilityType);
            if(EditorGUI.EndChangeCheck()) {
                property.objectReferenceValue = AbilityTypes.GetAbilities[(int)abilityType];
            }
            EditorGUI.EndProperty();
        }
    }
}