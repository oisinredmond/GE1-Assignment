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
        freqBands = new float[(int)Mathf.Log(frameSize, 2)];
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
        for (int i = 0; i < freqBands.Length; i++)
        {
            int start = (int)Mathf.Pow(2, i) - 1;
            int width = (int)Mathf.Pow(2, i);
            float avg = 0;
            for(int j = start; j < start + width; j++)
            {
                avg += spectrum[j] * (j + 1);
            }
            freqBands[i] = avg;
        }
    }
}
