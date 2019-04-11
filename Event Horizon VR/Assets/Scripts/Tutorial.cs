using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public float duration;
    public GameObject player;

    Animator anim;
    AudioSource beeping;

    void Awake()
    {
        anim = GetComponent<Animator>();
        beeping = GetComponent<AudioSource>();
        beeping.Play();
        StartCoroutine(tutorial());
    }

    void Update()
    {

    }

    IEnumerator tutorial()
    {
        yield return new WaitForSeconds(duration);
        beeping.Stop();
        anim.SetTrigger("Open");
        yield return new WaitForSeconds(3f);
        player.transform.Find("Ambience").gameObject.SetActive(true);
    }
}
