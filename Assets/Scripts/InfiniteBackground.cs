using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public float parralax = 2f;

    private Material _material;

    public void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    public void Update()
    {
        var offset = _material.mainTextureOffset;

        var cachedTransform = transform;
        var cachedPosition = cachedTransform.position;
        var cachedScale = cachedTransform.localScale;

        offset.x = cachedPosition.x / cachedScale.x / parralax;
        offset.y = cachedPosition.y / cachedScale.y / parralax;

        _material.mainTextureOffset = offset;
        cachedTransform.eulerAngles = Vector3.zero;
    }
}