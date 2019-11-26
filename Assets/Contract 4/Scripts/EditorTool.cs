using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.Events;
/*
 * author = Thomas O'Leary
 * GitHub repo = https://github.com/atdeJimmyG/Tinkering-Audio-Team-3
 * copyright = MIT License Copyright (c) 2019
 * license = MIT
 * 
 * 
 * This script is for the customer Editor Tool made for the 
 * designers so that they can edit the sounds that are outputted from the UI.
 */

public class EditorTool : EditorWindow //Changed Monobehavior to EditorWindow so that it can be treated as a window
{
    // General variable setup to use for generating tone
    private int pos;

    public int sampleRate = 44100;

    public int frequency = 440;
    public int endingFrequency;

    public float sampleDur = 1f;

    public string nameOfSample;

    public bool inflec = false;
    public int inflecCounter;

    public AudioSource audio;
    public AudioClip audioOutput;
    public GameObject selectedObj = null;

    public Button selectedButton;
    public AudioSource buttonAudio;

    [MenuItem("Window/Editor Tool")]// The tool will be found underneath the WINDOW tab, labelled Editor Tool
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EditorTool>("Editor Tool");
    }

    void OnGUI()
    {
        // Title displayed on the window
        GUILayout.Label("Custom Editor Tool", EditorStyles.boldLabel);

        // Sets all editable areas of the editor tool
        nameOfSample = (string)EditorGUILayout.TextField("Name of sample", nameOfSample);
        sampleDur = EditorGUILayout.FloatField("Duration", sampleDur);
        sampleRate = (int)EditorGUILayout.Slider("Sample Rate", sampleRate, 100, 44100);
        frequency = (int)EditorGUILayout.Slider("Frequency", frequency, 1, 1000);

        // Inflec is a toggle boolean. When toggled, a slider will appear on the window
        inflec = EditorGUILayout.Toggle("Vary in Frequency", inflec);

        if (inflec == true)
        {
            endingFrequency = (int)EditorGUILayout.Slider("Ending Frequency", endingFrequency, 0, 1000);
        }

        GUILayout.BeginHorizontal();

        // Creates a button on the GUI labelled "Generate Tone" that executes OutputAudio()
        if (GUILayout.Button("Generate Tone"))
        {
            ToneGenerate();
            OutputAudio();
        }
        if (GUILayout.Button("Save Generated Tone"))
        {
            SaveTone();
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Apply Tone to GameObject"))
        {
            ToneGenerate();
            ApplyToButton();
            
        }
    }

    // Created a new function called OutputAudio()
    // Function sees if there is a selected GameObject, if not
    // Creates a new GameObject called "Generated Sound" and gives it an AudioSource component
    public void OutputAudio()
    {
        if (selectedObj == null)
        {
            selectedObj = new GameObject("Generated Sound");
            audio = selectedObj.AddComponent<AudioSource>();
        }
        audio.clip = audioOutput;
        audio.Play();

    }

    // This function saves the tone within the editor and applies it
    // to every button that is selected in the Scene
    private void ApplyToButton()
    {
        SaveTone();
        foreach (GameObject obj in Selection.gameObjects)
        {
            selectedButton = obj.GetComponent<Button>();

            if (selectedButton != null)
            {
                buttonAudio = obj.GetComponent<AudioSource>();

                buttonAudio.clip = Resources.Load<AudioClip>("Sounds/" + nameOfSample);
                UnityEventTools.AddPersistentListener(selectedButton.onClick, buttonAudio.Play);
            }
        }

    }

    // Created a new function called ToneGenerate()
    // This function creates an AudioClip called audioOutput
    // And returns the new generated tone in audioOutput
    private AudioClip ToneGenerate()
    {
        // This counter is used for the inflection frequency loop
        inflecCounter = 0;

        audioOutput = AudioClip.Create(nameOfSample, (int)(sampleRate * sampleDur), 1, sampleRate, false, onAudioRead, SetPosition);
        return audioOutput;
    }



    // Is called within ToneGenerate() AudioClip.Create as the pcmreadercallback parameter
    void onAudioRead(float[] samples)
    {
        int counter = 0;
        float currentFrequency = frequency;

        inflecCounter++;

        while (counter < samples.Length)
        {
            // If the inflec toggle is enabled, the frequency transitions from one value to another
            if (inflec)
            {
                currentFrequency = Mathf.Lerp(frequency, endingFrequency, (float)inflecCounter / (1 + 10 * sampleDur));
            }

            samples[counter] = Mathf.Sin(2 * Mathf.PI * currentFrequency * pos / sampleRate);
            pos++;
            counter++;
        }
    }


    // Called within ToneGenerate() AudioClip.Create as the pcmsetpositioncallback parameter
    void SetPosition(int newPos)
    {
        pos = newPos;
    }

    // Function used to save the tone as a .wav file
    void SaveTone()
    {
        SaveWavUtil.Save(nameOfSample, audioOutput);
    }



}
