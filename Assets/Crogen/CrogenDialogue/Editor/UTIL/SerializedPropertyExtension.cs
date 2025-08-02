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
					// Enum은 좀 복잡하니까 밑에서 따로 설명할게!
					break;
					// 나머지도 필요하면 추가
			}
			return null;
		}
	}
}
