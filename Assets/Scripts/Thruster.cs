using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public Vector2 direction;
    public KeyCode keyCode;
    public GameObject thrusterEffect;

    private Rigidbody2D _shipBody;
    private Vector2 _initialPoint;
    private GameObject _effectInstance;

    public void Start()
    {
        _shipBody = Ship.Player.GetComponent<Rigidbody2D>(); 
        _initialPoint = new Vector2(0, 0);
    }

    public void Update()
    {        
        Vector2 relativeDirection = transform.TransformDirection(direction);

        if (Input.GetKeyDown(keyCode))
        {
            _effectInstance = Instantiate(thrusterEffect, transform);
            _effectInstance.transform.rotation = Quaternion.LookRotation(relativeDirection * -1);
        }
        
        if (Input.GetKeyUp(keyCode))
        {
            Destroy(_effectInstance);
        }
    }

    public void FixedUpdate()
    {
        Vector2 relativeDirection = transform.TransformDirection(direction);

        var accelerate = Input.GetKey(keyCode);
        ApplyForce(accelerate, relativeDirection);
    }

    private void ApplyForce(bool accelerate, Vector2 relativeDirection)
    {
        Transform thrusterTransform = transform;
        Vector2 thrusterDirection = relativeDirection;
        
        if (accelerate)
        {
            thrusterDirection = Vector3.Lerp(thrusterDirection, _initialPoint, Time.deltaTime);
        }
        else
        {
            thrusterDirection = Vector3.Lerp(_initialPoint, thrusterDirection, Time.deltaTime);
        }

        _shipBody.AddForceAtPosition( thrusterDirection, thrusterTransform.position);
    }
    
}
