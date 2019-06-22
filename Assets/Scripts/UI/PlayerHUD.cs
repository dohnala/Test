using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        public Bar healthBar;
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
                    healthBar.SetActive(true);
                    healthBar.SetValue((float) Math.Round(health.CurrentHealth / health.MaxHealth, 2));
                }
                
                var shield = player.GetComponentInChildren<Shield>();

                if (shield != null)
                {
                    shieldBar.SetActive(true);
                    shieldBar.SetValue((float) Math.Round(shield.CurrentStacks / shield.MaxStacks, 2));
                }
            }

            stats.text = $"FPS: {GameManager.GetFPS()} Ping: {GameManager.GetPing()}ms";
        }
    }
}