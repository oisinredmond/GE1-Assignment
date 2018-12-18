using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour {

    public float theta, scale, lerpInterval;
    public int nStart, steps, max;
    private int n, current;
    private bool isLerping;
    private TrailRenderer tr;
    private Vector3 startLerp, endLerp;
    private Vector2 pos;
    private float lerpTime;
   
    private Vector2 CalcSpiral(float calcTheta, float calcScale, int i)
    {
        float angle = i * (calcTheta * Mathf.Deg2Rad);
        float r = calcScale * Mathf.Sqrt(i);
        float x = r * (float)Mathf.Cos(angle);
        float y = r * (float)Mathf.Sin(angle);
        Vector2 position = new Vector2(x, y);
        return position;
    }

    private void Awake()
    {
        //trailRenderer = GetComponent<TrailRenderer>();
        //n = nStart;
        //transform.localPosition = CalcPT(theta, scale, n);
        tr = GetComponent<TrailRenderer>();
        n = nStart;
        transform.localPosition = CalcSpiral(theta, scale, n);
        StartLerping();
    }

    void StartLerping() {
        isLerping = true;
        lerpTime = Time.time;
        pos = CalcSpiral(theta, scale, n);
        startLerp = this.transform.localPosition;
        endLerp = new Vector3(pos.x, pos.y, 0);
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isLerping){
            float timeElapsed = Time.time - lerpTime;
            float percentComplete = timeElapsed / lerpInterval;
            transform.localPosition = Vector3.Lerp(startLerp, endLerp, percentComplete);
            if(percentComplete >= 0.97f)
            {
                transform.localPosition = endLerp;
                n += steps;
                current++;
                if(current <= max){
                    StartLerping();
                }
                else{
                    isLerping = false;
                }
            }
        }
    }

}
