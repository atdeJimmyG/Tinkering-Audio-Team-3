﻿using UnityEngine;
using UnityEditor;

/*
 This script is for the customer Editor Tool made for the 
 designers so that they can edit the sounds that are outputted from the UI.
 */

public class EditorTool : EditorWindow //Changed Monobehavior to EditorWindow so that it can be treated as a window
{
    // General variable setup to use for generating tone
    private int pos;

    public int sampleRate = 44100;
    public int frequency = 440;

    public float sampleDur = 0.5f;

    public string nameOfSample;

    

    [MenuItem("Window/Editor Tool")]// The tool will be found underneath the WINDOW tab, labelled Editor Tool
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EditorTool>("Editor Tool");
        // This gives the Editor Tool window
        // The name if Editor Tool in it's tab
    }

    void OnGUI()
    {
        // Creates a bold label at the top of the window
        GUILayout.Label("Custom Editor Tool", EditorStyles.boldLabel);

        // Creates a text field on the GUI so that the user is able to change the name of the sample
        // When the sound is generated, this updates the name of the AudioClip in its Audio Source component
        nameOfSample = (string)EditorGUILayout.TextField("Name of sample", nameOfSample);

        // Creates a slider on the GUI that changes the value of sampleRate
        sampleRate = (int)EditorGUILayout.Slider("Sample Rate", sampleRate, 100, 44100);

        // Creates a slider on the GUI that changes the value of frequency
        frequency = (int)EditorGUILayout.Slider("Frequency", frequency, 1, 1000);

        // Creates a button on the GUI labelled "Generate Tone"
        // Which outputs the generated tone created in the OutputAudio() function
        if (GUILayout.Button("Generate Tone"))
        {
            OutputAudio();
        }
    }

    // Created a new function called OutputAudio()
    // This function grabs every GameObject that is currently selected in the scene
    // Generates a tone using the values within ToneGenerate()
    // Then outputs the sound
    public void OutputAudio()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            AudioClip audioClip = ToneGenerate();
            AudioSource audio = obj.GetComponent<AudioSource>();
            audio.clip = audioClip;
            audio.Play();
        }
        
    }

    // Created a new function called ToneGenerate()
    // This function creates an AudioClip called audioOutput
    // And returns the new generated tone in audioOutput
    private AudioClip ToneGenerate()
    {
        AudioClip audioOutput = AudioClip.Create(nameOfSample, (int)(sampleRate * sampleDur), 1, sampleRate, true, onAudioRead, SetPosition);

        return audioOutput;
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