using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pomocnik : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject napadni;
    public bool napad = false;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(gameObject.transform.position.x, transform.position.y, gameObject.transform.position.z));
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
