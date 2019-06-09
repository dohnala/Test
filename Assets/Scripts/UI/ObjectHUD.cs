using UnityEngine;

namespace UI
{
    public class ObjectHUD : MonoBehaviour
    {
        public HealthBar healthBar;

        public HealthBar HealthBar => healthBar;

        private Transform _owner;
        private Vector3 _originalPosition;
        private Vector3 _originalScale;
        private Transform _cachedTransform;

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        private void Awake()
        {
            _cachedTransform = transform;

            _owner = _cachedTransform.parent;
            _originalPosition = _cachedTransform.localPosition;
            _originalScale = _cachedTransform.localScale;
        }

        private void LateUpdate()
        {
            var cachedOwnerScaleY = _owner.localScale.y;

            // HUD should always be at original position with respect to owner
            _cachedTransform.position = _owner.position + cachedOwnerScaleY * _originalPosition;

            // HUD should not scale on Y when owner is scaled
            _cachedTransform.localScale = new Vector3(
                _originalScale.x, _originalScale.y / cachedOwnerScaleY, _originalScale.z);

            // HUD should not rotate
            _cachedTransform.eulerAngles = Vector3.zero;
        }
    }
}