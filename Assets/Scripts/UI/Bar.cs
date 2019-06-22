using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Bar : MonoBehaviour
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