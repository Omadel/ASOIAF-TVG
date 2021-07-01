using UnityEngine;

[CreateAssetMenu(fileName = "RoundedQuadMeshData", menuName = "ScriptableObjects/RoundedQuadMeshData", order = 1)]
public class RoundedQuadMeshData : ScriptableObject {
    public float RoundEdges { get => roundEdges; }
    public float RoundTopLeft { get => roundTopLeft; }
    public float RoundTopRight { get => roundTopRight; }
    public float RoundBottomLeft { get => roundBottomLeft; }
    public float RoundBottomRight { get => roundBottomRight; }
    public float Width { get => width; }
    public bool UsePercentage { get => usePercentage; }
    public Rect Rect { get => rect; }
    public float Scale { get => scale; }
    public int CornerVertexCount { get => cornerVertexCount; }
    public bool CreateUV { get => createUV; }
    public bool FlipBackFaceUV { get => flipBackFaceUV; }
    public bool DoubleSided { get => doubleSided; }
    public bool DifferentBackFace { get => differentBackFace; }

    [SerializeField] private float roundEdges = 0.5f;
    [SerializeField] private float roundTopLeft = 0.0f, roundTopRight = 0.0f, roundBottomLeft = 0.0f, roundBottomRight = 0.0f;
    [SerializeField] private float width;
    [SerializeField] private bool usePercentage = true;
    [SerializeField] private Rect rect = new Rect(-0.5f, -0.5f, 1f, 1f);
    [SerializeField] private float scale = 1f;
    [SerializeField, Min(2)] private int cornerVertexCount = 8;
    [SerializeField] private bool createUV = true;
    [SerializeField] private bool flipBackFaceUV = false;
    [SerializeField] private bool doubleSided = false;
    [SerializeField] private bool differentBackFace = false;
}
