using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    public static class StyleLoader
    {
		public static void AddStyles(VisualElement container, string stylesheetName)
		{
			StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"Assets\\Crogen\\CrogenDialogue\\Editor\\Resources\\{stylesheetName}.uss");
			if (styleSheet == null)
			{
				Debug.LogError("Fail to get style sheet.");
				return;
			}

			container.styleSheets.Add(styleSheet);
		}
	}
}
