using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    public Color colorStart;
    public Color colorEnd;
    public float widthStart;
    public float widthEnd=0;
    public int fragments=20;
    private LineRenderer mLine;
    // Use this for initialization
    void Start () {
        mLine = gameObject.AddComponent<LineRenderer>();
        mLine.name = "asd" + Random.Range(0, 444444);
        mLine.material = new Material(Shader.Find("Sprites/Default"));
        mLine.startColor = colorEnd;
        mLine.endColor = colorStart;
        //mLine.SetColors(colEnd, colStart);
        mLine.positionCount = fragments;
        mLine.startWidth = widthEnd;
        mLine.endWidth = widthStart;
        //mLine.SetPosition(99, transform.position);

        for (int i = 0; i < mLine.positionCount ; i++)
        {
            mLine.SetPosition(i,transform.TransformPoint(Vector3.zero));
        }
        StartCoroutine(LineDraw());
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void AddLinePoint(Vector3 v3NewPoint)
    {
        for (int i = 0; i < mLine.positionCount - 1; i++)
        {
            mLine.SetPosition(i, mLine.GetPosition(i + 1));
            mLine.SetPosition(mLine.positionCount - 1, v3NewPoint);
        }
    }

    IEnumerator LineDraw()
    {
        //print("tick");
        yield return new WaitForSeconds(0.01f);
        if (transform.parent != null)
        {
            AddLinePoint(transform.TransformPoint(Vector3.zero));
        }
        else
        {
            AddLinePoint(transform.position);
        }
        StartCoroutine(LineDraw());
    }
}
