using UnityEngine;
using System.Collections;

/// <summary>
/// Example script how to access a scene picker from script. Loads the next scene after a certain amount of seconds.
/// </summary>

public class ScenePickerExample1 : MonoBehaviour {

	
	public float delay;
	public ScenePicker scenePicker;

	// Use this for initialization
	void Start () {
		Invoke("LoadNextScene", delay); // invoke loading the next scene after delay seonds
	}

	// load a scene using the scene picker
	void LoadNextScene () {
		Debug.Log ("Loading scene '" + scenePicker.SceneName + "'");
		scenePicker.LoadScene(); // load the scene that is currently selected on the scene picker
	}
}
