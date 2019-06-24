using System.Collections;
using Photon.Pun;
using UnityEngine;
using Weapons;

public class Shield : MonoBehaviourPun, IPunObservable, IDamageable, IHasOwner
{
    private const float ShieldHitEffectRadius = 0.73f;
    private const float ShieldEffectRadius = 2 * ShieldHitEffectRadius;
    
    public float radius = 1f;

    public int startStacks = 4;

    public int maxStacks = 4;

    public float refreshStackTime = 1f;

    public GameObject shieldEffect;
    
    public GameObject shieldHitEffect;

    private CircleCollider2D _collider;

    private GameObject _shieldEffect;

    private SpriteRenderer _shieldEffectRenderer;

    private int _currentStacks;

    public int CurrentStacks
    {
        get => _currentStacks;
        private set => SetStacks(value);
    }

    public float MaxStacks => maxStacks;

    private void Awake()
    {
        CreateShieldCollider();
        
        CurrentStacks = startStacks;
    }

    private void Start()
    {
        CreateShieldEffect();
        
        if (photonView.IsMine)
        {
            StartCoroutine(RefreshStacks());    
        }
    }

    private void LateUpdate()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }

    private void CreateShieldCollider()
    {
        _collider = gameObject.AddComponent<CircleCollider2D>();

        _collider.radius = radius;
        _collider.isTrigger = true;
    }

    public void TakeDamage(float damage, Weapon weapon, PhotonView source, Vector2 point)
    {
        if (photonView.IsMine || (photonView.IsSceneView && PhotonNetwork.IsMasterClient))
        {
            if (CurrentStacks > 0)
            {
                if (weapon.damageShield)
                {
                    CurrentStacks -= 1;
                }

                photonView.RPC("CreateShieldHitEffect", RpcTarget.All, point);    
            }  
        }
    }

    public GameObject GetOwner()
    {
        return transform.parent.gameObject;
    }

    public void CreateShieldEffect()
    {
        if (shieldEffect != null)
        {
            var cachedTransform = gameObject.transform;
            var cachedPosition = cachedTransform.position;

            _shieldEffect = Instantiate(shieldEffect, cachedPosition, Quaternion.identity);
            
            _shieldEffect.transform.SetParent(gameObject.transform);

            var scale = radius / ShieldEffectRadius;

            _shieldEffect.transform.localScale = new Vector3(scale, scale, scale);

            _shieldEffectRenderer = _shieldEffect.GetComponent<SpriteRenderer>();
        }
    }

    [PunRPC]
    public void CreateShieldHitEffect(Vector2 point)
    {
        if (shieldHitEffect != null)
        {
            var cachedTransform = gameObject.transform;
            var cachedPosition = cachedTransform.position;

            var center = new Vector2(cachedPosition.x, cachedPosition.y);
            var angle = Mathf.Atan2(point.y - center.y, point.x - center.x) * 180 / Mathf.PI;

            var effect = Instantiate(shieldHitEffect, cachedPosition,
                Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)));

            effect.transform.SetParent(gameObject.transform);

            var scale = radius / ShieldHitEffectRadius;

            effect.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // stacks must be synchronized

        if (stream.IsWriting)
        {
            stream.SendNext(CurrentStacks);
            stream.SendNext(maxStacks);
        }
        else if (stream.IsReading)
        {
            CurrentStacks = (int) stream.ReceiveNext();
            maxStacks = (int) stream.ReceiveNext();
        }
    }

    private void SetStacks(int stacks)
    {
        _currentStacks = Mathf.Clamp(stacks, 0, maxStacks);

        _collider.enabled = CurrentStacks > 0;

        if (_shieldEffectRenderer != null)
        {
            _shieldEffectRenderer.enabled = CurrentStacks > 0;
        }
    }

    private IEnumerator RefreshStacks()
    {
        while (true)
        {
            CurrentStacks += 1;

            yield return new WaitForSecondsRealtime(refreshStackTime);
        }
    }
}