using System;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Rigidbody rb;

    public float horizontalInput;
    public float verticalInput;

    public float steerAngle;

    [Header("input")]
    public float steerInput;

    public float turnSpeed = 10f;

    public float maxSteerAngle = 30f;
    public float engineForce = 50;

    public const int topSpeed = 300;

    private double speed = 0.0;

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steerAngle = maxSteerAngle * horizontalInput * Time.deltaTime;

        rb.AddTorque(transform.up * turnSpeed * steerAngle, ForceMode.Acceleration);

        //  FR.steerAngle = -steerAngle;
        //  FL.steerAngle = -steerAngle;
    }

    private void Accelerate()
    {
        speed = rb.velocity.x * 3.6;
        if (speed < topSpeed)
        {
            rb.AddForce(transform.forward * engineForce * verticalInput, ForceMode.Acceleration);
            Debug.Log("speed " + Math.Round(speed));
            if (speed < 0 && speed > -100)
            {
                Debug.Log(Math.Abs(Math.Round(speed)) + " (R)");
            }
        }
    }

    private void FixedUpdate()
    {
        GetInput();

        Accelerate();

        Steer();
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}