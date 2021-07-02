using UnityEngine;
using UnityEngine.InputSystem;

namespace ASOIAF {
    public class CameraController : MonoBehaviour {
        public Transform cameraTransform;
        public float normalSpeed = 30f, fastSpeed = 50f;
        public float movementSpeed, movementTime;
        public float rotationAmount;
        public float XAngleMax, XAngleMin;
        public float XAngle, YAngle;
        public Vector3 zoomAmount;

        public Vector3 newPosition;
        public Quaternion newRotation;
        public Vector3 newZoom;

        public InputActionReference panAxes, rotationAxes, fastCamera, zoomAxis;

        private void Awake() {
            fastCamera.action.performed += input => movementSpeed = fastSpeed;
            fastCamera.action.canceled += input => movementSpeed = normalSpeed;
        }

        private void Start() {
            newPosition = transform.position;
            newRotation = transform.rotation;
            newZoom = cameraTransform.localPosition;
            movementSpeed = normalSpeed;
        }

        private void Update() {
            PanCamera();
            RotateCamera();
            ZoomCamera();
        }

        private void OnEnable() {
            panAxes.action.Enable();
            rotationAxes.action.Enable();
            fastCamera.action.Enable();
            zoomAxis.action.Enable();
        }

        private void PanCamera() {
            Vector2 panAxes = this.panAxes.action.ReadValue<Vector2>();

            newPosition += transform.forward * panAxes.y * movementSpeed * Time.deltaTime;
            newPosition += transform.right * panAxes.x * movementSpeed * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        }

        private void RotateCamera() {
            Vector2 rotationAxes = this.rotationAxes.action.ReadValue<Vector2>();

            YAngle += rotationAxes.x * rotationAmount * Time.deltaTime;
            XAngle += rotationAxes.y * rotationAmount * Time.deltaTime;
            XAngle = Mathf.Clamp(XAngle, XAngleMin, XAngleMax);
            newRotation = Quaternion.Euler(new Vector3(XAngle, YAngle, 0));

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        }

        private void ZoomCamera() {
            float zoomAxis = Mathf.Clamp(this.zoomAxis.action.ReadValue<float>(), -1f, 1f);

            newZoom += zoomAmount * zoomAxis;

            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        }
    }
}