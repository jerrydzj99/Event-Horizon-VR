using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVADoor : MonoBehaviour
{

    public static bool openButtonPressed;
    public static bool closeButtonPressed;
    public static bool doorIsOpen;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        doorIsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (openButtonPressed)
        {
            doorIsOpen = true;
            animator.SetBool("character_nearby", true);
            openButtonPressed = false;
        }
        if (closeButtonPressed)
        {
            doorIsOpen = false;
            animator.SetBool("character_nearby", false);
            closeButtonPressed = false;
        }
    }

}
