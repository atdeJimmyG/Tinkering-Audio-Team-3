using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.Events;
/*
 * author = Thomas O'Leary
 * GitHub repo = https://github.com/atdeJimmyG/Tinkering-Audio-Team-3
 * license = GNU GPL 3.0
 * copyright = Copyright (c) 2019 <James, Gill>
 * Full license agreement can be found in the LICENSE file or at <https://www.gnu.org/licenses/gpl-3.0.html>
 * 
 * This script is for the customer Editor Tool made for the 
 * designers so that they can edit the sounds that are outputted from the UI.
 */

public class EditorTool : EditorWindow //Changed Monobehavior to EditorWindow so that it can be treated as a window
{
    // General variable setup to use for generating tone
    private int pos;

    public int SampleRate = 44100;

    public int frequency = 440;
    public int EndingFrequency;

    public float SampleDur = 1f;

    public string NameOfSample;

    public bool inflec = false;
    public int InflecCounter;

    public AudioSource audio;
    public AudioClip AudioOutput;
    public GameObject SelectedObj = null;

    public Button SelectedButton;
    public AudioSource ButtonAudio;

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
        NameOfSample = (string)EditorGUILayout.TextField("Name of sample", NameOfSample);
        SampleDur = EditorGUILayout.FloatField("Duration", SampleDur);
        SampleRate = (int)EditorGUILayout.Slider("Sample Rate", SampleRate, 100, 44100);
        frequency = (int)EditorGUILayout.Slider("Frequency", frequency, 1, 1000);

        // Inflec is a toggle boolean. When toggled, a slider will appear on the window
        inflec = EditorGUILayout.Toggle("Vary in Frequency", inflec);

        if (inflec == true)
        {
            EndingFrequency = (int)EditorGUILayout.Slider("Ending Frequency", EndingFrequency, 0, 1000);
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
        if (SelectedObj == null)
        {
            SelectedObj = new GameObject("Generated Sound");
            audio = SelectedObj.AddComponent<AudioSource>();
        }
        audio.clip = AudioOutput;
        audio.Play();

    }

    // This function saves the tone within the editor and applies it
    // to every button that is selected in the Scene
    private void ApplyToButton()
    {
        SaveTone();
        foreach (GameObject obj in Selection.gameObjects)
        {
            SelectedButton = obj.GetComponent<Button>();

            if (SelectedButton != null)
            {
                ButtonAudio = obj.GetComponent<AudioSource>();

                ButtonAudio.clip = Resources.Load<AudioClip>("Sounds/" + NameOfSample);
                UnityEventTools.AddPersistentListener(SelectedButton.onClick, ButtonAudio.Play);
            }
        }

    }

    // Created a new function called ToneGenerate()
    // This function creates an AudioClip called audioOutput
    // And returns the new generated tone in audioOutput
    private AudioClip ToneGenerate()
    {
        // This counter is used for the inflection frequency loop
        InflecCounter = 0;

        AudioOutput = AudioClip.Create(NameOfSample, (int)(SampleRate * SampleDur), 1, SampleRate, false, OnAudioRead, SetPosition);
        return AudioOutput;
    }



    // Is called within ToneGenerate() AudioClip.Create as the pcmreadercallback parameter
    void OnAudioRead(float[] samples)
    {
        int counter = 0;
        float currentFrequency = frequency;

        InflecCounter++;

        while (counter < samples.Length)
        {
            // If the inflec toggle is enabled, the frequency transitions from one value to another
            if (inflec)
            {
                currentFrequency = Mathf.Lerp(frequency, EndingFrequency, (float)InflecCounter / (1 + 10 * SampleDur));
            }

            samples[counter] = Mathf.Sin(2 * Mathf.PI * currentFrequency * pos / SampleRate);
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
        SaveUtil.Save(NameOfSample, AudioOutput);
    }



}
