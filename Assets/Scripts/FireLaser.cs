using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FireLaser : MonoBehaviourPun
{
    public GameObject laserPrefab;
    public GameObject collisionEffect;

    public Transform[] spawnPoints;

    public float duration;

    public void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("FireLasers", RpcTarget.All);
        }
    }

    [PunRPC]
    public void FireLasers()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var laser = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation);

            laser.GetComponent<Laser>().SpawnPoint = spawnPoint;
            laser.GetComponent<Laser>().CollisionEffect = collisionEffect;
            laser.GetComponent<Laser>().Owner = gameObject;
            
            Destroy(laser, duration);
        }
    }
}
