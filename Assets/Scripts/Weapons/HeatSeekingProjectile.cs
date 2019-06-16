using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class HeatSeekingProjectile : Projectile
    {
        private const int MaxObjects = 20;

        public float angularSpeed = 150;
        public float radius = 3;

        private Collider2D[] _visibleObjects;
        private GameObject _target;

        protected override void Awake()
        {
            base.Awake();

            _visibleObjects = new Collider2D[MaxObjects];
        }

        protected void FixedUpdate()
        {
            var target = FindTarget();

            if (target != null)
            {
                MoveTowards(target);
            }
        }

        private GameObject FindTarget()
        {
            var cachedTransform = transform;
            var center = cachedTransform.position + cachedTransform.up * radius;

            Physics2D.OverlapCircleNonAlloc(center, radius, _visibleObjects);

            _target = _visibleObjects
                .Where(o => o != null)
                .Where(o => o.gameObject != gameObject)
                .Where(o => !IsCollisionWithOwner(o.gameObject))
                .Select(o => o.gameObject.GetComponent<HeatSource>())
                .Where(o => o != null)
                .OrderBy(Brightness)
                .Select(o => o.gameObject)
                .FirstOrDefault();

            return _target;
        }

        private void MoveTowards(GameObject target)
        {
            var forward = transform.up;

            var direction = (Vector2) target.transform.position - _rigidbody2D.position;
            direction.Normalize();

            var rotation = Vector3.Cross(direction, forward).z;

            _rigidbody2D.angularVelocity = -rotation * angularSpeed;
            _rigidbody2D.velocity = (Vector2) (forward * speed) + _ownerVelocity;
        }

        private float Distance(GameObject other)
        {
            return Vector3.Distance(transform.position, other.transform.position);
        }

        private float Brightness(HeatSource other)
        {
            var distance = Distance(other.gameObject);

            return other.Intensity / 4 * Mathf.PI * distance * distance;
        }
    }
}