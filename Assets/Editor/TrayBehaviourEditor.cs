using UnityEditor;
using UnityEngine;

namespace ASOIAF {
    [CustomEditor(typeof(TrayBehaviour))]
    public class TrayBehaviourEditor : EtienneEditor.Editor<TrayBehaviour> {
        public override void OnInspectorGUI() {
            if(GUILayout.Button("Loose One Wound")) {
                Target.LosseOneWound();
            }
            if(GUILayout.Button("Block")) {
                Target.Block();
            }
            if(GUILayout.Button("Impact")) {
                Target.Impact();
            }
            base.OnInspectorGUI();
        }
    }
}