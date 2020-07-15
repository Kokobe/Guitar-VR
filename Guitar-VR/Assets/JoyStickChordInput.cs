using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickChordInput : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;

    public OVRHand hand;
    public PlayNote[] strings;
    public string[] chordProgression;
    public RectTransform imageTransform;
    int chordIndex = 0;

    public TextMesh[] chordTexts;

    public TextMesh debugText;

    public string currentChord = "";

    public string song = "Redbone";

    int degreeToIndex(int degree) //0 -> 5 -> 4 ... -> 1
    {
        return Mathf.FloorToInt(((degree + 20) % 360) / 60f);
    }

    private void initChordTexts()
    {
        for (int i = 0; i < chordTexts.Length; i++)
        {
            chordTexts[i].text = chordProgression[i];
        }
    }

    private void setStringsToChords(string chord)
    {
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


    private void Start()
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

    // Update is called once per frame
    void Update()
    {
        int newChordIndex = 0;
        if (!hand.isActiveAndEnabled)
        {
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller);
            float rotationDegree = 0;
            if (System.Math.Abs(thumbstick.x) > 0) // zero is north
            {
                rotationDegree = (thumbstick.x < 0) ? Vector2.Angle(thumbstick, Vector2.up) :
                                                    Vector2.Angle(thumbstick, Vector2.up) * -1;

                if (rotationDegree < 0)
                    rotationDegree = (rotationDegree + 360);
            }

            newChordIndex = degreeToIndex((int)Mathf.Abs(rotationDegree));
        }
        else
        {
            bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            bool isMiddleFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
            bool isRingFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
            bool isPinkyFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);
 
 
            if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index) && hand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
            {
                newChordIndex = 1;
            }
            else if (hand.GetFingerIsPinching(OVRHand.HandFinger.Middle) && hand.GetFingerIsPinching(OVRHand.HandFinger.Ring))
            {
                newChordIndex = 2;
            }
            else if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                newChordIndex = 0;
            }
            else if (hand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
            {
                newChordIndex = 5;
            }
            else if (hand.GetFingerIsPinching(OVRHand.HandFinger.Ring))
            {
                newChordIndex = 4;
            }
            else if (hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky))
            {
                newChordIndex = 3;
            }

            debugText.text = "Index: " + isIndexFingerPinching + "| Middle: " + isMiddleFingerPinching
                            + "| Ring: " + isRingFingerPinching + "| Pinky: " + isPinkyFingerPinching;
        }




        if (chordIndex != newChordIndex)
        {
            imageTransform.eulerAngles = new Vector3(0, 0, newChordIndex * 60);
            // Debug.Log("choosing chord: " + chordProgression[newChordIndex]);
            //debugText.text = rotationDegree + chordProgression[newChordIndex] + "new chord index" + newChordIndex;

            setStringsToChords(chordProgression[newChordIndex]);
            chordIndex = newChordIndex;
        }


    }






}
