using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISongSelector : MonoBehaviour
{
    public string song;
    [SerializeField]
    protected OVRInput.Controller m_controller;
    public OVRHand hand;
    public Image songSelectorIcon;
    public ActivateMenu activateMenu;
    public SongManager songManager;

    private bool isCR_Running = false;
    private Coroutine current;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Right Hand"))
        {
            if (!hand.isActiveAndEnabled && (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, m_controller)) ||
           (hand.isActiveAndEnabled && !hand.GetFingerIsPinching(OVRHand.HandFinger.Index)))
            {
                Vector3 iconSpawnPoint = other.transform.position;
                iconSpawnPoint.z = transform.position.z;
                songSelectorIcon.transform.position = other.transform.position;

                if (!isCR_Running)
                    current = StartCoroutine(selectSong());
            }
            else
            {
                stopSelection();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Right Hand"))
            stopSelection();
    }

    private void stopSelection()
    {

        StopCoroutine(current);
        isCR_Running = false;
        songSelectorIcon.fillAmount = 0;

    }

    IEnumerator selectSong()
    {
        isCR_Running = true;
        yield return new WaitForSeconds(1f);

        float pinchTime = 2f;
        songSelectorIcon.fillAmount = 0;

        while (songSelectorIcon.fillAmount < 1)
        {
            songSelectorIcon.fillAmount += (1.0f / pinchTime) * Time.deltaTime;
            yield return null;
        }
        songManager.setSong(song);
        activateMenu.toggleMenu();
        songSelectorIcon.fillAmount = 0;
        isCR_Running = false;
    }

}
