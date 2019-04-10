using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private float bounciness = 0.6f;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Vector3 rawVelocity = new Vector3(Random.Range(-10f, 10f), 1f, (Random.Range(-10f, 10f)));
        rigidbody.velocity = rawVelocity / Vector3.Magnitude(rawVelocity) / 6f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Anchor") || other.gameObject.CompareTag("Grabbable"))
        {
            Debug.Log("entered this shit");
            rigidbody.velocity = - (other.gameObject.GetComponent<Rigidbody>().velocity - rigidbody.velocity) * bounciness;
        }
    }*/
}
