using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    private static readonly int SpeedFloat = Animator.StringToHash("Forward");
    private static readonly int diedInt = Animator.StringToHash("died");
    static readonly int punch = Animator.StringToHash("Punch");
    private Animator anim;
    private NavMeshAgent navAgent;
    private Transform following;
    public GameObject napadni;
    public GameObject collider;
    public Transform teleportTarget;
    public GameObject gledaj;
    public GameObject chair;
    public bool napad = false;
    bool walking = true;
    public bool antibiotik = false;
    public bool jednom = false;
    bool lijek = false;
    float dist;
    public bool provjera=false;
    Vector3 pozicija;
    private float elapsed;
    // Start is called before the first frame update
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
        dist = Vector3.Distance(napadni.transform.position, transform.position);
        if (Input.GetKey(KeyCode.Alpha1) && napad == false && navAgent.enabled==true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navAgent.SetDestination(hit.point);
            }
        }
        if (napad == true && !anim.GetCurrentAnimatorStateInfo(0).IsTag("sleep") && napadni.GetComponent<Igrac>().counter != 5)
        {
            transform.LookAt(new Vector3(napadni.transform.position.x, transform.position.y, napadni.transform.position.z));
            anim.SetFloat(punch, 3);
        }
        if (napadni.GetComponent<Igrac>().counter == 5 && !jednom)
        {
            anim.SetInteger(diedInt, 1);
            navAgent.enabled = false;
            gameObject.GetComponent< DialogueManager2>().enabled = false;
            //GetComponent("ScriptName").enabled = false;
            elapsed += Time.deltaTime;
            if (elapsed >= 3f)
            {
                elapsed = 0f;
                anim.enabled = false;
                collider.GetComponent<BoxCollider>().enabled = false;
                promjenapozicije();
                elapsed += Time.deltaTime;
                float yRotation = 180-transform.eulerAngles.y;
                this.transform.Rotate(0f, (360 + yRotation), 0f);
                jednom = true;
            }
        }
        if (collider.GetComponent<BoxCollider>().enabled == true && dist <= 9 && napad == false && navAgent.enabled==true /*&& lijek==true*/)
            {
                navAgent.SetDestination(napadni.transform.position);
            }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = other.gameObject.transform;
        }
        if (other.CompareTag("napad"))
        {
            napad= true;
            walking = false;
            check(!provjera);
        }
        if (other.CompareTag("object"))
        {
            antibiotik = true;
            lijek = true;
            if (antibiotik == true)
            {
                //collider.SetActive(true);
                collider.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            following = null;
        }
        if (other.CompareTag("napad"))
        {
            napad = false;
            walking = true;
            if (napad == false /*&& CompareTag("Player")*/)
            {
                anim.SetFloat(punch, 0);
            }
        }
        if (other.CompareTag("object"))
        {
            antibiotik = false;
        }
    }
    void promjenapozicije()
    {
        transform.position = pozicija;
        //transform.LookAt(new Vector3(gledaj.transform.position.x, gledaj.transform.position.y, gledaj.transform.position.z));
    }

    void check(bool check)
    {
        check = true;
    }
    private static EnemyController enemy;
    public static EnemyController enem
    {
        get
        {
            return enemy;
        }
    }
}
