using System.Collections;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private const float OffsetZ = -5;
    
    public Color colorStart;
    public Color colorEnd;
    public float widthStart;
    public float widthEnd;
    public float randomPlace;
    public int fragments = 20;
    public string sortingLayer;

    private LineRenderer _mLine;

    public void Start()
    {
        _mLine = gameObject.AddComponent<LineRenderer>();
        _mLine.name = "trail";
        _mLine.material = new Material(Shader.Find("Sprites/Default"));
        _mLine.startColor = colorEnd;
        _mLine.endColor = colorStart;
        _mLine.positionCount = fragments;
        _mLine.startWidth = widthEnd;
        _mLine.endWidth = widthStart;

        if (sortingLayer != null)
        {
            _mLine.sortingLayerName = sortingLayer;
        }

        for (var i = 0; i < _mLine.positionCount; i++)
        {
            _mLine.SetPosition(i, transform.TransformPoint(Vector3.zero));
        }

        StartCoroutine(LineDraw());
    }

    private void AddLinePoint(Vector3 v3NewPoint)
    {
        for (var i = 0; i < _mLine.positionCount - 1; i++)
        {
            _mLine.SetPosition(i, _mLine.GetPosition(i + 1));
            _mLine.SetPosition(_mLine.positionCount - 1,
                v3NewPoint + new Vector3(Random.Range(-randomPlace, randomPlace),
                    Random.Range(-randomPlace, randomPlace), Random.Range(-randomPlace, randomPlace)));
        }
    }

    private IEnumerator LineDraw()
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