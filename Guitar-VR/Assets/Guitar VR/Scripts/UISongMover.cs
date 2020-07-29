using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISongMover : MonoBehaviour
{

    [SerializeField]
    protected OVRInput.Controller m_controller;
    public OVRHand hand;

    private Vector3 offset;
    private Transform hand_transform;
    public TextMesh debugText;
    private bool firstTimeClosed;

    public UISongMover[] otherSongIcons;

    // Start is called before the first frame update
    void Start()
    {
        otherSongIcons = Object.FindObjectsOfType<UISongMover>();
        for (int i = 0; i < otherSongIcons.Length; i++)
        {
            if (otherSongIcons[i].Equals(this))
            {
                otherSongIcons[i] = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Right Hand"))
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, m_controller) ||
           (hand.isActiveAndEnabled && hand.GetFingerIsPinching(OVRHand.HandFinger.Index)))
            {
                offset = transform.position - other.gameObject.transform.position;
                firstTimeClosed = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Right Hand"))
        {
            hand_transform = other.gameObject.transform;

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, m_controller) ||
               (hand.isActiveAndEnabled && hand.GetFingerIsPinching(OVRHand.HandFinger.Index) && firstTimeClosed))
            {
                offset = transform.position - other.gameObject.transform.position;
                firstTimeClosed = false;
            }

            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, m_controller) ||
               (hand.isActiveAndEnabled && hand.GetFingerIsPinching(OVRHand.HandFinger.Index)))
            {
                Vector3 newPos = hand_transform.position + offset;
                newPos.y = transform.position.y;
                newPos.z = transform.position.z;

                Vector3 delta = transform.position - newPos;
                //move other objects
                for (int i = 0; i < otherSongIcons.Length; i++)
                {
                    if (otherSongIcons[i] != null)
                        otherSongIcons[i].transform.position -= delta;
                }
                // move pinched object
                transform.position = newPos;

            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (hand.isActiveAndEnabled)
        {
            bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            if (!isIndexFingerPinching)
                firstTimeClosed = true;
        }


    }
}
