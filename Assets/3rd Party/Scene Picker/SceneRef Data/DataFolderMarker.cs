#if UNITY_EDITOR
namespace ScenePickerNamespace {
	
	using UnityEngine;
	using UnityEditor;
	using System.Collections;
	using System.IO;
	
	/// <summary>
	/// The folder in which this script is located will be used as the data folder for scene pickers.
	/// This script is used so a marker so that the data folder will still be found correctly if the package is moved around in the assets folder.
	/// </summary>
	
	public class DataFolderMarker : ScriptableObject {
		
		// returns the path of the folder this script is in
		public static string DataFolderAssetPath() {
			DataFolderMarker dummy = (DataFolderMarker)ScriptableObject.CreateInstance(typeof(DataFolderMarker));
			MonoScript script = MonoScript.FromScriptableObject(dummy);
			string assetPath = AssetDatabase.GetAssetPath(script);
			DestroyImmediate(dummy);
			assetPath = Path.GetDirectoryName(assetPath); // go up to folder
//			assetPath += "/";
			return assetPath;
		}
	}
	
}
#endif