using UnityEngine;
using System.Collections;

/// <summary>
/// Chord manager: controls output notes of guitar strings
/// </summary>
public class ChordManager : MonoBehaviour
{
    public string[] chordProgression;
    public TextMesh[] chordTexts;
    public PlayNote[] strings;
    public string currentChord = "";
    public string song = "Redbone";


    private void initChordTexts()
    {
        for (int i = 0; i < chordTexts.Length; i++)
        {
            chordTexts[i].text = chordProgression[i];
        }
    }

    public void setStringsToChords(int newChordIndex)
    {
        string chord = chordProgression[newChordIndex];
        if (song.Equals("Redbone"))
        {
            if (chord.Equals("B1"))
            {
                strings[0].note = "B1";
                strings[1].note = "D#2";
                strings[2].note = "F#2";
                strings[3].note = "";
                strings[4].note = "";
                strings[5].note = "";
                currentChord = "B1";

            }
            else if (chord.Equals("C#2"))
            {
                strings[0].note = "C#2";
                strings[1].note = "F2";
                strings[2].note = "G#2";
                strings[3].note = "";
                strings[4].note = "";
                strings[5].note = "";
                currentChord = "C#2";
            }
            else if (chord.Equals("D#2"))
            {
                strings[0].note = "D#2";
                strings[1].note = "F#2";
                strings[2].note = "A#2";
                strings[3].note = "";
                strings[4].note = "";
                strings[5].note = "";
                currentChord = "D#2";
            }
            else
            {
                strings[0].note = "D#2";
                strings[1].note = "F#2";
                strings[2].note = "A#2";
                strings[3].note = "";
                strings[4].note = "";
                strings[5].note = "";
                currentChord = "D#2";
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        strings[0].note = "B1";
        strings[1].note = "D#2";
        strings[2].note = "F#2";
        strings[3].note = "";
        strings[4].note = "";
        strings[5].note = "";
        currentChord = "B1";
        initChordTexts();
    }
}
