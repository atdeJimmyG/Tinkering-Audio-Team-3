﻿using System;
using System.IO;
using UnityEngine;
using UnityEditor;
/*
 * author = Thomas O'Leary
 * GitHub repo = https://github.com/atdeJimmyG/Tinkering-Audio-Team-3
 * copyright = MIT License Copyright (c) 2019
 * license = MIT
 * 
 * DISCLAIMER:
 *      WHEN THE SCENE IS RUN, THE BUTTONS DO NOT PLAY THE SOUND THAT IS GENERATED BY THIS EDITOR WINDOW
 * 
 * This script is for the customer Editor Tool made for the 
 * designers so that they can edit the sounds that are outputted from the UI.
 */

public class EditorTool : EditorWindow //Changed Monobehavior to EditorWindow so that it can be treated as a window
{
    // DISCLAIMER:
    // WHEN THE SCENE IS RUN, THE BUTTONS DO NOT PLAY THE SOUND THAT IS GENERATED BY THIS EDITOR WINDOW


    // General variable setup to use for generating tone
    private int pos;

    public int sampleRate = 44100;

    public int frequency = 440;
    public int endingFrequency;

    public float sampleDur = 1f;

    public string nameOfSample;

    public bool inflec = false;
    public int inflecCounter;

    public AudioClip audioOutput;
    
    // DISCLAIMER:
    // WHEN THE SCENE IS RUN, THE BUTTONS DO NOT PLAY THE SOUND THAT IS GENERATED BY THIS EDITOR WINDOW

    [MenuItem("Window/Editor Tool")]// The tool will be found underneath the WINDOW tab, labelled Editor Tool
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EditorTool>("Editor Tool");
        // This gives the Editor Tool window
        // The name if Editor Tool in it's tab
    }

    // DISCLAIMER:
    // WHEN THE SCENE IS RUN, THE BUTTONS DO NOT PLAY THE SOUND THAT IS GENERATED BY THIS EDITOR WINDOW

    void OnGUI()
    {
        // Title displayed on the window
        GUILayout.Label("Custom Editor Tool", EditorStyles.boldLabel);

        // Sets all editable areas of the editor tool
        nameOfSample = (string)EditorGUILayout.TextField("Name of sample", nameOfSample);
        sampleRate = (int)EditorGUILayout.Slider("Sample Rate", sampleRate, 100, 44100);
        frequency = (int)EditorGUILayout.Slider("Frequency", frequency, 1, 1000);

        // Inflec is a toggle boolean, when toggled a slider will appear on the window
        inflec = EditorGUILayout.Toggle("Inflection", inflec);

        if (inflec == true)
        {
            endingFrequency = (int)EditorGUILayout.Slider("Ending Frequency", endingFrequency, 0, 1000);
        }

        sampleDur = EditorGUILayout.FloatField("Duration", sampleDur);


        // Creates a button on the GUI labelled "Generate Tone" that executes OutputAudio()
        if (GUILayout.Button("Generate Tone"))
        {
            OutputAudio();
        }
        if (GUILayout.Button("Save Generated Tone"))
        {
            SaveTone();
        }
    }

    // DISCLAIMER:
    // WHEN THE SCENE IS RUN, THE BUTTONS DO NOT PLAY THE SOUND THAT IS GENERATED BY THIS EDITOR WINDOW

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

    // DISCLAIMER:
    // WHEN THE SCENE IS RUN, THE BUTTONS DO NOT PLAY THE SOUND THAT IS GENERATED BY THIS EDITOR WINDOW

    // Created a new function called ToneGenerate()
    // This function creates an AudioClip called audioOutput
    // And returns the new generated tone in audioOutput
    private AudioClip ToneGenerate()
    {
        inflecCounter = 0;

        audioOutput = AudioClip.Create(nameOfSample, (int)(sampleRate * sampleDur), 1, sampleRate, false, onAudioRead, SetPosition);
        return audioOutput;
    }

    // DISCLAIMER:
    // WHEN THE SCENE IS RUN, THE BUTTONS DO NOT PLAY THE SOUND THAT IS GENERATED BY THIS EDITOR WINDOW

    void onAudioRead(float[] samples)
    {
        int counter = 0;
        float currentFrequency = frequency;

        inflecCounter++;

        while (counter < samples.Length)
        {
            if (inflec)
            {
                currentFrequency = Mathf.Lerp(frequency, endingFrequency, (float)inflecCounter / (1 + 10 * sampleDur));
            }

            samples[counter] = Mathf.Sin(2 * Mathf.PI * currentFrequency * pos / sampleRate);
            pos++;
            counter++;
        }
    }

    // DISCLAIMER:
    // WHEN THE SCENE IS RUN, THE BUTTONS DO NOT PLAY THE SOUND THAT IS GENERATED BY THIS EDITOR WINDOW

    void SetPosition(int newPos)
    {
        pos = newPos;
    }

    void SaveTone()
    {
        SaveWavUtil.Save(nameOfSample, audioOutput);
    }



}
