using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {

    public float theta, scale;
    public int nStart; // Starting number for n
    public int steps, max; // Steps indicates number of trails to skip for lerping
    public float lerpInterval;

    private int n, current;
    private bool isLerping;
    private Vector3 lerpStart, lerpEnd; // End and start positions for a lerp
    private float lerpTime; // Time each lerp begins at
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
        LerpTrails();
    }

    private void FixedUpdate()
    {
        //pos = CalcPT(theta, scale, n);
        //transform.localPosition = new Vector3(pos.x, pos.y, 0);
        //n++;
        n += steps;
        if (isLerping)
        {
            float timeElapsed = Time.time - lerpTime; // Check how far along the current lerp is
        }
    }

    void LerpTrails()
    {
        isLerping = true;
        lerpTime = Time.time;
        pos = CalcPT(theta, scale, n);
        lerpStart = this.transform.localPosition;
        lerpEnd = new Vector3(pos.x, pos.y);
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        
    }

}
