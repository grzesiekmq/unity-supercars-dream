namespace ScenePickerNamespace {

	using UnityEngine;
	using UnityEditor;

	/// <summary>
	/// updates the scene dictionary when scenes are renamed or deleted.
	/// </summary>

	class ScenePostprocessor : AssetPostprocessor 
	{
		static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) 
		{

			// deal with renamed assets
			for (int i=0; i< movedAssets.Length; i++)
			{
				if (movedFromAssetPaths[i].Contains(".unity")) { // if the asset is a unity scene

					// know old and new scene names
					string oldName = ScenePickerEditor.ExtractSceneNameFromPath(movedFromAssetPaths[i]);
					string newName = ScenePickerEditor.ExtractSceneNameFromPath(movedAssets[i]);
//					Debug.Log ("Renaming '" + oldName + "' to '" + newName + "'");
					SceneReference.RenameAll(oldName, newName);
				}
			}

			// deal with deleted scenes
			foreach (string deletedPath in deletedAssets) 
			{
				if (deletedPath.Contains(".unity")) { // if the asset is a unity scene

					// set name of that scene to empty string in scene dictionary, to indicate that it has been deleted
					string deletedName = ScenePickerEditor.ExtractSceneNameFromPath(deletedPath);
//					Debug.Log ("Deleted '" + deletedName + "'");
					SceneReference.RenameAll(deletedName, "");
				}
			}		

		}
	}

}