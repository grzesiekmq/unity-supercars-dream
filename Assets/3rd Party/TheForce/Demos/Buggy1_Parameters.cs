using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("http://www.jstarlab.com/UserGuide/")]
public class Buggy1_Parameters : ScriptableObject
{
	public float maxVel = 1000;
	public float fForward = 0.1f;
	public float fNeutral = 0.01f;
	public float fReverse = 0.1f;
	public float susp_spring = 20f;
	public float susp_damping = 1f;
	public float susp_preload = -0.25f;
	public float corneringRadius = 0.6f;

	public bool frontWheelDrive = true;
	public bool backWheelDrive = true;
}
