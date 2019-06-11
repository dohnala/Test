using System;
using Photon.Pun;
using UI;
using UnityEngine;

public class Health : MonoBehaviour, IPunObservable, IDamageable
{
    public delegate void OnDied();

    public event OnDied onDied;

    public float startHealth;
    public float maxHealth;

    private float _currentHealth;

    public float CurrentHealth
    {
        get => _currentHealth;
        set => SetHealth(value);
    }

    public float MaxHealth => maxHealth;

    private HealthBar _healthBar;

    public void Awake()
    {
        CurrentHealth = startHealth;
    }

    private void SetHealth(float health)
    {
        _currentHealth = Mathf.Clamp(health, 0f, maxHealth);

        if (Math.Abs(_currentHealth) < maxHealth / 1000f)
        {
            onDied?.Invoke();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // health must be synchronized

        if (stream.IsWriting)
        {
            stream.SendNext(CurrentHealth);
            stream.SendNext(maxHealth);
        }
        else if (stream.IsReading)
        {
            CurrentHealth = (float) stream.ReceiveNext();
            maxHealth = (float) stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }
}