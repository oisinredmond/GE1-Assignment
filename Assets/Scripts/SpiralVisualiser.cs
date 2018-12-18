using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralVisualiser : MonoBehaviour {

    public GameObject spiralPrefab;
    public float scale = 10;
    private Vector3 pos;
    List<GameObject> spirals1 = new List<GameObject>();
	// Use this for initialization
	void Start () {
        SpiralBackground();
        Spirals1();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpiralBackground()
    {
        GameObject spiralObject = Instantiate(spiralPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        spiralObject.transform.parent = this.transform;
        Spiral spiralScript = spiralObject.GetComponent<Spiral>();
        TrailRenderer tr = spiralObject.GetComponent<TrailRenderer>();
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 1.0f);
        tr.widthCurve = curve;
        tr.widthMultiplier = 0.1f;
        spiralScript.theta = 137.5f;
        spiralScript.scale = 1;
        spiralScript.steps = 1;
        spiralScript.max = 1000;
        spiralScript.lerpInterval = 0.1f;
    }

    void Spirals1()
    {
        for(int i = 0; i < 7; i++){
            GameObject spiralObject = Instantiate(spiralPrefab, new Vector3(0, 0, -2), Quaternion.identity);
            spirals1.Add(spiralObject);
            spiralObject.transform.parent = this.transform;
            Spiral spiralScript = spiralObject.GetComponent<Spiral>();
            spiralScript.nStart = i;
            TrailRenderer tr = spiralObject.GetComponent<TrailRenderer>();
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 1.0f);
            curve.AddKey(1.0f, 1.0f);
            tr.widthCurve = curve;
            tr.widthMultiplier = 0.6f;
            spiralScript.theta = 51.0f;
            spiralScript.scale = 1;
            spiralScript.steps = 7;
            spiralScript.max = 1000;
            spiralScript.lerpInterval = 0.1f;
        }
    }
}
