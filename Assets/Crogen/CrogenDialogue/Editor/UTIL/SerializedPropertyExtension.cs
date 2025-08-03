using System;
using UnityEditor;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    public static class SerializedPropertyExtension
    {
		public static Type GetSystemTypeFromSerializedProperty(this SerializedProperty prop)
		{
			switch (prop.propertyType)
			{
				case SerializedPropertyType.Integer:
					return typeof(int);
				case SerializedPropertyType.Boolean:
					return typeof(bool);
				case SerializedPropertyType.Float:
					return typeof(float);
				case SerializedPropertyType.String:
					return typeof(string);
				case SerializedPropertyType.ObjectReference:
					return typeof(UnityEngine.Object);
				case SerializedPropertyType.Enum:
					break;
			}
			return null;
		}
	}
}
