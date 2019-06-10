using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Parralax : MonoBehaviour
{
    public float speedCoefficient = 0.5f;

    void FixedUpdate()
    {
        Rigidbody2D shipBody = Ship.Player.GetComponent<Rigidbody2D>();
        Vector2 speed = (speedCoefficient * Time.deltaTime * shipBody.velocity);
            
        transform.position = new Vector3(transform.position.x + speed.x, transform.position.y + speed.y);
    }
}
