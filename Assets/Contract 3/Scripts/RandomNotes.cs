using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNotes : MonoBehaviour {
    public double frequency = 440.0;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000.0;

    public float gain;
    public float volume = 0.1f;

    public float[] frequencies;
    public int thisFreq;
    public bool pressed = false;


    void Start() {
        frequencies = new float[8];
        frequencies[0] = 261.6f;
        frequencies[1] = 293.7f;
        frequencies[2] = 329.6f;
        frequencies[3] = 349.2f;
        frequencies[4] = 392.0f;
        frequencies[5] = 440.0f;
        frequencies[6] = 493.9f;
        frequencies[7] = 523.3f;

        StartCoroutine(delaySound());
    }

    /*void Update() {


        if (Input.GetKeyDown(KeyCode.Space)) {
            gain = volume;
            frequency = frequencies[thisFreq];
            thisFreq += 1;
            thisFreq = thisFreq % frequencies.Length;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
        gain = 0;
        }
    }*/

    IEnumerator delaySound() {
        yield return new WaitForSeconds(0.5f);
        gain = volume;

        frequency = frequencies[thisFreq];
        thisFreq += 1;
        thisFreq = thisFreq % frequencies.Length;

        StartCoroutine(delaySound());
    }
        

    private void OnAudioFilterRead(float[] data, int channels) {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

        for(int i = 0; i < data.Length; i += channels) {
            phase += increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            if(channels == 2) {
                data[i + 1] = data[i];
            }

            if(phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }
        }
    }

    
}
