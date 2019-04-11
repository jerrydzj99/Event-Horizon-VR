using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public float duration;
    public GameObject player;
    public OVRInput.Controller controller;

    Animator anim;
    AudioSource beeping;
    bool triedGrab, triedThrust;
    float gripState;

    void Awake()
    {
        anim = GetComponent<Animator>();
        beeping = GetComponent<AudioSource>();
        triedGrab = false;
        triedThrust = false;
        StartCoroutine(tutorial());
    }

    void Update()
    {
        gripState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
        if (!triedGrab && gripState > .9f)
        {
            triedGrab = true;
        }
        if (!triedThrust && (OVRInput.Get(OVRInput.Button.PrimaryThumbstick) || OVRInput.Get(OVRInput.Button.SecondaryThumbstick)))
        {
            triedThrust = true;
        }
    }

    IEnumerator tutorial()
    {
        yield return new WaitForSeconds(2f);
        beeping.Play();
        yield return new WaitForSeconds(duration);
        beeping.Stop();
        anim.SetTrigger("Open");
        yield return new WaitForSeconds(3f);
        player.transform.Find("Ambience").gameObject.SetActive(true);
        player.transform.Find("LocalAvatar").Find("controller_left").gameObject.SetActive(true);
        player.transform.Find("LocalAvatar").Find("controller_right").gameObject.SetActive(true);
        player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("LeftHandAnchor").gameObject.SetActive(true);
        player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("RightHandAnchor").gameObject.SetActive(true);
        do
        {
            yield return null;
        } while (!triedGrab || !triedThrust);
        player.transform.Find("LocalAvatar").Find("controller_left").gameObject.SetActive(false);
        player.transform.Find("LocalAvatar").Find("controller_right").gameObject.SetActive(false);
        player.transform.Find("LocalAvatar").Find("hand_left").gameObject.SetActive(true);
        player.transform.Find("LocalAvatar").Find("hand_right").gameObject.SetActive(true);
    }
}
