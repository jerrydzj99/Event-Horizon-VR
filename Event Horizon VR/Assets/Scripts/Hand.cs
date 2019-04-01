using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public OVRInput.Controller controller;
    public GameObject player;

    private float gripState;
    private bool anchored;
    private Vector3 anchorPosition;
    private Vector3 handVelocity;

    // Use this for initialization
    void Start()
    {
        anchored = false;
    }

    // Update is called once per frame
    void Update()
    {

        gripState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
        handVelocity = OVRInput.GetLocalControllerVelocity(controller);

        if (anchored)
        {

            Vector3 displacement = transform.position - anchorPosition;
            transform.position = anchorPosition;
            player.transform.position = player.transform.position - displacement;

            if (gripState <= 0.9f)
            {
                Release();
            }

        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Anchor"))
        {
            if (!anchored && gripState > 0.9f)
            {
                Anchor();
            }
        }
    }

    private void Anchor()
    {
        anchored = true;
        anchorPosition = transform.position;
    }

    private void Release()
    {
        anchored = false;
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.velocity = -handVelocity;
    }
}
