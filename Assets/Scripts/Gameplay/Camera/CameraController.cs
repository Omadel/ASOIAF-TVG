using UnityEngine;
using UnityEngine.InputSystem;

namespace ASOIAF {
    public class CameraController : MonoBehaviour {
        public float CameraRotationAmplitude => cameraRotationAmplitude;
        public Transform CameraTransform => cameraTransform;

        [SerializeField] private InputActionAsset inputActionMapAsset;
        [SerializeField] private string inputActionMapName = "Camera";

        [SerializeField] private Transform cameraTransform;
        [SerializeField] private float normalSpeed = 30f, fastSpeed = 50f;
        [SerializeField] private float movementTime;
        [SerializeField] private float rotationAmount;
        [SerializeField, Range(0f, 90f)] private float cameraRotationAmplitude;

        private void Awake() {
            inputActionMap = inputActionMapAsset.FindActionMap(inputActionMapName);

            InputAction fastAction = inputActionMap.FindAction("Fast");
            fastAction.performed += _ => SetSpeed(fastSpeed);
            fastAction.canceled += _ => SetSpeed(normalSpeed);
            InputAction mousePanActivationAction = inputActionMap.FindAction("MousePanActivation");
            mousePanActivationAction.performed += _ => canPanWithMouse = true;
            mousePanActivationAction.canceled += _ => canPanWithMouse = false;
            InputAction mouseRotationActivationAction = inputActionMap.FindAction("MouseRotateActivation");
            mouseRotationActivationAction.performed += _ => canRotateWithMouse = true;
            mouseRotationActivationAction.canceled += _ => canRotateWithMouse = false;

            panAxes = inputActionMap.FindAction("PanAxes");
            rotationAxes = inputActionMap.FindAction("RotationAxes");
            zoomAxis = inputActionMap.FindAction("Zoom");
            mouseDelta = inputActionMap.FindAction("MouseDelta");
        }

        private void SetSpeed(float speed) {
            movementSpeed = speed;
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
            foreach(InputAction action in inputActionMap.actions) {
                action.Enable();
            }
            //panAxes.action.Enable();
            //rotationAxes.action.Enable();
            //fastCamera.action.Enable();
            //zoomAxis.action.Enable();
            //mouseDelta.action.Enable();
            //mousePanActivation.action.Enable();
            //mouseRotationActivation.action.Enable();
        }

        private void OnDisable() {
            foreach(InputAction action in inputActionMap.actions) {
                action.Disable();
            }
            //panAxes.action.Disable();
            //rotationAxes.action.Disable();
            //fastCamera.action.Disable();
            //zoomAxis.action.Disable();
            //mouseDelta.action.Disable();
            //mousePanActivation.action.Disable();
            //mouseRotationActivation.action.Disable();
        }

        private void PanCamera() {
            Vector2 panAxes = this.panAxes.ReadValue<Vector2>();
            if(canPanWithMouse) {
                panAxes = mouseDelta.ReadValue<Vector2>();
            }

            newPosition += movementSpeed * panAxes.y * Time.deltaTime * transform.forward;
            newPosition += movementSpeed * panAxes.x * Time.deltaTime * transform.right;
            newPosition.y = 0;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        }

        private void RotateCamera() {
            Vector2 rotationAxes = this.rotationAxes.ReadValue<Vector2>();
            if(canRotateWithMouse) {
                rotationAxes = mouseDelta.ReadValue<Vector2>();
                rotationAxes.x = -rotationAxes.x;
            }

            YAngle += rotationAxes.x * rotationAmount * Time.deltaTime;
            XAngle += rotationAxes.y * rotationAmount * Time.deltaTime;
            XAngle = Mathf.Clamp(XAngle, -cameraRotationAmplitude / 2, cameraRotationAmplitude / 2);
            newRotation = Quaternion.Euler(new Vector3(XAngle, YAngle, 0));

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        }

        private void ZoomCamera() {
            float zoomAxis = this.zoomAxis.ReadValue<float>();

            newZoom += zoomAmount * zoomAxis;

            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        }

        private InputActionMap inputActionMap;
        private InputAction panAxes, rotationAxes, zoomAxis, mouseDelta;
        private bool canPanWithMouse, canRotateWithMouse;
        private float movementSpeed;
        private float XAngle, YAngle;
        private Quaternion newRotation;
        private Vector3 newPosition;
        private Vector3 zoomAmount;
        private Vector3 newZoom;
    }
}