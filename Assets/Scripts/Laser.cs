using UnityEngine;

public class Laser : MonoBehaviour
{
    public float maxDistance;
    public float laserFireDuration;

    public Transform SpawnPoint { private get; set; }

    public GameObject Owner { private get; set; }

    public GameObject CollisionEffect { private get; set; }

    private LineRenderer _lineRenderer;

    private Vector2 _previousEffectPoint;

    public void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        var cachedSpawnPosition = SpawnPoint.position;
        var start = new Vector2(cachedSpawnPosition.x, cachedSpawnPosition.y);
        var direction = SpawnPoint.up;

        _lineRenderer.SetPosition(0, cachedSpawnPosition);

        var hit = Physics2D.Raycast(start, direction, maxDistance);

        if (hit.collider != null)
        {
            _lineRenderer.SetPosition(1, hit.point);

            var distance = Vector2.Distance(hit.point, _previousEffectPoint);

            // apply effect only when distance is small
            if (distance > 0.1f)
            {
                // effect should be rotated towards ship
                var rotation = Owner.transform.rotation * Quaternion.Euler(Vector3.up * 180);
                var fireEffect = Instantiate(CollisionEffect, hit.point, rotation);

                Destroy(fireEffect, laserFireDuration);

                _previousEffectPoint = hit.point;
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, cachedSpawnPosition + direction * maxDistance);
        }
    }
}