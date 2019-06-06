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
        foreach (var spawnPoint in spawnPoints)
        {
            Instantiate(rocketPrefab, spawnPoint.position, spawnPoint.rotation);
        }    
    }
}
