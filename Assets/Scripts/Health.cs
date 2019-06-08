using System;
using Photon.Pun;
using UI;
using UnityEngine;

public class Health : MonoBehaviour, IPunObservable
{
    public float startHealth;
    public float maxHealth;
    public ObjectHUD objectHUD;

    private float _currentHealth;

    private float CurrentHealth
    {
        get => _currentHealth;
        set => SetHealth(value);
    }

    private HealthBar _healthBar;

    public void Awake()
    {
        CurrentHealth = startHealth;

        if (objectHUD != null)
        {
            objectHUD.HealthBar.SetActive(true);
        }
    }

    private void SetHealth(float health)
    {
        _currentHealth = Mathf.Clamp(health, 0f, maxHealth);

        if (objectHUD != null)
        {
            objectHUD.HealthBar.SetSize((float) Math.Round(_currentHealth / maxHealth, 1));
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
}