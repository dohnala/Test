using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIplacement : MonoBehaviour
{
    public GameObject source;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(FindObjectOfType<Canvas>().transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        print(source.GetComponentInChildren<SpriteRenderer>().bounds.size.z);
        transform.position = Ship.Player.shipCamera.GetComponent<Camera>().WorldToScreenPoint(source.transform.TransformPoint(Vector3.zero -Vector3.Scale(source.transform.localScale,new Vector3(2,-2,0))));
    }
}
