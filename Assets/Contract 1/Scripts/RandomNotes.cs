using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNotes : MonoBehaviour
{
    public float[] frequencies;

    void Start()
    {
        frequencies = new float[7];
        frequencies[0] = 261.6f;
        frequencies[1] = 293.7f;
        frequencies[2] = 329.6f;
        frequencies[3] = 349.2f;
        frequencies[4] = 392.0f;
        frequencies[5] = 440.0f;
        frequencies[6] = 493.9f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
