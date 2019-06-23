using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FloatingTextManager : MonoBehaviour
    {
        public static FloatingTextManager Instance;

        public RectTransform canvas;

        public GameObject damageText;
        
        public Vector2 damageTextOffset = new Vector2(0.3f, 0.3f);

        private void Awake()
        {
            Instance = this;
        }

        public void CreateDamageText(float damage, Vector2 position)
        {
            var text = Instantiate(damageText, position + damageTextOffset, Quaternion.identity);

            text.transform.SetParent(canvas);
            text.GetComponent<Text>().text = Mathf.RoundToInt(damage).ToString();
        }
    }
}