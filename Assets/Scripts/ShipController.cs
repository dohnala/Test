using Photon.Pun;
using UnityEngine;

public class ShipController : MonoBehaviourPun
{
    public float speed = 5;

    private Rigidbody2D rb2d;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            rb2d.AddForce(speed * direction);
        }
    }
}