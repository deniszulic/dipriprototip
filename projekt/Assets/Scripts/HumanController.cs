using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HumanController : MonoBehaviour
{
    static readonly int forwardFloat = Animator.StringToHash("Forward");
    static readonly int jumpTrigger = Animator.StringToHash("Jump");
    static readonly int jumpState = Animator.StringToHash("Jump");
    private Animator anim;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float rotate = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var v = Input.GetAxis("Vertical");
        var running = Input.GetButton("Sprint");
        rotate += speedH * Input.GetAxis("Mouse X");
        if (running)
            v *= 2;
        anim.SetFloat(forwardFloat, v, 0.1f, Time.deltaTime);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rotate += Input.GetAxis("Horizontal") * 80 * Time.deltaTime;
        }
        transform.eulerAngles = new Vector3(0.0f,rotate, 0.0f);
        var jump = Input.GetButtonDown("Jump");
        if (jump && anim.GetCurrentAnimatorStateInfo(0).shortNameHash!=jumpState && anim.GetNextAnimatorStateInfo(0).shortNameHash!=jumpState)
            anim.SetTrigger(jumpTrigger);
    }
}
