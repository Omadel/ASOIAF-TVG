using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public float viewOffset = 2.45f;
    [Min(0)]
    public float viewLenght;
    [Range(0, 360)]
    public float viewAngle;
    [Header("Mesh")]
    [Min(0)]
    public float meshResolution;
    public float edgeDistanceThreshold;
    public int edgeResolveIteration;
    public LayerMask targetMask, obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();
    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;
    private void Start() {
        viewMesh = new Mesh {
            name = "View Mesh"
        };
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    private void LateUpdate() {
        DrawFieldOfView();
    }

    private IEnumerator FindTargetsWithDelay(float delay) {
        while(true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider[] targetsVisibleInViewRadius = Physics.OverlapSphere(transform.position, viewLenght, targetMask);

        foreach(Collider target in targetsVisibleInViewRadius) {
            Transform targetTransform = target.transform;
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2) {
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask)) {
                    visibleTargets.Add(targetTransform);
                }
            }
        }
    }

    private ViewCastInfo ViewCast(float globalAngle) {
        Vector3 direction = DirFromAngle(globalAngle, true);
        float alpha = globalAngle * Mathf.Deg2Rad;
        float ob = viewOffset;
        float oh = ob * Mathf.Cos(alpha)
            + Mathf.Sqrt(
                    Mathf.Pow(ob, 2) * Mathf.Pow(Mathf.Cos(alpha), 2)
                    - Mathf.Pow(ob, 2) + Mathf.Pow(viewLenght, 2)
                );
        Debug.DrawLine(transform.position, transform.position + direction * oh);
        if(Physics.Raycast(transform.position, direction, out RaycastHit hit, oh, obstacleMask)) {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        } else {
            return new ViewCastInfo(false, transform.position + direction * oh, oh, globalAngle);
        }
    }

    private void DrawFieldOfView() {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for(int i = 0; i <= stepCount; i++) {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            //Debug.DrawLine(transform.position, newViewCast.point, Color.blue);
            //Debug.DrawRay(transform.position, (newViewCast.point - transform.position).normalized * viewLenght / 2, Color.cyan);
            if(i > 0) {
                bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;
                if(oldViewCast.hit != newViewCast.hit
                    || (oldViewCast.hit && newViewCast.hit && edgeDistanceThresholdExceeded)) {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA != Vector3.zero) {
                        viewPoints.Add(edge.pointA);
                    }
                    if(edge.pointB != Vector3.zero) {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertextCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertextCount];
        int[] triangles = new int[(vertextCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertextCount - 1; i++) {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if(i < vertextCount - 2) {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i = 0; i < edgeResolveIteration; i++) {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDistanceThresholdExceeded = Mathf.Abs(minViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;
            if(newViewCast.hit == minViewCast.hit
                && !edgeDistanceThresholdExceeded) {
                minAngle = angle;
                minPoint = newViewCast.point;
            } else {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool isAngleGlobal) {
        if(!isAngleGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        float angleInRad = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(angleInRad), 0, Mathf.Cos(angleInRad));
    }

    private struct ViewCastInfo {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool hit, Vector3 point, float distance, float angle) {
            this.hit = hit;
            this.point = point;
            this.distance = distance;
            this.angle = angle;
        }
    }

    private struct EdgeInfo {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 pointA, Vector3 pointB) {
            this.pointA = pointA;
            this.pointB = pointB;
        }
    }
}