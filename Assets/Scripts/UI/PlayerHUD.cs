using System;
using UnityEngine;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        public HealthBar healthBar;

        public void Update()
        {
            var player = Ship.Player;

            if (player != null)
            {
                var health = player.gameObject.GetComponent<Health>();

                if (health != null)
                {
                    healthBar.SetActive(true);
                    healthBar.SetSize((float) Math.Round(health.CurrentHealth / health.MaxHealth, 2));
                }
            }
        }
    }
}