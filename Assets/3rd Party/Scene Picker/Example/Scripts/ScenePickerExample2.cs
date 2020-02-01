using UnityEngine;
using System.Collections;

/// <summary>
/// An example how to use the scene picker script. Loads a scene with a delay when the rigidbody collides with something
/// </summary>

public class ScenePickerExample2 : MonoBehaviour {

	public float delay = 2f;
	public GameObject winningText;
	public ScenePicker scenePicker;

	// when the rigidbody collides with something...
	void OnCollisionEnter() {
		winningText.SetActive(true); // activate the "You Win!"-text
		Invoke("LoadNextScene", delay); // use invoke to call the method that will load the next scene after delay seconds
	}

	// load a scene using the scene picker
	void LoadNextScene () {
		Debug.Log ("Loading scene '" + scenePicker.SceneName + "'");
		scenePicker.LoadScene(); // load the scene that is currently selected on the scene picker
	}
}
