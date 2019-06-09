using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class Laser : Weapon
    {
        public float maxDistance;
        public float duration;
        public GameObject collisionEffect;
        public float collisionEffectDuration;

        private LineRenderer _lineRenderer;
        private Vector2 _previousEffectPoint;

        public void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();

            Destroy(gameObject, duration);
        }

        public void Update()
        {
            var cachedPosition = SpawnPoint.position;
            var start = new Vector2(cachedPosition.x, cachedPosition.y);
            var direction = SpawnPoint.up;

            _lineRenderer.SetPosition(0, cachedPosition);

            var hit = FindHit(start, direction, maxDistance);

            if (hit.collider != null)
            {
                _lineRenderer.SetPosition(1, hit.point);

                var distance = Vector2.Distance(hit.point, _previousEffectPoint);

                // apply effect only when distance is small
                if (distance > 0.1f)
                {
                    // effect should be rotated towards ship
                    var rotation = Owner.transform.rotation * Quaternion.Euler(Vector3.up * 180);
                    var fireEffect = Instantiate(collisionEffect, hit.point, rotation);

                    Destroy(fireEffect, collisionEffectDuration);

                    _previousEffectPoint = hit.point;
                }
            }
            else
            {
                _lineRenderer.SetPosition(1, cachedPosition + direction * maxDistance);
            }
        }

        private RaycastHit2D FindHit(Vector2 start, Vector3 direction, float distance)
        {
            // select the nearest hit while ignoring the owner

            // ReSharper disable once Unity.PreferNonAllocApi
            return Physics2D.RaycastAll(start, direction, distance)
                .Where(hit => hit.collider.gameObject != Owner)
                .OrderBy(hit => hit.distance)
                .FirstOrDefault();
        }
    }
}