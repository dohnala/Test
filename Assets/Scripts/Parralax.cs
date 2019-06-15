using UnityEngine;

public class Parralax : MonoBehaviour
{
    public Transform currentCamera;
    public float smooth = 1f;

    private float _scale;
    private Vector3 _previousCameraPosition;

    private void Start()
    {
        _previousCameraPosition = currentCamera.position;
        _scale = transform.position.z * -1;
    }

    private void LateUpdate()
    {
        var cachedCameraPosition = currentCamera.position;
        var cachedPosition = transform.position;

        var parallax = (_previousCameraPosition - cachedCameraPosition) * _scale;

        var targetPosition = new Vector3(
            cachedPosition.x + parallax.x,
            cachedPosition.y + parallax.y,
            cachedPosition.z);

        transform.position = Vector3.Lerp(cachedPosition, targetPosition, smooth * Time.deltaTime);

        _previousCameraPosition = cachedCameraPosition;
    }
}