using Photon.Pun;
using UI;
using UnityEngine;

public class Ship : Object, IPunObservable
{
    public static Ship Player;

    public GameObject shipCamera;

    public GameObject spriteMe;

    public GameObject spriteEnemy;

    public GameObject[] backgrounds;

    public ObjectHUD shipHUD;

    private Rigidbody2D _rb2D;

    public void Start()
    {
        shipCamera.SetActive(photonView.IsMine);
        spriteMe.SetActive(photonView.IsMine);
        spriteEnemy.SetActive(!photonView.IsMine);
        shipHUD.SetActive(!photonView.IsMine);

        _rb2D = GetComponent<Rigidbody2D>();

        if (photonView.IsMine)
        {
            Player = this;

            foreach (var background in backgrounds)
            {
                Instantiate(background, transform);
            }
        }
    }

    public void LateUpdate()
    {
        // do not rotate camera
        shipCamera.transform.eulerAngles = Vector3.zero;
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

    protected override void OnDied()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}