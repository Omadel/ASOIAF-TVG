using UnityEditor;
using UnityEngine;

namespace ASOIAF {
    [CustomPropertyDrawer(typeof(HouseData))]
    public class HouseDataEditor : PropertyDrawer {
        private HousesEnum house = HousesEnum.Stark;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            house = property.objectReferenceValue as HouseData;
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            EditorGUI.BeginChangeCheck();
            house = (HousesEnum)EditorGUI.EnumPopup(position, house);
            if(EditorGUI.EndChangeCheck()) {
                property.objectReferenceValue = Houses.GetHouses[(int)house];
            }
            EditorGUI.EndProperty();
        }
    }
}