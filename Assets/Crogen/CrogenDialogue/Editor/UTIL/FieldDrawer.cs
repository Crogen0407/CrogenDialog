using System;
using System.Reflection;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    public struct FieldDrawer
    {
		public static void DrawFieldElements(ScriptableObject baseSO, VisualElement container)
		{
			// SerializedObject 생성
			SerializedObject soSerialized = new SerializedObject(baseSO);
			var iterator = soSerialized.GetIterator();

			if (iterator.NextVisible(true))
			{
				do
				{
					if (FieldDrawer.IsRenderableField(iterator.name, baseSO.GetType()) == false) continue;

					PropertyField propField = new PropertyField(iterator.Copy());
					propField.Bind(soSerialized);
					container.Add(propField);
				}
				while (iterator.NextVisible(false));
			}
		}

		private static bool IsRenderableField(string propertyName, Type baseSOType)
		{
			if (baseSOType.IsDefined(typeof(RegisterNodeAttribute), false)
				&& propertyName == "m_Script") return false; // 에디터 자체 정의 노드는 렌더링하지 않음

			FieldInfo fieldInfo = FieldDrawer.GetAnyField(baseSOType, propertyName);

			if (fieldInfo != null)
			{
				if (fieldInfo.IsDefined(typeof(HideInEditorWindowAttribute), false))
				{
					return false;
				}
			}

			return true;
		}

		public static FieldInfo GetAnyField(Type type, string name)
		{
			while (type != null)
			{
				var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
				if (field != null)
					return field;
				type = type.BaseType;
			}
			return null;
		}
	}
}
