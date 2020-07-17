using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickChordInput : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;

    public OVRHand hand;
    public RectTransform imageTransform;
    public TextMesh debugText;
    public ChordManager chordManager;

    private int chordIndex = 0;

    private int degreeToIndex(int degree) //0 -> 5 -> 4 ... -> 1
    {
        return Mathf.FloorToInt(((degree + 20) % 360) / 60f);
    }

    // Tells the chord manager the chord index
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
            chordManager.setStringsToChords(newChordIndex);
            chordIndex = newChordIndex;
        }

    }

}
