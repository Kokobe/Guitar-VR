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
        if (song.Equals("redbone"))
        {
            chordManager.chordProgression[0] = new Chord("B", 1);
            chordManager.chordProgression[1] = new Chord("G#m", 2);
            chordManager.chordProgression[2] = new Chord("F#m", 2); 
            chordManager.chordProgression[3] = new Chord("Fbdim", 2);
            chordManager.chordProgression[4] = new Chord("D#m", 2);
            chordManager.chordProgression[5] = new Chord("C#", 2);
        }
        if (song.Equals("best part"))
        {
            chordManager.chordProgression[0] = new Chord("DMaj7", 2);
            chordManager.chordProgression[1] = new Chord("N", 1);
            chordManager.chordProgression[2] = new Chord("N", 1);
            chordManager.chordProgression[3] = new Chord("BbMaj7", 1);
            chordManager.chordProgression[4] = new Chord("GMaj7", 1);
            chordManager.chordProgression[5] = new Chord("Am7", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
