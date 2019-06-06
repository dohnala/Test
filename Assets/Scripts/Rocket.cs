using Photon.Pun;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 5;
    public float duration = 2;

    public void Start()
    {
        var direction = transform.up;
        
        GetComponent<Rigidbody2D>().velocity += speed * new Vector2(direction.x, direction.y);

        Debug.Log("Rocket velocity " + GetComponent<Rigidbody2D>().velocity);
        
        Destroy(gameObject, duration);
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