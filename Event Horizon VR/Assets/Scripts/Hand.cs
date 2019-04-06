using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public OVRInput.Controller controller;
    public GameObject player;
    public GameObject handModel;

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

            transform.position = anchorPosition;
            handModel.transform.position = anchorPosition;
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            rigidbody.velocity = -handVelocity / 2f;

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
        OVRInput.SetControllerVibration(1f, 1f, controller);
    }

    private void Release()
    {
        anchored = false;
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.velocity = - handVelocity / 2f;
    }
}
