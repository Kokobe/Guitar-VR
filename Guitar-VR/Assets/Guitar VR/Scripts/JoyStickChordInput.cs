using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickChordInput : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;

    public OVRHand hand;
    public RectTransform imageTransform;
    public Image selector;
    public TextMesh debugText;
    public ChordManager chordManager;

    private int chordIndex = 0;

    private int degreeToIndex(int degree) //0 -> 5 -> 4 ... -> 1
    {
        return Mathf.FloorToInt(((degree + 20) % 360) / 60f);
    }

    private void Start()
    {
        chordIndex = 0;
    }

    private float getRotationDegree(Vector2 thumbstick)
    {
        float rotationDegree = 0;
        if (System.Math.Abs(thumbstick.x) > 0) // zero is north
        {
            rotationDegree = (thumbstick.x < 0) ? Vector2.Angle(thumbstick, Vector2.up) :
                                                Vector2.Angle(thumbstick, Vector2.up) * -1;

            if (rotationDegree < 0)
                rotationDegree = (rotationDegree + 360);
        }
        return rotationDegree;
    }

    private int setChordIndexBasedOnHand()
    {
        int newChordIndex = 0;
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

        bool noInput = !(isIndexFingerPinching || isMiddleFingerPinching || isRingFingerPinching
                    || isPinkyFingerPinching);
       
       if (noInput)
            return -1;
        
        return newChordIndex;
    }

    // Tells the chord manager the chord index
    void Update()
    {
        bool noInput = false;
        int newChordIndex = 0;

        if (!hand.isActiveAndEnabled)
        {
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller);
            float rotationDegree = getRotationDegree(thumbstick);
            newChordIndex = degreeToIndex((int)Mathf.Abs(rotationDegree));
            noInput = thumbstick == Vector2.zero;
        }
        else
        {
            newChordIndex = setChordIndexBasedOnHand();
            noInput = newChordIndex == -1;
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            newChordIndex = 0;
            imageTransform.localRotation = Quaternion.Euler(0, 0, newChordIndex * 60);
            chordManager.setStringsToChords(newChordIndex);
            chordIndex = newChordIndex;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            newChordIndex = 5;

            imageTransform.localRotation = Quaternion.Euler(0, 0, newChordIndex * 60);
            chordManager.setStringsToChords(newChordIndex);
            chordIndex = newChordIndex;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            newChordIndex = 4;
            imageTransform.localRotation = Quaternion.Euler(0, 0, newChordIndex * 60);
            chordManager.setStringsToChords(newChordIndex);
            chordIndex = newChordIndex;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            newChordIndex = 3;
            imageTransform.localRotation = Quaternion.Euler(0, 0, newChordIndex * 60);
            chordManager.setStringsToChords(newChordIndex);
            chordIndex = newChordIndex;
        }

        //selector.enabled = !noInput;

        if (noInput)
        {
           // chordManager.setStringsToChords(-1);
        }
        else
        {
            if (chordIndex != newChordIndex)
            {
                imageTransform.localRotation =  Quaternion.Euler(0, 0, newChordIndex * 60);
                chordManager.setStringsToChords(newChordIndex);
                chordIndex = newChordIndex;
            }
        }


    }

}
