using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class AudioAnalyser : MonoBehaviour {

    public bool useMic = false;
    public AudioClip audioClip;
    AudioSource audioSource;
    public AudioMixerGroup master;

    public static int frameSize = 512;
    public static float[] spectrum;
    public static float[] freqBands;
    public float binWidth;
    public float sampleRate;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spectrum = new float[frameSize];
        bands = new float[(int)Mathf.Log(frameSize, 2)];
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = master;
    }

    // Use this for initialization
    void Start () {
        sampleRate = AudioSettings.outputSampleRate;
        binWidth = AudioSettings.outputSampleRate / 2 / frameSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetFreqBands()
    {

    }
}
