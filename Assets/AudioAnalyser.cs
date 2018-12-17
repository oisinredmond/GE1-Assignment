using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class AudioAnalyser : MonoBehaviour {

    public bool useMic = false;
    public AudioClip audioClip;
    AudioSource aSource;

    public static int framSize = 512;
    public static float[] spectrum;
    public static float[] bands;

    public void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
