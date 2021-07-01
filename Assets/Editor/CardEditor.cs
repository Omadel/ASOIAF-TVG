using ASOIAF;
using UnityEditor;
using UnityEngine;

namespace ASOIAFEditor {
    [CustomEditor(typeof(Card), true)]
    public class CardEditor : EtienneEditor.Editor<Card> {
        public override void OnInspectorGUI() {
            SerializedProperty cardData = serializedObject.FindProperty("cardData");
            if(GUILayout.Button("Update")) {
                EditorUtility.SetDirty(cardData.objectReferenceValue);
                Target.Validate();
            }
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if(EditorGUI.EndChangeCheck()) {
                Target.Validate();
            }

        }
    }
}