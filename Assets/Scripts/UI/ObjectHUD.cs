using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ObjectHUD : MonoBehaviour
    {
        public HealthBar healthBar;
        public TextMeshProUGUI nameText;

        private GameObject _owner;
        private Vector3 _ownerOriginalScale;

        private Vector3 _originalPosition;
        private Vector3 _originalScale;
        private RectTransform _cachedTransform;

        private Object _object;

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        private void Awake()
        {
            _cachedTransform = GetComponent<RectTransform>();

            _owner = _cachedTransform.parent.gameObject;
            _ownerOriginalScale = _owner.transform.localScale;

            _originalPosition = _cachedTransform.localPosition;
            _originalScale = _cachedTransform.localScale;

            // HUD should not scale when owner is scaled
            _cachedTransform.localScale = new Vector3(
                _originalScale.x / _ownerOriginalScale.x,
                _originalScale.y / _ownerOriginalScale.y,
                _originalScale.z);

            // HUD should change width based on owner scale
            var cachedSizeDelta = _cachedTransform.sizeDelta;

            _cachedTransform.sizeDelta = new Vector2(
                cachedSizeDelta.x * _ownerOriginalScale.x, cachedSizeDelta.y);

            _object = _owner.GetComponent<Object>();
        }

        private void Update()
        {
            nameText.text = _object.Name;

            var health = _object.Health;

            if (health != null)
            {
                healthBar.SetActive(true);
                healthBar.SetValue((float) Math.Round(health.CurrentHealth / health.MaxHealth, 2));
            }
        }

        private void LateUpdate()
        {
            var cachedOwnerTransform = _owner.transform;

            // HUD should always be at original position with respect to owner
            _cachedTransform.position = cachedOwnerTransform.position + _ownerOriginalScale.y * _originalPosition;

            // HUD should not rotate
            _cachedTransform.eulerAngles = Vector3.zero;
        }
    }
}