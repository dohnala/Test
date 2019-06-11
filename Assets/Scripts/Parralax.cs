using UnityEngine;

public class Parralax : MonoBehaviour
{
    public float speedCoefficient = 0.5f;

    private void FixedUpdate()
    {
        var cachedPosition = transform.position;

        var shipBody = Ship.Player.GetComponent<Rigidbody2D>();
        var speed = speedCoefficient * Time.deltaTime * shipBody.velocity;

        transform.position = new Vector3(cachedPosition.x + speed.x, cachedPosition.y + speed.y);
    }
}