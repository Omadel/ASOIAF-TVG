using UnityEditor;
using UnityEngine;


public class RoundedQuadMeshGenerator : MonoBehaviour {
    [SerializeField] private RoundedQuadMeshData data;

    private MeshFilter m_MeshFilter;
    private Mesh m_Mesh;
    private Vector3[] m_Vertices;
    private Vector3[] m_Normals;
    private Vector2[] m_UV;
    private int[] m_Triangles;
    private MeshRenderer meshRenderer;


    [ContextMenu("Generated Mesh")]
    public Mesh GenerateMesh() {
        m_MeshFilter = GetComponent<MeshFilter>();
        if(m_MeshFilter == null)
            m_MeshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer == null)
            gameObject.AddComponent<MeshRenderer>();
        m_Mesh = new Mesh();
        m_Mesh.name = "GeneratedQuad";
        m_MeshFilter.sharedMesh = m_Mesh;

        int sides = data.DoubleSided ? 2 : 1;
        int vCount = data.CornerVertexCount * 4 * sides + sides; //+sides for center vertices
        int triCount = (data.CornerVertexCount * 4) * sides;
        if(m_Vertices == null || m_Vertices.Length != vCount) {
            m_Vertices = new Vector3[vCount];
            m_Normals = new Vector3[vCount];
        }
        if(m_Triangles == null || m_Triangles.Length != triCount * 3)
            m_Triangles = new int[triCount * 3];
        if(data.CreateUV && (m_UV == null || m_UV.Length != vCount)) {
            m_UV = new Vector2[vCount];
        }
        int count = data.CornerVertexCount * 4;
        if(data.CreateUV) {
            m_UV[0] = Vector2.one * 0.5f;
            if(data.DoubleSided)
                m_UV[count + 1] = m_UV[0];
        }
        float tl = Mathf.Max(0, data.RoundTopLeft + data.RoundEdges);
        float tr = Mathf.Max(0, data.RoundTopRight + data.RoundEdges);
        float bl = Mathf.Max(0, data.RoundBottomLeft + data.RoundEdges);
        float br = Mathf.Max(0, data.RoundBottomRight + data.RoundEdges);
        float f = Mathf.PI * 0.5f / (data.CornerVertexCount - 1);
        float a1 = 1f;
        float a2 = 1f;
        float x = 1f;
        float y = 1f;
        Vector2 rs = Vector2.one;
        if(data.UsePercentage) {
            rs = new Vector2(data.Rect.width, data.Rect.height) * 0.5f;
            if(data.Rect.width > data.Rect.height)
                a1 = data.Rect.height / data.Rect.width;
            else
                a2 = data.Rect.width / data.Rect.height;
            tl = Mathf.Clamp01(tl);
            tr = Mathf.Clamp01(tr);
            bl = Mathf.Clamp01(bl);
            br = Mathf.Clamp01(br);
        } else {
            x = data.Rect.width * 0.5f;
            y = data.Rect.height * 0.5f;
            if(tl + tr > data.Rect.width) {
                float b = data.Rect.width / (tl + tr);
                tl *= b;
                tr *= b;
            }
            if(bl + br > data.Rect.width) {
                float b = data.Rect.width / (bl + br);
                bl *= b;
                br *= b;
            }
            if(tl + bl > data.Rect.height) {
                float b = data.Rect.height / (tl + bl);
                tl *= b;
                bl *= b;
            }
            if(tr + br > data.Rect.height) {
                float b = data.Rect.height / (tr + br);
                tr *= b;
                br *= b;
            }
        }
        m_Vertices[0] = data.Rect.center * data.Scale;
        if(data.DoubleSided)
            m_Vertices[count + 1] = (Vector3)data.Rect.center * data.Scale + Vector3.forward * data.Width;
        for(int i = 0; i < data.CornerVertexCount; i++) {
            float s = Mathf.Sin((float)i * f);
            float c = Mathf.Cos((float)i * f);
            Vector2 v1 = new Vector3(-x + (1f - c) * tl * a1, y - (1f - s) * tl * a2);
            Vector2 v2 = new Vector3(x - (1f - s) * tr * a1, y - (1f - c) * tr * a2);
            Vector2 v3 = new Vector3(x - (1f - c) * br * a1, -y + (1f - s) * br * a2);
            Vector2 v4 = new Vector3(-x + (1f - s) * bl * a1, -y + (1f - c) * bl * a2);

            m_Vertices[1 + i] = (Vector2.Scale(v1, rs) + data.Rect.center) * data.Scale;
            m_Vertices[1 + data.CornerVertexCount + i] = (Vector2.Scale(v2, rs) + data.Rect.center) * data.Scale;
            m_Vertices[1 + data.CornerVertexCount * 2 + i] = (Vector2.Scale(v3, rs) + data.Rect.center) * data.Scale;
            m_Vertices[1 + data.CornerVertexCount * 3 + i] = (Vector2.Scale(v4, rs) + data.Rect.center) * data.Scale;
            if(data.CreateUV) {
                if(!data.UsePercentage) {
                    Vector2 adj = new Vector2(2f / data.Rect.width, 2f / data.Rect.height);
                    v1 = Vector2.Scale(v1, adj);
                    v2 = Vector2.Scale(v2, adj);
                    v3 = Vector2.Scale(v3, adj);
                    v4 = Vector2.Scale(v4, adj);
                }
                m_UV[1 + i] = v1 * 0.5f + Vector2.one * 0.5f;
                m_UV[1 + data.CornerVertexCount * 1 + i] = v2 * 0.5f + Vector2.one * 0.5f;
                m_UV[1 + data.CornerVertexCount * 2 + i] = v3 * 0.5f + Vector2.one * 0.5f;
                m_UV[1 + data.CornerVertexCount * 3 + i] = v4 * 0.5f + Vector2.one * 0.5f;
            }
            if(data.DoubleSided) {
                m_Vertices[1 + data.CornerVertexCount * 8 - i] = m_Vertices[1 + i] + Vector3.forward * data.Width;
                m_Vertices[1 + data.CornerVertexCount * 7 - i] = m_Vertices[1 + data.CornerVertexCount + i] + Vector3.forward * data.Width;
                m_Vertices[1 + data.CornerVertexCount * 6 - i] = m_Vertices[1 + data.CornerVertexCount * 2 + i] + Vector3.forward * data.Width;
                m_Vertices[1 + data.CornerVertexCount * 5 - i] = m_Vertices[1 + data.CornerVertexCount * 3 + i] + Vector3.forward * data.Width;
                if(data.CreateUV) {
                    m_UV[1 + data.CornerVertexCount * 8 - i] = v1 * 0.5f + Vector2.one * 0.5f;
                    m_UV[1 + data.CornerVertexCount * 7 - i] = v2 * 0.5f + Vector2.one * 0.5f;
                    m_UV[1 + data.CornerVertexCount * 6 - i] = v3 * 0.5f + Vector2.one * 0.5f;
                    m_UV[1 + data.CornerVertexCount * 5 - i] = v4 * 0.5f + Vector2.one * 0.5f;
                }
            }
        }
        for(int i = 0; i < count + 1; i++) {
            m_Normals[i] = -Vector3.forward;
            if(data.DoubleSided) {
                m_Normals[count + 1 + i] = Vector3.forward;
                if(data.FlipBackFaceUV) {
                    Vector2 uv = m_UV[count + 1 + i];
                    uv.x = 1f - uv.x;
                    m_UV[count + 1 + i] = uv;
                }
            }
        }
        for(int i = 0; i < count; i++) {
            m_Triangles[i * 3] = 0;
            m_Triangles[i * 3 + 1] = i + 1;
            m_Triangles[i * 3 + 2] = i + 2;
            if(data.DoubleSided) {
                m_Triangles[(count + i) * 3] = count + 1;
                m_Triangles[(count + i) * 3 + 1] = count + 1 + i + 1;
                m_Triangles[(count + i) * 3 + 2] = count + 1 + i + 2;
            }
        }
        m_Triangles[count * 3 - 1] = 1;
        if(data.DoubleSided)
            m_Triangles[m_Triangles.Length - 1] = count + 1 + 1;

