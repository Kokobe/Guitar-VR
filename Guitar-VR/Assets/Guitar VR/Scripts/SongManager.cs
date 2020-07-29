using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public ChordManager chordManager;
    public string song;

    // Start is called before the first frame update
    public void init()
    {
        setSong(song);
    }

    public void setSong(string song)
    {
        this.song = song;
        if (song.Equals("Redbone"))
        {
            chordManager.chordProgression[0] = new Chord("B", 1);
            chordManager.chordProgression[1] = new Chord("G#m", 2);
            chordManager.chordProgression[2] = new Chord("F#m", 2);
            chordManager.chordProgression[3] = new Chord("Fbdim", 2);
            chordManager.chordProgression[4] = new Chord("D#m", 2);
            chordManager.chordProgression[5] = new Chord("C#", 2);
        }
        if (song.Equals("Best Part"))
        {
            chordManager.chordProgression[0] = new Chord("DMaj7", 2);
            chordManager.chordProgression[1] = new Chord("N", 1);
            chordManager.chordProgression[2] = new Chord("N", 1);
            chordManager.chordProgression[3] = new Chord("BbMaj7", 1);
            chordManager.chordProgression[4] = new Chord("GMaj7", 1);
            chordManager.chordProgression[5] = new Chord("Am7", 1);
        }
        if (song.Equals("LFILFTN"))
        {
            chordManager.chordProgression[0] = new Chord("Dm", 2);
            chordManager.chordProgression[1] = new Chord("EMaj7", 2);
            chordManager.chordProgression[2] = new Chord("GMaj7", 2);
            chordManager.chordProgression[3] = new Chord("C", 2);
            chordManager.chordProgression[4] = new Chord("Am", 1);
            chordManager.chordProgression[5] = new Chord("G", 1);
        }

        if (song.Equals("Easy"))
        {
            chordManager.chordProgression[0] = new Chord("F", 2);
            chordManager.chordProgression[1] = new Chord("C", 3);
            chordManager.chordProgression[2] = new Chord("Gm", 2);
            chordManager.chordProgression[3] = new Chord("D", 2);
            chordManager.chordProgression[4] = new Chord("Dm", 2);
            chordManager.chordProgression[5] = new Chord("FMaj7", 2);
        }

        chordManager.initChords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
