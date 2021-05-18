using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform theDest;
    public GameObject collider;

    void Update()
    {
        float dist = Vector3.Distance(theDest.transform.position, transform.position);
        if (dist < 0.3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                this.transform.position = theDest.position;
                this.transform.rotation = theDest.transform.rotation;
                this.transform.parent = GameObject.Find("Destination").transform;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                this.transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                this.transform.position = theDest.transform.position;
            }
        } 
    }
}
