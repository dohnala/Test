using System;
using Photon.Pun;
using UnityEngine;

public class ShipController : MonoBehaviourPun
{
    public float speed = 1;
    public float drag = 0.95f;

    public Transform[] leftThrusters;
    public Transform[] rightThrusters;
    public Transform[] frontThrusters;

    public Transform[] engines;

    private Rigidbody2D _rb2d;
    
    private float _angle;
    private float _acceleration;

    public void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        float forwardBackwardThrust = Input.GetAxis("Vertical");
        float leftRightThrust = Input.GetAxis("Horizontal");

        if (leftRightThrust < 0)
        {
            ApplyThrusters(leftThrusters, leftRightThrust, transform.right);
        }

        if (leftRightThrust > 0)
        {
            ApplyThrusters(rightThrusters, leftRightThrust, transform.right);
        }

        if (forwardBackwardThrust < 0)
        {
            ApplyThrusters(frontThrusters, forwardBackwardThrust, transform.up);
        }

        ApplyEngines();
    }

    private void ApplyEngines()
    {
        float forwardThrust = Input.GetAxis("Vertical");
        
        foreach (var engine in engines)
        {
            if (forwardThrust > 0)
            {
                Vector2 enginePos = new Vector2(engine.position.x, engine.transform.position.y);
                _rb2d.AddForceAtPosition(transform.up * Mathf.Abs(forwardThrust) * 2, enginePos);
            }
        }
    }

    private void ApplyThrusters(Transform[] thrusters, float thrust, Vector3 direction)
    {
        foreach (var thruster in thrusters)
        {
            Vector2 thrusterPos = new Vector2(thruster.position.x, thruster.transform.position.y);
            _rb2d.AddForceAtPosition(direction * thrust, thrusterPos);
        }
    }
}