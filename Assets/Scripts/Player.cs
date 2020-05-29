using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public Slider fuelGauge;
    public Slider boxGauge;
    public GameObject crash;
    public GameObject success;
    public GameObject box;
    public GameObject targetMasterObject;
    private ParticleSystem leftThruster;
    private ParticleSystem rightThruster;
    private ParticleSystem leftRcs;
    private ParticleSystem rightRcs;
    private MasterTarget targetMaster;
    private Boolean alive = true;
    private Boolean controllable = true;
    private Rigidbody rb;
    private readonly float thrust = 8000f;
    private readonly float torque = 6000f;
    private readonly float maxFuel = 100f;
    private float fuel;
    private readonly float consumption = 5f;
    private readonly float boxSpeed = 1500f;
    public float emitter = 60f;
    private Boolean grounded;
    private readonly float boxTimer = 3f;   // time to wait before box can be dropped
    private float boxTime = 0f;
    private readonly float uiTimer = 1.5f;  // time to wait before UI panel
    private float uiTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        targetMaster = targetMasterObject.GetComponent<MasterTarget>();
        rb = gameObject.GetComponent<Rigidbody>();
        leftThruster = GameObject.Find("ParticleLeft").GetComponent<ParticleSystem>();
        rightThruster = GameObject.Find("ParticleRight").GetComponent<ParticleSystem>();
        leftRcs = GameObject.Find("ParticleRcsLeft").GetComponent<ParticleSystem>();
        rightRcs = GameObject.Find("ParticleRcsRight").GetComponent<ParticleSystem>();
        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        CheckVictory();
        CheckFuel();       

        boxTime += Time.deltaTime;
        boxGauge.value = boxTime;
        fuelGauge.value = fuel;

        if (alive && controllable)
            Control();
        else if (!alive)
        {
            uiTime += Time.deltaTime;

            if (uiTime > uiTimer)
                crash.SetActive(true);
        }
    }

    private void CheckVictory()
    {
        if (targetMaster.isAllHit() && grounded)
        {
            uiTime += Time.deltaTime;

            if (uiTime > uiTimer)
            {
                success.SetActive(true);
                controllable = false;
            }
                
        }
    }

    private void CheckFuel()
    {
        if (fuel <= 0)
        {
            fuel = 0;
            alive = false;
        }
        else if (fuel > 100)
            fuel = 100;
    }

    private void Control()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl) && !grounded && boxTime >= boxTimer)
        {
            Transform spawn = gameObject.transform.GetChild(3);
            GameObject boxClone = Instantiate(box, spawn.position, spawn.rotation);
            Rigidbody boxRB = boxClone.GetComponent<Rigidbody>();
            boxRB.velocity = transform.TransformDirection(Vector3.down * boxSpeed * Time.deltaTime);
            boxTime = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            rb.AddRelativeForce(new Vector3(0, thrust * Time.deltaTime, 0));
            fuel -= consumption * Time.deltaTime;
            // play big rocket sound
            float emit = emitter * Time.deltaTime;
            leftThruster.Emit(1);
            rightThruster.Emit(1);
        }

        float zRotation = -Input.GetAxis("Horizontal");

        if (zRotation != 0)
        {
            rb.AddRelativeTorque(Vector3.forward * torque * zRotation * Time.deltaTime);
            // play small rocket sound

            if (zRotation < 0)
                leftRcs.Emit(1);
            else if (zRotation > 0)
                rightRcs.Emit(1);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Target")
        {
            alive = false;
            rb.constraints = RigidbodyConstraints.None;
            // play crash sound
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LandingPad")
        {
            grounded = false;
            uiTime = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LandingPad")
        {
            grounded = true;
            fuel += consumption * 5 * Time.deltaTime;
        }
    }
}
