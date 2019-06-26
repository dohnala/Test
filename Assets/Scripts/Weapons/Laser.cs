using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class Laser : Weapon
    {
        private const float OffsetZ = -5;

        public float maxDistance;
        public float duration;
        public int ticksPerSecond;
        public float damagePerTick;

        private LineRenderer _lineRenderer;
        private GameObject _currentTarget;
        private float _tickTime;
        private float _timeSinceLastTick;

        private GameObject _effect;

        private void Start()
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

        private void OnDestroy()
        {
            if (_effect != null)
            {
                Destroy(_effect);
            }
        }

        private void Update()
        {
            var cachedPosition = SpawnPoint.position;
            var start = new Vector2(cachedPosition.x, cachedPosition.y);
            var direction = SpawnPoint.up;

            _lineRenderer.SetPosition(0, cachedPosition + new Vector3(0, 0, OffsetZ));

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

                _lineRenderer.SetPosition(1, (Vector3) hit.point + new Vector3(0, 0, OffsetZ));
                _lineRenderer.enabled = true;

                if (collisionEffect != null)
                {
                    if (_effect == null)
                    {
                        _effect = Instantiate(collisionEffect, hit.point, Quaternion.identity);
                    }
                    else
                    {
                        _effect.transform.position = hit.point;
                    }
                }

                if (_tickTime > 0 && _timeSinceLastTick >= _tickTime)
                {
                    var ticks = _timeSinceLastTick / _tickTime;
                    var rest = _timeSinceLastTick % _tickTime;

                    _timeSinceLastTick = rest;

                    var damageable = _currentTarget.GetComponent<IDamageable>();

                    damageable?.TakeDamage(ticks * damagePerTick, this, _ownerPhotonView, hit.point);
                }
            }
            else
            {
                _currentTarget = null;
                _timeSinceLastTick = 0;
                _lineRenderer.SetPosition(1,
                    cachedPosition + direction * maxDistance + new Vector3(0, 0, OffsetZ));
                _lineRenderer.enabled = true;

                if (_effect)
                {
                    Destroy(_effect);
                }
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