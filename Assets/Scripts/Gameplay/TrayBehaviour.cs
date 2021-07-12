using UnityEngine;

namespace ASOIAF {
    public class TrayBehaviour : MonoBehaviour {
        public bool isControlled = false;
        public Transform CameraRig;
        [SerializeField] private GameObject agentPrefab;
        [SerializeField] private UnitBehaviour[] units;
        [SerializeField] private int wounds = 12;
        [SerializeField] private UnityEngine.Material material;
        [SerializeField] private Color c;

        private void OnMouseDown() {
            print("Clicked");
        }

        private void Awake() {
            UnityEngine.Material mat = new UnityEngine.Material(material) {
                color = c
            };
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            Debug.Log(renderers.Length, renderers[renderers.Length - 1]);
            foreach(Renderer renderer in renderers) {
                renderer.material = mat;
            }
        }

        private void Start() {
            wounds = units.Length;
        }

        private void Update() {
            if(isControlled) {
                transform.position = CameraRig.transform.position;
                Vector3 forward = CameraRig.forward;
                forward.y = transform.position.y;
                transform.forward = forward;
            }
            foreach(UnitBehaviour unit in units) {
                unit.transform.forward = transform.forward;
            }
        }

        [ContextMenu("Loose One Wound")]
        public void LosseOneWound() {
            wounds--;
            units[wounds].Die();
            Impact();
        }

        public void Block() {
            foreach(UnitBehaviour unit in units) {
                unit.ToggleAnimatorBool("IsBlocking");
            }
        }
        public void Impact() {
            foreach(UnitBehaviour unit in units) {
                unit.ToggleAnimatorBool("Impact");
            }
        }
    }
}
