using UnityEditor;
using UnityEngine;

namespace ASOIAF {
    [CustomPropertyDrawer(typeof(UnitTypeData))]
    public class UnitTypeDataEditor : PropertyDrawer {
        private UnitTypesEnum unitType = UnitTypesEnum.Infantry;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            unitType = property.objectReferenceValue as UnitTypeData;
            position = EditorGUI.PrefixLabel(position, label);
            EditorGUI.BeginChangeCheck();
            unitType = (UnitTypesEnum)EditorGUI.EnumPopup(position, unitType);
            if(EditorGUI.EndChangeCheck()) {
                property.objectReferenceValue = UnitTypes.GetUnitTypes[(int)unitType];
            }
        }
    }
}