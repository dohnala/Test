using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private static float effectTime = 0.1f;
    private LineRenderer lr;
    public Transform SpawnPoint{ private get; set; }
    
    public GameObject CollisionEffect { private get; set; }

    public float maxDistance;
    
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
            
            GameObject effect = Instantiate(CollisionEffect, hit.point, hit.transform.rotation);
            Destroy(effect, effectTime);
        }
        else
        {
            lr.SetPosition(1, position + (up * maxDistance));
        }
        
    }
}
