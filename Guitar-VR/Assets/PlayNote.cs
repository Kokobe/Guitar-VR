
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNote : MonoBehaviour
{
    public AudioSource audio;
    public string note = "C3"; // [A-G][#/b][octave] Ex: C4, C#3



    readonly Dictionary<string,int> LETTER_SEMITONES_FROM_C = new Dictionary<string, int>()
        {
            { "C", 0 },
            { "D", 2 },
            { "E", 4 },
            { "F", 5 },
            { "G", 7 },
            { "A", 9 },
            { "B", 11 }
        };

    private float convertStringToOffset(string note)
    {
        // TODO: Error check note parameter

        int octaveDiff = int.Parse(note.Substring(note.Length-1, 1)) - 3;
        int accidental = 0;
        int letterDiff = 0;

        if (note.Length == 3)
        {
            accidental = note.Substring(1, 1).Equals("#") ? 1 : -1;
        }
        letterDiff = LETTER_SEMITONES_FROM_C[note.Substring(0, 1)];
        Debug.Log("Octave diff: " + (octaveDiff * 12) 
            + "accidental: " + accidental + "; letterdiff = " + letterDiff);
        return octaveDiff * 12 + accidental + letterDiff;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (note.Length != 0)
        {
            audio.pitch = Mathf.Pow(2f, convertStringToOffset(note) / 12.0f);
            audio.priority -= 1;
            audio.PlayOneShot(audio.clip, 1.0F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            audio.priority = 256;
        }
    }
}
