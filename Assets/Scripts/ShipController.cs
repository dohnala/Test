using Photon.Pun;
using UnityEngine;

public class ShipController : MonoBehaviourPun
{
    public float speed = 5;
    public float angle;

    private Rigidbody2D rb2d;
    private Vector2 direction;
    private float actualSpeed;
    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            //turn left or right
            angle -= Input.GetAxis("Horizontal");

            // change object rotation
            transform.eulerAngles = new Vector3(0, 0, angle);

            // calculate direction from actual ship rotation
            direction = new Vector2(-Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad));

            // accelerate or decelerate
            actualSpeed -= (-Input.GetAxis("Vertical") * speed);

            // move ship
            rb2d.AddForce(actualSpeed * direction);

            //slowin down when no force to rb2d
            rb2d.velocity = rb2d.velocity * 0.95f;
            actualSpeed = actualSpeed * 0.95f;
        }
    }
}