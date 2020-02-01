using UnityEngine;
using System.Collections;
using TF;

/*

test the Coulomb friction approximation.

a 10x10 array of boxes is made, each of which rests on the ground.
a horizantal force is applied to each box to try and get it to slide.
box[i][j] has a mass (i+1)*MASS and a force (j+1)*FORCE. by the Coloumb
friction model, the box should only slide if the force is greater than MU
times the contact normal force, i.e.

  f > MU * body_mass * GRAVITY
  (j+1)*FORCE > MU * (i+1)*MASS * GRAVITY
  (j+1) > (i+1) * (MU*MASS*GRAVITY/FORCE)
  (j+1) > (i+1) * k

this should be independent of the number of contact points, as N contact
points will each have 1/N'th the normal force but the pushing force will
have to overcome N contacts. the constants are chosen so that k=1.
thus you should see a triangle made of half the bodies in the array start to
slide.

*/

[HelpURL("http://www.jstarlab.com/UserGuide/")]
public class demo_friction : MonoBehaviour 
{
	public GameObject cubePrefab = null;

	const float LENGTH = 0.2f;  // box length & width
	const float HEIGHT = 0.05f; // box height
	const float MASS = 0.2f;    // mass of box[i][j] = (i+1) * MASS
	const float FORCE = 0.05f;  // force applied to box[i][j] = (j+1) * FORCE
	const float MU = 0.5f;      // the global mu to use
	const float GRAVITY = 0.5f;	// the global gravity to use
    const int N1 = 10;      // number of different forces to try
	const int N2 = 10;      // number of different masses to try
	GameObject[,] body = new GameObject[N1,N2];

	//static float xyz[3] = { 1.7772, -0.7924, 2.7600 };
	//static float hpr[3] = { 90.0000, -54.0000, 0.0000 };
	//Vector3 xyz = new Vector3(1.7772f, 2.7600f, -0.7924f);
	//Vector3 hpr = new Vector3(54f, 0f, 0f);

	void Awake()
	{
		TFEngine.Instance.Gravity = new Vector3(0f,-GRAVITY,0f);

		for (int i = 0; i < N1; i++)
		{
			for (int j = 0; j < N2; j++)
			{
				body[i, j] = GameObject.Instantiate(cubePrefab);
				body[i, j].name = string.Format("body {0},{1}",i, j);
				body[i, j].GetComponent<TFRigidbody>().autoDisable.enabled = false;
				TFMassBehaviour massBehaviour = body[i, j].GetComponent<TFMassBehaviour>();
				massBehaviour.value = MASS * (j + 1);
				massBehaviour.computeMode = TFMassBehaviour.ComputeMode.ValueIsFinalMass;
				body[i, j].transform.localScale = new Vector3(LENGTH, HEIGHT, LENGTH);
				body[i, j].transform.position = new Vector3(i * 2f * LENGTH, HEIGHT * 0.5f, j * 2 * LENGTH);
				body[i, j].transform.parent = transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < N1; i++)
		{
			for (int j = 0; j < N2; j++)
			{
				body[i,j].GetComponent<TFRigidbody>().AddForce(new Vector3(FORCE * (i + 1), 0, 0));
			}
		}
	}

	void OnGUI()
	{
		Rect rect = new Rect(0f, Screen.height - 25f, Screen.width, 25f);
		GUI.Box(rect, "Horizontally: force, vertically: mass");
	}
}
