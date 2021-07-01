using UnityEngine;
using UnityEngine.InputSystem;

namespace ASOIAF {
    public class CameraController : MonoBehaviour {
        public float normalSpeed = 30f, fastSpeed = 50f;
        public float movementSpeed, movementTime;
        public float rotationAmount;
        public Vector3 newPosition;
        public Quaternion newRotation;

        public InputActionReference panAxes, rotationAxes, fastCamera;

        private void Awake() {
            fastCamera.action.performed += input => movementSpeed = fastSpeed;
            fastCamera.action.canceled += input => movementSpeed = normalSpeed;
        }

        private void Start() {
            newPosition = transform.position;
            newRotation = transform.rotation;
            movementSpeed = normalSpeed;
        }

        private void Update() {
            PanCamera();
        }


        private void OnEnable() {
            panAxes.action.Enable();
            rotationAxes.action.Enable();
            fastCamera.action.Enable();
        }

        private void PanCamera() {
            Vector2 XYaxis = panAxes.action.ReadValue<Vector2>();

            newPosition += transform.forward * XYaxis.y * movementSpeed * Time.deltaTime;
            newPosition += transform.right * XYaxis.x * movementSpeed * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        }
    }
}