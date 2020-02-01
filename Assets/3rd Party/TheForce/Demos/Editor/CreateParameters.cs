namespace TFE
{
	using UnityEngine;
	using UnityEditor;
	using TF;
	using System.IO;
	using UnityEditor.SceneManagement;

	public class CreateParameters
	{
#if UNITY_EDITOR
		[MenuItem("Tools/The Force Engine/Create Buggy1 Parameters")]
		public static void CreateBuggy1_Parameters()
		{
			TFE.TFEHelpers.CreateAsset<Buggy1_Parameters>();
		}
#endif
	}
}