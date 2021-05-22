using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    bool voznja = false;
    public GameObject igrac;
    public Transform teleportTarget;
    private Vector3 pozicija;
    private Animator anim;
    public Camera kamera;
    bool pokreni = false;
    private Rigidbody rigbody;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;


    private void FixedUpdate()
    {
        if (pokreni == true)
        {
            rigbody.isKinematic = false;
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
        if (pokreni == false)
        {
            rigbody.isKinematic = true;
            //rigbody.velocity = rigbody.velocity.normalized * 0;
        }
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel1(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel1(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    private void UpdateSingleWheel1(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        Quaternion rot1 = Quaternion.Euler(rot.x, rot.y, rot.z+180);
        rot *= rot1;
        wheelTransform.rotation = rot;
        //pos = new Vector3(pos.x, pos.y, -pos.z);
        wheelTransform.position = pos;

    }
    void Start()
    {
        anim = igrac.GetComponent<Animator>();
        //kamera = this.GetComponent<Camera>();
        kamera.enabled = false;
        kamera.GetComponent<AudioListener>().enabled = false;
        rigbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var stisnuto = Input.GetKeyDown(KeyCode.E);
        pozicija = new Vector3(teleportTarget.transform.position.x - 3, teleportTarget.transform.position.y, teleportTarget.transform.position.z);
        if (voznja == true && Input.GetKeyDown(KeyCode.E) && igrac.activeSelf)
        {
            /*igrac.transform.position = pozicija;
            anim.SetTrigger("drive");*/
            pokreni = true;
            igrac.SetActive(false);
            kamera.enabled = true;
            kamera.GetComponent<AudioListener>().enabled = true;
            AudioManager.Instance.Play(SoundType.EngineIdle);
        }
        if (!igrac.activeSelf && Input.GetKeyDown(KeyCode.F) && voznja == true)
        {
            pokreni = false;
            voznja = false;
            kamera.enabled = false;
            kamera.GetComponent<AudioListener>().enabled = false;
            AudioManager.Instance.Stop(SoundType.EngineIdle);
            AudioManager.Instance.Stop(SoundType.EngineSpeed);
            AudioManager.Instance.Stop(SoundType.Brakes);
            igrac.transform.position = pozicija;
            igrac.SetActive(true);
        }
        if (Input.GetKey(KeyCode.W) && !igrac.activeSelf)
        {
            AudioManager.Instance.Stop(SoundType.EngineIdle);
            AudioManager.Instance.Play(SoundType.EngineSpeed);
        }
        if (Input.GetKeyUp(KeyCode.W) && !igrac.activeSelf)
        {
            AudioManager.Instance.Stop(SoundType.EngineSpeed);
            AudioManager.Instance.Play(SoundType.EngineIdle);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !igrac.activeSelf)
        {
            AudioManager.Instance.Play(SoundType.Brakes);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voznja = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voznja = false;
        }
    }
}
