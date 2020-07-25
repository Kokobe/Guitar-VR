
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
    public string noteString = "C3"; // [A-G][#/b][octave] Ex: C4, C#3
    public OVRHand hand;

    private Note note;
    private bool mute;
    public OVRGrabber m_grabber;

    private void Start()
    {
        note = new Note(noteString);
        audio.clip = C3_Guitar;
    }

    public Note getNote()
    {
        return note;
    }

    public void setNote(string n)
    {
        note = new Note(n);
        noteString = n;
    }

    public void setNote(Note n)
    {
        note = n;
        noteString = n.toString();
    }

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

    public void playNote()
    {
        StartCoroutine(vibrate(0.1f, 0.8f));
        OVRInput.SetControllerVibration(0.05f, 0.1f, OVRInput.Controller.LTouch);
        if (note.isLegit())
        {
            if (mute)
            {
                audio.Stop();
            }
            audio.pitch = Mathf.Pow(2f, note.getSemitoneOffsetFromC3() / 12.0f);
            audio.priority -= 1;
            audio.PlayOneShot(audio.clip, 1.0F);
        }
    }

    IEnumerator vibrate(float sec, float strength)
    {
        OVRInput.SetControllerVibration(1, strength, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(sec);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }

    private void OnTriggerEnter(Collider other)
    {
        playNote();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            audio.priority = 256;
        }


        bool isHandClosed = false;
        if (hand.isActiveAndEnabled)
        {    
            bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            bool isMiddleFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
            bool isRingFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
            bool isPinkyFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);


            isHandClosed = isIndexFingerPinching;
            //isHandClosed = m_grabber.isGrabbing

        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, m_controller) ||
            (hand.isActiveAndEnabled && isHandClosed))
        {
            palmMuteSwitch(true);
            audio.priority = 256;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, m_controller) ||
            (hand.isActiveAndEnabled && !isHandClosed))
        {
            palmMuteSwitch(false);
            audio.priority = 256;
        }
    }

}
