using UnityEngine;
using System.Collections;
using System;
using TF;

[HelpURL("http://www.jstarlab.com/UserGuide/")]
public class Buggy1 : MonoBehaviour 
{
	public Camera[] cameras;
	int cameraId = 0;

	public TFRigidbody chassis;
	public TFHinge2 frontLeftWheel;
	public TFHinge2 frontRightWheel;
	public TFHinge2 backLeftWheel;
	public TFHinge2 backRightWheel;
	public Buggy1_Parameters parameters;

	float distanceFrontToBack;
	float distanceLeftToRight;

	IEnumerable DrivingWheels()
	{
		if (parameters.frontWheelDrive)
		{
			yield return frontLeftWheel;
			yield return frontRightWheel;
		}
		if (parameters.backWheelDrive)
		{
			yield return backLeftWheel;
			yield return backRightWheel;
		}
	}
	
	void Start()
	{
		TFHinge2[] allWheels = gameObject.GetComponentsInChildren<TFHinge2>();

		foreach (TFHinge2 wheel in allWheels)
		{
			wheel.body1.gameObject.GetComponent<TFCollider>().contactSetup += contactSetup;
		}

		foreach (Camera camera in cameras)
		{
			camera.enabled = false;
		}

		cameras[cameraId].enabled = true;

		distanceFrontToBack = (frontLeftWheel.transform.position - backLeftWheel.transform.position).magnitude;
		distanceLeftToRight = (frontLeftWheel.transform.position - frontRightWheel.transform.position).magnitude;
	}

	private void contactSetup(TFCollider collider, TFContact contact)
	{
		contact.contactInfo.fdir1 = Vector3.Cross(contact.contactGeom.normal, collider.transform.up).normalized;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			cameras[cameraId].enabled = false;
			cameraId++;
			cameraId %= cameras.Length;
			cameras[cameraId].enabled = true;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			chassis.velocities.Zero();

			Vector3 pos = chassis.transform.position;
			pos.y = 1f;
			chassis.transform.position = pos;

			Vector3 forward = chassis.transform.forward;
			forward.y = 0f;
			forward.Normalize();
			chassis.transform.LookAt(chassis.transform.position + forward, Vector3.up);
		}
	}

	void FixedUpdate()
	{ 
		float vel;
		float fmax;

		if (Input.GetKey(KeyCode.UpArrow))
		{
			vel = -parameters.maxVel;
			fmax = parameters.fForward;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			vel = parameters.maxVel;
			fmax = parameters.fReverse;
		}
		else
		{
			vel = 0f;
			fmax = parameters.fNeutral;
		}

		TFHinge2[] allWheels = gameObject.GetComponentsInChildren<TFHinge2>();
		foreach (TFHinge2 wheel in allWheels)
		{
			wheel.limot1.vel = 0;
			wheel.limot1.fmax = 0;
		}

		foreach (TFHinge2 wheel in DrivingWheels())
		{
			wheel.limot1.vel = vel;
			wheel.limot1.fmax = fmax;
		}

		float insideWheel;
		float outsideWheel;

		if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
		{
			computeSteering(distanceFrontToBack, distanceLeftToRight, parameters.corneringRadius,
					out insideWheel, out outsideWheel);

			frontLeftWheel.limot0.lostop = insideWheel;
			frontLeftWheel.limot0.histop = insideWheel;
			frontRightWheel.limot0.lostop = outsideWheel;
			frontRightWheel.limot0.histop = outsideWheel;
		}
		else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
		{
			computeSteering(distanceFrontToBack, distanceLeftToRight, parameters.corneringRadius,
					out insideWheel, out outsideWheel);

			frontRightWheel.limot0.lostop = -insideWheel;
			frontRightWheel.limot0.histop = -insideWheel;
			frontLeftWheel.limot0.lostop = -outsideWheel;
			frontLeftWheel.limot0.histop = -outsideWheel;
		}
		else
		{
			frontRightWheel.limot0.lostop = 0f;
			frontRightWheel.limot0.histop = 0f;
			frontLeftWheel.limot0.lostop = 0f;
			frontLeftWheel.limot0.histop = 0f;
		}

		float susp_erp;
		float susp_cfm;
		SetJointSpringAndDamping.ComputeERPandCFM(parameters.susp_spring, parameters.susp_damping, out susp_erp, out susp_cfm);

		foreach (TFHinge2 wheel in allWheels)
		{
			wheel.susp_cfm = susp_cfm;
			wheel.susp_erp = susp_erp;
			wheel.susp_preload = parameters.susp_preload;
		}
	}

	void computeSteering(float distanceFrontToBack, float distanceLeftToRight, float corneringRadius, 
		out float insideWheel, out float outsideWheel)
	{
		insideWheel = Mathf.Atan2(distanceFrontToBack, corneringRadius) * Mathf.Rad2Deg;
		outsideWheel = Mathf.Atan2(distanceFrontToBack, corneringRadius + distanceLeftToRight) * Mathf.Rad2Deg;
	}

	void OnGUI()
	{
		Rect rect = new Rect(0f, Screen.height - 25f, Screen.width, 25f);
		GUI.Box(rect, "Arrows to control buggy, Tab to switch between cameras, Space to reset buggy");
	}
}
