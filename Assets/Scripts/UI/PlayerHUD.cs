using UI;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public HealthBar healthBar;

    public HealthBar HealthBar => healthBar;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}