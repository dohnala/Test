using System.Collections.Generic;
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
            ApplyThrusters(leftThrusters, leftRightThrust, transform.right, 2);
        }

        if (leftRightThrust > 0)
        {
            ApplyThrusters(rightThrusters, leftRightThrust, transform.right, 2);
        }

        if (forwardBackwardThrust < 0)
        {
            ApplyThrusters(frontThrusters, forwardBackwardThrust, transform.up, 2);
        }

        if (forwardBackwardThrust > 0)
        {
            ApplyThrusters(engines, forwardBackwardThrust, transform.up, 5);
        }
    }

    private void ApplyThrusters(IEnumerable<Transform> thrusters, float thrust, Vector3 direction, float acceleration)
    {
        foreach (var thruster in thrusters)
        {
            var cachedThrusterPos = thruster.position;

            _rb2d.AddForceAtPosition(acceleration * thrust * direction,
                new Vector2(cachedThrusterPos.x, cachedThrusterPos.y));
        }
    }
}