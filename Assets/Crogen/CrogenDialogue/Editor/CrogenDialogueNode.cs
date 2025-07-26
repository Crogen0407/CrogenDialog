using Crogen.CrogenDialogue;
using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialog.Editor
{
	public class CrogenDialogueNode : Node
	{
		internal NodeSO BaseNodeSO { get; private set; }
		internal StorytellerSO StorytellerSO { get; private set; }
		public override string title => BaseNodeSO?.GetNodeName();

		public CrogenDialogueNode(NodeSO baseNodeSO, StorytellerSO storytellerSO)
		{
			this.BaseNodeSO = baseNodeSO;
			this.StorytellerSO = storytellerSO;

			// ���� �����̳�
			var container = new VisualElement();
			container.style.paddingLeft = 8;
			container.style.paddingRight = 8;

			// SerializedObject ����
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
				&& propertyName == "m_Script") return false; // ������ ��ü ���� ���� ���������� ����

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

		internal void OnDelete()
		{
			StorytellerSO.RemoveNode(BaseNodeSO);
		}

		internal void OnMove()
		{
			BaseNodeSO.Position = GetPosition().position;

			EditorUtility.SetDirty(StorytellerSO);
		}
	}
}
