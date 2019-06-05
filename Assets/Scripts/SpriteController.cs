using UnityEngine;

public class SpriteController : MonoBehaviour
{

    public bool enableIfMy;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<ShipController>().photonView.IsMine)
        {
            gameObject.SetActive(enableIfMy);
        }
        else
        {
            gameObject.SetActive(!enableIfMy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
