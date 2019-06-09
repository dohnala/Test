using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        public Transform SpawnPoint { get; set; }

        private GameObject _owner;

        public GameObject Owner
        {
            get => _owner;
            set => SetOwner(value);
        }

        protected virtual void SetOwner(GameObject owner)
        {
            _owner = owner;
        }
    }
}