using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActivateMenu : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;
    public OVRHand hand;
    public GameObject menu;
    public Image menuIcon;
    public Transform eye;

    private bool menuReady = false;
    private bool isMenuActive = false;
    private bool isCR_Running = false;
    private Coroutine current;
    public TextMesh debugText;

    private bool getMenuButtonDown()
    {
        debugText.text = "" + (hand.isActiveAndEnabled && hand.GetFingerIsPinching(OVRHand.HandFinger.Middle));
        return (hand.isActiveAndEnabled && hand.GetFingerIsPinching(OVRHand.HandFinger.Middle)) ||
                OVRInput.GetDown(OVRInput.Touch.Two, m_controller);
    }

    private bool getButtonUp()
    {
        return (hand.isActiveAndEnabled && !hand.GetFingerIsPinching(OVRHand.HandFinger.Middle)) ||
                OVRInput.GetUp(OVRInput.Touch.Two, m_controller);
    }

    IEnumerator holdingMiddlePinch()
    {
        isCR_Running = true;

        float pinchTime = 2f;
        menuIcon.fillAmount = 0;

        while (menuIcon.fillAmount < 1)
        {
            menuIcon.fillAmount += (1.0f / pinchTime) * Time.deltaTime;
            yield return null;
        }
        menuReady = true;
        isCR_Running = false;
    }

    public void toggleMenu()
    {
        menu.transform.position = eye.position + Vector3.forward/1.8f + Vector3.down/6;

        menu.SetActive(!isMenuActive);
        isMenuActive = !isMenuActive;
        menuReady = false;
        menuIcon.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((getMenuButtonDown() || Input.GetKeyDown(KeyCode.Q)) && !isCR_Running && !menuReady)
        {
            current = StartCoroutine(holdingMiddlePinch());
        }
        else if ((getButtonUp() || Input.GetKeyDown(KeyCode.W)) && isCR_Running)
        {
            StopCoroutine(current);
            menuIcon.fillAmount = 0;
            isCR_Running = false;
        }

        if (menuReady && (getButtonUp() || Input.GetKeyDown(KeyCode.W)))
        {
            toggleMenu();
        }
    }
}
