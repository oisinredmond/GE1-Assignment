using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyser : MonoBehaviour
{
    AudioSource audioSource;
    public static int frameSize = 512;
    public static float[] spectrum;
    public static float[] bands;
    public float binWidth;
    public float sampleRate;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spectrum = new float[frameSize];
        bands = new float[(int)Mathf.Log(frameSize, 2)];
        sampleRate = AudioSettings.outputSampleRate;
        binWidth = AudioSettings.outputSampleRate / 2 / frameSize;
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumData();
        GetFrequencyBands();
    }

    void GetSpectrumData()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
    }

    void GetFrequencyBands()
    {
        for (int i = 0; i < bands.Length; i++)
        {
            int start = (int)Mathf.Pow(2, i) - 1;
            int width = (int)Mathf.Pow(2, i);
            int end = start + width;
            float average = 0;
            for (int j = start; j < end; j++)
            {
                average += spectrum[j] * (j + 1);
            }
            average /= (float)width;
            bands[i] = average;
        }
    }
}
