using System;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
    public string noteLetter;
    public int accidental = 0;
    public int octave;

    private string noteString;
    private bool noteLegit;

    readonly Dictionary<string, int> LETTER_SEMITONES_FROM_C = new Dictionary<string, int>()
    {
        { "C", 0 },
        { "D", 2 },
        { "E", 4 },
        { "F", 5 },
        { "G", 7 },
        { "A", 9 },
        { "B", 11 }
    };

    public Note(string noteString)
    {
        this.noteString = noteString;
        noteLegit = noteString.Length > 1;
        if (noteLegit)
        {
            octave = int.Parse(noteString.Substring(noteString.Length - 1, 1));
            noteLetter = noteString.Substring(0, 1);
            if (noteString.Length == 3)
            {
                accidental = noteString.Substring(1, 1).Equals("#") ? 1 : -1;
            }
        }
    }

    public Note(string noteString, int octave)
    {
        this.octave = octave;
        noteLegit = (noteString.Length >= 1);
        if (noteLegit)
        { 
            noteLetter = noteString.Substring(0, 1);
            if (noteString.Length == 2)
            {
                accidental = noteString.Substring(1, 1).Equals("#") ? 1 : -1;
            }
        }
    }

    public bool isLegit()
    {
        return noteLegit;
    }

    public int getSemitonesFromC()
    {
        return LETTER_SEMITONES_FROM_C[noteLetter] + accidental;
    }

    public int getSemitoneOffsetFromC1()
    {
        var octaveDiff = octave - 1;
        return octaveDiff * 12 + getSemitonesFromC();
    }

    public int getSemitoneOffsetFromC3()
    {
        var octaveDiff = octave - 3;
        return octaveDiff * 12 + getSemitonesFromC();
    }

    public bool isLower(Note n)
    {
        return getSemitoneOffsetFromC3() < n.getSemitoneOffsetFromC3();
    }

    public bool isEqual(Note n)
    {
        return getSemitoneOffsetFromC3() == n.getSemitoneOffsetFromC3();
    }

    public string toString()
    {
        var accidentalString = "";

        if (accidental == 1)
            accidentalString = "#";
        else if (accidental == -1)
            accidentalString = "b";

        return noteLetter + accidentalString + octave;
    }

    public Note addSemitones(int semitones)
    {
        int currentSemitonesFromC = getSemitonesFromC();
        int desiredSemitonesFromC = currentSemitonesFromC + semitones;

        // search for the closest letter
        foreach (KeyValuePair<string, int> entry in LETTER_SEMITONES_FROM_C)
        {
            string noteString = "";
            string letter = entry.Key;
            noteString += letter;
            var newOctave = Mathf.FloorToInt((getSemitoneOffsetFromC3() + semitones) / 12.0f) + 3;

            if ((desiredSemitonesFromC % 12) ==  entry.Value)
            {
                noteString += newOctave;
                return new Note(noteString);
            }
            if ((desiredSemitonesFromC % 12) == entry.Value + 1) // its a sharp
            {
                noteString += "#";
                noteString += newOctave;
                return new Note(noteString);
            }
            if ((desiredSemitonesFromC % 12) == entry.Value - 1) // its a flat
            {
                noteString += "b";
                noteString += newOctave;
                return new Note(noteString);
            }
        }
        return this; // should never happen
    }

}

