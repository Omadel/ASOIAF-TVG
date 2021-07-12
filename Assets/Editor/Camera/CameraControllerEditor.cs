using EtienneEditor;
using UnityEditor;
using UnityEngine;
using Gizmos = EtienneEditor.Gizmos;

namespace ASOIAF {
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerEditor : Editor<CameraController> {
        private void OnSceneGUI() {
            Color color = new Color32(67, 160, 71, 255);
            Handles.color = color;

            float radius = 1f;
            Vector3 camPosition = Target.CameraTransform.position;
            Gizmos.DrawSphere(Target.transform.position, radius, color);
            Handles.DrawDottedLine(Target.transform.position, camPosition, 10f);

            Vector3 groundedPosition = camPosition;
            groundedPosition.y = Target.transform.position.y;

            radius = Vector3.Distance(Target.transform.position, groundedPosition);

            Vector3 center = Target.transform.position;
            center.y = camPosition.y;
            Gizmos.DrawCircle(center, Quaternion.identity, radius * 2, color);

            radius = Vector3.Distance(Target.transform.position, camPosition);
            float angle = Target.CameraRotationAmplitude / 2;
            float x = Target.transform.localRotation.eulerAngles.x;
            float euler = x > 180 ? x - 360 : x;
            center = Target.transform.position;
            Vector3 normal = -Target.transform.right;
            Vector3 from = camPosition - center;
            Handles.DrawWireArc(center, normal, from, euler + angle, radius, 4f);
            Handles.DrawWireArc(center, normal, from, euler - angle, radius, 4f);
        }
    }
}
