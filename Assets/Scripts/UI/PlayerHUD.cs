using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        public Bar healthBar;
        public Text healthBarValue;
        public Bar shieldBar;
        public Text stats;
        
        public void Update()
        {
            var player = Ship.Player;

            if (player != null)
            {
                var health = player.gameObject.GetComponent<Health>();

                if (health != null)
                {
                    healthBar.SetValue((float) Math.Round(health.CurrentHealth / health.MaxHealth, 2));
                    healthBarValue.text = $"{Mathf.RoundToInt(health.CurrentHealth)}/{Mathf.RoundToInt(health.MaxHealth)}";
                }
                
                var shield = player.GetComponentInChildren<Shield>();

                if (shield != null)
                {
                    shieldBar.SetValue((float) Math.Round(shield.CurrentStacks / shield.MaxStacks, 2));
                }
            }
            else
            {
                healthBar.SetValue(0f);
                healthBarValue.text = "0/0";
                shieldBar.SetValue(0f);
            }

            stats.text = $"FPS: {GameManager.GetFPS()} Ping: {GameManager.GetPing()}ms";
        }
    }
}