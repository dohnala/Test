using System;
using Photon.Pun;
using UnityEngine;

public class ShipController : MonoBehaviourPun
{
    public float speed = 5;
    public float drag = 0.95f;
    
    private Rigidbody2D _rb2d;
    
    private float _angle;
    private float _acceleration;

    public void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            var cachedTransform = transform;
            
            //turn left or right
            _angle -= Input.GetAxis("Horizontal");

            // change object rotation
            cachedTransform.eulerAngles = new Vector3(0, 0, _angle);

            var cachedEulerAngles = cachedTransform.eulerAngles;
            
            // calculate direction from actual ship rotation
            var direction = new Vector2(
                -Mathf.Sin(cachedEulerAngles.z * Mathf.Deg2Rad),
                Mathf.Cos(cachedEulerAngles.z * Mathf.Deg2Rad));

            // accelerate or decelerate
            _acceleration -= -Input.GetAxis("Vertical") * speed;
            _acceleration = Math.Max(0, _acceleration);

            // move ship
            _rb2d.AddForce(_acceleration * direction);

            //slowing down when no force to rb2d
            _rb2d.velocity *= drag;
            _acceleration *= drag;
        }
    }
}