using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressurizationController : MonoBehaviour
{
    public GameObject player;
    public GameObject refPoint;
    public static int pressurizationStatus;
    public GameObject button;
    public GameObject text;
    public Material consoleTexture;
    public Material pressurizedScreen;
    public Material depressurizedScreen;
    public Material pressurizedButton;
    public Material depressurizedButton;

    private static int timer;

    private bool playerIsSuckedOut;

    // Start is called before the first frame update
    void Start()
    {
        pressurizationStatus = 3;
        playerIsSuckedOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressurizationStatus == 3)
        {
            GetComponent<MeshRenderer>().materials = new Material[] {consoleTexture, pressurizedScreen};
            button.GetComponent<MeshRenderer>().material = pressurizedButton;
            text.GetComponent<TextMeshPro>().SetText("Pressurized");

            if (SpaceWalkDoor.doorIsOpen)
            {
                SuckOutPlayer();
            }

        }
        else if (pressurizationStatus == 0)
        {
            GetComponent<MeshRenderer>().materials = new Material[] { consoleTexture, depressurizedScreen };
            button.GetComponent<MeshRenderer>().material = depressurizedButton;
            text.GetComponent<TextMeshPro>().SetText("Depressurized");
        }
        else if (pressurizationStatus == 1)
        {
            text.GetComponent<TextMeshPro>().SetText("Pressurizing");
            timer = timer + 1;
            if (timer == 200)
            {
                pressurizationStatus = 3;
            }
        }
        else if (pressurizationStatus == 2)
        {
            text.GetComponent<TextMeshPro>().SetText("Depressurizing");
            timer = timer + 1;
            if (timer == 200)
            {
                pressurizationStatus = 0;
            }
        }
    }

    public static void pressurize()
    {
        if (SpaceWalkDoor.doorIsOpen || EVADoor.doorIsOpen)
        {
            return;
        }
        pressurizationStatus = 1;
        timer = 0;
    }

    public static void depressurize()
    {
        if (SpaceWalkDoor.doorIsOpen || EVADoor.doorIsOpen)
        {
            return;
        }
        pressurizationStatus = 2;
        timer = 0;
    }

    private void SuckOutPlayer()
    {
        if (playerIsSuckedOut)
        {
            return;
        }
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.velocity = 100f * (refPoint.transform.position - player.transform.position);
        playerIsSuckedOut = true;
    }
}
