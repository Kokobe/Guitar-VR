
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNote : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;
    public AudioClip C3_Guitar;
    public AudioClip C3_Guitar_mute;
    public AudioSource audio;
    public string note = "C3"; // [A-G][#/b][octave] Ex: C4, C#3
    public bool mute = false;

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

    public void palmMuteSwitch(bool muteOn)
    {
        if (muteOn)
        {
            audio.clip = C3_Guitar_mute;
        }
        else
        {
            audio.clip = C3_Guitar;
        }
        mute = muteOn;
    }

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
        return octaveDiff * 12 + accidental + letterDiff;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (note.Length != 0)
        {
            if (mute)
            {
                audio.Stop();
            }
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
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, m_controller))
        {
            palmMuteSwitch(true);
            audio.priority = 256;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, m_controller))
        {
            palmMuteSwitch(false);
            audio.priority = 256;
        }
    }
}
