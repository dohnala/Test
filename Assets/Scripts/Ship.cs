using Photon.Pun;
using UnityEngine;

public class Ship : MonoBehaviourPun, IPunObservable
{
    public GameObject shipCamera;

    public GameObject spriteMe;

    public GameObject spriteEnemy;

    private Rigidbody2D _rb2D;

    public void Start()
    {
        shipCamera.SetActive(photonView.IsMine);
        spriteMe.SetActive(photonView.IsMine);
        spriteEnemy.SetActive(!photonView.IsMine);

        _rb2D = GetComponent<Rigidbody2D>();
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
}