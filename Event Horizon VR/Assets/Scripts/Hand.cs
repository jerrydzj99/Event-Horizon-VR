using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public OVRInput.Controller controller;
    public GameObject player;
    public GameObject handModel;
	public GameObject centerEyeAnchor;

    private float gripState;

    private bool anchored;
    private Vector3 anchorPosition;
    private Vector3 handVelocity;
    private float timeOfAnchoring;
    private bool anchorVibrating;

    private bool holdingObject;
    private GameObject grabbedObject;

    // Use this for initialization
    void Start()
    {
        anchored = false;
        anchorVibrating = false;
        holdingObject = false;
    }

    // Update is called once per frame
    void Update()
    {

        gripState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
        handVelocity = OVRInput.GetLocalControllerVelocity(controller);

        if (Vector3.Magnitude(player.GetComponent<Rigidbody>().velocity) > 2f)
        {
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity / Vector3.Magnitude(player.GetComponent<Rigidbody>().velocity) * 3f;
        }

        if (anchored)
        {
            //transform.position = anchorPosition;
            handModel.transform.position = anchorPosition;
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            rigidbody.velocity = -handVelocity / 2.5f;

            if (Time.time - timeOfAnchoring > 0.1f)
            {
                EndTouchVibration();
            }

            if (Vector3.Magnitude(handModel.transform.position - transform.position) > 0.05f)
            {
                OVRInput.SetControllerVibration(0.0001f, 1f, controller);
            }
            else if (!anchorVibrating)
            {
                OVRInput.SetControllerVibration(0, 0, controller);
            }

            if (gripState <= 0.9f)
            {
                Disanchor();
            }
        }

        if (holdingObject)
        {
            if (Time.time - timeOfAnchoring > 0.1f)
            {
                EndTouchVibration();
            }

            if (gripState < 0.9f)
            {
                Release();
            }
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (!holdingObject && !other.isTrigger)
        {
            if (other.gameObject.CompareTag("Grabbable"))
            {
                if (gripState > 0.9f)
                {
                    Grab(other.gameObject);
                }
            }

            if (other.gameObject.CompareTag("Anchor"))
            {
                if (!anchored && gripState > 0.9f)
                {
                    Anchor();
                }
            }
        }

    }

    private void Anchor()
    {
        anchored = true;
        anchorPosition = transform.position;
        StartTouchVibration();
        GetComponent<AudioSource>().Play();
    }

    private void Disanchor()
    {
        anchored = false;
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.velocity = - handVelocity / 2.5f;
        EndTouchVibration();
    }

    private void StartTouchVibration()
    {
        anchorVibrating = true;
        timeOfAnchoring = Time.time;
        OVRInput.SetControllerVibration(0.5f, 1f, controller);
    }

    private void EndTouchVibration()
    {
        anchorVibrating = false;
        OVRInput.SetControllerVibration(0, 0, controller);
    }

    private void Grab(GameObject obj)
    {
        holdingObject = true;
        grabbedObject = obj;
        AnchorGrabbedObject();

        Rigidbody rigidbody = grabbedObject.GetComponent<Rigidbody>();
        SphereCollider collider = grabbedObject.GetComponent<SphereCollider>();
        rigidbody.isKinematic = true;
        collider.enabled = false;

        StartTouchVibration();
    }

    private void Release()
    {
        holdingObject = false;
        Rigidbody rigidbody = grabbedObject.GetComponent<Rigidbody>();
        SphereCollider collider = grabbedObject.GetComponent<SphereCollider>();

        grabbedObject.transform.parent = null;
        rigidbody.isKinematic = false;
        collider.enabled = true;
        rigidbody.velocity = handVelocity / 2.5f + player.GetComponent<Rigidbody>().velocity;

        grabbedObject = null;

        EndTouchVibration();
    }

    private void AnchorGrabbedObject()
    {
        grabbedObject.transform.parent = transform;

        if (controller == OVRInput.Controller.LTouch)
        {
            grabbedObject.transform.localPosition = new Vector3(0.55633f, -0.2084f, 0.32503f);
            grabbedObject.transform.localEulerAngles = new Vector3(-57.797f, -2.722f, 62.474f);
        }
        else
        {
            grabbedObject.transform.localPosition = new Vector3(-0.5512f, -0.2478f, 0.31402f);
            grabbedObject.transform.localEulerAngles = new Vector3(70.927f, 143.893f, 23.954f);
        }
    }

}
