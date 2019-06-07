using Photon.Pun;
using UnityEngine;

public class FireRockets : MonoBehaviourPun
{
    public GameObject rocketPrefab;

    public Transform[] spawnPoints;

    public void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("FireBullets", RpcTarget.All);
        }
    }

    [PunRPC]
    public void FireBullets()
    {
        var ownerVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        foreach (var spawnPoint in spawnPoints)
        {
            var rocket = Instantiate(rocketPrefab, spawnPoint.position, spawnPoint.rotation);

            // pass this game object as an owner of the rocket
            rocket.GetComponent<Rocket>().Owner = gameObject;

            // rocket should start with the same velocity as owner
            rocket.GetComponent<Rigidbody2D>().velocity += ownerVelocity;
        }
    }
}