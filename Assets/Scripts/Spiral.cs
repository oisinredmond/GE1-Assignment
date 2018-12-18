using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour {

    public AudioAnalyser analyser;
    public float theta, scale;
    public int nStart; // Starting number for n
    public int steps, max; // Steps indicates number of trails to skip for lerping

    private Material trailMaterial;
    public Color trailColor;
    private int n, current;
    private bool isLerping;
    private Vector3 lerpStart, lerpEnd; // End and start positions for a lerp
    private Vector2 pos;
    private TrailRenderer trailRenderer;
    private float lerpTimer, lerpSpeed;
    public Vector2 lerpMinMax;
    public AnimationCurve lerpAnimCurve;


    private Vector2 CalcSpiral(float calcTheta, float calcScale, int count)
    {
        float angle = count * (calcTheta * Mathf.Deg2Rad);
        float r = calcScale * Mathf.Sqrt(count);
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
        n = nStart;
        trailRenderer = GetComponent<TrailRenderer>();
        trailMaterial = new Material(trailRenderer.material);
        trailMaterial.SetColor("_TintColor", trailColor);
        trailRenderer.material = trailMaterial;
        transform.localPosition = CalcSpiral(theta, scale, n);
        isLerping = true;
        LerpTrails();

    }

    void LerpTrails()
    {
        pos = CalcSpiral(theta, scale, n);
        lerpStart = this.transform.localPosition;
        lerpEnd = new Vector3(pos.x, pos.y,0);
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        if (isLerping)
        {
            lerpSpeed = Mathf.Lerp(lerpMinMax.x, lerpMinMax.y, lerpAnimCurve.Evaluate(analyser.binWidth));
            lerpTimer += Time.deltaTime * lerpSpeed;
            transform.localPosition = Vector3.Lerp(lerpStart, lerpEnd, Mathf.Clamp01(lerpTimer));
            if (lerpTimer >= 1)
            {
                lerpTimer -= 1;
                n += steps;
                current++;
                LerpTrails();
            }
        }

    }

}
