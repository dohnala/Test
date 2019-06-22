using Photon.Pun;
using UI;
using UnityEngine;

public class Ship : Object, IPunObservable
{
    public static Ship Player;

    public GameObject spriteMe;

    public GameObject spriteEnemy;

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
        spriteMe.SetActive(photonView.IsMine);
        spriteEnemy.SetActive(!photonView.IsMine);
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