using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Image bar;
        
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetSize(float value)
        {
            bar.fillAmount = value;
        }
    }
}