using Photon.Pun;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage, PhotonView source, Vector2 point);
}