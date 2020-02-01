using UnityEngine;
using System.Collections;

[HelpURL("http://www.jstarlab.com/UserGuide/")]
public class FollowBuggy : MonoBehaviour 
{
	public Transform buggy;
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(buggy, Vector3.up);
	}
}
