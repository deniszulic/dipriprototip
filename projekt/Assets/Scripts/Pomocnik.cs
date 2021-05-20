using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pomocnik : MonoBehaviour
{
    public GameObject igrac;
    public bool napad = false;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(igrac.transform.position.x, transform.position.y, igrac.transform.position.z));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("napad"))
        {
            napad = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        napad = false;
    }
}
