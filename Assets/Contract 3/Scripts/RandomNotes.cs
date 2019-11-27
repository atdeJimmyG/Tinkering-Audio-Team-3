﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Contract 3 - Melody Generation (Non-Diegetic Audio)

Link to GitHub repository: https://github.com/atdeJimmyG/Tinkering-Audio-Team-3
Code and project authored by James Gill under the GPL-3.0 license

Copyright (C) <2019>  <James Gill>
The full licence is found at https://www.gnu.org/licenses.

This scripts is capable of creating a random order of tones, from the C Major scale

*/

public class RandomNotes : MonoBehaviour {
    public double frequency = 440.0;
    private double increment;
    private double phase;
    private double samplingFrequency = 48000.0;
    public float SampleDuration = 0f;
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    private float counter = 0f;

    public float gain;
    public float volume = 0.1f;

    public float[] frequencies;
    public int ThisFreq;
    public bool pressed = false;


    void Start() {
        //Setting array to all frequencies in C Major Scale
        frequencies = new float[8];
        frequencies[0] = 261.6f;
        frequencies[1] = 293.7f;
        frequencies[2] = 329.6f;
        frequencies[3] = 349.2f;
        frequencies[4] = 392.0f;
        frequencies[5] = 440.0f;
        frequencies[6] = 493.9f;
        frequencies[7] = 523.3f;

        StartCoroutine(DelaySound());
    }

   /*void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //starting the delaySound Coroutine
            
        }
    }*/

    //IEnumerator used for waiting time
    IEnumerator DelaySound() {
        while(counter <= SampleDuration) {
            // Waiting a random range for the duration of note playing
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));


            frequency = frequencies[ThisFreq];
            // Plays frequencies randomly between length of frequencies
            ThisFreq += (Random.Range(0, frequencies.Length));
            //Making sure no end, and loops frequencies
            ThisFreq %= frequencies.Length;
            counter++;
        }


        

        // Looping the Coroutine
        //StartCoroutine(DelaySound());
    }
        
    //Removing "unused" function will cause no audio to be played
    private void OnAudioFilterRead(float[] data, int channels) {
        gain = volume;
        increment = frequency * 2.0 * Mathf.PI / samplingFrequency;
        // Looping around the data array 
        for(int i = 0; i < data.Length; i += channels) {
            phase += increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));
            // Plays in both speaker channels
            if(channels == 2) {
                data[i + 1] = data[i];
            }
            if(phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }
        }
    }

    void GenerateClip() {

    }

    /*public void SaveToWav() {
        SaveWavUtil.Save("Example", AudioClip clip, bool trim = false);
    }*/

    
}
