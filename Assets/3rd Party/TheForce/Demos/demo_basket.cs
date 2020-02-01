using UnityEngine;
using System.Collections;
using TF;

[HelpURL("http://www.jstarlab.com/UserGuide/")]
public class demo_basket : MonoBehaviour
{
	public GameObject boxPrefab;
	public GameObject spherePrefab;

	public GameObject physxBoxPrefab;
	public GameObject physxSpherePrefab;

	public int numBodies = 0;
	public int numPhysxBodies = 0;

	GameObject CreateInstance(GameObject prefab, string name)
	{
#if TEST_DETERMINISM
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
		numBodies = 0;
		numPhysxBodies = 0;
		UnityEngine.Random.InitState(0);
#endif
		GameObject instance = GameObject.Instantiate(prefab);
		instance.name = name;
		Vector3 sides = new Vector3(0.28f,0.28f,0.28f);
		instance.transform.localScale = sides;
		instance.transform.position = DrawStuff.LH_YUP(new Vector3(0f, 3.4f, 7.15f));    //7.15f, 3.85f
		instance.transform.rotation = Quaternion.identity;
		instance.transform.parent = transform;

		return instance;
	}

	void Start()
	{
		CreateInstance(spherePrefab, "Sphere " + numBodies);
		numBodies++;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			if (Input.GetKeyDown(KeyCode.B))
			{
				CreateInstance(physxBoxPrefab, "PhysxBox " + numPhysxBodies);
				numPhysxBodies++;
			}

			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
			{
				CreateInstance(physxSpherePrefab, "PhysxSphere " + numBodies);
				numPhysxBodies++;
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.B))
			{
				CreateInstance(boxPrefab, "Box " + numBodies);
				numBodies++;
			}

			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
			{
				CreateInstance(spherePrefab, "Sphere " + numBodies);
				numBodies++;
			}
		}

		if (Input.GetKeyDown(KeyCode.Delete))
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject.Destroy(transform.GetChild(i).gameObject);
			}
			numBodies = 0;
			numPhysxBodies = 0;
		}
	}

	void OnGUI()
	{
		Rect rect = new Rect(0f, Screen.height - 25f, Screen.width, 25f);
		GUI.Box(rect, "S or Space for sphere, B for box, SHIFT + (S or Space) for Physx sphere, SHIFT + B for Physx box");
	}
}
