//using UnityEditor;
//using UnityEngine;

//namespace Crogen.CrogenDialogue.Editor.UTIL
//{
//	[InitializeOnLoad]
//	public static class MissingSOAutoCleaner
//	{
//		static MissingSOAutoCleaner()
//		{
//			EditorApplication.delayCall += CleanMissingScriptableObjects;
//		}

//		private static void CleanMissingScriptableObjects()
//		{
//			string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();

//			foreach (string path in allAssetPaths)
//			{
//				if (!path.EndsWith(".asset")) continue;

//				Object obj = AssetDatabase.LoadAssetAtPath<Object>(path);
//				if (obj == null)
//				{
//					Debug.Log($"삭제됨 (Missing SO): {path}");
//					AssetDatabase.DeleteAsset(path);
//				}
//			}
//		}
//	}

//}
