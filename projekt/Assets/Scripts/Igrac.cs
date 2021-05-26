using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igrac : MonoBehaviour
{
    private Animator anim;
    private static readonly int SpeedFloat = Animator.StringToHash("Speed");
    static readonly int punchTrigger = Animator.StringToHash("Punch");
    static readonly int idlepunchTrigger = Animator.StringToHash("idle_punch");
    static readonly int conditionTrigger = Animator.StringToHash("condition");
    static readonly int conditionState = Animator.StringToHash("condition");
    public Transform teleportTarget;
    private Animation animation;
    [HideInInspector]
    public int counter = 0;
    private float timer = 0.0f;
    public GameObject napadni;
    public GameObject pomocnik;
    public GameObject collider;
    public GameObject character;
    public Texture2D crosshair;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2 - (crosshair.width * 0.5f),
                                    Screen.height / 2 - (crosshair.height * 0.5f),
                                         crosshair.width, crosshair.height), crosshair);
    }
    void Awake()
    {
        collider.GetComponent<BoxCollider>().enabled = false;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        var punch = Input.GetMouseButton(0);
        var notpunch = Input.GetMouseButtonUp(0);

        // Set the input axes on the Animator Controller.
        if (punch)
        {
            timer += Time.deltaTime;
                anim.SetInteger(conditionTrigger, 1);
        }
        if (notpunch)
        {
            timer = 0;
            anim.SetInteger(conditionTrigger, 0);
        }
        if(napadni.GetComponent<EnemyController>().napad == true && System.Math.Round(timer,1)==0.6 && punch)
        {
            AudioManager.Instance.Play(SoundType.Punch);
        }
        if (napadni.GetComponent<EnemyController>().napad == true && System.Math.Round(timer, 1) == 2.1 && punch)
        {
            AudioManager.Instance.Play(SoundType.Punch);
        }
        if (napadni.GetComponent<EnemyController>().napad == true && System.Math.Round(timer, 1) == 3.8 && punch)
        {
            AudioManager.Instance.Play(SoundType.Punch);
        }
        if (napadni.GetComponent<EnemyController>().napad == true && System.Math.Round(timer, 1) == 5.3 && punch)
        {
            AudioManager.Instance.Play(SoundType.Punch);
        }
        if (napadni.GetComponent<EnemyController>().napad == true && System.Math.Round(timer, 1) == 6.8 && punch)
        {
            AudioManager.Instance.Play(SoundType.Punch);
        }
        if (timer >= 7.5f && napadni.GetComponent<EnemyController>().napad == true)
        {
            counter = 5;
        }
        if (System.Math.Round(timer, 1) == 8.0)
        {
            AudioManager.Instance.Play(SoundType.Death);
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("dodging");
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetTrigger("notdodging");
        }
    } 
}
