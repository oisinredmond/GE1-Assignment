using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {

    public GameObject dot;
    public float theta, scale;
    public int n;
    public float dotScale;

    private Vector2 calcPT(float calcTheta, float calcScale, int count)
    {
        float angle = count * (calcTheta * Mathf.Deg2Rad);
        float r = scale * calcScale * Mathf.Sqrt(count);
        float x = r * (float)Mathf.Cos(angle);
        float y = r * (float)Mathf.Sin(angle);
        Vector2 pos = new Vector2(x, y);
        return pos;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {		
	}
}
