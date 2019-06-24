using Photon.Pun;
using UnityEngine;
using Weapons;

public interface IDamageable
{
    void TakeDamage(float damage, Weapon weapon, PhotonView source, Vector2 point);
}