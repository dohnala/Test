using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Shield : MonoBehaviourPun, IPunObservable, IDamageable, IHasOwner
{
    private const float ShieldEffectRadius = 0.5f;

    public float radius = 1f;

    public int startStacks = 4;

    public int maxStacks = 4;

    public float refreshStackTime = 1f;

    public GameObject shieldEffect;

    private CircleCollider2D _collider;

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
        if (photonView.IsMine)
        {
            StartCoroutine(RefreshStacks());    
        }
    }

    private void CreateShieldCollider()
    {
        _collider = gameObject.AddComponent<CircleCollider2D>();

        _collider.radius = radius;
        _collider.isTrigger = true;
    }

    public void TakeDamage(float damage, Vector2 point)
    {
        if (CurrentStacks > 0)
        {
            CurrentStacks -= 1;

            photonView.RPC("CreateShieldEffect", RpcTarget.All, point);    
        }
    }

    public GameObject GetOwner()
    {
        return transform.parent.gameObject;
    }

    [PunRPC]
    public void CreateShieldEffect(Vector2 point)
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