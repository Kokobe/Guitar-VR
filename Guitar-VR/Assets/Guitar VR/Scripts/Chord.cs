using System;
using UnityEngine;

public class Chord 
{
    public string chordString;
    public int octave;

    public Chord(string chordString = "C", int octave = 3)
    {
        this.chordString = chordString;
        this.octave = octave;
    }
}
