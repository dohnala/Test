using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool smooth;
    public float dampTime = 0.15f;

    private Camera _camera;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        var target = Ship.Player;

        if (target)
        {
            var cachedPosition = transform.position;
            var cachedTargetPosition = target.transform.position;
            
            if (smooth)
            {
                var point = _camera.WorldToViewportPoint(cachedTargetPosition);
                var delta = cachedTargetPosition - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                var destination = cachedPosition + delta;

                transform.position = Vector3.SmoothDamp(cachedPosition, destination, ref _velocity, dampTime);    
            }
            else
            {
                transform.position = cachedTargetPosition;
            }
        }
    }
}