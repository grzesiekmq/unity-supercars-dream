using Rgn;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarCtrl : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float verticalInput;

    [SerializeField]
    private List<Transform> Wheels;

    private GameObject brakingPlace;

    private float acceleration;

    [SerializeField]
    private int engineForce;

    [SerializeField]
    private int brakeForce = 10;

    [SerializeField]
    private float accSec;

    [SerializeField]
    private int topSpeed;

    private Vector3 force;
    private GameObject speedGo;
    private GameObject distanceGo;
    private TextMeshProUGUI distanceText;

    private int distance;

    private float speed = 0.0f;
    private int speedKph;

    [SerializeField]
    private bool stopwatchEnabled = true;

    /// <summary>
    ///
    /// </summary>
    private void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// car acceleration
    /// </summary>
    private void Accelerate()
    {
        // speed in m/s
        speed = rb.velocity.magnitude;

        distance = (int)(transform.position - brakingPlace.transform.position).magnitude;

        topSpeed = CarInfo.topSpeedValue;

        force = GetForce();

        speedKph = Car.MpsToKmh(speed);

        if (speedKph <= topSpeed)
        {
            var speedtxt = speedKph.ToString();

            
            
            rb.AddForce(force, ForceMode.Acceleration);

            var speedText = speedGo.GetComponent<TextMeshProUGUI>();

            speedText.text = $"speed: {speedtxt} kmh";

            distanceText = distanceGo.GetComponent<TextMeshProUGUI>();
            distanceText.text = $"distance to go: {distance.ToString()}";

            RollWheels();
        }

        TestBraking();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private Vector3 GetForce()
    {
        if (verticalInput > 0)
        {
            force = transform.forward * engineForce * verticalInput;
        }
        else if (verticalInput < 0)
        {
            force = transform.forward * brakeForce * verticalInput;
        }

        return force;
    }

    /// <summary>
    ///
    /// </summary>
    private void TestBraking()
    {
        if ((distance == 1 || distance == 2) && speedKph == 0)
        {
            Debug.Log("pass!");

            Debug.Log(gameObject.name);
            stopwatchEnabled = false;

            Garage.cars.Add(gameObject.name);

            Debug.Log("cars " + Garage.cars);

            NextScene();
        }
    }

    /// <summary>
    ///
    /// </summary>
    private void NextScene()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++index);
    }

    private void FixedUpdate()
    {
        GetInput();

        Accelerate();
    }

    private void Awake()
    {
        Time.timeScale = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        speedGo = GameObject.Find("speed");
        distanceGo = GameObject.Find("distance");
        brakingPlace = GameObject.Find("BrakingPlace");

        transform.localEulerAngles = new Vector3(0, -90, 0);

        

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    private void OnGUI()
    {
       // GUI.Box(new Rect(100, 200, 100, 100), $"acc time {accSec.ToString("F")}");
    }

    private void Update()
    {
        Time.timeScale = 1;

        if (speedKph == 100 && stopwatchEnabled)
        {
            accSec = GetAccSec();

            stopwatchEnabled = false;
        }
    }

    /// <summary>
    /// display acceleration 0-100 kmh in seconds
    ///
    /// </summary>
    /// <returns></returns>
    private float GetAccSec()
    {
        var time = Time.time;
        accSec = time - accSec;
        return accSec;
    }

    /// <summary>
    /// roll all wheels of car
    /// </summary>
    private void RollWheels()
    {
        var angle = engineForce * verticalInput;
        foreach (var wheel in Wheels)
        {
            wheel.Rotate(Vector3.right, angle);
        }
    }
}