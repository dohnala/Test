using Photon.Pun;
using UnityEngine;

namespace Weapons
{
    public class Projectile : Weapon
    {
        public float speed = 5;
        public float duration = 2;

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
            if (other.gameObject == Owner) return;

            var otherPhotonView = other.gameObject.GetComponent<PhotonView>();

            if (otherPhotonView != null && otherPhotonView.IsMine)
            {
                // take damage via RPC
            }

            Destroy(gameObject);
        }

        protected override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);

            // add owner's velocity
            GetComponent<Rigidbody2D>().velocity += owner.GetComponent<Rigidbody2D>().velocity;
        }
    }
}