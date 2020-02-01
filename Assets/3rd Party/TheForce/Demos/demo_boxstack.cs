#define COMPOUND_USE_CAPSULE

using UnityEngine;
using System.Collections;
using TF;

[HelpURL("http://www.jstarlab.com/UserGuide/")]
public class demo_boxstack : MonoBehaviour
{
	public GameObject boxPrefab;
	public GameObject physxBoxPrefab;

	public GameObject spherePrefab;
	public GameObject physxSpherePrefab;

	public GameObject capsulePrefab;
	public GameObject physxCapsulePrefab;

	public GameObject wheelPrefab;

	public int numBodies = 0;
	public int numPhysxBodies = 0;

	public GameObject spawnPoint = null;

	[Range(0f,1f)]
	public float randomPos = 1f;

	GameObject CreateInstance(GameObject prefab, string name)
	{
		GameObject instance = GameObject.Instantiate(prefab);
		instance.name = name;
		Vector3 sides = new Vector3(Random.value * 0.5f + 0.1f, Random.value * 0.5f + 0.1f, Random.value * 0.5f + 0.1f);
		instance.transform.localScale = sides;
		instance.transform.position = spawnPoint.transform.position + new Vector3(Random.Range(-randomPos, randomPos), Random.Range(0f, randomPos), Random.Range(-randomPos, randomPos));
		instance.transform.rotation = Random.rotation;
		instance.transform.parent = transform;
		return instance;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			if (Input.GetKeyDown(KeyCode.B))
			{
				GameObject boxGO = CreateInstance(physxBoxPrefab, "PhysxBox " + numPhysxBodies);
				boxGO.GetComponent<Rigidbody>().SetDensity(1f);
				numPhysxBodies++;
			}

			if (Input.GetKeyDown(KeyCode.C))
			{
				GameObject capsuleGO = CreateInstance(physxCapsulePrefab, "Capsule " + numBodies);
				CapsuleCollider capsuleCollider = capsuleGO.GetComponent<CapsuleCollider>();
				float diameter = capsuleGO.transform.localScale.x;
				capsuleCollider.radius = diameter * 0.5f;
				capsuleCollider.height = capsuleGO.transform.localScale.z * 0.5f + diameter;
				capsuleGO.transform.localScale = Vector3.one;
				capsuleGO.GetComponent<CapsuleHelper>().UpdateSize();
				capsuleGO.GetComponent<Rigidbody>().SetDensity(1f);
				numPhysxBodies++;
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				GameObject physxSphereGO = CreateInstance(physxSpherePrefab, "PhysxSphere " + numBodies);
				float radius = physxSphereGO.transform.localScale.x;
				physxSphereGO.transform.localScale = new Vector3(radius, radius, radius);
				physxSphereGO.GetComponent<Rigidbody>().SetDensity(1f);
				numPhysxBodies++;
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				GameObject compoundGO = new GameObject("PhysxCompound " + numBodies);
				compoundGO.transform.parent = transform;

				GameObject sphereGO = CreateInstance(physxSpherePrefab, "PhysxSphere " + numBodies);
				float radius = sphereGO.transform.localScale.x;
				sphereGO.transform.localScale = new Vector3(radius, radius, radius);
				DestroyImmediate(sphereGO.GetComponent<Rigidbody>());
				sphereGO.transform.parent = compoundGO.transform;

				GameObject capsuleGO = CreateInstance(physxCapsulePrefab, "Capsule " + numBodies);
				CapsuleCollider capsuleCollider = capsuleGO.GetComponent<CapsuleCollider>();
				float diameter = capsuleGO.transform.localScale.x;
				capsuleCollider.radius = diameter * 0.5f;
				capsuleCollider.height = capsuleGO.transform.localScale.z * 0.5f + diameter;
				capsuleGO.transform.localScale = Vector3.one;
				capsuleGO.GetComponent<CapsuleHelper>().UpdateSize();
				DestroyImmediate(capsuleGO.GetComponent<Rigidbody>());
				capsuleGO.transform.parent = compoundGO.transform;

				GameObject boxGO = CreateInstance(physxBoxPrefab, "PhysxBox " + numBodies);
				DestroyImmediate(boxGO.GetComponent<Rigidbody>());
				boxGO.transform.parent = compoundGO.transform;

				compoundGO.AddComponent<Rigidbody>().SetDensity(1f);

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

			if (Input.GetKeyDown(KeyCode.S))
			{
				GameObject sphereGO = CreateInstance(spherePrefab, "Sphere " + numBodies);
				float diameter = sphereGO.transform.localScale.x;
				sphereGO.transform.localScale = new Vector3(diameter, diameter, diameter);
				numBodies++;
			}

			if (Input.GetKeyDown(KeyCode.C))
			{
				GameObject capsuleGO = CreateInstance(capsulePrefab, "Capsule " + numBodies);
				float diameter = capsuleGO.transform.localScale.x;
				float length = capsuleGO.transform.localScale.z * 0.5f;
				capsuleGO.transform.localScale = new Vector3(diameter, diameter, length + diameter);
				numBodies++;
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				GameObject compoundGO = new GameObject("Compound " + numBodies);
				compoundGO.transform.parent = transform;
				compoundGO.transform.rotation = Random.rotation;

				GameObject sphereGO = CreateInstance(spherePrefab, "Sphere " + numBodies);
				float radius = sphereGO.transform.localScale.x;
				sphereGO.transform.localScale = new Vector3(radius, radius, radius);
				DestroyImmediate(sphereGO.GetComponent<TFRigidbody>());
				sphereGO.transform.parent = compoundGO.transform;
#if COMPOUND_USE_CAPSULE
				GameObject capsuleGO = CreateInstance(capsulePrefab, "Capsule " + numBodies);
				float diameter = capsuleGO.transform.localScale.x;
				float length = capsuleGO.transform.localScale.z * 0.5f;
				capsuleGO.transform.localScale = new Vector3(diameter, diameter, length + diameter);
				DestroyImmediate(capsuleGO.GetComponent<TFRigidbody>());
				capsuleGO.transform.parent = compoundGO.transform;
#else
				GameObject wheelGO = CreateInstance(wheelPrefab, "Wheel " + numBodies);
				TFWheelCollider wheel = wheelGO.GetComponent<TFWheelCollider>();
				wheel.r = wheelGO.transform.localScale.x;
				wheel.s = wheelGO.transform.localScale.y * 0.3f;
				wheel.transform.localScale = Vector3.one;
				DestroyImmediate(wheelGO.GetComponent<TFRigidbody>());
				wheelGO.transform.parent = compoundGO.transform;
#endif
				GameObject boxGO = CreateInstance(boxPrefab, "Box " + numBodies);
				DestroyImmediate(boxGO.GetComponent<TFRigidbody>());
				boxGO.transform.parent = compoundGO.transform;

				compoundGO.AddComponent<TFRigidbody>();
				
				numBodies++;
			}

			if (Input.GetKeyDown(KeyCode.W))
			{
				GameObject wheelGO = CreateInstance(wheelPrefab, "Wheel " + numBodies);
				TFWheelCollider wheel = wheelGO.GetComponent<TFWheelCollider>();
				wheel.r = wheelGO.transform.localScale.x;
				wheel.s = wheelGO.transform.localScale.y * 0.3f;
				wheel.transform.localScale = Vector3.one;
				numBodies++;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Delete))
		{
			for (int i=0;i<transform.childCount;i++)
			{
				if (transform.GetChild(i).gameObject != spawnPoint)
				{
					GameObject.Destroy(transform.GetChild(i).gameObject);
				}
			}
			numBodies = 0;
			numPhysxBodies = 0;
		}
	}

	void OnGUI()
	{
		Rect rect = new Rect(0f, Screen.height - 25f, Screen.width, 25f);
		GUI.Box(rect,"B for box, S for sphere, C for capsule, X for compound object, "
		+ "SHIFT + B for Physx box, SHIFT + S for Physx sphere, SHIFT + X for Physx compound object");
	}
}
