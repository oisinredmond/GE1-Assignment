using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {

    public float theta, scale;
    public int nStart;
    public int steps, max;
    public float dotScale;
    public float lerpInterval;

    private int n, current;
    private Vector3 lerpStart, lerpEnd;
    private float lerpTime;
    private Vector2 pos;
    private TrailRenderer trailRenderer;

    private Vector2 CalcPT(float calcTheta, float calcScale, int count)
    {
        float angle = count * (calcTheta * Mathf.Deg2Rad);
        float r = scale * calcScale * Mathf.Sqrt(count);
        float x = r * (float)Mathf.Cos(angle);
        float y = r * (float)Mathf.Sin(angle);
        Vector2 pos = new Vector2(x, y);
        return pos;
    }

    private void Awake()
    {
        //trailRenderer = GetComponent<TrailRenderer>();
        //n = nStart;
        //transform.localPosition = CalcPT(theta, scale, n);
    }

    private void FixedUpdate()
    {
        pos = CalcPT(theta, scale, n);
        transform.localPosition = new Vector3(pos.x, pos.y, 0);
        n++;
    }

    void LerpTrails()
    {

    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        
    }

}
