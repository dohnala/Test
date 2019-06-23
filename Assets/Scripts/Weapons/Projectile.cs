using UnityEngine;

namespace Weapons
{
    public class Projectile : Weapon, IDealDamage, IHasOwner
    {
        public float speed = 5;
        public float duration = 2;
        public float damage = 50;
        public GameObject collisionEffect;

        protected Rigidbody2D _rigidbody2D;
        protected Vector2 _ownerVelocity;

        public GameObject GetOwner()
        {
            return Owner;
        }

        protected virtual void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        protected void Start()
        {
            _rigidbody2D.velocity += (Vector2) (speed * transform.up);

            Destroy(gameObject, duration);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            // ignore collision with owner
            if (IsCollisionWithOwner(other.gameObject)) return;
            
            if (CanHandleCollision(other.gameObject))
            {
                var damageable = other.GetComponent<IDamageable>();

                damageable?.TakeDamage(damage, gameObject.transform.position);
            }

            Destroy(gameObject);
        }

        protected override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);

            _ownerVelocity = owner.GetComponent<Rigidbody2D>().velocity;

            // add owner's velocity
            _rigidbody2D.velocity += _ownerVelocity;
        }

        protected void OnDestroy()
        {
            var cachedTransform = gameObject.transform;

            if (collisionEffect != null)
            {
                var rotation = collisionEffect.transform.rotation * cachedTransform.rotation;

                Instantiate(collisionEffect, cachedTransform.position, rotation);
            }
        }
    }
}