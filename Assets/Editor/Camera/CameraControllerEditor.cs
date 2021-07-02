using EtienneEditor;
using UnityEditor;
using UnityEngine;
using Gizmos = EtienneEditor.Gizmos;

namespace ASOIAF {
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerEditor : Editor<CameraController> {
        private void OnSceneGUI() {
            Color color = Color.green;
            Handles.color = color;
            float radius = 1f;
            Gizmos.DrawSphere(Target.transform.position, radius, color);
            Gizmos.DrawLine(Target.transform.position, Target.cameraTransform.position, color);
            Vector3 camPosition = Target.cameraTransform.position;
            Vector3 position = camPosition;
            position.y = Target.transform.position.y;
            radius = Vector3.Distance(Target.transform.position, position);
            position = Target.transform.position;
            position.y = camPosition.y;
            Gizmos.DrawCircle(position, Quaternion.identity, radius * 2, color);
        }
    }
}
