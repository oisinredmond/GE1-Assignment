using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualiser : MonoBehaviour {
    public float scale = 20;
    public float radius = 15;
    List<GameObject> elements = new List<GameObject>();
	// Use this for initialization
	void Start () {
        Create();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i<elements.Count; i++){
            elements[i].transform.localScale = new Vector3(1, 1 + AudioAnalyser.spectrum[i] * scale, 1);
        }
	}

    void Create()
    {
        float theta = (Mathf.PI * 2.0f) / 32;
        // Spawning shapes in a circle
        for (int i = 0; i < AudioAnalyser.frameSize/32; i++){
            Vector3 pos = new Vector3(Mathf.Sin(theta * i) * radius, 0, Mathf.Cos(theta * i) * radius);
            pos = transform.TransformPoint(pos); // Translates local pos to world space
            Quaternion angle = Quaternion.AngleAxis(theta * i * Mathf.Rad2Deg, Vector3.up);
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.SetPositionAndRotation(pos, angle);
            cylinder.transform.parent = this.transform;
            cylinder.GetComponent<Renderer>().material.color = Color.HSVToRGB(i/(float)AudioAnalyser.frameSize,1,1);
            elements.Add(cylinder);
        }
    }
}
