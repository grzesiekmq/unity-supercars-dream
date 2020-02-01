using UnityEngine;
using System.Collections;
using TF;

[HelpURL("http://www.jstarlab.com/UserGuide/")]
public class demo_chain : MonoBehaviour 
{
	public GameObject spherePrefab;

	public int numSpheres = 10;		
	const float SIDE = 0.2f;		//side length of a box
	const float MASS = 1.0f;		//mass of a box
	const float RADIUS = 0.1732f;   //sphere radius

	TFRigidbody[] rigidBodies = null;

	// Use this for initialization
	void Start()
	{
		rigidBodies = new TFRigidbody[numSpheres];

		TFMass mass = new TFMass();
		mass.SetBoxTotal(MASS, new Vector3(SIDE, SIDE, SIDE));

		for (int i = 0; i < numSpheres; i++)
		{
			GameObject sphere = GameObject.Instantiate(spherePrefab);
			sphere.name = "Sphere " + i;
			rigidBodies[i] = sphere.GetComponent<TFRigidbody>();
			rigidBodies[i].mass = mass;
			rigidBodies[i].massFromAttachedMassBehaviours = false;
			sphere.transform.localScale = new Vector3(RADIUS,RADIUS,RADIUS) / 0.5f;

			float k = (float)i * SIDE;
			sphere.transform.position = new Vector3(k, k + 0.4f, k);
		}

		for (int i = 0; i < (numSpheres - 1); i++)
		{
			GameObject ballJoint = new GameObject("ballJoint " + i);
			TFBall ball = ballJoint.AddComponent<TFBall>();
			ballJoint.AddComponent<TFResetScale>();
			ball.body0 = rigidBodies[i];
			ball.body1 = rigidBodies[i+1];
			ballJoint.transform.parent = rigidBodies[i].transform;

			float k = ((float)i + 0.5f) * SIDE;
			ball.transform.position = new Vector3(k, k + 0.4f, k);
		}
	}

	float angle = 0f;
	
	void FixedUpdate() 
	{
		rigidBodies[numSpheres - 1].AddForce(new Vector3(0f, 0.15f * (float)numSpheres * Mathf.Sin(angle) + 1.0f, 0f));
		angle += Time.fixedDeltaTime;
	}
}
