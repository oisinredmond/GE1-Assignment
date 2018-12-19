using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    public AudioAnalyser audioAnalyser;
    public int freqBand;

    // Lerping
    public float theta, scale, interval;
    public int nStart, steps, max;
    public bool lerpOnAudio;
    public AnimationCurve lerpAnimCurve;
    public Vector2 minMaxSpeed;
    public Color trailColor;

    // Scaling
    public float scaleAnimSpeed;
    public Vector2 scaleAnimMinMax;
    public AnimationCurve scaleAnimCurve;
    public bool scaling, scaleCurve;

    private Material trailMat;
    private int n, current;
    private bool isLerping, invert;
    private TrailRenderer tr;
    private Vector3 startLerp, endLerp;
    private Vector2 pos;
    private float lerpPosTimer, lerpSpeed, lerpTime, scaleTimer, currScale;
    private int[] stepValues = {5, 20, 30, 40, 50, 60, 70, 80, 90, 100};
   
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
        currScale = scale;
        isLerping = true;
        tr = GetComponent<TrailRenderer>();
        trailMat = new Material(tr.material);
        trailMat.SetColor("_TintColor", trailColor);
        tr.material = trailMat;
        n = nStart;
        transform.localPosition = CalcSpiral(theta, currScale, n);
        SetLerpPositions();
    }

    void SetLerpPositions() {
        lerpTime = Time.time;
        pos = CalcSpiral(theta, currScale, n);
        startLerp = this.transform.localPosition;
        endLerp = new Vector3(pos.x, pos.y, 0);
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        if (scaling) {
            Scale();
        }

        if (lerpOnAudio) {
            AudioLerp();
        } else {
            for (int i = 0; i < 10; i++)
            {
                if(Input.GetKeyDown("" + i))
                {
                    steps = stepValues[i];
                }
            }
            if (Input.GetKeyDown("space"))
            {
                steps = 0;
            }
            RegularLerp();
        }
    }


    void AudioLerp()
    {
        if (isLerping)
        {
            lerpSpeed = Mathf.Lerp(minMaxSpeed.x, minMaxSpeed.y, lerpAnimCurve.Evaluate(AudioAnalyser.bands[freqBand]));
            lerpPosTimer += Time.deltaTime * lerpSpeed;
            transform.localPosition = Vector3.Lerp(startLerp, endLerp, Mathf.Clamp01(lerpPosTimer));
            if (lerpPosTimer >= 1)
            {
                lerpPosTimer -= 1;
                n += steps;
                SetLerpPositions();
            }
        }
    }

    void RegularLerp()
    {
        float timeElapsed = Time.time - lerpTime; // Check how far along the current lerp is
        transform.localPosition = Vector3.Lerp(startLerp, endLerp, timeElapsed / interval);
        if (timeElapsed / interval >= 0.97f)
        {
            if (n >= max)
            {
                invert = true;
            }
            else if (n <= nStart)
            {
                invert = false;
            }

            if (invert)
            {
                n -= steps;
            }
            else
            {
                n += steps;
            }

            transform.localPosition = endLerp;
            SetLerpPositions();
        }

    }

    void Scale()
    {
        if (scaleCurve)
        {
            scaleTimer += (scaleAnimSpeed * AudioAnalyser.bands[freqBand]) * Time.deltaTime;
            if (scaleTimer >= 1)
            {
                scaleTimer -= 1;
            }
            currScale = Mathf.Lerp(scaleAnimMinMax.x, scaleAnimMinMax.y, scaleAnimCurve.Evaluate(scaleTimer));
        }
        else
        {
            currScale = Mathf.Lerp(scaleAnimMinMax.x, scaleAnimMinMax.y, AudioAnalyser.bands[freqBand]);
        }
    }
}
