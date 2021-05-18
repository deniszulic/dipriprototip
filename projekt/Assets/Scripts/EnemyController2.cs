using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyController2 : MonoBehaviour
{
    private static readonly int SpeedFloat = Animator.StringToHash("Speed");
    private Animator anim;
    private NavMeshAgent navAgent;
    private Transform following;
    bool sjestinav1 = false;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = navAgent.velocity.magnitude / navAgent.speed;
        anim.SetFloat(SpeedFloat, velocity);
        if (Input.GetKeyDown(KeyCode.Alpha3) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navAgent.SetDestination(hit.point);
            }
        }
        if (sjestinav1 == true && Input.GetKeyDown(KeyCode.R) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("sjedni");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit") && sjestinav1==true)
        {
            anim.SetTrigger("notsitting");
        }
        if (Input.GetKeyDown(KeyCode.R) && anim.GetCurrentAnimatorStateInfo(0).IsName("Sit To Stand") && sjestinav1 == true)
        {
            anim.SetTrigger("sitagain");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sit To Stand") && Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("standingidle");
        }       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = other.gameObject.transform;
            Debug.Log("following:" + following);
        }
        if (other.CompareTag("sjestnavagent1"))
        {
            sjestinav1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = null;
        }
        if (other.CompareTag("sjestnavagent1"))
        {
            sjestinav1 = false;
        }
    }
}
