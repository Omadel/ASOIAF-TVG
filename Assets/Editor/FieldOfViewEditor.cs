using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor {
    private void OnSceneGUI() {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position
            + fow.unitType.ArrowOffset * fow.transform.forward
            , Vector3.up, Vector3.forward, 360, fow.viewRadius + fow.unitType.Center);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.unitType.Angle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.unitType.Angle / 2, false);

        Vector3 center = fow.transform.position + fow.transform.forward * fow.unitType.Center;
        Handles.DrawLine(center, center + viewAngleA * fow.viewRadius);
        Handles.DrawLine(center, center + viewAngleB * fow.viewRadius);

        EtienneEditor.Gizmos.DrawSphere(fow.transform.position + fow.transform.forward * fow.unitType.ArrowOffset, .2f, Color.white);
        EtienneEditor.Gizmos.DrawSphere(center, .2f, Color.white);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fow.visibleTargets) {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }

}
