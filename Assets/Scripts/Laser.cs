using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    public Transform SpawnPoint{ private get; set; }
    
    public GameObject Owner { private get; set; }
    
    public GameObject CollisionEffect { private get; set; }

    public float maxDistance;
    public float laserFireDuration;

    private Vector2 previousEffectPoint;
    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        var position = SpawnPoint.position;
        var position2d = new Vector2(position.x, position.y);
        var up = SpawnPoint.up;

        lr.SetPosition(0, position);

        RaycastHit2D hit = Physics2D.Raycast(position2d, up, maxDistance);
        
        if (hit.collider != null)
        {
            lr.SetPosition(1, hit.point);

            var distance = Vector2.Distance(hit.point, previousEffectPoint);
            
            // apply effect only when distance is small
            if (distance > 0.1f)
            {
                // effect should be rotated towards ship
                var rotation = Owner.transform.rotation * Quaternion.Euler(Vector3.up * 180);
                GameObject fireEffect = Instantiate(CollisionEffect, hit.point, rotation);
                Destroy(fireEffect, laserFireDuration);
                
                previousEffectPoint = hit.point;
            }

        }
        else
        {
            lr.SetPosition(1, position + (up * maxDistance));
        }
        
    }
    
}
