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
    private float timeOfAnchoring;
    private bool anchorVibrating;

    // Use this for initialization
    void Start()
    {
        anchored = false;
        anchorVibrating = false;
    }

    // Update is called once per frame
    void Update()
    {

        gripState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
        handVelocity = OVRInput.GetLocalControllerVelocity(controller);

        if (anchored)
        {
            
            //transform.position = anchorPosition;
            handModel.transform.position = anchorPosition;
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            rigidbody.velocity = - handVelocity / 2.5f;

            if (Time.time - timeOfAnchoring > 0.1f)
            {
                EndAnchorVibration();
            }

            if (Vector3.Magnitude(handModel.transform.position - transform.position) > 0.02f)
            {
                OVRInput.SetControllerVibration(0.0001f, 1f, controller);
            }
            else if (!anchorVibrating)
            {
                OVRInput.SetControllerVibration(0, 0, controller);
            }

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
        StartAnchorVibration();
    }

    private void Release()
    {
        anchored = false;
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.velocity = - handVelocity / 2.5f;
        EndAnchorVibration();
    }

    private void StartAnchorVibration()
    {
        anchorVibrating = true;
        timeOfAnchoring = Time.time;
        OVRInput.SetControllerVibration(0.5f, 1f, controller);
    }

    private void EndAnchorVibration()
    {
        anchorVibrating = false;
        OVRInput.SetControllerVibration(0, 0, controller);
    }

}
