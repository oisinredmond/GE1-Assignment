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
        GameObject spiralObject = Instantiate(spiralPrefab, new Vector3(0,0,0), Quaternion.identity);
        spiralObject.transform.parent = this.transform;
        Spiral spiralScript = spiralObject.GetComponent<Spiral>();
        spiralScript.theta = 137.5f;
        spiralScript.scale = 1;
        spiralScript.steps = 1;
        spiralScript.max = 1000;
        spiralScript.lerpInterval = 0.1f;
        //Spirals1();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spirals1()
    {

    }
}
