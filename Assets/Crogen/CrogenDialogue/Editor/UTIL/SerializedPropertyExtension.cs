using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    public static class SerializedPropertyExtension
    {
		public static Type GetSystemTypeFromSerializedProperty(SerializedProperty prop)
		{
			switch (prop.propertyType)
			{
				case SerializedPropertyType.Integer: return typeof(int);
				case SerializedPropertyType.Boolean: return typeof(bool);
				case SerializedPropertyType.Float: return typeof(float);
				case SerializedPropertyType.String: return typeof(string);
				case SerializedPropertyType.Vector2: return typeof(Vector2);
				case SerializedPropertyType.Vector3: return typeof(Vector3);
				case SerializedPropertyType.Vector4: return typeof(Vector4);
				case SerializedPropertyType.Color: return typeof(Color);
				case SerializedPropertyType.ObjectReference: return typeof(UnityEngine.Object); // 또는 prop.objectReferenceValue?.GetType()
				case SerializedPropertyType.Enum: return typeof(Enum); // 정확한 enum은 reflection 써야 함
				case SerializedPropertyType.Rect: return typeof(Rect);
				case SerializedPropertyType.AnimationCurve: return typeof(AnimationCurve);
				case SerializedPropertyType.Bounds: return typeof(Bounds);
				case SerializedPropertyType.Quaternion: return typeof(Quaternion);
				default: return null;
			}
		}
	}
}
