using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponController : MonoBehaviourPun
{
    [Serializable]
    public struct WeaponBinding
    {
        public KeyCode key;
        public GameObject weapon;
    }

    public WeaponBinding[] weaponBindings;

    public KeyCode fireKey;

    public Transform[] spawnPoints;

    private Dictionary<KeyCode, GameObject> _weaponsDictionary;

    private GameObject _currentWeapon;

    public void Start()
    {
        if (weaponBindings.Length > 0)
        {
            _currentWeapon = weaponBindings[0].weapon;
        }

        _weaponsDictionary = new Dictionary<KeyCode, GameObject>();

        foreach (var weaponBinding in weaponBindings)
        {
            _weaponsDictionary.Add(weaponBinding.key, weaponBinding.weapon);
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
    }

    [PunRPC]
    public void FireWeapon()
    {
        if (!_currentWeapon) return;

        foreach (var spawnPoint in spawnPoints)
        {
            var weapon = Instantiate(_currentWeapon, spawnPoint.position, spawnPoint.rotation);

            // pass spawn point
            weapon.GetComponent<Weapon>().SpawnPoint = spawnPoint;

            // pass this game object as an owner of the weapon
            weapon.GetComponent<Weapon>().Owner = gameObject;
        }
    }
}