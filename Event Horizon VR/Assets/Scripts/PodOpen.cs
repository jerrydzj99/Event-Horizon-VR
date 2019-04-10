using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodOpen : MonoBehaviour
{
    int count = 0;
    Vector3 rotation;
    Vector3 pivot;
    // Start is called before the first frame update
    void Start()
    {
        Quaternion temp = this.gameObject.GetComponentInParent<Transform>().rotation;
        rotation = new Vector3(temp.x, temp.y, temp.z);
        pivot = new Vector3(-.6287f, 2.5052f, 8.903f);
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 750)
        {
            this.gameObject.GetComponent<Transform>().RotateAround(pivot, rotation, -.1f);
        }
        count++;
    }
}