        m_Mesh.Clear();
        m_Mesh.vertices = m_Vertices;
        m_Mesh.normals = m_Normals;
        if(data.CreateUV)
            m_Mesh.uv = m_UV;
        if(data.DifferentBackFace) {
            int[] frontTriangles = new int[m_Triangles.Length / 2];
            int[] backTriangles = new int[m_Triangles.Length / 2];
            for(int i = 0; i < m_Triangles.Length / 2; i++) {
                frontTriangles[i] = m_Triangles[i];
                backTriangles[i] = m_Triangles[i + m_Triangles.Length / 2];
            }
            m_Mesh.subMeshCount = 2;
            m_Mesh.SetTriangles(frontTriangles, 0);
            m_Mesh.SetTriangles(backTriangles, 1);
        } else {
            m_Mesh.triangles = m_Triangles;
        }
        meshRenderer.materials = new Material[sides];
        return m_Mesh;
    }

    [ContextMenu("Save Mesh")]
    private void SaveMesh() {
        string path = EditorUtility.SaveFilePanel("Save Generated Mesh", "Assets/Meshes", m_Mesh.name, "asset");
        path = FileUtil.GetProjectRelativePath(path);
        MeshUtility.Optimize(m_Mesh);
        AssetDatabase.CreateAsset(m_Mesh, path);
        AssetDatabase.SaveAssets();
    }
}