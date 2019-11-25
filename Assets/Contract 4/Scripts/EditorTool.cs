using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject))]
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


        sampleRate = (int)EditorGUILayout.Slider("Sample Rate", sampleRate, 100, 44100);

        frequency = (int)EditorGUILayout.Slider("Frequency", frequency, 1, 1000);

        if (GUILayout.Button("Press me"))
        {
            OutputAudio();
        }
    }

    public void OutputAudio()
    {
        AudioClip audioClip = ToneGenerate();
        GameObject audioHolder = Instantiate(new GameObject());
        AudioSource audioSource = audioHolder.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
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
