using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public GameObject refPoint;

    private bool playerSuckedOut;

    // Start is called before the first frame update
    void Start()
    {
        playerSuckedOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ReferencePoint") && !playerSuckedOut)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = 100f * (refPoint.transform.position - transform.position);
            playerSuckedOut = true;
        }
    }
}
