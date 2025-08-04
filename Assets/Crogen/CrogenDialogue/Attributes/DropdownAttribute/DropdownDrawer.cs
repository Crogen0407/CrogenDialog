using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[CustomPropertyDrawer(typeof(DropdownAttribute))]
	public class DropdownDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			DropdownAttribute dropdown = (DropdownAttribute)attribute;
			int index = Mathf.Max(0, System.Array.IndexOf(dropdown.options, property.stringValue));

			index = EditorGUI.Popup(position, label.text, index, dropdown.options);
			property.stringValue = dropdown.options[index];
		}
	}
}
