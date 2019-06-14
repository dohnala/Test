using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Image fill;
        
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetValue(float value)
        {
            fill.fillAmount = value;
        }
    }
}