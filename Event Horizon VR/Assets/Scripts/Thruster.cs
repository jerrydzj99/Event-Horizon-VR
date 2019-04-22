using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{

    public GameObject player;
    public GameObject handModel;
    public GameObject thrusterSound;
    public bool isLeftHand;

    private bool thumbstickPressed;
    private float timeOfBoosting;
    private bool boostingFrozen;

    // Start is called before the first frame update
    void Start()
    {
        boostingFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeftHand)
        {
            thumbstickPressed = OVRInput.Get(OVRInput.Button.PrimaryThumbstick);
        } else
        {
            thumbstickPressed = OVRInput.Get(OVRInput.Button.SecondaryThumbstick);
        }

        if (thumbstickPressed && !boostingFrozen)
        {
            boostingFrozen = true;
            timeOfBoosting = Time.time;
            player.GetComponent<Rigidbody>().velocity += handModel.transform.forward / Vector3.Magnitude(handModel.transform.forward) * 0.5f;
            thrusterSound.GetComponent<AudioSource>().Play();
        }

        if (Time.time - timeOfBoosting > 0.5f)
        {
            boostingFrozen = false;
        }
    }
}
