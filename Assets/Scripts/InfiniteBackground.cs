using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public float parralax = 2f;
    
    void Update () {

        SpriteRenderer mr = GetComponent<SpriteRenderer>();
        Material mat = mr.material;
        
        Vector2 offset = mat.mainTextureOffset;

        var transform1 = transform;
        var position = transform1.position;
        var localScale = transform1.localScale;
        
        offset.x = position.x / localScale.x / parralax;
        offset.y = position.y / localScale.y / parralax;

        mat.mainTextureOffset = offset;
        transform1.eulerAngles = Vector3.zero;
    }
}
