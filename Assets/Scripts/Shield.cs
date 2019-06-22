using UnityEngine;

public class Shield : MonoBehaviour, IDamageable, IHasOwner
{
    private const float ShieldEffectRadius = 0.5f;

    public float radius = 1f;

    public GameObject shieldEffect;

    private CircleCollider2D _collider;

    private void Start()
    {
        CreateShieldCollider();
    }

    private void CreateShieldCollider()
    {
        _collider = gameObject.AddComponent<CircleCollider2D>();

        _collider.radius = radius;
        _collider.isTrigger = true;
    }

    public void TakeDamage(float damage, Vector2 point)
    {
        CreateShieldEffect(point);
    }

    public GameObject GetOwner()
    {
        return transform.parent.gameObject;
    }

    private void CreateShieldEffect(Vector2 point)
    {
        if (shieldEffect != null)
        {
            var cachedTransform = gameObject.transform;
            var cachedPosition = cachedTransform.position;

            var center = new Vector2(cachedPosition.x, cachedPosition.y);
            var angle = Mathf.Atan2(point.y - center.y, point.x - center.x) * 180 / Mathf.PI;

            var effect = Instantiate(shieldEffect, cachedPosition,
                Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)));

            effect.transform.SetParent(gameObject.transform);

            var scale = radius / ShieldEffectRadius;

            effect.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}