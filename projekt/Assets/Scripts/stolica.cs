using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stolica : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Animator navanim;
    public GameObject character;
    public GameObject nav1;
    bool sjesti = false;
    void Start()
    {
        //anim = GetComponent<Animator>();
        anim = character.GetComponent<Animator>();
        navanim = nav1.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sjesti==true && Input.GetKeyDown(KeyCode.R) && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            anim.SetTrigger("sjedni");
        }
        if (Input.GetKeyDown(KeyCode.R) && this.anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            anim.SetTrigger("notsitting");
            anim.SetTrigger("idle");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            
            Vector3 rotacija = new Vector3(0, 90, 0);
            character.transform.Rotate(rotacija);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sjest"))
        {
            sjesti = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("sjest"))
        {
            sjesti = false;
        }
    }
}
