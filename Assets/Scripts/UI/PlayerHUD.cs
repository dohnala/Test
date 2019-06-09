using System;
using UnityEngine;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        public HealthBar healthBar;

        public void Update()
        {
            var ship = Ship.Player.gameObject;

            if (ship != null)
            {
                var health = ship.GetComponent<Health>();

                if (health != null)
                {
                    healthBar.SetActive(true);
                    healthBar.SetSize((float) Math.Round(health.CurrentHealth / health.MaxHealth, 2));
                }
            }
        }
    }
}