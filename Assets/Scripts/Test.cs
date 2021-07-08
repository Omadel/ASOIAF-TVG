using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour {
    public InputAction input;
    public Color c;
    public Material mat;


    [ContextMenu("Change")]
    private void ChangeColor() {
        c = new Color32(67, 160, 71, 1);
    }

    private void Start() {
        input.Enable();
        Material mat = new Material(this.mat) {
            color = c
        };
        GetComponent<MeshRenderer>().material = mat;
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(input.ReadValue<Vector2>());
        if(Physics.Raycast(ray, out RaycastHit hit, 100)) {
            Debug.DrawLine(ray.origin, hit.point);
            hit.transform.GetComponent<ASOIAF.TrayBehaviour>().isControlled = true;
        }
    }
}