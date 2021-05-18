using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyController1 : MonoBehaviour
{
    private static readonly int SpeedFloat = Animator.StringToHash("Forward");
    private Animator anim;
    private NavMeshAgent navAgent;
    private Transform following;
    private float elapsed;
    private bool antibiotik=false;
    bool sjestinav2 = false;
    private Vector3 pozicija;
    public Transform teleportTarget;
    private bool jednom = false;
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        pozicija = new Vector3(teleportTarget.transform.position.x, (teleportTarget.transform.position.y + 0.55f), teleportTarget.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = navAgent.velocity.magnitude / navAgent.speed;
        anim.SetFloat(SpeedFloat, velocity);
        if (Input.GetKeyDown(KeyCode.Alpha2) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navAgent.SetDestination(hit.point);
            }
        }
        if (sjestinav2 == true && Input.GetKeyDown(KeyCode.R) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("sjedni");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit") && sjestinav2 == true)
        {
            anim.SetTrigger("notsitting");
        }
        if (Input.GetKeyDown(KeyCode.R) && anim.GetCurrentAnimatorStateInfo(0).IsName("Sit To Stand") && sjestinav2 == true)
        {
            anim.SetTrigger("sitagain");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sit To Stand") && Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("standingidle");
        }
        if (antibiotik == true && !jednom)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 2f)
            {
                elapsed = 0f;
                anim.SetInteger("seizure", 2);
                StartCoroutine(ActivateOnTimer());
                jednom = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = other.gameObject.transform;
        }
        if (other.CompareTag("object"))
        {
            anim.SetInteger("seizure", 1);
            antibiotik = true;
        }
        if (other.CompareTag("sjestnavagent1"))
        {
            sjestinav2 = true;
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
            sjestinav2 = false;
        }
    }
    void promjenapozicije()
    {
        transform.position = pozicija;
    }
    private IEnumerator ActivateOnTimer()
    {
        yield return new WaitForSeconds(6f);
        promjenapozicije();
        float yRotation = 180 - transform.eulerAngles.y;
        this.transform.Rotate(0f, (360 + yRotation), 0f);
    }
}
