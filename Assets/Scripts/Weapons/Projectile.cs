using Photon.Pun;
using UnityEngine;

namespace Weapons
{
    public class Projectile : Weapon, IDealDamage, IHasOwner
    {
        public float speed = 5;
        public float duration = 2;
        public float damage = 50;

        public void Start()
        {
            var direction = transform.up;
            var velocity = speed * new Vector2(direction.x, direction.y);

            GetComponent<Rigidbody2D>().velocity += velocity;

            Destroy(gameObject, duration);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            // ignore collision with owner
            if (IsCollisionWithOwner(other.gameObject)) return;

            var photonView = other.gameObject.GetComponent<PhotonView>();

            if (photonView != null && (photonView.IsMine || photonView.IsSceneView))
            {
                var damageable = other.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    photonView.RPC("TakeDamage", RpcTarget.AllBuffered, damage);
                }
            }

            Destroy(gameObject);
        }

        protected override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);

            // add owner's velocity
            GetComponent<Rigidbody2D>().velocity += owner.GetComponent<Rigidbody2D>().velocity;
        }

        public GameObject GetOwner()
        {
            return Owner;
        }
    }
}