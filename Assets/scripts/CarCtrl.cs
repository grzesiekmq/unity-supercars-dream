using Rgn;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarCtrl : MonoBehaviour
{
    private Rigidbody rb;

    public float horizontalInput;
    public float verticalInput;

    public float steerAngle;

    public List<Transform> Wheels;

    [Header("input")]
    public float steerInput;

    public float turnSpeed = 10f;

    public float maxSteerAngle = 30f;
    public float engineForce = 20;

    public const int topSpeed = 300;

    private TextMeshProUGUI speedText;

    private float speed = 0.0f;

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        var dt = Time.deltaTime;
        var left = (float)Car.AckermannLeft(2, 2, 10);

        steerAngle = left * horizontalInput;
        var smoothedSteering = Mathf.Lerp(0.0f, steerAngle, 10);
        var turnForce = transform.up * turnSpeed * steerAngle * dt;
        // Debug.Log(turnForce);

        var turnVec = new Vector3(0, 0, steerAngle * -1);
        Wheels[0].localEulerAngles = turnVec;
        Wheels[1].localEulerAngles = turnVec;

        rb.AddRelativeTorque(turnForce, ForceMode.Acceleration);
    }

    private void Accelerate()
    {
        // speed in m/s
        speed = rb.velocity.magnitude;
        var force = transform.forward * -1 * engineForce * verticalInput;
        var speedKph = Car.MpsToKmh(speed);
        if (speedKph < topSpeed)
        {
            var speedtxt = speedKph.ToString();
            rb.AddForce(force, ForceMode.Acceleration);
            speedText = (TextMeshProUGUI)FindObjectOfType<TextMeshProUGUI>();
            speedText.text = $"speed: {speedtxt} kmh";

            rollWheels();
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
        //stop rotating
        Destroy(GetComponent<Turn>());

        var car = new Car("make", "model");
        print(Car.AckermannLeft(2, 2, 10));

        this.gameObject.transform.Translate(-30, 0, 0, Space.World);

        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
    }

    private void rollWheels()
    {
        var angle = engineForce * verticalInput;
        foreach (var wheel in Wheels)
        {
            wheel.Rotate(Vector3.right, angle);
        }
    }
}