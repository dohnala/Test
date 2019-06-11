using Photon.Pun;

public class Object : MonoBehaviourPun
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();

        if (_health != null)
        {
            _health.onDied += OnDiedInternal;
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