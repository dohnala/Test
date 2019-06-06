using System;
using Photon.Pun;
using UnityEngine;

public class ShipController : MonoBehaviourPun
{
    public Transform[] leftThrusters;
    public Transform[] rightThrusters;
    public Transform[] frontThrusters;

    public Transform[] engines;

    private Rigidbody2D _rb2d;

    public void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        var forwardBackwardThrust = Input.GetAxis("Vertical");
        var leftRightThrust = Input.GetAxis("Horizontal");

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
        var forwardThrust = Input.GetAxis("Vertical");
        
        if (forwardThrust > 0)
        {
            foreach (var engine in engines)
            {
                Vector2 enginePos = new Vector2(engine.position.x, engine.transform.position.y); 
                _rb2d.AddForceAtPosition(Mathf.Abs(forwardThrust) * 2 * transform.up, enginePos);
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