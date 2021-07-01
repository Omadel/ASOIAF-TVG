using UnityEditor;
using UnityEngine;

namespace ASOIAF {
    public abstract class StaticScriptableObject : ScriptableObject {
        public abstract void StaticSetup();
    }
    public static class StaticScriptableObjectLoader {

        [InitializeOnLoadMethod]
        private static void Load() {
            string[] assets = AssetDatabase.FindAssets("t:StaticScriptableObject");
            //Debug.Log("Static Scriptable Objects loaded:");
            foreach(string asset in assets) {
                string path = AssetDatabase.GUIDToAssetPath(asset);
                ASOIAF.StaticScriptableObject staticSO = AssetDatabase.LoadAssetAtPath<ASOIAF.StaticScriptableObject>(path);
                staticSO.StaticSetup();
                //Debug.Log($"{staticSO.name}", staticSO);
            }
        }
    }
}
