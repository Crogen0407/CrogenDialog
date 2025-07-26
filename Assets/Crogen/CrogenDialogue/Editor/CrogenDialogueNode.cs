using Crogen.CrogenDialogue;
using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialog.Editor
{
	public class CrogenDialogueNode : Node
	{
		public NodeSO BaseNodeSO { get; private set; }
		public override string title => BaseNodeSO?.GetNodeName();

		public CrogenDialogueNode(NodeSO baseNodeSO)
		{
			this.BaseNodeSO = baseNodeSO;
			
			// 메인 컨테이너
			var container = new VisualElement();
			container.style.paddingLeft = 8;
			container.style.paddingRight = 8;

			// SerializedObject 생성
			SerializedObject soSerialized = new SerializedObject(baseNodeSO);
			var iterator = soSerialized.GetIterator();

			if (iterator.NextVisible(true))
			{
				do
				{
					if (IsCanRender(iterator.name, baseNodeSO) == false) continue;
					
					PropertyField propField = new PropertyField(iterator.Copy());
					propField.Bind(soSerialized);
					container.Add(propField);
				}
				while (iterator.NextVisible(false));
			}

			Label title = new Label("This is title");
			title.style.paddingLeft = 8;
			title.style.paddingRight = 8;

			this.titleContainer.Add(title);
			this.mainContainer.Add(container);

			AddPorts();

			RefreshExpandedState();
			RefreshPorts();
		}

		private bool IsCanRender(string propertyName, NodeSO baseNodeSO)
		{
			Type nodeType = baseNodeSO.GetType();

			if (nodeType.IsDefined(typeof(RegisterNodeAttribute), false)
				&& propertyName == "m_Script") return false; // 에디터 자체 정의 노드는 렌더링하지 않음

			FieldInfo fieldInfo = GetAnyField(nodeType, propertyName);

			if (fieldInfo != null)
			{
				if (fieldInfo.IsDefined(typeof(HideInEditorWindowAttribute), false))
				{
					return false;
				}
			}
			
			return true;
		}

		private	FieldInfo GetAnyField(Type type, string name)
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

		private void AddPorts()
		{
			if (BaseNodeSO.InputList != null && BaseNodeSO.InputList.Count != 0)
			{
				for (int i = 0; i < BaseNodeSO.InputList.Count; i++)
				{
					inputContainer.Add(
						InstantiatePort(Orientation.Horizontal, 
						Direction.Input,
						Port.Capacity.Single, 
						typeof(CrogenDialogueNode)));
				}
			}

			if (BaseNodeSO.OutputList != null && BaseNodeSO.OutputList.Count != 0)
			{
				for (int i = 0; i < BaseNodeSO.OutputList.Count; i++)
				{
					outputContainer.Add(
						InstantiatePort(Orientation.Horizontal,
						Direction.Output,
						Port.Capacity.Single,
						typeof(CrogenDialogueNode)));
				}
			}
		}
	}
}
