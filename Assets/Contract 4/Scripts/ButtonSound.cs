using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private int pos;

    public int sampleRate = 44100;
    public int frequency = 440;

    public float sampleDur = 0.5f;

    public string nameOfSample;

    public void ButtonPress()
    {
        AudioClip audioOutput = AudioClip.Create(nameOfSample, (int)(sampleRate * sampleDur), 1, sampleRate, true, onAudioRead, SetPosition);
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = audioOutput;
        audio.Play();
    }

    void onAudioRead(float[] samples)
    {
        int count = 0;
        while (count < samples.Length)
        {
            samples[count] = Mathf.Sin(2 * Mathf.PI * frequency * pos / sampleRate);
            pos++;
            count++;
        }
    }

    void SetPosition(int newPos)
    {
        pos = newPos;
    }

}
