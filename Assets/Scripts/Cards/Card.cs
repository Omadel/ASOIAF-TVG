using UnityEngine;

namespace ASOIAF {
    public abstract class Card : MonoBehaviour {
        [SerializeField] private bool preview = true;
        [SerializeField] private float previewSpeed = 20f;

        private void Start() {
            Validate();
        }

        private void Update() {
            if(preview) {
                transform.Rotate(Vector3.up * previewSpeed * Time.deltaTime, Space.Self);
            }
        }

        [ContextMenu("Update")]
        public virtual void Validate() {
            if(!gameObject.scene.IsValid()) {
                return;
            }

            SetTexts();
            SetImages();
        }

        protected abstract void SetTexts();
        protected abstract void SetImages();
    }
}
