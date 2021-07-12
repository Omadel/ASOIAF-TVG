using UnityEditor;
using UnityEngine;

namespace ASOIAF {
    [CustomEditor(typeof(AngleFinder))]
    public class AngleFinderEditor : EtienneEditor.Editor<AngleFinder> {

        private void OnSceneGUI() {
            if(Target.unitType != null) {
                Target.angle = Target.unitType.Angle;
                Target.center = Target.unitType.Center;
                Target.size = Target.unitType.Size;
                Target.arrowOffset = Target.unitType.ArrowOffset;
            }
            Handles.color = Color.red;

            Vector3 center = Target.transform.position
                + Target.transform.TransformDirection(Vector3.forward * Target.center)
                + Vector3.up * .4f;
            EtienneEditor.Gizmos.DrawSphere(center, .5f, Handles.color);

            Vector3 size = Target.size;

            Vector3 topLeftDirection = new Vector3(-1, 0, 1);
            Vector3 topLeftPosition = DrawLineWithDirection(center, size, topLeftDirection);

            Vector3 topRightDirection = new Vector3(1, 0, 1);
            Vector3 topRightPosition = DrawLineWithDirection(center, size, topRightDirection);

            float angle = Vector3.SignedAngle(center - topLeftPosition, center - topRightPosition, Vector3.up);
            Target.angle = angle;
            Handles.DrawSolidArc(center, Vector3.up, topLeftPosition - center, angle, 1f);

            Vector3 arrowPosition = DrawLineWithDirection(center, size, Vector3.forward);
            //Target.arrowOffset = Target.unitType.ArrowOffset;
            EtienneEditor.Gizmos.DrawSphere(arrowPosition, .1f, Handles.color);

            Handles.color = Color.green;

            center = Target.transform.position
                + Target.transform.TransformDirection((Vector3.forward * Target.center).Multiply(new Vector3(1, 1, -1)))
                + Vector3.up * .4f;
            EtienneEditor.Gizmos.DrawSphere(center, .5f, Handles.color);

            Vector3 botLeftDirection = new Vector3(-1, 0, -1);
            Vector3 botLeftPosition = DrawLineWithDirection(center, size, botLeftDirection);

            Vector3 botRightDirection = new Vector3(1, 0, -1);
            DrawLineWithDirection(center, size, botRightDirection);

            Handles.DrawSolidArc(center, Vector3.up, botLeftPosition - center, -angle, 1f);

        }

        private Vector3 DrawLineWithDirection(Vector3 center, Vector3 size, Vector3 direction) {
            //Vector3 corner = size.Multiply(direction);
            Vector3 corner = center + Target.transform.TransformPoint(size.Multiply(direction));
            Vector3[] points = new Vector3[2] { center, corner };
            Handles.DrawAAPolyLine(4f, points);
            return corner;
        }
    }

}
public static class Vector3Extentions {
    public static Vector3 Multiply(this Vector3 vectorA, Vector3 vectorB) {
        Vector3 result = new Vector3();
        result.x = vectorA.x * vectorB.x;
        result.y = vectorA.y * vectorB.y;
        result.z = vectorA.z * vectorB.z;
        return result;
    }
}