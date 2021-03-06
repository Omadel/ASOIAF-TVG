using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ForceDebugModeAttribute))]
public class ForceDebugModeDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.PropertyField(position, property, label);
    }
}
