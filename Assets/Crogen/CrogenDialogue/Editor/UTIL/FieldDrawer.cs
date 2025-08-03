using System;
using System.Reflection;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    public struct FieldDrawer
    {
		public static void DrawFieldElements(ScriptableObject baseSO, VisualElement container, bool showFieldLabel = true)
		{
			if (baseSO == null) return;

			List<PropertyField> propertyFieldList = new();

			// SerializedObject 생성
			SerializedObject soSerialized = new SerializedObject(baseSO);
			var iterator = soSerialized.GetIterator();

			if (iterator.NextVisible(true))
			{
				do
				{
					if (FieldDrawer.IsRenderableField(iterator.name, baseSO.GetType()) == false) continue;
					PropertyField propField = new PropertyField(iterator.Copy());
					if (showFieldLabel == false) propField.label = string.Empty;
					propField.Bind(soSerialized);
					propertyFieldList.Add(propField);
					container.Add(propField);
				}
				while (iterator.NextVisible(false));
			}
		}

		public static bool IsRenderableField(string propertyName, Type baseSOType)
		{
			if (baseSOType.IsDefined(typeof(CrogenRegisterScriptAttribute), false)
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
