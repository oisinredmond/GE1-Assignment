using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    public AudioAnalyser audioAnalyser;
    public int freqBand;

    // Lerping
    public float theta, scale;
    public int nStart, steps, max, interval;
    public bool lerpOnAudio;
    public AnimationCurve lerpAnimCurve;
    public Vector2 minMaxSpeed;
    public Color trailColor;

    // Scaling
    public float scaleAnimSpeed;
    public Vector2 scaleAnimMinMax;
    public AnimationCurve scaleAnimCurve;
    public bool scaling, scaleCurve;

    private Material trailMaterial;
    private int n;
    private bool invert;
    private TrailRenderer tr;
    private Vector3 startLerp, endLerp;
    private Vector2 pos;
    private float lerpPosTimer, lerpSpeed, lerpTime, scaleTimer, currScale;
    private int[] stepValues = {2, 15, 30, 45, 50, 60, 72, 87, 90, 120};
   

    // Calculates position based on angle, scale and n
    private Vector2 CalcSpiral(float calcTheta, float calcScale, int num)
    {
        float angle = num * (calcTheta * Mathf.Deg2Rad);
        float r = calcScale * Mathf.Sqrt(num);
        Vector2 position = new Vector2(r * (float)Mathf.Cos(angle), r * (float)Mathf.Sin(angle));
        return position;
    }

    // Sets trail renderer material and color, assigns user defined variables
    private void Awake()
    {
        currScale = scale;
        tr = GetComponent<TrailRenderer>();
        trailMaterial = new Material(tr.material);
        trailMaterial.SetColor("_TintColor", trailColor);
        tr.material = trailMaterial;
        n = nStart;
        transform.localPosition = CalcSpiral(theta, currScale, n);
        SetLerpPositions();
    }

    // Sets start and end positions for current lerp
    void SetLerpPositions() {
        pos = CalcSpiral(theta, currScale, n);
        startLerp = this.transform.localPosition;
        endLerp = new Vector3(pos.x, pos.y, 0);
    }

    private void Update()
    {
        if (scaling) {
            Scale();
        }

        if (lerpOnAudio) {
            AudioLerp();
        } else {
            // Allows user to choose step size of outer trails - this changes the shape of their path
            for (int i = 0; i < 10; i++){ 
                if(Input.GetKeyDown("" + i)){
                    steps = stepValues[i];
                }
            }
            if (Input.GetKeyDown("space")) {
                steps = 0;
            }
            RegularLerp();
        }
    }


    // Speed of a trail lerp is determined by the value of chosed frequncy band
    void AudioLerp()
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


    void RegularLerp()
    {
        float timeElapsed = Time.time - lerpTime;
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

    // Sets the currScale variable based on value in chosen audio frequency band
    void Scale()
    {
        // Scale animation curve allows a threshold of 
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
