using UnityEngine;
using UnityEditor;

public class EditorTool : EditorWindow
{
    private int pos;

    public int sampleRate = 44100;
    public int frequency = 440;

    public float sampleDur = 0.5f;

    public string nameOfSample;

    

    [MenuItem("Window/Editor Tool")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EditorTool>("Editor Tool");
    }

    void OnGUI()
    {
        GUILayout.Label("Custom Editor Tool", EditorStyles.boldLabel);

        nameOfSample = (string)EditorGUILayout.TextField("Name of sample", nameOfSample);

        sampleRate = (int)EditorGUILayout.Slider("Sample Rate", sampleRate, 100, 44100);

        frequency = (int)EditorGUILayout.Slider("Frequency", frequency, 1, 1000);

        if (GUILayout.Button("Generate Tone"))
        {
            OutputAudio();
        }
    }

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
