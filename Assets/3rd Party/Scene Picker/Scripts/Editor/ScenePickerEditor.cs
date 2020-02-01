namespace ScenePickerNamespace {

	using UnityEngine;
	using UnityEditor;
	using System.Collections;

	[CustomEditor(typeof(ScenePicker))]

	public class ScenePickerEditor : Editor {    

		string[] sceneList; // list of all scenes in build
		ScenePicker myScenePicker; // reference to script that is edited
		SerializedObject serializedScenePicker;

		bool noScenesInBuild = false; // show a warning if no scenes are in build
		bool sceneNameNotFound = false; // also show a warning if the scene that was saved as selected does not exist in the current build.

		string currentSceneName; // the name that the scene picker is currently set to
		SceneReference currentSceneReference; // remember this in case the scene picker is destroyed, so that the scene reference file can be destroyed

		int changesMade = 0;

		// this is like awake for editor
		void OnEnable() {
			// get list of all scenes in build
			ReloadSceneList();

			// get reference to the scene picker script
			myScenePicker = target as ScenePicker;
			serializedScenePicker = new SerializedObject(myScenePicker);

			// if the scene reference is null, assume that scene picker is newly created, call its creation event.
			if (myScenePicker.SceneReference == null) {
				OnScenePickerCreated();
			}

			// then, get the current scene name and scene reference
			currentSceneName = myScenePicker.SceneName;
			if (string.IsNullOrEmpty(currentSceneName)) sceneNameNotFound = true;
			currentSceneReference = myScenePicker.SceneReference;

		}

		// when a new scene picker was created
		void OnScenePickerCreated () {
			// assign some kind of scene name
			string newSceneName = "";
			if (sceneList.Length > 0) newSceneName = sceneList[0];
			// initialize scene picker with that name
			myScenePicker.OnCreated(newSceneName);
		}
		
		// draw GUI
		override public void OnInspectorGUI ()
		{
//			DrawDefaultInspector();

			
			serializedScenePicker.Update();

			EmptyLine();

			// if no scenes are found in build, show warning
			if (noScenesInBuild) EditorGUILayout.HelpBox("No scenes found. Please add at least one scene in the build settings.", MessageType.Warning);

			// other ways, update scene selection
			else {

				// dropdown list of scenes
				int sceneIndex = IndexOfSceneName(currentSceneName, sceneList); // have to find the index of the scene as int first because that's what EditorGUILayout.Popup requires
				if (sceneNameNotFound) {
					EditorGUILayout.HelpBox("The last selected scene was deleted or renamed outside of Unity.", MessageType.Error);
					if (GUILayout.Button("Pick a New One")) FixMissingSceneName();
				}

				else { // only if everything's okay update and write to the selected scene name
					sceneIndex = EditorGUILayout.Popup("Load Scene", sceneIndex, sceneList); // draw the popup, get the index as int
					string newSelectedScene =  sceneList[sceneIndex]; // translate the gotten index into scene string name
					if (newSelectedScene != currentSceneName) OnSceneNameChanged(newSelectedScene); // if it's not the currently selected one, throw event to change it
				}

				EmptyLine();

			}

			// option to update scene list
			if (GUILayout.Button("Refresh Scene List")) ReloadSceneList();
							
			// don't forget changes
//			SaveChanges();

			// if the scene name has been modified by choosing a new one or by an undo step
			if (serializedScenePicker.ApplyModifiedProperties()) {
				changesMade++;
				UpdateSceneRefFromTmp();
			}
			else if (Event.current.type == EventType.ValidateCommand &&
			 	Event.current.commandName == "UndoRedoPerformed")
			{
				if (changesMade > 0) {
					changesMade--;
					UpdateSceneRefFromTmp();
				}
			}
		}
				
		// update the scene reference from temporarily stored serialized string value
		// also updates the shown current scene name
		void UpdateSceneRefFromTmp() {
			currentSceneName = myScenePicker.UpdateSceneNameFromTmp();
		}

		// what happens when scene name has changed
		void OnSceneNameChanged (string newSceneName) {
//			currentSceneName = newSceneName;

			// change the value of the tmp serialized property.
			// updating that value to the scene reference will be done at the end
			// of OnInspectorGUI.

			SerializedProperty p = serializedScenePicker.FindProperty("tmp");
			p.stringValue = newSceneName;


//			myScenePicker.SceneName = newSceneName;
		}

		// this method is called when a scene name wasn't found and the "Fix" button is pressed
		void FixMissingSceneName() {
			ReloadSceneList(); // make sure scene list is up to date

			// set the name to just the first name in the list
			string newSceneName = sceneList[0];
			currentSceneName = newSceneName;
			myScenePicker.SceneName = newSceneName;
		}

		// return the index of a string in an array
		// returns 0 also if nothing was found
		int IndexOfSceneName(string sceneName, string[] allScenes) {
			if (sceneName == "") {
				sceneNameNotFound = true;
				return 0;
			}

			int i = 0;
			foreach (string currentString in allScenes) {
				if (currentString == sceneName) { // if scene was found
					sceneNameNotFound = false;
					return i;
				}
				i++;
			}
			sceneNameNotFound = true;
			return 0;
		}
			
		// draw an empty line
		void EmptyLine() {
			GUILayout.Label("");
		}	
		
		// create a list of all scenes in build
		void ReloadSceneList () {
			int sceneCount = EditorBuildSettings.scenes.Length;

			if (sceneCount > 0) { // if there are any scenes to load
				noScenesInBuild = false;
				sceneList = new string[sceneCount];
				int i = 0;
				foreach ( EditorBuildSettingsScene scene in EditorBuildSettings.scenes ) {
					sceneList[i] = ExtractSceneNameFromPath(scene.path);
					i++;
				}
			}
			else { // if there are no scenes, default to something
				noScenesInBuild = true;
				sceneList = new string[1];
				sceneList[0] = "";
	//			Debug.LogWarning("ScenePicker could not list scenes because no scenes are added to the build. Please add at least one scene in the build settings.");
			}		
		}
		
		// extracts the name of a scene from the path pointing to it
		public static string ExtractSceneNameFromPath(string path) {
			int i = path.Length - 1;
			string currentChar = "";
			
			// browse through characters
			while (i > 0 && currentChar != "/" && currentChar != "\\") {
				i--;
				currentChar = path[i].ToString();
			};
			
			int start = i + 1, length = path.Length - i - 7;
			if (length < 0) { length = 0; Debug.LogWarning("Failed to extract scene name from scene path."); }
			return path.Substring(start, length);
		}
		
		// if something changed mark this script as dirty so that scene will be saved
		void SaveChanges() {
			if (GUI.changed) {
				EditorUtility.SetDirty(myScenePicker);
			}	
		}

		// this on destroy is called more or less every time the inspector window goes out of sight
		void OnDestroy () {
			if (myScenePicker == null) {
				if (!Application.isPlaying) { // if all the conditions for a script being removed are fulfilled
					// delete the scene reference that belongs to the deleted scene picker
					SceneReference.Delete(currentSceneReference);
				}
			}
		}

	}

}
