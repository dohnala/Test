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
                var cachedEnginePos = engine.position;

                _rb2d.AddForceAtPosition(Mathf.Abs(forwardThrust) * 2 * transform.up,
                    new Vector2(cachedEnginePos.x, cachedEnginePos.y));
            }
        }
    }

    private void ApplyThrusters(IEnumerable<Transform> thrusters, float thrust, Vector3 direction)
    {
        foreach (var thruster in thrusters)
        {
            var cachedThrusterPos = thruster.position;

            _rb2d.AddForceAtPosition(direction * thrust,
                new Vector2(cachedThrusterPos.x, cachedThrusterPos.y));
        }
    }
}