using Photon.Pun;

public class Object : MonoBehaviourPun
{
    public new string name;

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
        if (photonView.IsMine || (photonView.IsSceneView && PhotonNetwork.IsMasterClient))
        {
            OnDied();
        }
    }
}