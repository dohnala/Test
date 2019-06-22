﻿using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Weapons
{
    public class WeaponController : MonoBehaviourPun
    {
        [Serializable]
        public struct WeaponBinding
        {
            public KeyCode key;
            public GameObject weapon;
            public Transform[] spawnPoints;
        }

        public WeaponBinding[] weaponBindings;

        public KeyCode fireKey;

        public GameObject[] shipColliders;

        private Dictionary<KeyCode, GameObject> _weaponsDictionary;

        private Dictionary<KeyCode, Transform[]> _spawnPointsDictionary;

        private GameObject _currentWeapon;

        private Transform[] _currentSpawnPoints;

        public void Start()
        {
            if (weaponBindings.Length > 0)
            {
                _currentWeapon = weaponBindings[0].weapon;
                _currentSpawnPoints = weaponBindings[0].spawnPoints;
            }

            _weaponsDictionary = new Dictionary<KeyCode, GameObject>();
            _spawnPointsDictionary = new Dictionary<KeyCode, Transform[]>();

            foreach (var weaponBinding in weaponBindings)
            {
                _weaponsDictionary.Add(weaponBinding.key, weaponBinding.weapon);
                _spawnPointsDictionary.Add(weaponBinding.key, weaponBinding.spawnPoints);
            }
        }

        public void Update()
        {
            if (!photonView.IsMine) return;

            if (Input.GetKeyDown(fireKey))
            {
                photonView.RPC("FireWeapon", RpcTarget.All);
            }

            foreach (var key in _weaponsDictionary.Keys)
            {
                if (Input.GetKeyDown(key))
                {
                    photonView.RPC("SwitchWeapon", RpcTarget.All, key);
                }
            }
        }

        [PunRPC]
        public void SwitchWeapon(KeyCode key)
        {
            _currentWeapon = _weaponsDictionary[key];
            _currentSpawnPoints = _spawnPointsDictionary[key];
        }

        [PunRPC]
        public void FireWeapon()
        {
            if (!_currentWeapon) return;

            foreach (var spawnPoint in _currentSpawnPoints)
            {
                var weapon = Instantiate(_currentWeapon, spawnPoint.position, spawnPoint.rotation);

                // pass spawn point
                weapon.GetComponent<Weapon>().SpawnPoint = spawnPoint;

                // pass this game object as an owner of the weapon
                weapon.GetComponent<Weapon>().Owner = gameObject;

                // ignore collision with ship's colliders
                foreach (var shipCollider in shipColliders)
                {
                    if (shipCollider != null)
                    {
                        Physics2D.IgnoreCollision(
                            weapon.GetComponent<Collider2D>(), 
                            shipCollider.GetComponent<Collider2D>());     
                    }
                }
            }
        }
    }
}