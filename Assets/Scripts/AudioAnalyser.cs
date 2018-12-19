/*  Oisin Redmond - C154922202 - DT228/4
    Game Engines 1 - Assignment 1
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyser : MonoBehaviour
{
    AudioSource audioSource;
    public static int frameSize = 512;
    public static float[] spectrum; // Range of frequencies divided into 512 averages
    public static float[] bands;  // Divides spectrum into 7 psychoacoustic bands from subbass to brilliance
    public float binWidth;
    public float sampleRate;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spectrum = new float[frameSize];
        bands = new float[(int)Mathf.Log(frameSize, 2)]; // Log 512 base 2
        sampleRate = AudioSettings.outputSampleRate;
        binWidth = AudioSettings.outputSampleRate / 2 / frameSize;
    }

    void Update()
    {
        GetSpectrumData();
        GetFrequencyBands();
    }

    void GetSpectrumData()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
    }

    // Divides spectrum data into 7 psychoacoustic bands from subbass to brilliance
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
            average /= (float)width; // Average frequency for each psychoacoustic band at a given time
            bands[i] = average;
        }
    }
}
