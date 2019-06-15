using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class HeatSeekingProjectile : Projectile
    {
        private const int MaxObjects = 10;

        public float angularSpeed = 200;
        public float radius = 8;

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

        protected GameObject FindTarget()
        {
            var cachedTransform = transform;
            var center = cachedTransform.position + cachedTransform.up * radius;

            Physics2D.OverlapCircleNonAlloc(center, radius, _visibleObjects);

            _target = _visibleObjects
                .Where(c => c != null)
                .Where(c => c.gameObject != gameObject)
                .Where(c => !IsCollisionWithOwner(c.gameObject))
                .OrderBy(Distance)
                .Select(c => c.gameObject)
                .FirstOrDefault();

            return _target;
        }

        protected void MoveTowards(GameObject target)
        {
            var forward = transform.up;

            var direction = (Vector2) target.transform.position - _rigidbody2D.position;
            direction.Normalize();

            var rotation = Vector3.Cross(direction, forward).z;

            _rigidbody2D.angularVelocity = -rotation * angularSpeed;
            _rigidbody2D.velocity = (Vector2) (forward * speed) + _ownerVelocity;
        }

        protected float Distance(Collider2D other)
        {
            return Vector3.Distance(transform.position, other.gameObject.transform.position);
        }
    }
}