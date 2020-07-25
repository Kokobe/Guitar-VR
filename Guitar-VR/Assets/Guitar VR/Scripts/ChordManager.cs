using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Chord manager: controls output notes of guitar strings
/// </summary>
public class ChordManager : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;

    public Chord[] chordProgression = new Chord[6];
    public TextMesh[] chordTexts;
    public TextMesh[] handChordTexts;
    public PlayNote[] strings;
    public int currentChordIndex = 0;
    public string song = "Redbone";
    private bool playOnlyTop = false;
    public TextMesh debugText;
    public SongManager songManager;

    private Note addTwoSemitones(string noteString)
    {
        Note n = new Note(noteString);
        return n.addSemitones(3);
    }

    private Note addTwoSemitones(Note n)
    {
        return n.addSemitones(3);
    }

    private Note addThreeSemitones(string noteString)
    {
        Note n = new Note(noteString);
        return n.addSemitones(4);
    }

    private Note addThreeSemitones(Note n)
    {
        return n.addSemitones(4);
    }

    public void setStringsToChords(int newChordIndex)
    {
        if (newChordIndex == -1)
        {
            strings[0].setNote("E1"); // actually E2 but garageband -1 each octave
            strings[1].setNote("A1");
            strings[2].setNote("D2");
            strings[3].setNote("G2");
            strings[4].setNote("B2");
            strings[5].setNote("E3");
        }
        else
        {
            currentChordIndex = newChordIndex;
            Chord chord = chordProgression[newChordIndex];
            string chordString = chord.chordString;
            if (!chordString.Equals("N"))
            {
                strings[0].setNote("");
                strings[1].setNote("");
                strings[2].setNote("");
                strings[3].setNote("");
                strings[4].setNote("");
                strings[5].setNote("");

                if (chordString.Contains("dim")) // calculate double minor
                {
                    string noteString = chordString.Substring(0, chordString.Length - 3);
                    strings[0].setNote(new Note(noteString, chord.octave)); // first note
                    strings[1].setNote(addTwoSemitones(strings[0].getNote()));
                    strings[2].setNote(addTwoSemitones(strings[1].getNote()));
                }
                else if (chordString.Contains("m")) // calculate minor
                {
                    string noteString;
                    if (chordString.Contains("7"))
                    {
                        noteString = chordString.Substring(0, chordString.Length - 2);
                    }
                    else
                    {
                        noteString = chordString.Substring(0, chordString.Length - 1);
                    }

                    strings[0].setNote(new Note(noteString, chord.octave)); // first note
                    strings[1].setNote(addTwoSemitones(strings[0].getNote()));
                    strings[2].setNote(addThreeSemitones(strings[1].getNote()));
                    if (chordString.Contains("7"))
                    {
                        strings[3].setNote(addTwoSemitones(strings[2].getNote()));
                    }
                }
                else // calculate major
                {
                    string noteString;
                    if (chordString.Contains("Maj"))
                    {
                        noteString = chordString.Substring(0, chordString.Length - 4);
                    }
                    else
                    {
                        noteString = chordString;
                    }
                    strings[0].setNote(new Note(noteString, chord.octave)); // first note
                    strings[1].setNote(addThreeSemitones(strings[0].getNote()));
                    strings[2].setNote(addTwoSemitones(strings[1].getNote()));
                    if (chordString.Contains("7"))
                    {
                        strings[3].setNote(addThreeSemitones(strings[2].getNote()));
                    }
                }

            }
        }


    }

    private void initChordTexts()
    {
        for (int i = 0; i < chordTexts.Length; i++)
        {
            chordTexts[i].text = chordProgression[i].chordString;
        }

        for (int i = 0; i < handChordTexts.Length; i++)
        {
            handChordTexts[i].text = chordProgression[i].chordString;
        }
    }

    // Use this for initialization
    void Start()
    {
        songManager.init();
        currentChordIndex = -1;
        setStringsToChords(currentChordIndex);
        initChordTexts();
    }



    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, m_controller))
        {
            playOnlyTop = true;
            strings[0].playNote();
            // setStringsToChords(currentChordIndex);
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, m_controller))
        {
            playOnlyTop = false;
            //setStringsToChords(currentChordIndex);
        }

    }
}
