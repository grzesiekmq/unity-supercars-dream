using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using ScenePickerNamespace;
#endif

/// <summary>
/// Helper for loading scenes with uGui elements. Let's you select a scene in the inspector, call
/// LoadScene from e.g. a uGui button to then load that selected scene.
/// </summary>

[ExecuteInEditMode]
public class ScenePicker : MonoBehaviour {

	
	// method to load the selected scene
	public void LoadScene() {
		Application.LoadLevel(SceneName);
	}

	// accessor for scene name
	public string SceneName {
		// returning a scene name
		get {
			// make sure the scene reference is assigned
			if (sceneReference != null) {
				if (sceneReference.SceneName == "") Debug.LogError("Empty scene name in scene reference.");
				return sceneReference.SceneName;
			}
			// if no scene reference is assigned...
			else
			{
				// ... that's bad news
				Debug.LogError("Trying to get scene name from a scene picker that has no scene reference assigned.");
				return "";
			}
		}
		set {
			// make sure the scene reference is assigned
			if (sceneReference != null) {
				sceneReference.SceneName = value;
			}
			else {
				Debug.LogError("Trying to set scene name on a scene picker that has no scene reference assigned.");
			}
		}
	}

	// temporarily stores the scene name when editing to enable serialized field steps
	[SerializeField]
	string tmp;

	// assigns the value of a newly selected scene name to the scene reference
	public string UpdateSceneNameFromTmp() {
//		Debug.Log ("Assigning new scene name: " + tmp);
//		if (!string.IsNullOrEmpty(tmp))
			SceneName = tmp;
		return tmp;
	}
	
	// reference to a scriptable object that contains the scene name
	[SerializeField]
	SceneReference sceneReference;
	public SceneReference SceneReference {
		get {
			return sceneReference;
		}
	}

	#if UNITY_EDITOR
	// called by the custom inspector script when this is created
	// editor only
	public void OnCreated(string sceneName) {
		// 
		sceneReference = SceneReference.Create(sceneName);
	}
	#endif


}
