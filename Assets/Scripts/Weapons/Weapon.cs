using Photon.Pun;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [Tooltip("Check whether a weapon passes through the shield")]
        public bool passShield;

        [Tooltip("Check whether a weapon damages the shield")]
        public bool damageShield;
        
        public Transform SpawnPoint { get; set; }

        protected GameObject _owner;

        protected PhotonView _ownerPhotonView;

        public GameObject Owner
        {
            get => _owner;
            set => SetOwner(value);
        }

        protected virtual void SetOwner(GameObject owner)
        {
            _owner = owner;
            _ownerPhotonView = _owner.GetComponent<PhotonView>();
        }
        
        protected bool IsCollisionWithOwner(GameObject other)
        {
            if (other == Owner)
            {
                return true;
            }

            var hasOwner = other.GetComponent<IHasOwner>();

            return hasOwner != null && hasOwner.GetOwner() == Owner;
        }

        protected bool CanHandleCollision(GameObject other)
        {
            var photonView = other.GetComponent<PhotonView>();

            if (photonView == null)
            {
                photonView = _owner.GetComponent<PhotonView>();
            }
            
            return photonView != null &&
                   (photonView.IsMine || (photonView.IsSceneView && PhotonNetwork.IsMasterClient));
        }
    }
}