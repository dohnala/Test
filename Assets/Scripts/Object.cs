using Photon.Pun;
using UnityEngine;

public class Object : MonoBehaviourPun
{
    public new string name;
    
    public GameObject destroyEffect;

    public string Name => name;

    public Health Health { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();

        if (Health != null)
        {
            Health.onDied += OnDiedInternal;
        }
    }

    protected virtual void OnDied()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnDiedInternal()
    {
        if (destroyEffect != null)
        {
            photonView.RPC("DestroyObject", RpcTarget.All);
        }
    }

    [PunRPC]
    public void DestroyObject()
    {
        gameObject.SetActive(false);
        
        Instantiate(destroyEffect, gameObject.transform.position, Quaternion.identity);    
        
        if (photonView.IsMine || (photonView.IsSceneView && PhotonNetwork.IsMasterClient))
        {
            OnDied();
        }
    }
}