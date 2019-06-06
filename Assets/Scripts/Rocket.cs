using Photon.Pun;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 0.1f;
    public float duration = 1;

    public void Start()
    {
        Destroy(gameObject, duration);
    }

    public void FixedUpdate()
    {
        transform.Translate(0, speed, 0, Space.Self);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var otherPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (otherPhotonView != null && otherPhotonView.IsMine)
        {
            // Take damage via RPC
        }

        Destroy(gameObject);
    }
}