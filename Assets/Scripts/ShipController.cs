using Photon.Pun;
using UnityEngine;

public class ShipController : MonoBehaviourPun
{
    [SerializeField] private float speed = 5;

    private Rigidbody2D rb2d;
    private Camera myCamera;
    public void Start()
    {
        myCamera = GetComponentInChildren<Camera>();
        rb2d = GetComponent<Rigidbody2D>();
        if (photonView.IsMine)
        {
            myCamera.enabled = true;
        }
    }

    Vector2 myPosition;
    Vector2 mousePosition;

    public void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            myPosition = (Vector2)transform.position;
            mousePosition = (Vector2)(myCamera.ScreenToWorldPoint(Input.mousePosition));
            print("mp1" + mousePosition);
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            rb2d.AddForce(direction * speed);
            transform.eulerAngles = new Vector3(0, 0, GetAngleV2(myPosition, mousePosition));
            myCamera.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    float GetAngleV2(Vector2 vec1, Vector2 vec2) {
        float resu = Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x) * (180f / Mathf.PI);
        if (vec2.y == 1f)
        {
            resu = 90.0f;
        }
        if (vec2.y == -1f)
        {
            resu = -90.0f;
        }
        resu = resu - 90f;
        if (resu < 0)
        {
            resu += 360f;
        }
        return resu;
    }
}