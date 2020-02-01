using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using ScenePickerNamespace;
using UnityEditor;
using System.Collections.Generic;
#endif

/// <summary>
/// Scriptable object that contains the reference to a scene as a string.
/// ScenePickers contain references to these and read the name
/// When scenes are renamed in the editor, the scene name is renamed here as well
/// </summary>

public class SceneReference : ScriptableObject {

	// in the build, all that the scene reference does is contain the scene name
	[SerializeField]
	string sceneName;
	public string SceneName {
		get {
			return sceneName;
		}
		set {
			sceneName = value;
			#if UNITY_EDITOR
			UpdateChangesToAssetFile();
			#endif
		}
	}

	// the following methods for creating and changing scene references are only used in the editor
#if UNITY_EDITOR
	public static SceneReference Create (string sceneName) {
		SceneReference newReference = ScriptableObject.CreateInstance<SceneReference>();
		string dataFolderPath = DataFolderMarker.DataFolderAssetPath();

		// get a list of all existing scene ref file names
		List<string> allSceneRefNames = new List<string>();
		foreach (SceneReference sceneRef in LoadAllSceneReferences(dataFolderPath)) {
			allSceneRefNames.Add(sceneRef.name + ".asset");
		}
		// find a unique file name for the new scene ref		
		string fileName;
		int index = 0;
		fileName = "SceneRef" + index.ToString() + ".asset";
		while (allSceneRefNames.Contains(fileName)) {
			fileName = "SceneRef" + index.ToString() + ".asset";
			index++;
		}

		// build the path and save the new scene ref
		string newReferencePath = dataFolderPath + "/" + fileName;
		AssetDatabase.CreateAsset(newReference, newReferencePath);
		AssetDatabase.SaveAssets();

		newReference.SceneName = sceneName;

		return newReference;
	}

	// loads all scene references in the specified data folder
	static SceneReference[] LoadAllSceneReferences (string dataFolderPath) {		
		List<SceneReference> allReferences = new List<SceneReference>();
		string[] folderArray = new string[1];
		folderArray[0] = dataFolderPath;
		string[] guids = AssetDatabase.FindAssets("", folderArray);
		foreach (string guid in guids) {
			Object loadObj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(SceneReference));
			if (loadObj != null) {
				SceneReference sceneRef = (SceneReference)loadObj;
				if (sceneRef != null) {
					allReferences.Add(sceneRef);
				}
			}
		}

		return allReferences.ToArray();
	}

	// renames all scene names in all scene references in the data folder
	public static void RenameAll(string oldName, string newName ) {
		string datafolderPath = DataFolderMarker.DataFolderAssetPath();
		foreach (SceneReference sceneRef in LoadAllSceneReferences(datafolderPath)) {
			if (sceneRef.SceneName == oldName) sceneRef.SceneName = newName;
		}
	}

	// update changes in the scriptable object to the asset file
	void UpdateChangesToAssetFile () {
		AssetDatabase.Refresh();
		EditorUtility.SetDirty(this);
		AssetDatabase.SaveAssets();
	}

	// how to delete a scene reference
	public static void Delete(SceneReference sceneRef) {
		string deletePath = AssetDatabase.GetAssetPath(sceneRef);
		AssetDatabase.DeleteAsset(deletePath);
		AssetDatabase.Refresh();
	}

#endif

}
