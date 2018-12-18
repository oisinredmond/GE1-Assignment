using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour {

    public AudioAnalyser audioAnalyser;
    public float theta, scale, interval;
    public int nStart, steps, max;
    public Color trailColor;
    public Vector2 minMaxSpeed;
    public AnimationCurve lerpAnimCurve;
    public int freqBand;
    public bool lerpOnAudio;

    private Material trailMat;
    private int n, current;
    private bool isLerping;
    private TrailRenderer tr;
    private Vector3 startLerp, endLerp;
    private Vector2 pos;
    private float lerpPosTimer, lerpSpeed, lerpTime;
   
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
        isLerping = true;
        tr = GetComponent<TrailRenderer>();
        trailMat = new Material(tr.material);
        trailMat.SetColor("_TintColor", trailColor);
        tr.material = trailMat;
        n = nStart;
        transform.localPosition = CalcSpiral(theta, scale, n);
        SetLerpPositions();
    }

    void SetLerpPositions() {
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
        if (lerpOnAudio)
        {
            if (isLerping)
            {
                lerpSpeed = Mathf.Lerp(minMaxSpeed.x, minMaxSpeed.y, lerpAnimCurve.Evaluate(AudioAnalyser.bands[freqBand]));
                transform.localScale = new Vector3(transform.localScale.x, (AudioAnalyser.bands[freqBand] * 15) + 2, transform.localScale.z);
                lerpPosTimer += Time.deltaTime * lerpSpeed;
                transform.localPosition = Vector3.Lerp(startLerp, endLerp, Mathf.Clamp01(lerpPosTimer));
                if (lerpPosTimer >= 1)
                {
                    lerpPosTimer -= 1;
                    n += steps;
                    current++;
                    SetLerpPositions();
                }
            }
        }
        else
        {
            float timeElapsed = Time.time - lerpTime; // Check how far along the current lerp is
            transform.localPosition = Vector3.Lerp(startLerp, endLerp, timeElapsed / interval);
            if (timeElapsed / interval >= 0.97f)
            {
                transform.localPosition = endLerp;
                n += steps;
                current++;
                if (current < max)
                {
                    SetLerpPositions();
                }
                else
                {
                    isLerping = false;
                }
            }
        }
    }
}
