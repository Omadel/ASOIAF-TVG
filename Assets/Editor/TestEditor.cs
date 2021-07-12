using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Test))]
public class TestEditor : EtienneEditor.Editor<Test> {
    private void OnSceneGUI() {

        Handles.color = Color.white;
        Handles.DrawWireArc(
            Target.transform.position + Target.transform.forward * Target.viewOffset,
            Vector3.up, Target.transform.forward,
            Target.viewAngle / 2, Target.viewLenght);
        Handles.DrawWireArc(
            Target.transform.position + Target.transform.forward * Target.viewOffset,
            -Vector3.up, Target.transform.forward,
            Target.viewAngle / 2, Target.viewLenght);
        Handles.color = Color.yellow;
        Handles.DrawWireArc(
            Target.transform.position + Target.transform.forward * Target.viewOffset,
            Vector3.up, Target.transform.forward,
            Target.viewAngle / 2, Target.viewLenght / 2);
        Handles.DrawWireArc(
            Target.transform.position + Target.transform.forward * Target.viewOffset,
            -Vector3.up, Target.transform.forward,
            Target.viewAngle / 2, Target.viewLenght / 2);

        Vector3 viewAngleA = Target.DirFromAngle(-Target.viewAngle / 2, false);
        Vector3 viewAngleB = Target.DirFromAngle(Target.viewAngle / 2, false);
        Handles.DrawLine(Target.transform.position, Target.transform.position + viewAngleA * Target.viewLenght);
        Handles.DrawLine(Target.transform.position, Target.transform.position + viewAngleB * Target.viewLenght);
        EtienneEditor.Gizmos.DrawSphere(Target.transform.position + Target.transform.forward * Target.viewOffset, .5f, Color.green);

        //foreach(Transform visibleTarget in Target.visibleTargets) {
        //    Handles.DrawLine(Target.transform.position, visibleTarget.transform.position);
        //}
    }
}