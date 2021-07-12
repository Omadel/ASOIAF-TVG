using System;
using System.Reflection;
using UnityEditor;

internal static class EditorMenus {
    // taken from: http://answers.unity3d.com/questions/282959/set-inspector-lock-by-code.html
    [MenuItem("Tools/Toggle Inspector Lock %l")] // Ctrl + L
    private static void ToggleInspectorLock() {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }

    [MenuItem("Tools/Toggle Inspector Debug Mode &d")] // Alt + D
    private static void ToggleInspectorDebugMode() {
        EditorWindow focusedInspector = EditorWindow.focusedWindow;
        if(focusedInspector != null && focusedInspector.GetType().Name == "InspectorWindow") {
            //Get the type of the inspector window to find out the variable/method from
            Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.InspectorWindow");
            //get the field we want to read, for the type (not our instance)
            FieldInfo field = type.GetField("m_InspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

            //read the value for our target inspector
            InspectorMode mode = (InspectorMode)field.GetValue(focusedInspector);
            //toggle the value
            mode++;
            mode = (mode > InspectorMode.DebugInternal ? InspectorMode.Normal : mode);
            // mode = (mode == InspectorMode.Normal ? InspectorMode.Debug : InspectorMode.Normal);
            //Debug.Log("New Inspector Mode: " + mode.ToString());

            //Find the method to change the mode for the type
            MethodInfo method = type.GetMethod("SetMode", BindingFlags.NonPublic | BindingFlags.Instance);
            //Call the function on our targetInspector, with the new mode as an object[]
            method.Invoke(focusedInspector, new object[] { mode });

            //refresh inspector
            focusedInspector.Repaint();
        }
    }
}