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
        public int ticksPerSecond;
        public float damagePerTick;

        private LineRenderer _lineRenderer;
        private Vector2 _previousEffectPoint;
        private GameObject _currentTarget;
        private float _tickTime;
        private float _timeSinceLastTick;
        
        public void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;

            if (ticksPerSecond > 0)
            {
                _tickTime = 1f / ticksPerSecond;    
            }
            else
            {
                _tickTime = 0;
            }

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
                if (hit.collider.gameObject == _currentTarget)
                {
                    _timeSinceLastTick += Time.deltaTime;
                }
                else
                {
                    _currentTarget = hit.collider.gameObject;
                    
                    // start with a tick
                    _timeSinceLastTick = _tickTime;
                }
                
                _lineRenderer.SetPosition(1, hit.point);
                _lineRenderer.enabled = true;

                var distance = Vector2.Distance(hit.point, _previousEffectPoint);

                // apply effect only when distance is small
                if (distance > 0.1f)
                {
                    // effect should be rotated towards ship
                    var rotation = Owner.transform.rotation * Quaternion.Euler(Vector3.up * 180);
                    var fireEffect = Instantiate(collisionEffect, hit.transform, true);
                    fireEffect.transform.position = hit.point;
                    fireEffect.transform.rotation = rotation;
                    fireEffect.transform.localScale = new Vector3(1, 1, 1);

                    Destroy(fireEffect, collisionEffectDuration);

                    _previousEffectPoint = hit.point;
                }

                if (_tickTime > 0 && _timeSinceLastTick >= _tickTime)
                {
                    var ticks = _timeSinceLastTick / _tickTime;
                    var rest = _timeSinceLastTick % _tickTime;

                    _timeSinceLastTick = rest;
                    
                    if (CanHandleCollision(_currentTarget))
                    {
                        var damageable = _currentTarget.GetComponent<IDamageable>();

                        damageable?.TakeDamage(ticks * damagePerTick, hit.point);
                    }   
                }
            }
            else
            {
                _currentTarget = null;
                _timeSinceLastTick = 0;
                _lineRenderer.SetPosition(1, cachedPosition + direction * maxDistance);
                _lineRenderer.enabled = true;
            }
        }

        private RaycastHit2D FindHit(Vector2 start, Vector3 direction, float distance)
        {
            // select the nearest hit while ignoring the owner

            // ReSharper disable once Unity.PreferNonAllocApi
            return Physics2D.RaycastAll(start, direction, distance)
                .Where(hit => !IsCollisionWithOwner(hit.collider.gameObject))
                .OrderBy(hit => hit.distance)
                .FirstOrDefault();
        }
    }
}