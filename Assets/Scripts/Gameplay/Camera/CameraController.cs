using UnityEngine;
using UnityEngine.InputSystem;

namespace ASOIAF {
    public class CameraController : MonoBehaviour {
        public float CameraRotationAmplitude => cameraRotationAmplitude;
        public Transform CameraTransform => cameraTransform;

        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float normalSpeed = 30f, fastSpeed = 50f;
        [SerializeField] private float movementSpeed, movementTime;
        [SerializeField] private float rotationAmount;
        [SerializeField, Range(0f, 90f)] private float cameraRotationAmplitude;
        [SerializeField] private float XAngle, YAngle;
        [SerializeField] private Vector3 zoomAmount;

        [SerializeField] private Vector3 newPosition;
        [SerializeField] private Quaternion newRotation;
        [SerializeField] private Vector3 newZoom;

        [SerializeField] private InputActionReference panAxes, rotationAxes, fastCamera, zoomAxis, mouseDelta, mousePanActivation, mouseRotationActivation;
        [SerializeField] private bool canPanWithMouse, canRotateWithMouse;
        private void Awake() {
            fastCamera.action.performed += _ => movementSpeed = fastSpeed;
            fastCamera.action.canceled += _ => movementSpeed = normalSpeed;

            mousePanActivation.action.performed += _ => canPanWithMouse = true;
            mousePanActivation.action.canceled += _ => canPanWithMouse = false;
            mouseRotationActivation.action.performed += _ => canRotateWithMouse = true;
            mouseRotationActivation.action.canceled += _ => canRotateWithMouse = false;
        }

        private void Start() {
            newPosition = transform.position;
            newRotation = transform.rotation;

            newZoom = cameraTransform.localPosition;
            zoomAmount = newZoom.normalized;

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
            mouseDelta.action.Enable();
            mousePanActivation.action.Enable();
            mouseRotationActivation.action.Enable();
        }

        private void PanCamera() {
            Vector2 panAxes = this.panAxes.action.ReadValue<Vector2>();
            if(canPanWithMouse) {
                panAxes = mouseDelta.action.ReadValue<Vector2>();
            }

            newPosition += transform.forward * panAxes.y * movementSpeed * Time.deltaTime;
            newPosition += transform.right * panAxes.x * movementSpeed * Time.deltaTime;
            newPosition.y = 0;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        }

        private void RotateCamera() {
            Vector2 rotationAxes = this.rotationAxes.action.ReadValue<Vector2>();
            if(canRotateWithMouse) {
                rotationAxes = mouseDelta.action.ReadValue<Vector2>();
                rotationAxes.x = -rotationAxes.x;
            }

            YAngle += rotationAxes.x * rotationAmount * Time.deltaTime;
            XAngle += rotationAxes.y * rotationAmount * Time.deltaTime;
            XAngle = Mathf.Clamp(XAngle, -cameraRotationAmplitude / 2, cameraRotationAmplitude / 2);
            newRotation = Quaternion.Euler(new Vector3(XAngle, YAngle, 0));

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        }

        private void ZoomCamera() {
            float zoomAxis = this.zoomAxis.action.ReadValue<float>();

            newZoom += zoomAmount * zoomAxis;

            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        }
    }
}