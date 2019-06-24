using Photon.Pun;
using UI;
using UnityEngine;

public class Ship : Object, IPunObservable
{
    public static Ship Player;

    public GameObject spriteMe;

    public GameObject spriteEnemy;
    
    public GameObject minimapIconMe;

    public GameObject minimapIconEnemy;

    public ObjectHUD shipHUD;

    private Rigidbody2D _rb2D;

    protected override void Awake()
    {
        base.Awake();
        
        name = photonView.Owner.NickName;
        
        _rb2D = GetComponent<Rigidbody2D>();
    }
    
    public void Start()
    {
        Physics2D.IgnoreLayerCollision(Layers.Shield, Layers.Object);
        Physics2D.IgnoreLayerCollision(Layers.Shield, Layers.Shield);
        
        spriteMe.SetActive(photonView.IsMine);
        spriteEnemy.SetActive(!photonView.IsMine);
        
        minimapIconMe.SetActive(photonView.IsMine);
        minimapIconEnemy.SetActive(!photonView.IsMine);
        
        shipHUD.SetActive(!photonView.IsMine);

        if (photonView.IsMine)
        {
            Player = this;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ship's velocity must be synchronized
        if (stream.IsWriting)
        {
            stream.SendNext(_rb2D.velocity);
        }
        else if (stream.IsReading)
        {
            _rb2D.velocity = (Vector2) stream.ReceiveNext();
        }
    }
}