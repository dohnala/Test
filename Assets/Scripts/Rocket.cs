using Photon.Pun;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 5;
    public float duration = 2;

    public GameObject Owner { private get; set; }

    public void Start()
    {
        var direction = transform.up;

        GetComponent<Rigidbody2D>().velocity += speed * new Vector2(direction.x, direction.y);

        Destroy(gameObject, duration);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // ignore collision with owner
        if (other.gameObject == Owner) return;

        var otherPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (otherPhotonView != null && otherPhotonView.IsMine)
        {
            // take damage via RPC
        }

        Destroy(gameObject);
    }
}