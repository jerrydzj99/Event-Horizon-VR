using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVAButtonInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OpenEVAButton"))
        {
            EVADoor.openButtonPressed = true;
        }
        if (other.CompareTag("CloseEVAButton"))
        {
            EVADoor.closeButtonPressed = true;
        }
    }

}
