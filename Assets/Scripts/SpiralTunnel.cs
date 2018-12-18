using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralTunnel : MonoBehaviour {
    public Transform tunnel;
    public float speed, dist;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tunnel.position = new Vector3(tunnel.position.x, tunnel.position.y, tunnel.position.z);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, tunnel.position.z + dist);
	}
}
