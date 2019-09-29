using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public float duration;
    public GameObject player;
    public OVRInput.Controller controller;
    public AudioClip beeping;
    public AudioClip tutorial1;
    public AudioClip tutorial2;
    public AudioClip tutorial3;
    public AudioClip tutorial4;
    public AudioClip tutorial5;
    public AudioClip tutorial6;
    public AudioClip tutorial7;
    public AudioClip tutorial8;

    Animator anim;
    AudioSource beepingSource;
    AudioSource tutorial1Source;
    AudioSource tutorial2Source;
    AudioSource tutorial3Source;
    AudioSource tutorial4Source;
    AudioSource tutorial5Source;
    AudioSource tutorial6Source;
    AudioSource tutorial7Source;
    AudioSource tutorial8Source;
    bool triedGrab, triedThrust;
    float gripState;

    void addAudio(AudioSource source, AudioClip clip, bool loop, float vol)
    {
        source.clip = clip;
        source.loop = loop;
        source.volume = vol;
        source.playOnAwake = false;
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        addAudio(beepingSource, beeping, true, 1);
        addAudio(tutorial1Source, tutorial1, false, 1);
        addAudio(tutorial2Source, tutorial2, false, 1);
        addAudio(tutorial3Source, tutorial3, false, 1);
        addAudio(tutorial4Source, tutorial4, false, 1);
        addAudio(tutorial5Source, tutorial5, false, 1);
        addAudio(tutorial6Source, tutorial6, false, 1);
        addAudio(tutorial7Source, tutorial7, false, 1);
        addAudio(tutorial8Source, tutorial8, false, 1);
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
        if (triedGrab && !triedThrust && (OVRInput.Get(OVRInput.Button.PrimaryThumbstick) || OVRInput.Get(OVRInput.Button.SecondaryThumbstick)))
        {
            triedThrust = true;
        }
    }

    IEnumerator tutorial()
    {
        yield return new WaitForSeconds(2f);
        beepingSource.Play();
        yield return new WaitForSeconds(duration);
        beepingSource.Stop();
        anim.SetTrigger("Open");
        yield return new WaitForSeconds(3f);
        player.transform.Find("Ambience").gameObject.SetActive(true);
        tutorial1Source.Play();
        yield return new WaitForSeconds(tutorial1.length);
        tutorial2Source.Play();
        yield return new WaitForSeconds(tutorial2.length);
        player.transform.Find("LocalAvatar").Find("controller_left").gameObject.SetActive(true);
        player.transform.Find("LocalAvatar").Find("controller_right").gameObject.SetActive(true);
        player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("LeftHandAnchor").gameObject.SetActive(true);
        player.transform.Find("OVRCameraRig").Find("TrackingSpace").Find("RightHandAnchor").gameObject.SetActive(true);
        tutorial3Source.Play();
        yield return new WaitForSeconds(tutorial3.length);
        do
        {
            tutorial4Source.Play();
            yield return new WaitForSeconds(tutorial4.length + 10f);
            yield return null;
        } while (!triedGrab);
        tutorial5Source.Play();
        yield return new WaitForSeconds(tutorial5.length);
        do
        {
            tutorial6Source.Play();
            yield return new WaitForSeconds(tutorial6.length + 10f);
        } while (!triedThrust);
        tutorial7Source.Play();
        yield return new WaitForSeconds(tutorial7.length);
        player.transform.Find("LocalAvatar").Find("controller_left").gameObject.SetActive(false);
        player.transform.Find("LocalAvatar").Find("controller_right").gameObject.SetActive(false);
        player.transform.Find("LocalAvatar").Find("hand_left").gameObject.SetActive(true);
        player.transform.Find("LocalAvatar").Find("hand_right").gameObject.SetActive(true);
        tutorial8Source.Play();
        yield return new WaitForSeconds(tutorial8.length);
    }
}
